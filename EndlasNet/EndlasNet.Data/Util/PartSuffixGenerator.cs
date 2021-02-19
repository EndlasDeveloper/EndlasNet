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
            if (n < 0)
                return null;

            // counter for base-26 number array 
            char[] hexaDeciNum = new char[100];
            int i = 0;
            do
            {
                // storing remainder in temp variable. 
                int temp = n % NUM_LETTERS;
                hexaDeciNum[i] = (char)(temp + 'A');
                i++;
                // divide and repeat if necessary
                n /= NUM_LETTERS;
            } while (n != 0);

            string suffix = "";
            for (int j = i - 1; j >= 0; j--)
            {
                if (j != 0)
                    hexaDeciNum[j] -= (char)1;
                suffix += hexaDeciNum[j];              
            }
            return suffix;
        }
    }
}
