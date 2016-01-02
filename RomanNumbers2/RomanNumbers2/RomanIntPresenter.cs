using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;

namespace RomanNumbers2
{
    class RomanIntPresenter
    {
        IIntToRoman intToRomanInterfaceObj;
        IRomanToInt romanToIntInterfaceObj;
        RomanIntView romanIntViewObj;

        public RomanIntPresenter()
        {
            intToRomanInterfaceObj = new IntToRoman();
            romanToIntInterfaceObj = new RomanToInt();            
        }

        public void addView(RomanIntView _romanIntViewObj)
        {
            romanIntViewObj = _romanIntViewObj;
        }
                
        internal void Convert(string inputNumber)
        {                        
            try
            {
                int integerInput = -1;
                bool isInteger = int.TryParse(inputNumber, out integerInput);

                if (isInteger)
                {
                    string romanNumber = intToRomanInterfaceObj.Convert(integerInput);
                    romanIntViewObj.DisplayResult(romanNumber);
                }
                else
                {
                    int integerResult = romanToIntInterfaceObj.Convert(inputNumber);
                    romanIntViewObj.DisplayResult(integerResult.ToString());
                }

            }
            catch (Exception ex)
            {
                romanIntViewObj.HandleErrorMessage(ex);
            }
        }

        internal void ClearResult()
        {
            romanIntViewObj.ClearResult();
        }
    }
}
