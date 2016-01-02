using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RomanToInt : IRomanToInt
    {
        List<char> allowedRomanCharacters = new List<char>() { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };
        Dictionary<int, string> romanIntPairs;
        
        public RomanToInt()
        {
            romanIntPairs = RomanIntDictionary.GetIntToRomanBasicDictionary();
        }

        public int Convert(string romanNo)
        {
            string romanNumbTrimmed = BasicValidate(romanNo);

            int convertedIntNumber = ConvertSimpleRomanNumbersToInt(romanNumbTrimmed); //returns 0 if the number is not defined in a dictionary

            if (convertedIntNumber == 0)
            {
                convertedIntNumber += ConvertThousands(ref romanNumbTrimmed);

                convertedIntNumber += ConvertHundreds(ref romanNumbTrimmed);

                convertedIntNumber += ConvertUpTo100(romanNumbTrimmed);
            }

            return convertedIntNumber;
        }

        private static int ConvertThousands(ref string romanNumbTrimmed)
        {
            int convertedIntNumber = 0;
            if (romanNumbTrimmed.StartsWith("MMM"))
            {
                convertedIntNumber += 3000;
                romanNumbTrimmed = romanNumbTrimmed.Substring(3);
            }
            else if (romanNumbTrimmed.StartsWith("MM"))
            {
                convertedIntNumber += 2000;
                romanNumbTrimmed = romanNumbTrimmed.Substring(2);
            }
            else if (romanNumbTrimmed.StartsWith("M"))
            {
                convertedIntNumber += 1000;
                romanNumbTrimmed = romanNumbTrimmed.Substring(1);
            }

            return convertedIntNumber;
        }

        private static int ConvertHundreds(ref string romanNumberWithoutHundreds)
        {
            int convertedIntNumber = 0;
            if (romanNumberWithoutHundreds.StartsWith("D"))
            {
                convertedIntNumber = 500;
                romanNumberWithoutHundreds = romanNumberWithoutHundreds.Substring(1);
                if (romanNumberWithoutHundreds.Contains('M')) throw new ConversionException();
            }

            if (romanNumberWithoutHundreds.StartsWith("CCC"))
            {
                convertedIntNumber += 300;
                romanNumberWithoutHundreds = romanNumberWithoutHundreds.Substring(3);
                if (romanNumberWithoutHundreds.Contains('D') || romanNumberWithoutHundreds.Contains('M')) throw new ConversionException();

            }
            else if (romanNumberWithoutHundreds.StartsWith("CC"))
            {
                convertedIntNumber += 200;
                romanNumberWithoutHundreds = romanNumberWithoutHundreds.Substring(2);
                if (romanNumberWithoutHundreds.Contains('D') || romanNumberWithoutHundreds.Contains('M'))  throw new ConversionException();
            }
            else if (romanNumberWithoutHundreds.StartsWith("C"))
            {
                convertedIntNumber += 100;
                romanNumberWithoutHundreds = romanNumberWithoutHundreds.Substring(1);

                if (romanNumberWithoutHundreds.Length > 0)
                {
                    if (romanNumberWithoutHundreds[0] == 'D')
                    {
                        convertedIntNumber = 400;
                        romanNumberWithoutHundreds = romanNumberWithoutHundreds.Substring(1);
                        if (romanNumberWithoutHundreds.Contains('C')) throw new ConversionException();
                    }
                    if (romanNumberWithoutHundreds[0] == 'M')
                    {
                        convertedIntNumber = 900;
                        romanNumberWithoutHundreds = romanNumberWithoutHundreds.Substring(1);                        
                    }                    
                }
            }

            return convertedIntNumber;
        }

        private int ConvertUpTo100(string romanNumbTrimmed)
        {
            if (romanNumbTrimmed.Length == 0) return 0;
            int convertedIntNumber = 0;
            char firstChar = romanNumbTrimmed[0];
            
            if (firstChar == 'L') convertedIntNumber = 50;
            if (firstChar == 'X') convertedIntNumber = 10;

            string continuation = convertedIntNumber != 0 ? romanNumbTrimmed.Substring(1) : romanNumbTrimmed;
            if (continuation.Length == 0) return convertedIntNumber;

            if (continuation[0] == 'X')            
                convertedIntNumber += HandleXContinuations(continuation);            
            else if (continuation[0] == 'V')            
                convertedIntNumber += HandleLastContinuation(continuation);            
            else if (continuation[0] == 'L')
            {
                convertedIntNumber = 40;
                if (continuation.Substring(1).Length > 0) convertedIntNumber += HandleLastContinuation(continuation.Substring(1));
            }
            else if (continuation[0] == 'C')
            {
                if (firstChar == 'L') throw new ConversionException();
                else
                {
                    convertedIntNumber = 90;
                    if (continuation.Substring(1).Length > 0) convertedIntNumber += HandleLastContinuation(continuation.Substring(1));
                }
            }
            else if (continuation[0] == 'M')            
                throw new ConversionException();            
            else
            {
                int rest = ConvertSimpleRomanNumbersToInt(continuation);
                if(rest == 0) throw new ConversionException();
                convertedIntNumber += rest;
            }
                        
            return convertedIntNumber;
        }

        private int HandleXContinuations(string continuation)
        {
            int convertedIntNumber = 10;
            if (continuation.Length > 1)
            {
                string X2ndContinuation = continuation.Substring(1);
                if (X2ndContinuation[0] == 'X')
                {
                    convertedIntNumber += 10;
                    if (X2ndContinuation.Length > 1)
                    {
                        string X3rdContinuation = X2ndContinuation.Substring(1);

                        if (X3rdContinuation[0] == 'X')
                        {
                            convertedIntNumber += 10;
                            if (X3rdContinuation.Substring(1).Length > 1)
                                convertedIntNumber += HandleLastContinuation(X3rdContinuation.Substring(1));
                        }
                        else
                            convertedIntNumber += HandleLastContinuation(X3rdContinuation);

                    }
                }
                else
                {
                    convertedIntNumber += HandleLastContinuation(X2ndContinuation);
                }
            }

            return convertedIntNumber;
        }

        //does not check all cases - lots of checking is done during conversion process
        private string BasicValidate(string romanNo)
        {
            if (romanNo == null) throw new ArgumentNullException();
            if (romanNo == string.Empty) throw new ConversionException();

            string romanNoTrimmed = romanNo.Trim();

            foreach (char c in romanNoTrimmed)            
                if (hasNonRomanNumberCharacters(c)) throw new ConversionException();
            
            if (hasIllegalSuccessiveCharacters(romanNoTrimmed)) throw new ConversionException();
            if (hasTooMany_L_D_characters(romanNoTrimmed)) throw new ConversionException();

            return romanNoTrimmed;
        }

        private static bool hasTooMany_L_D_characters(string romanNoTrimmed)
        {
            return romanNoTrimmed.Count(x => x == 'L') > 1 || romanNoTrimmed.Count(x => x == 'D') > 1;
        }

        private bool hasNonRomanNumberCharacters(char c)
        {
            return !allowedRomanCharacters.Contains(c);
        }

        private static bool hasIllegalSuccessiveCharacters(string romanNoTrimmed)
        {
            return romanNoTrimmed.Contains("IIII") || romanNoTrimmed.Contains("XXXX") || romanNoTrimmed.Contains("CCCC") || romanNoTrimmed.Contains("MMMM")
                            || romanNoTrimmed.Contains("LL") || romanNoTrimmed.Contains("DD");
        }

        private int HandleLastContinuation(string lastContinuation)
        {            
            int rest = ConvertSimpleRomanNumbersToInt(lastContinuation);
            if (rest == 0)
                throw new ConversionException();

            return rest;
        }

        private int ConvertSimpleRomanNumbersToInt(string romanNoTrimmed)
        {
            return romanIntPairs.FirstOrDefault(x => x.Value == romanNoTrimmed).Key;
        }
    }
}