using System;
using System.Collections.Generic;
using System.Text;

namespace EndlasNet.UnitTest
{
    public static class UnitTestUtil
    {
        static Random rd = new Random();
        public static string getRandomString(int stringLength)
        {

            const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz0123456789!@$?_-";
            char[] chars = new char[stringLength];

            for (int i = 0; i < stringLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}
