using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        {
            int TestCases = 0;
            long totalTime = 0;
            long maxTime = -1;
            FileStream file;
            StreamReader sr;
            StreamReader srr;
            StreamWriter sw;
            Stopwatch sww;
            //Console.WriteLine("First Number To Division: ");
            //string s = Console.ReadLine();


            //Console.WriteLine("Second Number To Division: ");
            //string r = Console.ReadLine();

            //Pair<string, string> ans = BigInteger.Division(s, r);


            //Console.WriteLine("Result = " + ans.First + "      Remainder = " + ans.Second);

            ////Console.WriteLine(BigInteger.ModOfPower("2003", "8", "3713"));

            //Console.WriteLine(BigInteger.BinaryRepresentation("8"));
            //Console.WriteLine(BigInteger.ModOfPower("2003", "8", "3713"));
            string EM = "", M = "";
            Console.WriteLine("RSA Encryption, Decryption\n[1] Sample test cases\n[2] Complete testing");
            char choice = (char)Console.ReadLine()[0];
            switch (choice)
            {
                case '1':
                    File.Delete(@"SampleRSA_II\SampleRSA_Output.txt");
                    srr = new StreamReader(@"SampleRSA_II\SampleRSA.txt");
                    Console.SetIn(srr);
                    TestCases = int.Parse(Console.ReadLine());
                    file = new FileStream(@"SampleRSA_II\SampleRSA_Output.txt", FileMode.Append, FileAccess.Write);
                    sw = new StreamWriter(file);
                    for (int i = 0; i < TestCases; i++)
                    {
                        string N = Console.ReadLine();
                        string e_OR_d = Console.ReadLine();
                        string M_or_EM = Console.ReadLine();
                        string encrypt_or_decrypt = Console.ReadLine();

                        if (encrypt_or_decrypt == "0")
                        {
                            EM = BigInteger.Encryption(M_or_EM, e_OR_d, N);
                            sw.WriteLine(EM);
                        }
                        else if (encrypt_or_decrypt == "1")
                        {
                            M = BigInteger.Decryption(M_or_EM, e_OR_d, N);
                            sw.WriteLine(M);
                        }
                        sw.WriteLine();
                        Console.WriteLine("Case " + (i + 1).ToString() + ": Done!");
                    }
                    sw.Close();
                   // srr.Close();
                    file.Close();
                    break;
                case '2':
                     File.Delete(@"Complete Test\CompleteRSA_Output.txt");
                     srr = new StreamReader(@"Complete Test\TestRSA.txt");
                    Console.SetIn(srr);
                    TestCases = int.Parse(Console.ReadLine());
                    file = new FileStream(@"Complete Test\CompleteRSA_Output.txt", FileMode.Append, FileAccess.Write);
                    StreamWriter ssww = new StreamWriter(file);
                    for (int i = 0; i < TestCases; i++)
                    {
                        string N = Console.ReadLine();
                        string e_OR_d = Console.ReadLine();
                        string M_or_EM = Console.ReadLine();
                        string encrypt_or_decrypt = Console.ReadLine();
                        long timebefore = System.Environment.TickCount;
                        if (encrypt_or_decrypt == "0")
                        {
                            EM = BigInteger.Encryption(M_or_EM, e_OR_d, N);
                            ssww.WriteLine(EM);
                        }
                        else if (encrypt_or_decrypt == "1")
                        {
                            M = BigInteger.Decryption(M_or_EM, e_OR_d, N);
                            ssww.WriteLine(M);
                        }
                        long timeafter = System.Environment.TickCount;
                        //if (timeafter - timebefore > 4000)
                        //{
                        //    Console.WriteLine("Time limit exceed: case # " + (i + 1));
                        //    srr.Close();
                        //    return;
                        //}
                        ssww.WriteLine("execution time = " + Math.Round((double)(timeafter - timebefore), 2) + "ms");
                        ssww.WriteLine("execution time = " + Math.Round((double)(timeafter - timebefore), 2) / 1000 + "s");
                        ssww.WriteLine();
                        Console.WriteLine("Case " + (i + 1).ToString() + ": Done!");
                    }
                    ssww.Close();
                   // srr.Close();
                    file.Close();
                    break;
                default:
                   
                   
            Console.WriteLine("\n[1] Adding BigInteger Two Numper\n[2] Substract BigInteger Two Numper\n[3] Multiply BigInteger Two Numper\n[4] Divided BigInteger Two Numper");
            Console.Write("\nEnter your choice [1-2-3]:");
            

            char c = (char)Console.ReadLine()[0];
            long TimeBefore = System.Environment.TickCount;
            switch (c)
            {
                case '1':
                    File.Delete("Add_Output.txt");
                    file = new FileStream("AddTestCases.txt", FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(file);
                    Console.SetIn(sr);
                    TestCases = int.Parse(Console.ReadLine());
                    totalTime = 0;
                    maxTime = -1;

                    for (int i = 0; i < TestCases; i++)
                    {
                        sr.ReadLine();
                        string First_Number = Console.ReadLine();
                        string Second_Number = Console.ReadLine();
                        sww = Stopwatch.StartNew();

                        string Result = BigInteger.Addition(First_Number, Second_Number);

                        sww.Stop();
                        if (sww.ElapsedMilliseconds > maxTime)
                            maxTime = sww.ElapsedMilliseconds;
                        totalTime += sww.ElapsedMilliseconds;

                        FileStream f = new FileStream("Add_Output.txt", FileMode.Append, FileAccess.Write);
                         sw = new StreamWriter(f);
                        sw.WriteLine(Result);
                        if (i != TestCases - 1)
                            sw.WriteLine();
                        sw.Close();
                        f.Close();
                    }
                    file.Close();
                    sr.Close();
                    break;
                case '2':
                    File.Delete("Subtract_Output.txt");
                    file = new FileStream("SubtractTestCases.txt", FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(file);
                    Console.SetIn(sr);
                    TestCases = int.Parse(Console.ReadLine());
                    totalTime = 0;
                    maxTime = -1;

                    for (int i = 0; i < TestCases; i++)
                    {
                        sr.ReadLine();
                        string First_Number = Console.ReadLine();
                        string Second_Number = Console.ReadLine();
                        sww = Stopwatch.StartNew();

                        string Result = BigInteger.Subtraction(First_Number, Second_Number);
                        
                        sww.Stop();
                        if (sww.ElapsedMilliseconds > maxTime)
                            maxTime = sww.ElapsedMilliseconds;
                        totalTime += sww.ElapsedMilliseconds;
                        
                        FileStream f = new FileStream("Subtract_Output.txt", FileMode.Append, FileAccess.Write);
                        sw = new StreamWriter(f);
                        sw.WriteLine(Result);
                        if (i != TestCases - 1)
                            sw.WriteLine();
                        sw.Close();
                        f.Close();
                    }
                    file.Close();
                    sr.Close();
                    break;
                case '3':
                    File.Delete("Multiply_Output.txt");

                    file = new FileStream("MultiplyTestCases.txt", FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(file);
                    Console.SetIn(sr);
                    TestCases = int.Parse(Console.ReadLine());
                    totalTime = 0;
                    maxTime = -1;

                    for (int i = 0; i < TestCases; i++)
                    {
                        sr.ReadLine();
                        string First_Number = Console.ReadLine();
                        string Second_Number = Console.ReadLine();
                        sww = Stopwatch.StartNew();
                        
                        string Result = BigInteger.multiply(First_Number, Second_Number);
                       
                        sww.Stop();
                        if (sww.ElapsedMilliseconds > maxTime)
                            maxTime = sww.ElapsedMilliseconds;
                        totalTime += sww.ElapsedMilliseconds;
                        
                        FileStream f = new FileStream("Multiply_Output.txt", FileMode.Append, FileAccess.Write);
                        sw = new StreamWriter(f);
                        sw.WriteLine(Result);
                        if (i != TestCases - 1)
                            sw.WriteLine();
                        sw.Close();
                        f.Close();
                    }
                    file.Close();
                    sr.Close();
                    break;
                case '4':
                    File.Delete("Division_Output.txt");

                    file = new FileStream("DivisionTestCases.txt", FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(file);
                    Console.SetIn(sr);
                    TestCases = int.Parse(Console.ReadLine());
                    totalTime = 0;
                    maxTime = -1;

                    for (int i = 0; i < TestCases; i++)
                    {
                        sr.ReadLine();
                        string First_Number = Console.ReadLine();
                        string Second_Number = Console.ReadLine();
                        sww = Stopwatch.StartNew();
                        Pair<string, string> Result = BigInteger.Division(First_Number, Second_Number);
                        sww.Stop();
                        if (sww.ElapsedMilliseconds > maxTime)
                            maxTime = sww.ElapsedMilliseconds;
                        totalTime += sww.ElapsedMilliseconds;
                        FileStream f = new FileStream("Division_Output.txt", FileMode.Append, FileAccess.Write);
                        sw = new StreamWriter(f);
                        sw.WriteLine("Result = " + Result.First);
                        sw.WriteLine("Remainder = " + Result.Second);

                        if (i != TestCases - 1)
                            sw.WriteLine();
                        sw.Close();
                        f.Close();
                    }
                    file.Close();
                    sr.Close();

                    break;
                default:
                    break;
            }
            Console.WriteLine("Average execution time of test cases = " + Math.Round((double)totalTime / TestCases, 2) + "(ms)");
            Console.WriteLine("Max execution time of test cases = " + Math.Round((double)maxTime, 2) + "(ms)");
            long TimeAfter = System.Environment.TickCount;
            Console.WriteLine("total time of all test cases = " + (TimeAfter - TimeBefore) + "(ms)");
                   break;
            }
            //else if (a == 2)
            //{
            //    string EM = Console.ReadLine();
            //    string d = Console.ReadLine();
            //    string n = Console.ReadLine();

            //    string M = BigInteger.Decryption(EM, d, n);
            //    Console.WriteLine("Decrypted messeage = " + M);
            //    Console.WriteLine();
            //    Console.WriteLine();

            //}
            
        }
    }
}
