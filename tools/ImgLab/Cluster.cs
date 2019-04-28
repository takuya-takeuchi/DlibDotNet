using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DlibDotNet;
using DlibDotNet.ImageDatasetMetadata;
using Microsoft.Extensions.CommandLineUtils;

namespace ImgLab
{

    partial class Program
    {

        #region Methods

        private static IEnumerable<Assignment> AngularCluster(IList<Matrix<double>> features, ulong numberClusters)
        {
            if (features == null)
                throw new ArgumentNullException(nameof(features));

            var size = features.Count;
            if (size == 0)
                throw new ArgumentException("The dataset can't be empty", nameof(features));

            var featureSize = features[0].Size;
            for (var index = 0; index < size; ++index)
                if (features[index].Size != featureSize)
                    throw new ArgumentException("All feature vectors must have the same length.", nameof(features));

            // find the centroid of feats
            Matrix<double> tmp;
            var m = Matrix<double>.CreateTemplateParameterizeMatrix(0, 1);
            for (var index = 0; index < size; ++index)
            {
                tmp = m + features[index];
                m.Dispose();
                m = tmp;
            }

            tmp = m;
            m = tmp / size;
            tmp.Dispose();

            // Now center feats and then project onto the unit sphere.  The reason for projecting
            // onto the unit sphere is so pick_initial_centers() works in a sensible way.
            for (var index = 0; index < size; ++index)
            {
                tmp = features[index] - m;
                features[index].Dispose();
                features[index] = tmp;

                var length = Dlib.Length(features[index]);
                if (Math.Abs(length) > double.Epsilon)
                {
                    tmp = features[index] / length;
                    features[index].Dispose();
                    features[index] = tmp;
                }
            }

            // now do angular clustering of the points
            var linearKernel = new LinearKernel<double, Matrix<double>>(0, 1);
            var tempCenters = Dlib.PickInitialCenters((int)numberClusters, features, linearKernel, 0.05).ToArray();
            var centers = Dlib.FindClustersUsingAngularKMeans(features, tempCenters).ToArray();
            foreach (var center in tempCenters)
                center.Dispose();
            linearKernel.Dispose();

            // and then report the resulting assignments
            var assignments = new List<Assignment>(size);
            for (var index = 0; index < size; ++index)
            {
                var temp = new Assignment();
                temp.C = Dlib.NearestCenter(centers, features[index]);
                using (var temp2 = features[index] - centers[temp.C])
                    temp.Distance = Dlib.Length(temp2);
                temp.Index = (ulong)index;
                assignments.Add(temp);
            }

            return assignments;
        }

