using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Quasigroup; //generator

namespace CMPT317Assignment3
{
    /// <summary>
    /// MAIN PROGRAM CLASS
    /// </summary>
    class Program
    {
        //data directory
        static string datadir = "data/"; 

        //randomness seeds - to consistently use the same seed values in multiple related tests
        static int[] seeds = new int[5];
        //static int seed1 = 12345;
        //static int seed2 = 54321;
        //static int seed3 = 35813;
        //static int seed4 = 13853;
        //static int seed5 = 31415;

        static int seed1 = 1;
        static int seed2 = 2;
        static int seed3 = 3;
        static int seed4 = 4;
        static int seed5 = 5;


        static void Main(string[] args)
        {
            //load seeds
            seeds[0] = seed1;
            seeds[1] = seed2;
            seeds[2] = seed3;
            seeds[3] = seed4;
            seeds[4] = seed5;

            //QWH qwh = new QWH(50, 625, 12345);

            ////qwh.displayDetail();

            //SaveQuasigroup(qwh, datadir + "test.txt");

            //QuasigroupSolver.readFile(datadir + "test.txt", false);
            //QuasigroupSolver.Solve(false, false);
            //Console.ReadKey();

            //GenerateTestCases(50);

            //RunSmallTestsCases();
            RunMediumTestsCases();
            //RunLargeTestsCases();
        }

        /// <summary>
        /// A series of test problems of a 10x10 latin square
        /// </summary>
        static void RunSmallTestsCases()
        {
            QuasigroupSolver.timeLimit = 10000; //10 second time limit for small problems
            QuasigroupSolver.branchLimit = 1000000; //1 million branches
            QuasigroupSolver.failLimit = 1000000; //1 million fails

            int dimension = 10;

            RunTestCases(dimension);
        }

        /// <summary>
        /// A series of test problems of a 10x10 latin square
        /// </summary>
        static void RunMediumTestsCases()
        {
            QuasigroupSolver.timeLimit = 90000; //bump search time to 90 seconds for medium problems
            QuasigroupSolver.branchLimit = 10000000; //10 million branches
            QuasigroupSolver.failLimit = 10000000; //10 million fails

            int dimension = 50;

            RunTestCases(dimension);
        }

        /// <summary>
        /// A series of test problems of a 10x10 latin square
        /// </summary>
        static void RunLargeTestsCases()
        {
            QuasigroupSolver.timeLimit = 180000; //bump search time to 3 minutes for bigger problems
            QuasigroupSolver.branchLimit = 100000000; //100 million branches
            QuasigroupSolver.failLimit = 100000000; //100 million fails

            int dimension = 100;

            RunTestCases(dimension);
        }

        /// <summary>
        /// standard test cases to run on a set of a given size
        /// </summary>
        static void RunTestCases(int dimension)
        {
            //want to run some tests relative to the size of the square
            //all tests run with stopAtOne set to maintain comparability between tests
            // a test with 19 solutions can't very well be compared to one with only 1
            // more solutions always take more time.
                    //OR DO THEY? INVESTIGATE WITH A SEPARATE TEST SET TO SEE IF ACTUALLY A FACTOR
            bool verbosePrinting = true;
            bool stopAtOne = true;
            
            GenerateTestCases(dimension);

            Stopwatch totalRun = Stopwatch.StartNew(); //keep track of running time for full test set (AFTER generating any missing test problems)
            long watchStart = Stopwatch.GetTimestamp();

            //with 10% squares missing
            int tenPercentCount = (int)Math.Round(Math.Pow(dimension, 2.0) * 0.1, 0);
            Console.WriteLine("\n10 PERCENT EMPTY");
            for (int i = 0; i < 5; i++)
            {
                string filepath = datadir + dimension + "ten" + seeds[i] + ".txt";
                QuasigroupSolver.readFile(filepath);
                Console.WriteLine("\nTest For Seed: {0}", seeds[i]);
                QuasigroupSolver.Solve(verbosePrinting, stopAtOne);
            }
            Console.WriteLine("Group time {0}", Stopwatch.GetTimestamp() - watchStart);
            watchStart = Stopwatch.GetTimestamp();

            //with 25% squares missing
            int twentyfivePercentCount = (int)Math.Round(Math.Pow(dimension, 2.0) * 0.25, 0);
            Console.WriteLine("\n25 PERCENT EMPTY");
            for (int i = 0; i < 5; i++)
            {
                string filepath = datadir + dimension + "twentyfive" + seeds[i] + ".txt";
                QuasigroupSolver.readFile(filepath);
                Console.WriteLine("\nTest For Seed: {0}", seeds[i]);
                QuasigroupSolver.Solve(verbosePrinting, stopAtOne);
            }
            Console.WriteLine("Group time {0}", Stopwatch.GetTimestamp() - watchStart);
            watchStart = Stopwatch.GetTimestamp();

            //with 50% squares missing
            int fiftyPercentCount = (int)Math.Round(Math.Pow(dimension, 2.0) * 0.5, 0);
            Console.WriteLine("\n50 PERCENT EMPTY");
            for (int i = 0; i < 5; i++)
            {
                string filepath = datadir + dimension + "fifty" + seeds[i] + ".txt";
                QuasigroupSolver.readFile(filepath);
                Console.WriteLine("\nTest For Seed: {0}", seeds[i]);
                QuasigroupSolver.Solve(verbosePrinting, stopAtOne);
            }
            Console.WriteLine("Group time {0}", Stopwatch.GetTimestamp() - watchStart);
            watchStart = Stopwatch.GetTimestamp();

            //with 75% squares missing
            int seventyfivePercentCount = (int)Math.Round(Math.Pow(dimension, 2.0) * 0.75, 0);
            Console.WriteLine("\n75 PERCENT EMPTY");
            for (int i = 0; i < 5; i++)
            {
                string filepath = datadir + dimension + "seventyfive" + seeds[i] + ".txt";
                QuasigroupSolver.readFile(filepath);
                Console.WriteLine("\nTest For Seed: {0}", seeds[i]);
                QuasigroupSolver.Solve(verbosePrinting, stopAtOne);
            }
            Console.WriteLine("Group time {0}", Stopwatch.GetTimestamp() - watchStart);
            watchStart = Stopwatch.GetTimestamp();

            totalRun.Stop();
            Console.WriteLine("\nTotal Test Running Time:\n Wall Time {0} \n Miliseconds {1} \n Ticks {2} ", 
                totalRun.Elapsed, 
                totalRun.ElapsedMilliseconds, 
                totalRun.ElapsedTicks);
        }

