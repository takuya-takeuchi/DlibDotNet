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

        private static void FlipDataset(CommandArgument fileArgs, CommandOption jpgOption, bool basic)
        {
            //var flip = parser.GetOptions().FirstOrDefault(option => option.ShortName == "flip");
            //var flipBasic = parser.GetOptions().FirstOrDefault(option => option.ShortName == "flip-basic");
            var dataSource = fileArgs.Value;

            using (var metadata = Dlib.ImageDatasetMetadata.LoadImageDatasetMetadata(dataSource))
            using (var origMetadata = Dlib.ImageDatasetMetadata.LoadImageDatasetMetadata(dataSource))
            {
                // Set the current directory to be the one that contains the
                // metadata file. We do this because the file might contain
                // file paths which are relative to this folder.
                var parentDir = Path.GetDirectoryName(Path.GetFullPath(dataSource));
                Environment.CurrentDirectory = parentDir;

                var metadataFilename = Path.Combine(parentDir, $"flipped_{Path.GetFileName(dataSource)}");

                var images = metadata.Images;
                for (int i = 0, iCount = images.Count; i < iCount; ++i)
                {
                    var f = new FileInfo(images[i].FileName);
                    var parent = Path.GetDirectoryName(f.FullName);
                    var filename = Path.Combine(parent, $"flipped_{ToPngName(f.Name)}");

                    using (var img = Dlib.LoadImage<RgbPixel>(images[i].FileName))
                    using (var temp = new Array2D<RgbPixel>())
                    {
                        Dlib.FlipImageLeftRight(img, temp);

                        if (jpgOption.HasValue())
                        {
                            filename = ToJpgName(filename);
                            Dlib.SaveJpeg(temp, filename, JpegQuality);
                        }
                        else
                        {
                            Dlib.SavePng(temp, filename);
                        }

                        var boxes = images[i].Boxes;
                        for (int j = 0, bCount = boxes.Count; j < bCount; ++j)
                        {
                            boxes[j].Rect = Dlib.FlipRectLeftRight(boxes[j].Rect, img.Rect);

                            // flip all the object parts
                            foreach (var kvp in boxes[j].Parts.ToArray())
                            {
                                var rect = new Rectangle(kvp.Value, kvp.Value);
                                var flipRect = Dlib.FlipRectLeftRight(rect, img.Rect);
                                boxes[j].Parts[kvp.Key] = flipRect.TopLeft;
                            }
                        }

                        images[i].FileName = filename;
                    }
                }

                //if (flipBasic == null || !flipBasic.HasValue())
                if (!basic)
                    MakePartLabelingMatchTargetDataset(origMetadata, metadata);

                Dlib.ImageDatasetMetadata.SaveImageDatasetMetadata(metadata, metadataFilename);
            }
        }

        #region Helpers

        private static IList<long> AlignPoints(IList<DPoint> from,
                                                IList<DPoint> to,
                                                double minAngle = -90 * Math.PI / 180.0,
                                                double maxAngle = 90 * Math.PI / 180.0,
                                                long numAngles = 181)
        {
            /*!
                ensures
                    - Figures out how to align the points in from with the points in to.  Returns an
                      assignment array A that indicates that from[i] matches with to[A[i]].

                      We use the Hungarian algorithm with a search over reasonable angles.  This method
                      works because we just need to account for a translation and a mild rotation and
                      nothing else.  If there is any other more complex mapping then you probably don't
                      have landmarks that make sense to flip.
            !*/
            if (from.Count != to.Count)
                throw new ArgumentException();

            long[] bestAssignment = null;
            var bestAssignmentCost = double.PositiveInfinity;

            using (var dists = new Matrix<double>(from.Count, to.Count))
            {
                foreach (var angle in Dlib.Linspace(minAngle, maxAngle, (int)numAngles))
                {
                    using (var rot = Dlib.RotationMatrix(angle))
                        for (int r = 0, rows = dists.Rows; r < rows; ++r)
                            using (var tmp = rot * from[r])
                                for (int c = 0, columns = dists.Columns; c < columns; ++c)
                                    using (var tmp2 = tmp - to[c])
                                        dists[r, c] = Dlib.LengthSquared(tmp2);

                    using (var tmp = dists / Dlib.Max(dists))
                    using (var tmp2 = long.MaxValue * tmp)
                    using (var tmp3 = Dlib.Round(tmp2))
                    using (var idists = Dlib.MatrixCast<long>(-tmp3))
                    {
                        var assignment = Dlib.MaxCostAssignment(idists).ToArray();
                        var cost = Dlib.AssignmentCost(dists, assignment);
                        if (cost < bestAssignmentCost)
                        {
                            bestAssignmentCost = cost;
                            bestAssignment = assignment.ToArray();
                        }
                    }
                }

                // Now compute the alignment error in terms of average distance moved by each part.  We
                // do this so we can give the user a warning if it's impossible to make a good
                // alignment.
                using (var rs = new RunningStats<double>())
                {
                    var tmp = new List<DPoint>(Enumerable.Range(0, to.Count).Select(i => new DPoint()));
                    for (var i = 0; i < to.Count; ++i)
                        tmp[(int)bestAssignment[i]] = to[i];

                    using (var tform = Dlib.FindSimilarityTransform(from, tmp))
                        for (var i = 0; i < from.Count; ++i)
                        {
                            var p = tform.Operator(from[i]) - tmp[i];
                            rs.Add(Dlib.Length(p));
                        }

                    if (rs.Mean > 0.05)
                    {
                        Console.WriteLine("WARNING, your dataset has object part annotations and you asked imglab to ");
                        Console.WriteLine("flip the data.  Imglab tried to adjust the part labels so that the average");
                        Console.WriteLine("part layout in the flipped dataset is the same as the source dataset.  ");
                        Console.WriteLine("However, the part annotation scheme doesn't seem to be left-right symmetric.");
                        Console.WriteLine("You should manually review the output to make sure the part annotations are ");
                        Console.WriteLine("labeled as you expect.");
                    }

                    return bestAssignment;
                }
            }
        }

        private static IDictionary<string, DPoint> AverageParts(Dataset data)
        {
            /*!
                ensures
                    - returns the average part layout over all objects in data.  This is done by
                      centering the parts inside their rects and then averaging all the objects.
            !*/
            var psum = new Dictionary<string, DPoint>();
            var pcnt = new Dictionary<string, double>();

            var images = data.Images;
            foreach (var image in images)
            {
                var boxes = image.Boxes;
                foreach (var box in boxes)
                {
                    foreach (var p in NormalizedParts(box))
                    {
                        if (!psum.ContainsKey(p.Key))
                            psum.Add(p.Key, p.Value);
                        else
                            psum[p.Key] += p.Value;

                        if (!pcnt.ContainsKey(p.Key))
                            pcnt.Add(p.Key, 1);
                        else
                            pcnt[p.Key] += 1;
                    }
                }
            }

            // make into an average
            var keys = psum.Keys.ToArray();
            for (var index = 0; index < keys.Length; index++)
            {
                var key = keys[index];
                var p = psum[key];
                var c = pcnt[key];
                psum[key] = p / c;
            }

            return psum;
        }

        private static void MakePartLabelingMatchTargetDataset(Dataset target, Dataset data)
        {
            /*!
                This function tries to adjust the part labels in data so that the average part layout
                in data is the same as target, according to the string labels.  Therefore, it doesn't
                adjust part positions, instead it changes the string labels on the parts to achieve
                this.  This really only makes sense when you flipped a dataset that contains left-right
                symmetric objects and you want to remap the part labels of the flipped data so that
                they match the unflipped data's annotation scheme.
            !*/
            var targetParts = AverageParts(target);
            var dataParts = AverageParts(data);

            // Convert to a form align_points() understands.  We also need to keep track of the
            // labels for later.
            var from = new List<DPoint>();
            var to = new List<DPoint>();
            var fromLabels = new List<string>();
            var toLabels = new List<string>();
            foreach (var p in targetParts)
            {
                fromLabels.Add(p.Key);
                from.Add(p.Value);
            }
            foreach (var p in dataParts)
            {
                toLabels.Add(p.Key);
                to.Add(p.Value);
            }

            var assignment = AlignPoints(from, to);
            // so now we know that from_labels[i] should replace to_labels[assignment[i]]
            var labelMapping = new Dictionary<string, string>();
            for (var i = 0; i < assignment.Count; ++i)
                labelMapping[toLabels[(int)assignment[i]]] = fromLabels[i];

            // now apply the label mapping to the dataset
            var images = data.Images;
            foreach (var image in images)
            {
                var boxes = image.Boxes;
                foreach (var box in boxes)
                {
                    var temp = new Dictionary<string, Point>();
                    foreach (var p in box.Parts)
                        temp[labelMapping[p.Key]] = new Point(p.Value.X, p.Value.Y);

                    box.Parts.Clear();
                    foreach (var kvp in temp)
                        box.Parts[kvp.Key] = kvp.Value;
                }
            }
        }

        private static IDictionary<string, DPoint> NormalizedParts(Box b)
        {
            using (var tform = Dlib.NormalizingTForm(b.Rect))
            {
                var temp = new Dictionary<string, DPoint>();
                foreach (var p in b.Parts)
                    temp[p.Key] = tform.Operator(new DPoint(p.Value.X, p.Value.Y));
                return temp;
            }
        }

        #endregion

        #endregion

    }

}