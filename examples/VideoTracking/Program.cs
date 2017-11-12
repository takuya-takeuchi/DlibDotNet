/*
 * This sample program is ported by C# from examples\video_tracking_ex.cpp.
*/

using System;
using System.IO;
using System.Linq;
using DlibDotNet;

namespace VideoTracking
{

    internal class Program
    {

        private static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Call this program like this: ");
                Console.WriteLine("VideoTracking.exe <path of video_frames directory>");
                return;
            }

            var path = args[0];
            var files = new DirectoryInfo(path).GetFiles("*.jpg").Select(info => info.FullName).ToList();
            files.Sort();

            if (files.Count == 0)
            {
                Console.WriteLine($"No images found in {path}");
                return;
            }

            using (var win = new ImageWindow())
            using (var tracker = new CorrelationTracker())
            {
                var firstFile = files.First();
                using (var img = Dlib.LoadImage<byte>(firstFile))
                {
                    var rect = DRectangle.CenteredRect(93, 110, 38, 86);
                    tracker.StartTrack(img, rect);
                }

                foreach (var file in files.GetRange(1, files.Count - 1))
                    using (var img = Dlib.LoadImage<byte>(file))
                    {
                        tracker.Update(img);

                        win.SetImage(img);
                        win.ClearOverlay();
                        win.AddOverlay(tracker.GetPosition());

                        Console.WriteLine("hit enter to process next frame");
                        Console.ReadKey();
                    }
            }
        }

    }

}
