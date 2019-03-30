/*
 * This sample program is ported by C# from tools\imglab.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DlibDotNet;
using DlibDotNet.ImageDatasetMetadata;
using Microsoft.Extensions.CommandLineUtils;

namespace ImgLab
{

    partial class Program
    {

        #region Fields

        private const int ExitSuccess = 0;

        private const int ExitFailure = 1;

        internal const string Version = "1.15";

        #endregion

        #region Methods

        private static void Main(string[] args)
        {
            var app = new CommandLineApplication(false);
            app.Name = nameof(ImgLab);
            app.HelpOption("-h|--help");

            var createOption = new CommandOption("-c|--create", CommandOptionType.SingleValue);
            var convertOption = new CommandOption("-convert|--convert", CommandOptionType.SingleValue);
            var clusterOption = new CommandOption("-cluster|--cluster", CommandOptionType.SingleValue);
            var flipOption = new CommandOption("-flip|--flip", CommandOptionType.SingleValue);
            var flipBasicOption = new CommandOption("-flip-basic|--flip-basic", CommandOptionType.SingleValue);
            var fileOption = new CommandOption("-file|--file", CommandOptionType.SingleValue);
            var partsOption = new CommandOption("-parts|--parts", CommandOptionType.SingleValue);

            app.Options.Add(createOption);
            app.Options.Add(clusterOption);
            app.Options.Add(new CommandOption("-r|--r", CommandOptionType.NoValue));
            app.Options.Add(convertOption);
            app.Options.Add(flipOption);
            app.Options.Add(flipBasicOption);
            app.Options.Add(fileOption);
            app.Options.Add(partsOption);

            app.OnExecute(() =>
            {
                if (createOption.HasValue())
                {
                    if (convertOption.HasValue())
                    {
                        var value = convertOption.Value();
                        switch (value)
                        {
                            case "pascal-xml":
                                ConvertPascalXml(app);
                                break;
                            case "pascal-v1":
                                ConvertPascalV1(app);
                                break;
                            //case "idl":
                            //    ConvertIdl(app);
                            //    break;
                        }
                    }
                    else
                    {
                        CreateNewDataset(app);
                    }

                    return ExitSuccess;
                }

                if (clusterOption.HasValue())
                    ClusterDataset(app);

                if (flipOption.HasValue() || flipBasicOption.HasValue())
                    FlipDataset(app);

                if (fileOption.HasValue())
                {
                    using (var editor = new MetadataEditor(fileOption.Value()))
                    {
                        if (partsOption.HasValue())
                        {
                            foreach (var value in partsOption.Value().Split(' ', '\t', '\n', '\r', '\t'))
                                editor.AddLabelablePartName(value);
                        }

                        editor.WaitUntilClosed();
                    }
                }

                return 0;
            });

            app.Execute(args);
        }

        #region Helpers

        private static void CreateNewDataset(CommandLineApplication parser)
        {
            var createOption = parser.GetOptions().FirstOrDefault(option => option.ShortName == "c");
            var depthOption = parser.GetOptions().FirstOrDefault(option => option.ShortName == "r");

            var filename = createOption.Value();

            // make sure the file exists so we can use the get_parent_directory() command to
            // figure out it's parent directory.
            MakeEmptyFile(filename);
            var parentDir = Path.GetDirectoryName(Path.GetFullPath(filename));

            var depth = 0;
            if (depthOption.HasValue())
                depth = 30;

            using (var meta = new Dataset())
            {
                meta.Name = "imglab dataset";
                meta.Comment = "Created by imglab tool.";

                var images = meta.Images;
                for (var i = 0; i < parser.RemainingArguments.Count; ++i)
                {
                    var arg = parser.RemainingArguments[i];

                    try
                    {
                        if (!File.Exists(arg))
                            throw new FileNotFoundException();

                        var temp = StripPath(arg, parentDir);
                        images.Add(new Image(temp));
                    }
                    catch (FileNotFoundException)
                    {
                        // then parser[i] should be a directory
                        const string ext = "(.png|.PNG|.jpeg|.JPEG|.jpg|.JPG|.bmp|.BMP|.dng|.DNG|.gif|.GIF)$";
                        var files = GetFilesMostDeep(arg, ext, depth).ToList();
                        files.Sort();

                        foreach (var t in files)
                            images.Add(new Image(StripPath(t, parentDir)));
                    }

                    Dlib.ImageDatasetMetadata.SaveImageDatasetMetadata(meta, filename);
                }
            }
        }

        public static string[] GetFilesMostDeep(string rootPath, string pattern, int depth)
        {
            var list = Directory.GetFiles(rootPath)
                                .Where(s => Regex.IsMatch(s, pattern, RegexOptions.Compiled))
                                .Select(Path.GetFullPath).ToList();
            if (depth > 0)
            {
                foreach (var stDirPath in Directory.GetDirectories(rootPath))
                {
                    var paths = GetFilesMostDeep(stDirPath, pattern, depth - 1);
                    if (paths != null)
                        list.AddRange(paths);
                }
            }

            var returns = new string[list.Count];
            list.CopyTo(returns, 0);

            return returns;
        }

        #endregion

        #endregion

    }

}