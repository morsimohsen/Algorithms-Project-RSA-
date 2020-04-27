using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace RSA
{
    /// <summary>
    /// Creat a BigInteger Data Type To able To Doing Operation in Huge
    /// Numper That's Number Don't Fit in int Data Type(32 bit) Or long Data Type(64 Bit).
    /// </summary>
    /// Class Paramter 
    /// <param name = "BigValue1"> Big Integer Number</param>
    /// <param name = "FirstNumber_Length"> The Length Of First Number</param>
    /// <param name = "SecondNumber_Length"> The Length Of Second Number</param>
    class BigInteger
    {

        public static string resultOfAdding;
        public static string resultOfSubracting;
        public static string resultOfMultiplication;
        public static Pair<string, string> pair;

        public BigInteger()
        {
            pair = new Pair<string, string>("", "");
            resultOfAdding = "";
            resultOfSubracting = "";
            resultOfMultiplication = "";
        }

        /*
         * That's Function take two integer numbers  
         * and doing an addition operation to this numbers
         */
        public static string Addition(string Value1, string Value2)
        {
            int sum = 0;
            int Capacity = Math.Max(Value1.Length, Value2.Length); // get the maximum length of both number
            int Remainder = 0;
            int c = 1;
            char[] Summation = new char[Capacity + 1];
            List<char> l = new List<char>(Capacity);
            /*
             * add Zeros at the end of the number to make it equal to the second number
             * to can sum the both numbers.
             */

            Equlization(ref Value1, ref Value2);

            /*
             * adding two first digits in the number 
             * if the result biger than or equal 10 put 
             * the carry (1) in the next two digits otherwise
             * adding two digit normal.
             */
            for (int i = Capacity - 1; i >= 0; i--)
            {
                sum = (Value1[i] - '0') + (Value2[i] - '0') + Remainder;
                if (sum >= 10)
                {
                    sum -= 10;
                    Remainder = 1;
                }
                else
                    Remainder = 0;
                Summation[i + 1] = (char)(sum + 48);
                if (sum != 0)
                    c = i + 1;

            }
            if (Remainder == 1)
            {
                Summation[0] = (char)(Remainder + 48);
                c = 0;
            }

            resultOfAdding = new string(Summation, c, (Capacity + 1) - c);
            return resultOfAdding;
        }
        //=================================================================================================================================================================

        /*
        * That's Function take two integer numbers  
        * and doing an Subtraction operation to this numbers
        */
        public static string Subtraction(string Value, string value2)
        {

            int c = 0;
            int Capacity = Math.Max(Value.Length, value2.Length);
            char[] Subtract = new char[Capacity];
            int sub = 0;

            /*
              * add Zeros at the end of the number to make it equal to the second number
              * to can substract the both numbers.
              */
            Equlization(ref Value, ref value2);

            char[] NewBorrowValue = Value.ToCharArray();
            /*
             * if the first number less than the second number
             * Swap to number to be able substract them
             */

            //if (Value.Length - value2.Length == 0)
            //{
            //    for (int i = 0; i < Value.Length; i++)
            //    {
            //        if (Value[i] - '0' < value2[i] - '0')
            //        {
            //            temp = Value;
            //            Value = value2;
            //            value2 = temp;
            //            check = true;
            //            break;

            //        }
            //        else if (Value[i] - '0' > value2[i] - '0')
            //            break;
            //    }
            //}
            if (Value == value2)
                return "0";

            /*
             * substract two number by folowing this steps:
             * sub first two digits
             * if substract result positive number continue 
             * otherwise borrow from the next digit
             */
            for (int i = Capacity - 1; i >= 0; i--)
            {
                sub = (NewBorrowValue[i] - '0') - (value2[i] - '0');

                if (sub >= 0)
                {
                    Subtract[i] = (char)(sub + 48);

                }
                else
                {

                    int NextValue = i - 1;

                    NewBorrowValue[NextValue] = (char)((((Value[NextValue] - '0') - 1)) + '0');

                    sub += 10;
                    Subtract[i] = (char)(sub + 48);
                }
                if (sub != 0)
                    c = i;
            }

            /*
             * Remove zero's form the end of result
             * For example:
             * 00899
             *      +    result will be like this (00854)
             *    45
             * But the correct result must be 854
             */
            // Remove_Zeros(ref Subtract);

            //if (check)
            //    Subtract.Insert(0, '-');

            resultOfSubracting = new string(Subtract, c, Capacity - c);

            return resultOfSubracting;
        }

        //=================================================================================================================================================================
        /*
         * That's Function take two integer numbers  
         * multiply two N-digit large integer numbers
         */
        public static string multiply(string Value, string value2)
        {


            int Capacity = Math.Max(Value.Length, value2.Length); // get the maximum length of both number
            

            /*
             * add Zeros at the end of the number to make it equal to the second number
             * to can multiply the both numbers.
             */
            Equlization(ref Value, ref value2);

            /*
             *   Karatsuba Algorithm
             * ------------------------
             * It's used to multiply two N-digit large integer numbers
             * the algorithms steps:
             * step1: split the first number into two halves ===>> Number1 = 9876  First_Half1 = 98, Second_Half1 = 76 
             * step2: split the first number into two halves ===>> Number2 = 4532  First_Half2 = 45, Second_Half2 = 32
             * 
             * step3: Recursively calculate 3 multiplications ====>> (First_Half1 * First_Half2), (Second_Half1 * Second_Half2),
             *        ((First_Half1+Second_Half1)*(First_Half2+Second_Half2))
             * 
             * 
             * Multiplying Number1 by Number2 will give the following:
             * 
             * result ====>> 10^N * (First_Half1 * First_Half2) + 10^N/2 * (First_Half1*Second_Half2 + First_Half2*Second_Half2) + (Second_Half1 * Second_Half2)
             * 
             * Now, we have four multiplications, each of size N/2… 
             * 
             * However, the two multiplications (First_Half1*Second_Half2 + First_Half2*Second_Half2) can be reduced to one using the ((Gauss trick))), as follows:
             * 
             * Step1: Z = (First_Half1+Second_Half1)*(First_Half2+Second_Half2) ====>> (First_Half1 * First_Half2) + (Second_Half1 * Second_Half2) 
             *                                                               + (First_Half1 * Second_Half2) + (First_Half2 * Second_Half1)
             *                                                               
             * 
             * Step2: subtract (First_Half1 * First_Half2) & (Second_Half1 * Second_Half2) from Z
             * so the Result = Z - (First_Half1 * First_Half2) - (Second_Half1 * Second_Half2).
             */
            if (Capacity == 1)
                return ((Value[0] - '0') * (value2[0] - '0')).ToString();

            string First_Half1 = Value.Substring(0, Capacity / 2);

            string Second_Half1 = Value.Substring(Capacity / 2);

            string First_Half2 = value2.Substring(0, Capacity / 2);

            string Second_Half2 = value2.Substring(Capacity / 2);



            string firstnumber_twoparts = Addition(First_Half1, Second_Half1);
            string secondnumber_twoparts = Addition(First_Half2, Second_Half2);


            string Result_first_step = multiply(First_Half1, First_Half2);
            string Result_second_step = multiply(Second_Half1, Second_Half2);
            string Result_third_step = multiply(firstnumber_twoparts, secondnumber_twoparts);


            string firstR_X_secondR = Addition(Result_first_step, Result_second_step);
            string Z = Subtraction(Result_third_step, firstR_X_secondR);


            char[] aa = new char[Result_first_step.Length + (((Capacity + 1) / 2) * 2)];
            char[] bb = new char[Z.Length + ((Capacity + 1) / 2)];

            for (int i = 0; i < bb.Length; i++)
            {
                if (i < Z.Length)
                    bb[i] = Z[i];
                else
                    bb[i] = '0';
            }
            for (int i = 0; i < aa.Length; i++)
            {
                if (i < Result_first_step.Length)
                    aa[i] = Result_first_step[i];
                else
                    aa[i] = '0';
            }
           
            Result_first_step = new String(aa);
            Z = new String(bb);

            string q = Addition(Result_first_step, Result_second_step);
            string Final_Result = Addition(q, Z);
            
            resultOfMultiplication = Final_Result;
            return resultOfMultiplication;
        }

        //=================================================================================================================================================================

        /*
         * That's Function take two integer numbers  
         * divided two N-digit large integer numbers
         */
        public static Pair<string, string> Division(string Value1, string Value2)
        {

            long remainder = 0;
            long q = 0;
            long divisor = 0;
            int index = 0;
            string s = "";
            List<char> l = new List<char>();
            if (Value1 == "1" && Value2 == "2")
            {
                Pair<string, string> newpair = new Pair<string, string>("0", "1");
                pair = newpair;
                return pair;
            }
            if (Value2 == "2")
            {
                divisor = long.Parse(Value2);
                q = (long)Value1[index] - '0';
                while (q < divisor)
                {
                    q = q * 10 + (long)(Value1[index + 1] - '0');
                    index++;
                }
                index++;

                while (Value1.Length > index)
                {
                    s = (q / divisor).ToString();
                    l.Add(s[0]);
                    q = (q % divisor) * 10 + (long)(Value1[index] - '0');
                    index++;
                }

                s = (q / divisor).ToString();
                l.Add(s[0]);
                remainder = q % divisor;
                Pair<string, string> newpair = new Pair<string, string>();
                newpair.First = new string(l.ToArray());
                newpair.Second = remainder.ToString();
                pair = newpair;
                return pair;
            }
            int c = Compare(Value1, Value2);
            if (c == -1)
            {
                Pair<string, string> newpair = new Pair<string, string>("0", Value1);
                pair = newpair;
                return pair;
            }
            pair = Division(Value1, Addition(Value2, Value2));
            pair.First = Addition(pair.First, pair.First);
            int d = Compare(pair.Second, Value2);
            if (d == -1)
                return pair;


            Pair<string, string> p = new Pair<string, string>();
            p.First = Addition(pair.First, "1");
            p.Second = Subtraction(pair.Second, Value2);
            pair = p;

            return pair;

        }
        //=======================================================================================================================================================================

        public static string ModOfPower(string number, string power, string mod)
        {
            //String sum = "0";
            //string q = "1";
            //while (power != "0")
            //{

            //    q = multiply(number, q);
            //    power = Subtraction(power, "1");
            //}
            //string w = Division(q, mod).Second;
            //return w;
            //Console.WriteLine("The Length  :   ");
            //Console.WriteLine(w.Length);
            //return w;


            //string ans = "";
            //if (power == "0")
            //    return "1";
            //Pair<string, string> p = new Pair<string, string>();
            //p = Division(power, "2");
            //ans = ModOfPower(number, p.First, mod);

            //string r = (Division(ans, mod)).Second;
            //string q = (multiply(r, r));

            //if (((power[power.Length - 1]) - '0') % 2 == 0)
            //{
            //    return Division(q, mod).Second;
            //}
            //else
            //{
            //    string w = (Division(number, mod)).Second;
            //    string e = multiply(q, w);
            //    return Division(e, mod).Second;
            //}

            string c = "0";
            string d = "1";
            string q;
            string newb = BinaryRepresentation(power);
            for (int i = 0; i < newb.Length; i++)
            {
                //  c = Addition(c, c);
                q = multiply(d, d);
                d = Division(q, mod).Second;
                if (newb[i] == '1')
                {
                    //    c = Addition(c, "1");
                    q = multiply(d, number);
                    d = Division(q, mod).Second;
                }
            }
            return d;
        }

        //============================================================================================================================================================================

        public static string Encryption(string M, string e, string n)
        {
            string encryptedResult = ModOfPower(M, e, n);
            return encryptedResult;
        }
        //===========================================================================================================================================================================

        public static string Decryption(string EM, string d, string n)
        {
            string decryptedResult = ModOfPower(EM, d, n);
            return decryptedResult;
        }
        //===========================================================================================================================================================================



        private static void Remove_Zeros(ref List<char> str)
        {
            long Length = str.Count;
            for (int i = 0; i < Length; i++)
            {
                if (str[0] != '0')
                {
                    break;
                }
                if (str[0] == '0' && i != Length - 1)
                {
                    str.RemoveAt(0);
                }

                if (str[0] == '0' && i == Length - 1)
                {

                    break;
                }
            }
        }
        //=================================================================================================================================================================


        private static void Equlization(ref string Value, ref string value2)
        {

            int FirstNumber_Length = Value.Length;
            int SecondNumber_Length = value2.Length;

            int Capacity = Math.Max(FirstNumber_Length, SecondNumber_Length); // get the maximum length of both number


            char[] Term3 = new char[Capacity];
            char[] Term4 = new char[Capacity];


            /*
             * add Zeros at the end of the number to make it equal to the second number
             * to can sum the both numbers.
             */

            if (Value.Length < Capacity)
            {
                for (int i = 0; i < Capacity - FirstNumber_Length; i++)
                    Term3[i] = '0';
                for (int i = Capacity - FirstNumber_Length, j = 0; i < Capacity; i++, j++)
                    Term3[i] = Value[j];

                Value = new string(Term3);


            }

            if (value2.Length < Capacity)
            {
                for (int i = 0; i < Capacity - SecondNumber_Length; i++)
                    //Term2.Insert(0, '0');
                    Term4[i] = '0';
                for (int i = Capacity - SecondNumber_Length, j = 0; i < Capacity; i++, j++)
                    Term4[i] = value2[j];

                value2 = new string(Term4);

            }

        }




        //=================================================================================================================================================================

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        ///
        public static int Compare(string a, string b)
        {
            Equlization(ref a, ref b);

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] < b[i])
                    return -1;
                if (a[i] > b[i])
                    return 1;
            }
            return 0;
        }
        public static string BinaryRepresentation(string s)
        {
            List<char> conc = new List<char>();
            string bin = "";
            string find;
            pair = new Pair<string, string>();
            while (s != "0")
            {
                pair = Division(s, "2");
                find = pair.Second;
                conc.Insert(0, find[0]);
                s = pair.First;
            }
            bin = new string(conc.ToArray());
            return bin;
        }
    }
}