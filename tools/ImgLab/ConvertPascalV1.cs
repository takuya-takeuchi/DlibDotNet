using System;
using System.Collections.Generic;
using System.IO;
using DlibDotNet;
using DlibDotNet.ImageDatasetMetadata;
using Microsoft.Extensions.CommandLineUtils;

namespace ImgLab
{

    partial class Program
    {

        #region Methods

        private static void ConvertPascalV1(CommandArgument fileArgs, CommandOption xmlOption)
        {
            Console.WriteLine("Convert from PASCAL v1.00 annotation format...");

            using (var dataset = new Dataset())
            {
                var filename = fileArgs.Value;

                // make sure the file exists so we can use the get_parent_directory() command to
                // figure out it's parent directory.
                MakeEmptyFile(filename);
                var parentDir = Path.GetDirectoryName(Path.GetFullPath(filename));

                var images = dataset.Images;
                for (var i = 0; i < xmlOption.Values.Count; ++i)
                {
                    var arg = xmlOption.Values[i];

                    try
                    {
                        ParseAnnotationFile(arg, out var image, out var datasetName);

                        dataset.Name = datasetName;
                        image.FileName = StripPath(FigureOutFullPathToImage(arg, image.FileName), parentDir);
                        images.Add(image);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Error while processing file {arg}\n");
                        throw;
                    }
                }

                Dlib.ImageDatasetMetadata.SaveImageDatasetMetadata(dataset, filename);
            }
        }

        #region Helpers

        private static void ParseAnnotationFile(string file, out Image image, out string datasetName)
        {
            image = null;
            datasetName = null;

            try
            {
                using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    image = new Image();

                    using (var sr = new StreamReader(fs))
                    {
                        var boxes = new List<Box>();
                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            if (line == null)
                                break;

                            var words = line.Split(' ', '\r', '\n', '\t', ':', '(', ',', '-', ')', '\"');
                            if (words.Length <= 2)
                                continue;

                            if (words[0] == "#")
                                continue;

                            if (words[0] == "Image" && words[1] == "filename")
                            {
                                image.FileName = PickOutQuotedString(line);
                            }
                            else if (words[0] == "Database")
                            {
                                datasetName = PickOutQuotedString(line);
                            }
                            else if (words[0] == "Objects" && words[1] == "with" && words.Length >= 5)
                            {
                                var num = int.Parse(words[4]);
                                boxes.Clear();
                                for (var index = 0; index < num; index++)
                                    boxes.Add(new Box());
                            }
                            else if (words.Length > 4 && (words[2] == "for" || words[2] == "on") && words[3] == "object")
                            {
                                var idx = int.Parse(words[4]);
                                --idx;
                                if (idx >= boxes.Count)
                                    throw new Exception($"Invalid object id number of {words[4]}");

                                if (words[0] == "Center" && words[1] == "point" && words.Length > 9)
                                {
                                    var x = int.Parse(words[8]);
                                    var y = int.Parse(words[9]);
                                    boxes[idx].Parts["head"] = new Point(x, y);
                                }
                                else if (words[0] == "Bounding" && words[1] == "box" && words.Length > 13)
                                {
                                    boxes[idx].Rect = new Rectangle
                                    {
                                        Left = int.Parse(words[10]),
                                        Top = int.Parse(words[11]),
                                        Right = int.Parse(words[12]),
                                        Bottom = int.Parse(words[13])
                                    };
                                }
                                else if (words[0] == "Original" && words[1] == "label" && words.Length > 6)
                                {
                                    boxes[idx].Label = words[6];
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception($"Unable to open file {file}");
            }
        }

        private static string FigureOutFullPathToImage(string annotationFile, string imageName)
        {
            var parent = new DirectoryInfo(Path.GetDirectoryName(Path.GetFullPath(annotationFile)));
            while (true)
            {
                string temp;
                var isRoot = parent.Parent == null;
                if (isRoot)
                    temp = Path.Combine(parent.FullName, imageName);
                else
                    temp = Path.Combine(parent.FullName, imageName);

                if (File.Exists(temp))
                    return temp;

                if (isRoot)
                    throw new Exception($"Can\'t figure out where the file {imageName} is located.");

                parent = parent.Parent;
            }
        }

        private static string PickOutQuotedString(string str)
        {
            var temp = "";
            var inQuotes = false;

            foreach (var t in str)
            {
                if (t == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (inQuotes)
                {
                    temp += t;
                }
            }

            return temp;
        }

        #endregion

        #endregion

    }

}