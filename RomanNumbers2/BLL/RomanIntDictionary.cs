using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    static class RomanIntDictionary
    {
        public static Dictionary<int, string> GetIntToRomanBasicDictionary()
        {
            Dictionary<int, string> romanIntPairs = new Dictionary<int, string>();

            romanIntPairs.Add(1, "I");
            romanIntPairs.Add(2, "II");
            romanIntPairs.Add(3, "III");
            romanIntPairs.Add(4, "IV");
            romanIntPairs.Add(5, "V");
            romanIntPairs.Add(6, "VI");
            romanIntPairs.Add(7, "VII");
            romanIntPairs.Add(8, "VIII");
            romanIntPairs.Add(9, "IX");
            romanIntPairs.Add(10, "X");
            romanIntPairs.Add(20, "XX");
            romanIntPairs.Add(30, "XXX");
            romanIntPairs.Add(40, "XL");
            romanIntPairs.Add(50, "L");
            romanIntPairs.Add(60, "LX");
            romanIntPairs.Add(70, "LXX");
            romanIntPairs.Add(80, "LXXX");
            romanIntPairs.Add(90, "XC");
            romanIntPairs.Add(100, "C");
            romanIntPairs.Add(200, "CC");
            romanIntPairs.Add(300, "CCC");
            romanIntPairs.Add(400, "CD");
            romanIntPairs.Add(500, "D");
            romanIntPairs.Add(600, "DC");
            romanIntPairs.Add(700, "DCC");
            romanIntPairs.Add(800, "DCCC");
            romanIntPairs.Add(900, "CM");
            romanIntPairs.Add(1000, "M");
            romanIntPairs.Add(2000, "MM");
            romanIntPairs.Add(3000, "MMM");

            return romanIntPairs;
        }
    }
}
