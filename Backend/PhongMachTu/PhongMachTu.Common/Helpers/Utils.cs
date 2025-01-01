﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.Helpers
{
    public static class Utils
    {
        public static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static string RandomPassword()
        {
            Random random = new Random();
            int length = random.Next(6, 11);
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = chars[random.Next(chars.Length)];
            }
            return new string(result);
        }



    }
}