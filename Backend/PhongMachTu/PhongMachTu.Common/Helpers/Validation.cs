using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhongMachTu.Common.Helpers
{
    public static class Validation
    {
        public static bool IsAlphanumeric(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return Regex.IsMatch(input, "^[a-zA-Z0-9]+$");
        }

    }
}
