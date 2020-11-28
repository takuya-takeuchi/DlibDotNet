using System;
using System.Drawing;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using FaceDetection.Server.Api;
using Image = FaceDetection.Server;

namespace FaceDetection.Client
{

    internal class Program
    {

        #region Methods

        private static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("[Error] FaceDetectionClient <url> <image file path>");
                return;
            }

            var url = args[0];
            var file = args[1];
            if (!File.Exists(file))
            {
                Console.WriteLine($"[Error] '{file}' does not exist");
                return;
            }

            var api = new FaceDetectionApi(url);
            try
            {
                var image = new Server.Model.Image(File.ReadAllBytes(file));
                var result = api.ApiFaceDetectionLocationsPostWithHttpInfo(image);
                if (result.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    Console.WriteLine($"[Error] API returns {result.StatusCode}");
                    return;
                }

                Console.WriteLine($"[Info] Find {result.Data.Count} faces");

                using (var ms = new MemoryStream(image.Data))
                using (var bitmap = (Bitmap)System.Drawing.Image.FromStream(ms))
                using(var g = Graphics.FromImage(bitmap))
                using (var pen = new Pen(Color.Red, 2))
                {
                    foreach (var area in result.Data)
                    {
                        var x = area.Left;
                        var y = area.Top;
                        var w = area.Right - x;
                        var h = area.Bottom - y;
                        g.DrawRectangle(pen, x, y, w, h);
                    }

                    bitmap.Save("result.jpg");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion

    }

}
