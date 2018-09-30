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

        private static void ConvertIdl(CommandLineApplication parser)
        {
            Console.WriteLine("Convert from IDL annotation format...");

            using (var dataset = new Dataset())
            {
                for (var i = 0; i < parser.RemainingArguments.Count; ++i)
                {
                    var arg = parser.RemainingArguments[i];
                    ParseAnnotationFile(arg, dataset);
                }

                var filename = parser.GetOptions().FirstOrDefault(option => option.ShortName == "c").Value();
                Dlib.ImageDatasetMetadata.SaveImageDatasetMetadata(dataset, filename);
            }
        }

        #region Helpers

        private static bool NextIsNumber(StreamReader streamReader)
        {
            return ('0' <= streamReader.Peek() && streamReader.Peek() <= '9') || streamReader.Peek() == '-' || streamReader.Peek() == '+';
        }

        private static void ParseAnnotationFile(string file, Dataset data)
        {
            FileStream fs = null;
            StreamReader sr = null;

            try
            {
                fs = new FileStream(file, FileMode.Create);
                sr = new StreamReader(fs);
            }
            catch (Exception)
            {
                sr?.Dispose();
                fs?.Dispose();

                throw new Exception($"Unable to open file {file}");
            }

            try
            {
                var inQuote = false;
                var pointCount = 0;
                var inPointList = false;
                var sawAnyPoints = false;

                using (var img = new Image())
                {
                    var label = "";
                    var p1 = new Point();
                    var p2 = new Point();

                    var boxes = new List<Box>();
                    while (!sr.EndOfStream)
                    {
                        if (inPointList && NextIsNumber(sr))
                        {
                            var val = ReadInt(sr);
                            switch (pointCount)
                            {
                                case 0: p1.X = val; break;
                                case 1: p1.Y = val; break;
                                case 2: p2.X = val; break;
                                case 3: p2.Y = val; break;
                                default:
                                    throw new Exception($"parse error in file {file}");
                            }

                            ++pointCount;
                        }

                        var ch = Convert.ToChar(sr.Read());
                        if (ch == ':')
                            continue;

                        if (ch == '"')
                        {
                            inQuote = !inQuote;
                            continue;
                        }

                        if (inQuote)
                        {
                            img.FileName += ch;
                            continue;
                        }

                        if (ch == '(')
                        {
                            inPointList = true;
                            pointCount = 0;
                            label = "";
                            sawAnyPoints = true;
                        }

                        if (ch == ')')
                        {
                            inPointList = false;
                            label = "";

                            while (!sr.EndOfStream &&
                                   sr.Peek() != ';' && sr.Peek() != ',')
                            {
                                var ch2 = Convert.ToChar(sr.Read());
                                if (ch2 == ':')
                                    continue;

                                label += ch;
                            }
                        }

                        if (ch == ',' && !inPointList)
                        {
                            var b = new Box
                            {
                                Rect = new Rectangle(p1, p2),
                                Label = label
                            };
                            boxes.Add(b);
                        }

                        if (ch == ';')
                        {
                            if (sawAnyPoints)
                            {
                                var b = new Box
                                {
                                    Rect = new Rectangle(p1, p2),
                                    Label = label
                                };
                                boxes.Add(b);
                                sawAnyPoints = false;
                            }

                            img.Boxes = boxes.ToArray();

                            var tmp = data.Images.ToList();
                            tmp.Add(img);
                            data.Images = tmp.ToArray();

                            img.FileName = "";
                            boxes.Clear();
                        }
                    }
                }
            }
            finally
            {
                sr.Dispose();
                fs.Dispose();
            }
        }

        private static int ReadInt(TextReader streamReader)
        {
            var isNegative = false;
            if (streamReader.Peek() == '-')
            {
                isNegative = true;
                streamReader.Read();
            }

            if (streamReader.Peek() == '+')
                streamReader.Read();

            var val = 0;
            while ('0' <= streamReader.Peek() && streamReader.Peek() <= '9')
                val = 10 * val + streamReader.Read() - '0';

            return isNegative ? -val : val;
        }

        #endregion

        #endregion

    }

}
