/*
 * This sample program is ported by C# from tools\imglab.
*/

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

            app.Command("add", command =>
            {
                command.HelpOption("-?|-h|--help");
                var srcArgs = command.Argument("src", "");
                var destArgs = command.Argument("dest", "");

                command.OnExecute(() =>
                {
                    MergeMetadataFiles(srcArgs, destArgs);
                    return 0;
                });
            });

            app.Command("c", command =>
            {
                command.HelpOption("-?|-h|--help");
                var fileArgs = command.Argument("file", "");
                var convertOption = command.Option("-convert", "", CommandOptionType.SingleValue);
                var xmlOption = command.Option("-xml", "", CommandOptionType.MultipleValue);
                var imgOption = command.Option("-img", "", CommandOptionType.MultipleValue);
                var depthOption = command.Option("-r", "", CommandOptionType.NoValue);

                command.OnExecute(() =>
                {
                    if (convertOption.HasValue())
                    {
                        var value = convertOption.Value();
                        switch (value)
                        {
                            case "pascal-xml":
                                ConvertPascalXml(fileArgs, xmlOption);
                                break;
                            case "pascal-v1":
                                ConvertPascalV1(fileArgs, xmlOption);
                                break;
                                //case "idl":
                                //    ConvertIdl(app);
                                //    break;
                        }
                    }
                    else
                    {
                        CreateNewDataset(fileArgs, imgOption, depthOption);
                    }

                    return ExitSuccess;
                });
            });

            app.Command("cluster", command =>
            {
                command.HelpOption("-?|-h|--help");
                var fileArgs = command.Argument("file", "");
                var clusterArgs = command.Argument("cluster", "");
                var sizeOption = command.Option("-size", "", CommandOptionType.SingleValue);

                command.OnExecute(() =>
                {
                    ClusterDataset(fileArgs.Value, clusterArgs, sizeOption);
                    return ExitSuccess;
                });
            });

            app.Command("gui", command =>
            {
                command.HelpOption("-?|-h|--help");
                var fileArgs = command.Argument("file", "");
                var partsOption = command.Option("-parts|--parts", "", CommandOptionType.SingleValue);

                command.OnExecute(() =>
                {
                    using (var editor = new MetadataEditor(fileArgs.Value))
                    {
                        if (partsOption.HasValue())
                        {
                            foreach (var value in partsOption.Value().Split(' ', '\t', '\n', '\r', '\t'))
                                editor.AddLabelablePartName(value);
                        }

                        editor.WaitUntilClosed();
                    }

                    return ExitSuccess;
                });
            });

            app.Command("flip", command =>
            {
                command.HelpOption("-?|-h|--help");
                var fileArgs = command.Argument("file", "");
                var jpgOption = command.Option("-jpg", "", CommandOptionType.NoValue);

                command.OnExecute(() =>
                {
                    FlipDataset(fileArgs, jpgOption, false);
                    return ExitSuccess;
                });
            });

            app.Command("flip-basic", command =>
            {
                command.HelpOption("-?|-h|--help");
                var fileArgs = command.Argument("file", "");
                var jpgOption = command.Option("-jpg", "", CommandOptionType.NoValue);

                command.OnExecute(() =>
                {
                    FlipDataset(fileArgs, jpgOption, true);
                    return ExitSuccess;
                });
            });

            app.Execute(args);
        }

        #region Helpers

        private static void CreateNewDataset(CommandArgument fileArgs, CommandOption imgOption, CommandOption depthOption)
        {
            var filename = fileArgs.Value;

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
                for (var i = 0; i < imgOption.Values.Count; ++i)
                {
                    var arg = imgOption.Values[i];

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

        public static void MergeMetadataFiles(CommandArgument srcArg, CommandArgument destArg)
        {
            using (var src = Dlib.ImageDatasetMetadata.LoadImageDatasetMetadata(srcArg.Value))
            using (var dest = Dlib.ImageDatasetMetadata.LoadImageDatasetMetadata(destArg.Value))
            {
                var mergedData = new Dictionary<string, Image>();
                for (int i = 0, count = dest.Images.Count; i < count; ++i)
                    mergedData[dest.Images[i].FileName] = dest.Images[i];

                // now add in the src data and overwrite anything if there are duplicate entries.
                for (int i = 0, count = src.Images.Count; i < count; ++i)
                    mergedData[src.Images[i].FileName] = src.Images[i];

                // copy merged data into dest
                dest.Images.Clear();
                foreach (var kvp in mergedData)
                    dest.Images.Add(kvp.Value);

                Dlib.ImageDatasetMetadata.SaveImageDatasetMetadata(dest, "merged.xml");
            }
        }

        #endregion

        #endregion

    }

}