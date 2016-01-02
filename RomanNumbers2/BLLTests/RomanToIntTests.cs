using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BLL;

namespace BLLTests
{
    [TestFixture]
    public class RomanToIntTests
    {
        RomanToInt romanToInt;

        [SetUp]
        public void SetupTests()
        {
            romanToInt = new RomanToInt();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullInput_ReturnNullException()
        {
            romanToInt.Convert(null);
        }

        [TestCase("")]
        [TestCase("I I")]
        [TestCase("c M V I")]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidCharactersInput_ReturnArgumentException(string stringCorrupted)
        {
            romanToInt.Convert(stringCorrupted);
        }

        [Test]
        //numbers defined in dictionary list
        public void ConvertBasicRomanNumbersToInt()
        {
            Assert.That(romanToInt.Convert("IV"), Is.EqualTo(4));
            Assert.That(romanToInt.Convert("V"), Is.EqualTo(5));
            Assert.That(romanToInt.Convert("VII"), Is.EqualTo(7));
            Assert.That(romanToInt.Convert("L"), Is.EqualTo(50));
            Assert.That(romanToInt.Convert("CM"), Is.EqualTo(900));
        }

        [Test]
        public void ConverNumberBetween_11_and_20()
        {
            Assert.That(romanToInt.Convert("XVII"), Is.EqualTo(17));
        }

        [Test]
        public void ConverNumberBetween_20_and_30()
        {
            Assert.That(romanToInt.Convert("XXV"), Is.EqualTo(25));
        }

        [Test]
        public void ConverNumberBetween_30_and_40()
        {
            Assert.That(romanToInt.Convert("XXXIII"), Is.EqualTo(33));
            Assert.That(romanToInt.Convert("XXXIX"), Is.EqualTo(39));
        }

        [Test]
        public void ConverNumberBetween_40_and_50()
        {
            Assert.That(romanToInt.Convert("XLIV"), Is.EqualTo(44));
        }

        [Test]
        public void ConverNumberBetween_50_and_60()
        {
            Assert.That(romanToInt.Convert("LIV"), Is.EqualTo(54));
            Assert.That(romanToInt.Convert("LVI"), Is.EqualTo(56));
        }

        [Test]
        public void ConverNumberBetween_60_and_70()
        {
            Assert.That(romanToInt.Convert("LXVII"), Is.EqualTo(67));
        }

        [Test]
        public void ConverNumberBetween_70_and_80()
        {
            Assert.That(romanToInt.Convert("LXXIII"), Is.EqualTo(73));
            Assert.That(romanToInt.Convert("LXXIX"), Is.EqualTo(79));
        }

        [Test]
        public void ConverNumberBetween_80_and_90()
        {
            Assert.That(romanToInt.Convert("LXXXVII"), Is.EqualTo(87));
            Assert.That(romanToInt.Convert("LXXXIV"), Is.EqualTo(84));
        }

        [Test]
        public void ConverNumberBetween_90_and_100()
        {
            Assert.That(romanToInt.Convert("XC"), Is.EqualTo(90));
        }
                
        [Test]
        public void ConvertNumberBetween_100_and_110()
        {
            Assert.That(romanToInt.Convert("CV"), Is.EqualTo(105));
            Assert.That(romanToInt.Convert("CVI"), Is.EqualTo(106));
            Assert.That(romanToInt.Convert("CX"), Is.EqualTo(110));
        }

        [Test]
        public void ConvertNumber_120_130()
        {
            Assert.That(romanToInt.Convert("CXX"), Is.EqualTo(120));
            Assert.That(romanToInt.Convert("CXXX"), Is.EqualTo(130));
        }
        [Test]
        public void ConvertNumber_140()
        {
            Assert.That(romanToInt.Convert("CXL"), Is.EqualTo(140));
            Assert.That(romanToInt.Convert("CXLIV"), Is.EqualTo(144));
        }
        [Test]
        public void ConvertNumber_150()
        {
            Assert.That(romanToInt.Convert("CL"), Is.EqualTo(150));
            Assert.That(romanToInt.Convert("CLV"), Is.EqualTo(155));
        }
        [Test]
        public void ConvertNumber_160_180()
        {
            Assert.That(romanToInt.Convert("CLX"), Is.EqualTo(160));
            Assert.That(romanToInt.Convert("CLXXX"), Is.EqualTo(180));
        }
        [Test]
        public void ConvertNumber_190_199()
        {
            Assert.That(romanToInt.Convert("CXC"), Is.EqualTo(190));
        }
        [Test]
        public void ConvertNumber_200_240()
        {
            Assert.That(romanToInt.Convert("CCV"), Is.EqualTo(205));
            Assert.That(romanToInt.Convert("CCXXXII"), Is.EqualTo(232));
        }

        [Test]
        public void ConvertNumber_240_280()
        {
            Assert.That(romanToInt.Convert("CCXLI"), Is.EqualTo(241));
            Assert.That(romanToInt.Convert("CCLXXXVII"), Is.EqualTo(287));
        }

        [Test]
        public void ConvertNumber_290_300()
        {
            Assert.That(romanToInt.Convert("CCXCII"), Is.EqualTo(292));
        }
        [Test]
        public void ConvertNumber_333()
        {
            Assert.That(romanToInt.Convert("CCCXXXIII"), Is.EqualTo(333));
        }

        [Test]
        public void ConvertNumber_400_500()
        {
            Assert.That(romanToInt.Convert("CDXXV"), Is.EqualTo(425));
            Assert.That(romanToInt.Convert("CDLXXXIX"), Is.EqualTo(489));
        }
        [Test]
        public void ConvertNumber_500_900()
        {
            //Assert.That(romanToInt.Convert("DXXXVII"), Is.EqualTo(537));
            Assert.That(romanToInt.Convert("DCCCXCIX"), Is.EqualTo(899));

        }

        [Test]
        public void ConvertNumber_900_1000()
        {
            Assert.That(romanToInt.Convert("CMXLV"), Is.EqualTo(945));

        }
        
        [Test]
        public void ConvertNumber_1100()
        {
            Assert.That(romanToInt.Convert("MC"), Is.EqualTo(1100));
        }

        [Test]
        public void ConvertNumber_1455()
        {
            Assert.That(romanToInt.Convert("MCDLV"), Is.EqualTo(1455));
        }

        [Test]
        public void ConvertNumber_2999()
        {
            Assert.That(romanToInt.Convert("MMCMXCIX"), Is.EqualTo(2999));
        }

        [Test]
        public void ConvertNumber_3999()
        {
            Assert.That(romanToInt.Convert("MMMCMXCIX"), Is.EqualTo(3999));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Convert_XM_ThrowException()
        {
            romanToInt.Convert("XM");
        }


        [TestCase("IIII")]
        [TestCase("XXXX")]
        [TestCase("CCCC")]
        [TestCase("MMMM")]
        [TestCase("DD")]
        [TestCase("LL")]
        [ExpectedException(typeof(ArgumentException))]
        public void MultipleSameCharacters_ThrowException(string romanNumber)
        {
            romanToInt.Convert(romanNumber);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Convert_DCD_ThrowException()
        {
            romanToInt.Convert("DCD");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Convert_LXL_ThrowException()
        {
            romanToInt.Convert("LXL");
        }

        [TestCase("MMMCMM")]
        [TestCase("CCDCC")]
        [TestCase("CDC")]
        [TestCase("LCX")]
        [TestCase("IIX")]
        [TestCase("IXI")]
        [TestCase("XIIV")]
        [TestCase("IXIL")]
        [TestCase("CXIL")]
        [TestCase("DM")]
        [ExpectedException(typeof(ArgumentException))]
        public void RandomCorruptedNumbers_ThrowException(string corruptedRomanNumber)
        {
            romanToInt.Convert(corruptedRomanNumber);
        }
    }
}
