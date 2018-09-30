using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using DlibDotNet;
using DlibDotNet.ImageDatasetMetadata;
using Microsoft.Extensions.CommandLineUtils;

namespace ImgLab
{

    partial class Program
    {

        #region Methods

        private static void ConvertPascalXml(CommandLineApplication parser)
        {
            Console.WriteLine("Convert from PASCAL XML annotation format...");

            using (var dataset = new Dataset())
            {
                var filename = parser.GetOptions().FirstOrDefault(option => option.ShortName == "c").Value();

                // make sure the file exists so we can use the get_parent_directory() command to
                // figure out it's parent directory.
                MakeEmptyFile(filename);
                var parentDir = Path.GetDirectoryName(Path.GetFullPath(filename));

                var images = new List<Image>();
                for (var i = 0; i < parser.RemainingArguments.Count; ++i)
                {
                    var arg = parser.RemainingArguments[i];

                    try
                    {
                        using (var fs = new FileStream(arg, FileMode.Open, FileAccess.Read))
                        {
                            var formmater = new XmlSerializer(typeof(Annotation));
                            var annotation = formmater.Deserialize(fs) as Annotation;
                            ParseAnnotationFile(arg, annotation, out var image, out var datasetName);

                            var root = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetFullPath(arg)));
                            var imagePath = Path.Combine(root, "JPEGImages");

                            dataset.Name = datasetName;
                            image.FileName = StripPath(Path.Combine(imagePath, image.FileName), parentDir);
                            images.Add(image);
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Error while processing file {arg}\n");
                        throw;
                    }
                }

                dataset.Images = images.ToArray();

                Dlib.ImageDatasetMetadata.SaveImageDatasetMetadata(dataset, filename);
            }
        }

        #region Helpers

        private static bool ToBoolean(string value)
        {
            return !(string.IsNullOrWhiteSpace(value) ||value == "0" || value == "false");
        }

        private static void ParseAnnotationFile(string filename, Annotation annotation, out Image image, out string datasetName)
        {
            image = null;
            datasetName = annotation?.Source?.Database ?? "";

            try
            {
                image = new Image
                {
                    FileName = annotation.FileName
                };

                var boxes = new List<Box>();

                foreach (var annotationObject in annotation.Objects)
                {
                    var box = new Box
                    {
                        Rect = new Rectangle
                        {
                            Top = annotationObject.BoundingBox.YMin,
                            Left = annotationObject.BoundingBox.XMin,
                            Right = annotationObject.BoundingBox.XMax,
                            Bottom = annotationObject.BoundingBox.YMax
                        },
                        Label = annotationObject.Name,
                        Occluded = ToBoolean(annotationObject.Occluded),
                        Truncated = ToBoolean(annotationObject.Truncated),
                        Difficult = ToBoolean(annotationObject.Difficult)
                    };

                    boxes.Add(box);
                }

                image.Boxes = boxes.ToArray();
            }
            catch (Exception)
            {
                throw new Exception($"Unable to open file {filename}");
            }
        }

        #endregion

        #endregion

    }

}
