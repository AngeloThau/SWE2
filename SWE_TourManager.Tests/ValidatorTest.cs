using NUnit.Framework;
using SWE_TourManager.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.Tests
{
    public class ValidatorTest
    {

        Validator validator = new Validator();

        [Test]
        public void TourNameTest()
        {
            Assert.IsTrue(validator.TourNameDoesNotExist("waopefjnqwofnqowfn"));
        }

        [Test]
        public void TourNameTestFalse()
        {
            Assert.IsFalse(validator.TourNameDoesNotExist("First Tour"));
        }

        [Test]
        public void NumberTestFalse()
        {
            Assert.IsFalse(validator.IsNumber("F"));
        }
        [Test]
        public void NumberTest()
        {
            Assert.IsTrue(validator.IsNumber("1"));
        }
        [Test]
        public void InputTest()
        {
            Assert.IsTrue(validator.IsAllowedInput("dfgedh  fgd 88"));
        }
        [Test]
        public void InputTestFalse()
        {
            Assert.IsFalse(validator.IsAllowedInput("dfgedh / fgd 88"));
        }
        [Test]
        public void AlphabetTestFalse()
        {
            Assert.IsFalse(validator.IsAlphabetOrNumber(" 8?8"));
        }
        [Test]
        public void AlphabetTest()
        {
            Assert.IsTrue(validator.IsAlphabetOrNumber("b B 3 4"));
        }
        [Test]
        public void AlphabetTestAlphabet()
        {
            Assert.IsTrue(validator.IsAlphabetOrNumber(" abcdefö"));
        }
        [Test]
        public void AlphabetTestNumber()
        {
            Assert.IsTrue(validator.IsAlphabetOrNumber(" 3 4"));
        }
    }
}
