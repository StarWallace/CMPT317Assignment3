using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using Google.OrTools.ConstraintSolver;

namespace CMPT317Assignment3
{
    /// <summary>
    /// SOLVER CLASS
    /// </summary>
    public class QuasigroupSolver
    {
        static int X = 0;

        /*
         * default problem
         *
         * Example from Ruben Martins and Inès Lynce
         * Breaking Local Symmetries in Quasigroup Completion Problems, page 3
         * The solution is unique:
         *
         *   1 3 2 5 4
         *   2 5 4 1 3
         *   4 1 3 2 5
         *   5 4 1 3 2
         *   3 2 5 4 1
         */
        static int default_n = 5;
        static int[,] default_problem = {{1, X, X, X, 4},
                                   {X, 5, X, X, X},
                                   {4, X, X, 2, X},
                                   {X, 4, X, X, X},
                                   {X, X, 5, X, 1}};


        // for the actual problem
        static int n;
        static int[,] problem;


        /**
         *
         * Solves the Quasigroup Completion problem.
         * See http://www.hakank.org/or-tools/quasigroup_completion.py
         *
         */
        public static void Solve(bool stopAtOne = false, bool verbosePrinting = true)
        {
            Solver solver = new Solver("QuasigroupCompletion");

            //
            // data
            //
            Console.WriteLine("Problem:");
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (problem[i, j] != 0)
                    {
                        Console.Write(problem[i, j] + " ");
                    }
                    else { Console.Write(". "); }
                }

                Console.WriteLine();
            }
            Console.WriteLine();


            //
            // Decision variables
            //
            IntVar[,] x = solver.MakeIntVarMatrix(n, n, 1, n, "x");
            IntVar[] x_flat = x.Flatten();

            //
            // Constraints
            //  
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (problem[i, j] > X)
                    {
                        solver.Add(x[i, j] == problem[i, j]);
                    }
                }
            }

            //
            // rows and columns must be different
            //

            // rows
            for (int i = 0; i < n; i++)
            {
                IntVar[] row = new IntVar[n];
                for (int j = 0; j < n; j++)
                {
                    row[j] = x[i, j];
                }
                solver.Add(row.AllDifferent());
            }

            // columns
            for (int j = 0; j < n; j++)
            {
                IntVar[] col = new IntVar[n];
                for (int i = 0; i < n; i++)
                {
                    col[i] = x[i, j];
                }
                solver.Add(col.AllDifferent());
            }


            //
            // Search
            //
            DecisionBuilder db = solver.MakePhase(x_flat,
                                                  Solver.INT_VAR_SIMPLE,
                                                  Solver.ASSIGN_MIN_VALUE);

            solver.NewSearch(db);

            int sol = 0;
            solver.NextSolution();
            do
            {
                sol++;
                if (verbosePrinting)
                {
                    Console.WriteLine("Solution #{0} ", sol + " ");
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j < n; j++)
                        {
                            Console.Write("{0} ", x[i, j].Value());
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            } while (!stopAtOne && solver.NextSolution());

            Console.WriteLine("\nSolutions: {0}", solver.Solutions());
            Console.WriteLine("WallTime: {0}ms", solver.WallTime());
            Console.WriteLine("Failures: {0}", solver.Failures());
            Console.WriteLine("Branches: {0} ", solver.Branches());

            solver.EndSearch();

        }


        /**
         *
         * Reads a Quasigroup completion file.
         * File format:
         *  # a comment which is ignored
         *  % a comment which also is ignored
         *  number of rows (n)
         *  <
         *    row number of space separated entries
         *  >
         * 
         * "." or "0" means unknown, integer 1..n means known value
         * 
         * Example
         *   5
         *    1 . . . 4
         *   . 5 . . .
         *   4 . . 2 .
         *   . 4 . . .
         *   . . 5 . 1
         *
         */
        public static void readFile(String file, bool displayRead = false)
        {
            if (displayRead) //only print sometimes
                Console.WriteLine("readFile(" + file + ")");
            int lineCount = 0;

            TextReader inr = new StreamReader(file);
            String str;
            while ((str = inr.ReadLine()) != null && str.Length > 0)
            {

                str = str.Trim();

                // ignore comments
                if (str.StartsWith("#") || str.StartsWith("%"))
                {
                    continue;
                }

                if (displayRead) // only print sometimes
                    Console.WriteLine(str);
                if (lineCount == 0)
                {
                    n = Convert.ToInt32(str); // number of rows
                    problem = new int[n, n];
                }
                else
                {
                    // the problem matrix
                    String[] row = Regex.Split(str, " ");
                    for (int i = 0; i < n; i++)
                    {
                        String s = row[i];
                        if (s.Equals("."))
                        {
                            problem[lineCount - 1, i] = 0;
                        }
                        else
                        {
                            problem[lineCount - 1, i] = Convert.ToInt32(s);
                        }
                    }
                }

                lineCount++;

            } // end while

            inr.Close();


        } // end readFile
    }

}
