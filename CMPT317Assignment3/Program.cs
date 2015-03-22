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

            //RunGranularTestCases();

            ScalingTest(5, 16, 17, 18, 19, 20, 0.5f);
            //ScalingTest(2, 15, 20, 50, 75, 100, 0.25f);

            //RunSmallTestsCases();
            //RunMediumTestsCases();
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

            int dimension = 25;

            RunTestCases(dimension);
        }

        /// <summary>
        /// A series of test problems of a 10x10 latin square
        /// </summary>
        static void RunLargeTestsCases()
        {
            QuasigroupSolver.timeLimit = 180000; //bump search time to 3 minutes for bigger problems
            QuasigroupSolver.branchLimit = 1000000000; //1 billion branches
            QuasigroupSolver.failLimit = 1000000000; //1 billion fails

            int dimension = 100;

            RunTestCases(dimension);
        }

        static void RunGranularTestCases()
        {
            int d = 20;
            float lvl1 = 0.48f, lvl2 = 0.49f, lvl3 = 0.5f, lvl4 = 0.51f, lvl5 = 0.52f;

            //Console.WriteLine("////// USING SEED 1 //////");
            //GranularTest(d, 1, lvl1, lvl2, lvl3, lvl4, lvl5);
            //Console.WriteLine("////// USING SEED 2 //////");
            //GranularTest(d, 2, lvl1, lvl2, lvl3, lvl4, lvl5);
            //Console.WriteLine("////// USING SEED 3 //////");
            //GranularTest(d, 3, lvl1, lvl2, lvl3, lvl4, lvl5);
            Console.WriteLine("////// USING SEED 4 //////");
            GranularTest(d, 4, lvl1, lvl2, lvl3, lvl4, lvl5);
            //Console.WriteLine("////// USING SEED 5 //////");
            //GranularTest(d, 5, lvl1, lvl2, lvl3, lvl4, lvl5);
        }

        /// <summary>
        /// standard test cases to run on a set of a given size
        /// </summary>
        static void RunTestCases(int dimension)
        {
            //want to run some tests relative to the size of the square
            //all tests run with stopAtOne set to maintain comparability between tests
            // a test with 19 solutions can't very well be compared to one with only 1,
            // more solutions always take more time.
                  
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
            Console.WriteLine("Group time: {0} ticks", Stopwatch.GetTimestamp() - watchStart);
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
            Console.WriteLine("Group time: {0} ticks", Stopwatch.GetTimestamp() - watchStart);
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
            Console.WriteLine("Group time: {0} ticks", Stopwatch.GetTimestamp() - watchStart);
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
            Console.WriteLine("Group time: {0} ticks", Stopwatch.GetTimestamp() - watchStart);
            watchStart = Stopwatch.GetTimestamp();

            totalRun.Stop();
            Console.WriteLine("\nTotal Test Running Time:\n Wall Time:\t{0} \n Milliseconds:\t{1} \n Ticks:\t\t{2} ", 
                totalRun.Elapsed, 
                totalRun.ElapsedMilliseconds, 
                totalRun.ElapsedTicks);
        }

        /// <summary>
        /// given a size, generate the standard set of test cases
        /// </summary>
        static void GenerateTestCases(int dimension)
        {
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
        /// Granular test created to try and understand why only certain seed values caused the 50% mark to run indefinitely.
        /// </summary>
        /// <param name="dimension"></param>
        /// <param name="seed"></param>
        /// <param name="lvl1"></param>
        /// <param name="lvl2"></param>
        /// <param name="lvl3"></param>
        /// <param name="lvl4"></param>
        /// <param name="lvl5"></param>
        static void GranularTest(int dimension, int seed, float lvl1 = 0.35f, float lvl2 = 0.40f, float lvl3 = 0.50f, float lvl4 = 0.60f, float lvl5 = 0.65f)
        {
            QuasigroupSolver.timeLimit = 30000; //30 second time limit for small problems
            QuasigroupSolver.branchLimit = 10000000; //10 million branches
            QuasigroupSolver.failLimit = 10000000; //10 million fails

            bool verbosePrinting = true;
            bool stopAtOne = true;

            Stopwatch totalRun = Stopwatch.StartNew(); //keep track of running time for full test set (AFTER generating any missing test problems)

            //35%
            int lvl1Percent = (int)Math.Round(Math.Pow(dimension, 2.0) * lvl1, 0);
            Console.WriteLine("\n{0} PERCENT EMPTY", lvl1);
            string filepath = datadir + "g" + dimension + "lvl1" + seed + ".txt";
            if (!(new FileInfo(filepath).Exists))//only generate if there isn't a file already
            {
                QWH qwh = new QWH(dimension, lvl1Percent, seed);
                SaveQuasigroup(qwh, filepath);
            }
            QuasigroupSolver.readFile(filepath, false);
            QuasigroupSolver.Solve(verbosePrinting, stopAtOne);

            //40%
            int lvl2Percent = (int)Math.Round(Math.Pow(dimension, 2.0) * lvl2, 0);
            Console.WriteLine("\n{0} PERCENT EMPTY", lvl2);
            filepath = datadir + "g" + dimension + "lvl2" + seed + ".txt";
            if (!(new FileInfo(filepath).Exists))//only generate if there isn't a file already
            {
                QWH qwh = new QWH(dimension, lvl2Percent, seed);
                SaveQuasigroup(qwh, filepath);
            }
            QuasigroupSolver.readFile(filepath, false);
            QuasigroupSolver.Solve(verbosePrinting, stopAtOne);

            //50%
            int lvl3Percent = (int)Math.Round(Math.Pow(dimension, 2.0) * lvl3, 0);
            Console.WriteLine("\n{0} PERCENT EMPTY", lvl3);
            filepath = datadir + "g" + dimension + "lvl3" + seed + ".txt";
            if (!(new FileInfo(filepath).Exists))//only generate if there isn't a file already
            {
                QWH qwh = new QWH(dimension, lvl3Percent, seed);
                SaveQuasigroup(qwh, filepath);
            }
            QuasigroupSolver.readFile(filepath, false);
            QuasigroupSolver.Solve(verbosePrinting, stopAtOne);

            //60%
            int lvl4Percent = (int)Math.Round(Math.Pow(dimension, 2.0) * lvl4, 0);
            Console.WriteLine("\n{0} PERCENT EMPTY", lvl4);
            filepath = datadir + "g" + dimension + "lvl4" + seed + ".txt";
            if (!(new FileInfo(filepath).Exists))//only generate if there isn't a file already
            {
                QWH qwh = new QWH(dimension, lvl4Percent, seed);
                SaveQuasigroup(qwh, filepath);
            }
            QuasigroupSolver.readFile(filepath, false);
            QuasigroupSolver.Solve(verbosePrinting, stopAtOne);

            //65%
            int lvl5Percent = (int)Math.Round(Math.Pow(dimension, 2.0) * lvl5, 0);
            Console.WriteLine("\n{0} PERCENT EMPTY", lvl5);
            filepath = datadir + "g" + dimension + "lvl5" + seed + ".txt";
            if (!(new FileInfo(filepath).Exists))//only generate if there isn't a file already
            {
                QWH qwh = new QWH(dimension, lvl5Percent, seed);
                SaveQuasigroup(qwh, filepath);
            }
            QuasigroupSolver.readFile(filepath, false);
            QuasigroupSolver.Solve(verbosePrinting, stopAtOne);

            totalRun.Stop();
            Console.WriteLine("\nTotal Test Running Time:\n Wall Time:\t{0} \n Milliseconds:\t{1} \n Ticks:\t\t{2} ",
                totalRun.Elapsed,
                totalRun.ElapsedMilliseconds,
                totalRun.ElapsedTicks);
        }

        /// <summary>
        /// A scaling test to compare differnt sizes of the same seed and hole density
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <param name="d3"></param>
        /// <param name="d4"></param>
        /// <param name="d5"></param>
        /// <param name="emptiness"></param>
        static void ScalingTest(int seed, int d1 = 10, int d2 = 15, int d3 = 25, int d4 = 50, int d5 = 100, float emptiness = 0.10f)
        {
            QuasigroupSolver.timeLimit = 30000; //30 second time limit for small problems
            QuasigroupSolver.branchLimit = 10000000; //10 million branches
            QuasigroupSolver.failLimit = 10000000; //10 million fails

            bool verbosePrinting = true;
            bool stopAtOne = true;

            Stopwatch totalRun = Stopwatch.StartNew(); //keep track of running time for full test set (AFTER generating any missing test problems)

            //35%
            int lvl1Percent = (int)Math.Round(Math.Pow(d1, 2.0) * emptiness, 0);
            Console.WriteLine("\nDIMENSION: {0} ", d1);
            string filepath = datadir + "s" + d1 + emptiness + seed + ".txt";
            if (!(new FileInfo(filepath).Exists))//only generate if there isn't a file already
            {
                QWH qwh = new QWH(d1, lvl1Percent, seed);
                SaveQuasigroup(qwh, filepath);
            }
            QuasigroupSolver.readFile(filepath, false);
            QuasigroupSolver.Solve(verbosePrinting, stopAtOne);

            //40%
            int lvl2Percent = (int)Math.Round(Math.Pow(d2, 2.0) * emptiness, 0);
            Console.WriteLine("\nDIMENSION: {0} ", d2);
            filepath = datadir + "s" + d2 + emptiness + seed + ".txt";
            if (!(new FileInfo(filepath).Exists))//only generate if there isn't a file already
            {
                QWH qwh = new QWH(d2, lvl2Percent, seed);
                SaveQuasigroup(qwh, filepath);
            }
            QuasigroupSolver.readFile(filepath, false);
            QuasigroupSolver.Solve(verbosePrinting, stopAtOne);

            //50%
            int lvl3Percent = (int)Math.Round(Math.Pow(d3, 2.0) * emptiness, 0);
            Console.WriteLine("\nDIMENSION: {0} ", d3);
            filepath = datadir + "s" + d3 + emptiness + seed + ".txt";
            if (!(new FileInfo(filepath).Exists))//only generate if there isn't a file already
            {
                QWH qwh = new QWH(d3, lvl3Percent, seed);
                SaveQuasigroup(qwh, filepath);
            }
            QuasigroupSolver.readFile(filepath, false);
            QuasigroupSolver.Solve(verbosePrinting, stopAtOne);

            //60%
            int lvl4Percent = (int)Math.Round(Math.Pow(d4, 2.0) * emptiness, 0);
            Console.WriteLine("\nDIMENSION: {0} ", d4);
            filepath = datadir + "s" + d4 + emptiness + seed + ".txt";
            if (!(new FileInfo(filepath).Exists))//only generate if there isn't a file already
            {
                QWH qwh = new QWH(d4, lvl4Percent, seed);
                SaveQuasigroup(qwh, filepath);
            }
            QuasigroupSolver.readFile(filepath, false);
            QuasigroupSolver.Solve(verbosePrinting, stopAtOne);

            //65%
            int lvl5Percent = (int)Math.Round(Math.Pow(d5, 2.0) * emptiness, 0);
            Console.WriteLine("\nDIMENSION: {0} ", d5);
            filepath = datadir + "s" + d5 + emptiness + seed + ".txt";
            if (!(new FileInfo(filepath).Exists))//only generate if there isn't a file already
            {
                QWH qwh = new QWH(d5, lvl5Percent, seed);
                SaveQuasigroup(qwh, filepath);
            }
            QuasigroupSolver.readFile(filepath, false);
            QuasigroupSolver.Solve(verbosePrinting, stopAtOne);

            totalRun.Stop();
            Console.WriteLine("\nTotal Test Running Time:\n Wall Time:\t{0} \n Milliseconds:\t{1} \n Ticks:\t\t{2} ",
                totalRun.Elapsed,
                totalRun.ElapsedMilliseconds,
                totalRun.ElapsedTicks);
        }

        /// <summary>
        /// To save a generated quasigroup in a form that can be read in again later - kim372
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