        /// <summary>
        /// standard test cases to run on a set of a given size
        /// </summary>
        static void GenerateTestCases(int dimension)
        {
            //want to run some tests relative to the size of the square
            //all tests run with stopAtOne set to maintain comparability between tests
            // a test with 19 solutions can't very well be compared to one with only 1
            // more solutions always take more time.
            //OR DO THEY? INVESTIGATE WITH A SEPARATE TEST SET TO SEE IF ACTUALLY A FACTOR

            //with 10% squares missing
            int tenPercentCount = (int)Math.Round(Math.Pow(dimension, 2.0) * 0.1, 0);
            for (int i = 0; i < 5; i++)
            {
                string filepath = datadir + dimension + "ten" + seeds[i] + ".txt";
                if (!(new FileInfo(filepath).Exists))//only generate if there isn't a file already
                {
                    QWH qwh = new QWH(dimension, tenPercentCount, seeds[i]);
                    SaveQuasigroup(qwh, filepath);
                }
            }

            //with 25% squares missing
            int twentyfivePercentCount = (int)Math.Round(Math.Pow(dimension, 2.0) * 0.25, 0);
            for (int i = 0; i < 5; i++)
            {
                string filepath = datadir + dimension + "twentyfive" + seeds[i] + ".txt";
                if (!(new FileInfo(filepath).Exists))//only generate if there isn't a file already
                {
                    QWH qw = new QWH(dimension, twentyfivePercentCount, seeds[i]);
                    SaveQuasigroup(qw, filepath);
                }
            }

            //with 50% squares missing
            int fiftyPercentCount = (int)Math.Round(Math.Pow(dimension, 2.0) * 0.5, 0);
            for (int i = 0; i < 5; i++)
            {
                string filepath = datadir + dimension + "fifty" + seeds[i] + ".txt";
                if (!(new FileInfo(filepath).Exists))//only generate if there isn't a file already
                {
                    QWH qwh = new QWH(dimension, fiftyPercentCount, seeds[i]);
                    SaveQuasigroup(qwh, filepath);
                }
            }

            //with 75% squares missing
            int seventyfivePercentCount = (int)Math.Round(Math.Pow(dimension, 2.0) * 0.75, 0);
            for (int i = 0; i < 5; i++)
            {
                string filepath = datadir + dimension + "seventyfive" + seeds[i] + ".txt";
                if (!(new FileInfo(filepath).Exists))//only generate if there isn't a file already
                {
                    QWH qwh = new QWH(dimension, seventyfivePercentCount, seeds[i]);
                    SaveQuasigroup(qwh, filepath);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        /// <param name="fileName"></param>
        static void SaveQuasigroup(QWH group, string fileName)
        {
            //open a file stream for writing
            FileStream F = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite);

            //write number of lines in first line
            byte[] numLines = new UTF8Encoding(true).GetBytes(group.fqg[0].GetLength(0) + "\r\n");
            F.Write(numLines, 0, numLines.Length); //and write to the stream

            //loop through group
            for (int i = 0; i < group.fqg.GetLength(0); i++)
            {
                for (int j = 0; j < group.fqg[i].GetLength(0); j++)
                {
                    //if not a blank
                    if (group.fqg[i][j] != -1)
                    {
                        //convert the cell value to a byte string with a space at the end
                        byte[] cell = new UTF8Encoding(true).GetBytes((group.fqg[i][j]+1) + " ");
                        F.Write(cell, 0, cell.Length); //and write to the stream
                    }
                    else //else it is a blank, write a period
                    {
                        //convert a period to a byte string with a space at the end
                        byte[] cell = new UTF8Encoding(true).GetBytes(". ");
                        F.Write(cell, 0, cell.Length); //and write to the stream
                    }
                }
                //write line breaks
                byte[] newline = new UTF8Encoding(true).GetBytes("\r\n");
                F.Write(newline, 0, newline.Length); //and write to the stream
            }

            //close the filestream
            F.Flush(false); //flush loose data before closing to prevent malformed syntax
            F.Close();
        }
    }

}
