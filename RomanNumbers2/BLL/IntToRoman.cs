using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class IntToRoman : IIntToRoman
    {
        Dictionary<int, string> romanIntPairs;

        public IntToRoman()
        {
            romanIntPairs = RomanIntDictionary.GetIntToRomanBasicDictionary();
        }
        
        public string Convert(int v)
        {
            string romanNumber = "I";

            if (v <= 0 || v > 3999) throw new ConversionException(); 

            if (isOneDigitRomanNumber(v) || (v < 100 && v % 10 == 0) || (v > 100 && v % 100 == 0))
                romanIntPairs.TryGetValue(v, out romanNumber);
            else
            {
                if(v > 10 && v < 100)                
                    romanNumber = ConvertDecadesToRomanNumber(v);                
                if (v > 100 && v < 1000)                
                    romanNumber = ConvertHundredsToRomanNumber(v);
                if (v > 1000)
                {
                    romanNumber = ConvertThousandsToRomanNumber(v);
                }
            }
            return romanNumber;
        }

        private string ConvertThousandsToRomanNumber(int v)
        {
            string romanNumber;
            int thousands = (v / 1000) * 1000;
            string thousandsRoman = "";
            romanIntPairs.TryGetValue(thousands, out thousandsRoman);

            romanNumber = thousandsRoman + ConvertHundredsToRomanNumber(v - thousands);
            return romanNumber;
        }

        private string ConvertHundredsToRomanNumber(int v)
        {
            string romanNumber;
            int hundreds = (v / 100) * 100;
            int decades = ((v - hundreds) / 10) * 10;
            int oneToNine = v - hundreds - decades;

            string hundredsRoman = "";
            romanIntPairs.TryGetValue(hundreds, out hundredsRoman);

            romanNumber = hundredsRoman + ConvertDecadesToRomanNumber(v - hundreds);
            return romanNumber;
        }

        private string ConvertDecadesToRomanNumber(int twoDigitNumber)
        {
            string romanNumber;
            int decades = (twoDigitNumber / 10) * 10;
            int oneToNine = twoDigitNumber % 10;

            string tensRoman = "";
            string sipleRoman = "";

            romanIntPairs.TryGetValue(decades, out tensRoman);

            if (oneToNine != 0)
                romanIntPairs.TryGetValue(oneToNine, out sipleRoman);

            romanNumber = tensRoman + sipleRoman;
            return romanNumber;
        }

        private bool isOneDigitRomanNumber(int v)
        {
            if (v <= 10 || v == 50 || v == 100 || v == 500 || v == 1000) return true;
            else return false;
        }
    }
}