        private static int ClusterDataset(string filename, CommandArgument clusterNumArgs, CommandOption sizeOption)
        {
            // make sure the user entered an argument to this program
            if (string.IsNullOrWhiteSpace(filename))
            {
                Console.WriteLine("The --cluster option requires you to give one XML file on the command line.");
                return ExitFailure;
            }

            var clusterValue = clusterNumArgs.Value ?? "2";
            var sizeValue = sizeOption.HasValue() ? sizeOption.Value() : "8000";

            if (!uint.TryParse(clusterValue, out var numClusters))
                return ExitFailure;

            if (!uint.TryParse(sizeValue, out var chipSize))
                return ExitFailure;

            using (var data = Dlib.ImageDatasetMetadata.LoadImageDatasetMetadata(filename))
            {
                Environment.CurrentDirectory = Path.GetDirectoryName(filename);


                double aspectRatio = MeanAspectRatio(data);

                var images = new Array<Array2D<RgbPixel>>();
                var feats = new List<Matrix<double>>();

                var dataImages = data.Images;
                //console_progress_indicator pbar(dataImages.Length);
                // extract all the object chips and HOG features.
                Console.WriteLine("Loading image data...");

                for (int i = 0, count = dataImages.Count; i < count; ++i)
                {
                    //pbar.print_status(i);
                    if (!HasNonIgnoredBoxes(dataImages[i]))
                        continue;

                    using (var img = Dlib.LoadImage<RgbPixel>(dataImages[i].FileName))
                    {
                        var boxes = dataImages[i].Boxes;
                        for (var j = 0; j < boxes.Count; ++j)
                        {
                            if (boxes[j].Ignore || boxes[j].Rect.Area < 10)
                                continue;

                            var rect = new DRectangle(boxes[j].Rect);
                            rect = DRectangle.SetAspectRatio(rect, aspectRatio);
                            using (var chipDetail = new ChipDetails(rect, chipSize))
                            {
                                var chip = Dlib.ExtractImageChip<RgbPixel>(img, chipDetail);
                                Dlib.ExtractFHogFeatures<RgbPixel>(chip, out var feature);
                                feats.Add(feature);
                                images.PushBack(chip);
                            }
                        }
                    }

                }

                if (!feats.Any())
                {
                    Console.WriteLine("No non-ignored object boxes found in the XML dataset.  You can't cluster an empty dataset.");
                    return ExitFailure;
                }

                Console.WriteLine("\nClustering objects...");
                var assignments = AngularCluster(feats, numClusters).ToList();


                // Now output each cluster to disk as an XML file.
                for (uint c = 0; c < numClusters; ++c)
                {
                    // We are going to accumulate all the image metadata for cluster c.  We put it
                    // into idata so we can sort the images such that images with central chips
                    // come before less central chips.  The idea being to get the good chips to
                    // show up first in the listing, making it easy to manually remove bad ones if
                    // that is desired.
                    var idata = new List<Pair<double, Image>>(dataImages.Count);
                    var idx = 0;
                    for (int i = 0, count = dataImages.Count; i < count; ++i)
                    {
                        idata.Add(new Pair<double, Image> { Second = new Image() });

                        idata[i].First = double.PositiveInfinity;
                        idata[i].Second.FileName = dataImages[i].FileName;

                        if (!HasNonIgnoredBoxes(dataImages[i]))
                            continue;

                        var idataBoxes = new List<Box>();
                        var boxes = dataImages[i].Boxes;
                        for (var j = 0; j < boxes.Count; ++j)
                        {
                            if (boxes[j].Ignore || boxes[j].Rect.Area < 10)
                                continue;

                            // If this box goes into cluster c then update the score for the whole
                            // image based on this boxes' score.  Otherwise, mark the box as
                            // ignored.
                            if (assignments[idx].C == c)
                                idata[i].First = Math.Min(idata[i].First, assignments[idx].Distance);
                            else
                                idataBoxes.Last().Ignore = true;

                            ++idx;
                        }
                    }

                    // now save idata to an xml file.
                    idata.Sort((a, b) =>
                    {
                        var diff = a.First - b.First;
                        return diff > 0 ? 1 : diff < 0 ? -1 : 0;
                    });

                    using (var cdata = new Dataset())
                    {
                        cdata.Comment = $"{data.Comment}\n\n This file contains objects which were clustered into group {c + 1} of {numClusters} groups with a chip size of {chipSize} by imglab.";
                        cdata.Name = data.Name;

                        var cdataImages = cdata.Images;
                        for (var i = 0; i < idata.Count; ++i)
                        {
                            // if this image has non-ignored boxes in it then include it in the output.
                            if (!double.IsPositiveInfinity(idata[i].First))
                                cdataImages.Add(idata[i].Second);
                        }

                        var outfile = $"cluster_{c + 1:D3}.xml";
                        Console.WriteLine($"Saving {outfile}");
                        Dlib.ImageDatasetMetadata.SaveImageDatasetMetadata(cdata, outfile);
                    }
                }

                // Now output each cluster to disk as a big tiled jpeg file.  Sort everything so, just
                // like in the xml file above, the best objects come first in the tiling.
                assignments.Sort();
                for (uint c = 0; c < numClusters; ++c)
                {
                    var temp = new Array<Array2D<RgbPixel>>();
                    for (var i = 0; i < assignments.Count(); ++i)
                    {
                        if (assignments[i].C == c)
                            temp.PushBack(images[(int)assignments[i].Index]);
                    }

                    var outfile = $"cluster_{c + 1:D3}.jpg";
                    Console.WriteLine($"Saving {outfile}");
                    using (var tile = Dlib.TileImages(temp))
                        Dlib.SaveJpeg(tile, outfile);
                }
            }

            return ExitSuccess;
        }

        private static bool HasNonIgnoredBoxes(Image image)
        {
            var boxes = image.Boxes;

            try
            {
                return boxes.Any(box => !box.Ignore);
            }
            finally
            {
                foreach (var box in boxes)
                    box.Dispose();
            }
        }

        private static double MeanAspectRatio(Dataset data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            double sum = 0;
            var cnt = 0;
            var images = data.Images;
            for (int index = 0, iCount = images.Count; index < iCount; ++index)
            {
                var boxes = images[index].Boxes;
                for (int j = 0, bCount = boxes.Count; j < bCount; ++j)
                {
                    var rect = boxes[j].Rect;
                    if (rect.Area == 0 || boxes[j].Ignore)
                        continue;

                    sum += rect.Width / (double)rect.Height;
                    ++cnt;
                }
            }

            return cnt != 0 ? sum / cnt : 0;
        }

        #endregion

        public sealed class Assignment : IComparable<Assignment>
        {

            #region Properties

            public ulong C
            {
                get;
                set;
            }

            public double Distance
            {
                get;
                set;
            }

            public ulong Index
            {
                get;
                set;
            }

            #endregion

            #region IComparable<Assignment> Implementation

            public int CompareTo(Assignment other)
            {
                var diff = this.Distance - other.Distance;
                return diff > 0 ? 1 : diff < 0 ? -1 : 0;
            }

            #endregion

        }

        private sealed class Pair<T, V>
        {

            #region Properties

            public T First
            {
                get;
                set;
            }

            public V Second
            {
                get;
                set;
            }

            #endregion

        }

    }

}
