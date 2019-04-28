/*
 * This sample program is ported by C# from examples\queue_ex.cpp.
*/

using System;
using System.IO;
using DlibDotNet;

using queue_of_int = DlibDotNet.Queue<int>.Sort1BC;

namespace Queue
{

    internal class Program
    {

        #region Methods

        private static void Main()
        {
            using (var q = new queue_of_int())
            {
                // initialize rand()
                var rand = new Random(0);

                for (var i = 0; i < 20; ++i)
                {
                    var a = rand.Next() & 0xFF;

                    // note that adding a to the queue "consumes" the value of a because
                    // all container classes move values around by swapping them rather
                    // than copying them.   So a is swapped into the queue which results 
                    // in a having an initial value for its type (for int types that value
                    // is just some undefined value. )
                    q.Enqueue(a);
                }

                Console.WriteLine("The contents of the queue are:");
                while (q.MoveNext)
                    Console.Write($"{q.Element()} ");

                Console.WriteLine("\n\nNow we sort the queue and its contents are:");
                q.Sort();  // note that we don't have to call q.reset() to put the enumerator
                // back at the start of the queue because calling sort() does
                // that automatically for us.  (In general, modifying a container
                // will reset the enumerator).
                while (q.MoveNext)
                    Console.Write($"{q.Element()} ");


                Console.WriteLine("\n\nNow we remove the numbers from the queue:");
                while (q.Count > 0)
                {
                    q.Dequeue(out var a);
                    Console.Write($"{a} ");
                }


                Console.WriteLine();
            }
        }

        #endregion

    }

}