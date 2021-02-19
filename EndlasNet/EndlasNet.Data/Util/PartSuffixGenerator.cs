using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public static class PartSuffixGenerator
    {
        // think of alphabet as a set of unique numbers in base-26 counting
        private readonly static int NUM_LETTERS = 26;

        public static string GetPartSuffix(int n)
        {
            return GetPartSuffix(n, new char[100], 0);
        }
        private static string GetPartSuffix(int n, char[] hexaDeciNum, int i)
        {
            // counter for base-26 number array 

            while (n >= 1)
            {
                if (n <= 26)
                {
                    hexaDeciNum[i] = (char)(n + 'A' - 1);
                    i++;
                    // get char array to string and return
                    break;
                }

                // temporary variable to store remainder 
                int temp = 0;
                // storing remainder in temp variable. 
                temp = n % NUM_LETTERS;
                if(temp == 0)
                {
                    hexaDeciNum[i] = (char)(temp + 'Z');
                    i++;
                    GetPartSuffix(n, hexaDeciNum, i);
                }
                else
                {
                    hexaDeciNum[i] = (char)(temp + 'A' - 1);
                    i++;
                }

                // divide and repeat if necessary
                n /= NUM_LETTERS;
            }
            string suffix = "";
            for (int j = i - 1; j >= 0; j--)
                suffix += hexaDeciNum[j];
            return suffix;
        }

        public static void SetPartSubSuffix(PartForJob partForJob, int i)
        {
        }
    }
}
