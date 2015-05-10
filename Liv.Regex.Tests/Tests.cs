using System;
using System.Collections.Generic;
using System.Text;
using Liv.RegExp;
using NUnit.Framework;

namespace Liv.RegexTests
{
	[TestFixture]
    public class Tests
    {

		[TestFixtureSetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConvertStringToIntTest()
        {
			var m = Regex.Match("a(b)c", @"\(.+?\)");

        }
    }
}
