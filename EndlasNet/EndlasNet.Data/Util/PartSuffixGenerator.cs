using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.Data
{
    public static class PartSuffixGenerator
    {
        public static void SetPartSuffix(PartForJob partForJob, int i)
        {

            int numChars = 1;
            
            for(int index = i; index > 26; index /= 26)
            {
                numChars++;
            }

            var suffix = new List<char>(numChars);

            for (int j = 0; j < numChars; j++)
            {
                suffix.Insert(j, (char)(i + 'A'));
            }
           
            partForJob.Suffix = new string(suffix.ToArray()); ;
        }
    }
}
