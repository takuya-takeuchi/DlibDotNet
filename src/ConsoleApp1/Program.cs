using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DlibDotNet;

namespace ConsoleApp1
{
    class Program
    {
        private static void Main(string[] args)
        {
            var path = @"D:\Works\Lib\DLib\19.7\examples\faces\2007_007763.jpg";
            var img = Dlib.LoadImage<int>(path);
        }
    }
}
