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
    public class IntToRomanTests
    {
        IntToRoman intToRoman;

        [SetUp]
        public void SetupTests()
        {            
            intToRoman = new IntToRoman();
        }

        [TestCase(-100)]
        [TestCase(0)]
        [TestCase(4000)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        //Supported conversion range in this version is 1-3999
        public void Convert_InputOutOfRange_ThrowException(int inputNumber)
        {
            intToRoman.Convert(inputNumber);
        }

        [Test]               
        public void ConvertBasicNumbersToRoman()
        {
            Assert.That(intToRoman.Convert(1), Is.EqualTo("I"));
            Assert.That(intToRoman.Convert(2), Is.EqualTo("II"));
            Assert.That(intToRoman.Convert(3), Is.EqualTo("III"));
            Assert.That(intToRoman.Convert(4), Is.EqualTo("IV"));
            Assert.That(intToRoman.Convert(5), Is.EqualTo("V"));
            Assert.That(intToRoman.Convert(6), Is.EqualTo("VI"));
            Assert.That(intToRoman.Convert(7), Is.EqualTo("VII"));
            Assert.That(intToRoman.Convert(8), Is.EqualTo("VIII"));
            Assert.That(intToRoman.Convert(9), Is.EqualTo("IX"));
            Assert.That(intToRoman.Convert(10), Is.EqualTo("X"));
            Assert.That(intToRoman.Convert(50), Is.EqualTo("L"));
            Assert.That(intToRoman.Convert(100), Is.EqualTo("C"));
            Assert.That(intToRoman.Convert(500), Is.EqualTo("D"));
            Assert.That(intToRoman.Convert(1000), Is.EqualTo("M"));            
        }

        [Test]
        public void ConvertTensUpTo100()
        {
            Assert.That(intToRoman.Convert(20), Is.EqualTo("XX"));
            Assert.That(intToRoman.Convert(30), Is.EqualTo("XXX"));
            Assert.That(intToRoman.Convert(40), Is.EqualTo("XL"));
            Assert.That(intToRoman.Convert(50), Is.EqualTo("L"));
            Assert.That(intToRoman.Convert(60), Is.EqualTo("LX"));
            Assert.That(intToRoman.Convert(70), Is.EqualTo("LXX"));
            Assert.That(intToRoman.Convert(80), Is.EqualTo("LXXX"));
            Assert.That(intToRoman.Convert(90), Is.EqualTo("XC"));
        }

        [Test]
        public void ConvertHundreedsUpTo1000()
        {
            Assert.That(intToRoman.Convert(200), Is.EqualTo("CC"));
            Assert.That(intToRoman.Convert(300), Is.EqualTo("CCC"));
            Assert.That(intToRoman.Convert(400), Is.EqualTo("CD"));
            Assert.That(intToRoman.Convert(500), Is.EqualTo("D"));
            Assert.That(intToRoman.Convert(600), Is.EqualTo("DC"));
            Assert.That(intToRoman.Convert(700), Is.EqualTo("DCC"));
            Assert.That(intToRoman.Convert(800), Is.EqualTo("DCCC"));
            Assert.That(intToRoman.Convert(900), Is.EqualTo("CM"));
        }

        [Test]
        public void ConvertAnyBetween_10_and_100()
        {
            Assert.That(intToRoman.Convert(77), Is.EqualTo("LXXVII"));
            Assert.That(intToRoman.Convert(35), Is.EqualTo("XXXV"));
            Assert.That(intToRoman.Convert(94), Is.EqualTo("XCIV"));
        }

        [Test]
        public void ConvertAnyBetween_100_and_1000()
        {
            Assert.That(intToRoman.Convert(331), Is.EqualTo("CCCXXXI"));
            Assert.That(intToRoman.Convert(777), Is.EqualTo("DCCLXXVII"));
            Assert.That(intToRoman.Convert(999), Is.EqualTo("CMXCIX"));
        }

        [Test]
        public void ConvertAnyBetween_1000_and_3999()
        {
            Assert.That(intToRoman.Convert(1021), Is.EqualTo("MXXI"));            
            Assert.That(intToRoman.Convert(1754), Is.EqualTo("MDCCLIV"));
            Assert.That(intToRoman.Convert(2983), Is.EqualTo("MMCMLXXXIII"));
            Assert.That(intToRoman.Convert(3999), Is.EqualTo("MMMCMXCIX"));
        }





    }
}

