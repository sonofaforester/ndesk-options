//
// OptionTest.cs
//
// Authors:
//  Jonathan Pryor <jpryor@novell.com>
//
// Copyright (C) 2008 Novell (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;

using NDesk.Options;
using NUnit.Framework;

namespace Tests.NDesk.Options {
	class DefaultOption : Option {
		public DefaultOption (string prototypes, string description)
			: base (prototypes, description)
		{
		}

		public DefaultOption (string prototypes, string description, int c)
			: base (prototypes, description, c)
		{
		}

		protected override void OnParseComplete (OptionContext c)
		{
			throw new NotImplementedException ();
		}
	}

	[TestFixture]
	public class OptionTest {
		[Test]
		public void Exceptions ()
		{
			object p = null;
			Assert.That(() => new DefaultOption (null, null), Throws.TypeOf<ArgumentNullException>());
			// Utils.AssertException (typeof(ArgumentNullException), 
			// 		"Argument cannot be null.\nParameter name: prototype", 
			// 		p, v => { new DefaultOption (null, null); });
			Assert.That(() => new DefaultOption ("", null), Throws.TypeOf<ArgumentException>());
			// Utils.AssertException (typeof(ArgumentException), 
			// 		"Cannot be the empty string.\nParameter name: prototype",
			// 		p, v => { new DefaultOption ("", null); });
			Assert.That(() => new DefaultOption ("a|b||c=", null), Throws.TypeOf<ArgumentException>());
			// Utils.AssertException (typeof(ArgumentException),
			// 		"Empty option names are not supported.\nParameter name: prototype",
			// 		p, v => { new DefaultOption ("a|b||c=", null); });
			Assert.That(() => new DefaultOption ("a=|b:", null), Throws.TypeOf<ArgumentException>());
			// Utils.AssertException (typeof(ArgumentException),
			// 		"Conflicting option types: '=' vs. ':'.\nParameter name: prototype",
			// 		p, v => { new DefaultOption ("a=|b:", null); });
			Assert.That(() => new DefaultOption ("<>=", null), Throws.TypeOf<ArgumentException>());
			// Utils.AssertException (typeof(ArgumentException),
			// 		"The default option handler '<>' cannot require values.\nParameter name: prototype",
			// 		p, v => { new DefaultOption ("<>=", null); });
			Assert.That(() => new DefaultOption ("<>:", null), Throws.TypeOf<ArgumentException>());
			// Utils.AssertException (typeof(ArgumentException),
			// 		"The default option handler '<>' cannot require values.\nParameter name: prototype",
			// 		p, v => { new DefaultOption ("<>:", null); });
			Assert.That(() => new DefaultOption ("t|<>=", null), Throws.Nothing);
			// Utils.AssertException (null, null,
			// 		p, v => { new DefaultOption ("t|<>=", null, 1); });
			Assert.That(() => new DefaultOption ("t|<>=", null, 2), Throws.TypeOf<ArgumentException>());
			// Utils.AssertException (typeof(ArgumentException),
			// 		"The default option handler '<>' cannot require values.\nParameter name: prototype",
			// 		p, v => { new DefaultOption ("t|<>=", null, 2); });
			Assert.That(() => new DefaultOption ("a|b=", null, 2), Throws.Nothing);
			// Utils.AssertException (null, null,
			// 		p, v => { new DefaultOption ("a|b=", null, 2); });
			Assert.That(() => new DefaultOption ("a", null, -1), Throws.TypeOf<ArgumentOutOfRangeException>());
			// Utils.AssertException (typeof(ArgumentOutOfRangeException),
			// 		"Argument is out of range.\nParameter name: maxValueCount",
			// 		p, v => { new DefaultOption ("a", null, -1); });
			Assert.That(() => new DefaultOption ("a=", null, 0), Throws.TypeOf<ArgumentException>());
			// Utils.AssertException (typeof(ArgumentException),
			// 		"Cannot provide maxValueCount of 0 for OptionValueType.Required or " +
			// 			"OptionValueType.Optional.\nParameter name: maxValueCount",
			// 		p, v => { new DefaultOption ("a=", null, 0); });
			Assert.That(() => new DefaultOption ("a={", null), Throws.TypeOf<ArgumentException>());
			// Utils.AssertException (typeof(ArgumentException),
			// 		"Ill-formed name/value separator found in \"a={\".\nParameter name: prototype",
			// 		p, v => { new DefaultOption ("a={", null); });
			Assert.That(() => new DefaultOption ("a=}", null), Throws.TypeOf<ArgumentException>());
			// Utils.AssertException (typeof(ArgumentException),
			// 		"Ill-formed name/value separator found in \"a=}\".\nParameter name: prototype",
			// 		p, v => { new DefaultOption ("a=}", null); });
			Assert.That(() => new DefaultOption ("a={{}}", null), Throws.TypeOf<ArgumentException>());
			// Utils.AssertException (typeof(ArgumentException),
			// 		"Ill-formed name/value separator found in \"a={{}}\".\nParameter name: prototype",
			// 		p, v => { new DefaultOption ("a={{}}", null); });
			Assert.That(() => new DefaultOption ("a={}}", null), Throws.TypeOf<ArgumentException>());
			// Utils.AssertException (typeof(ArgumentException),
			// 		"Ill-formed name/value separator found in \"a={}}\".\nParameter name: prototype",
			// 		p, v => { new DefaultOption ("a={}}", null); });
			Assert.That(() => new DefaultOption ("a={}{", null), Throws.TypeOf<ArgumentException>());
			// Utils.AssertException (typeof(ArgumentException),
			// 		"Ill-formed name/value separator found in \"a={}{\".\nParameter name: prototype",
			// 		p, v => { new DefaultOption ("a={}{", null); });
			Assert.That(() => new DefaultOption ("a==", null), Throws.TypeOf<ArgumentException>());
			// Utils.AssertException (typeof(ArgumentException),
			// 		"Cannot provide key/value separators for Options taking 1 value(s).\nParameter name: prototype",
			// 		p, v => { new DefaultOption ("a==", null); });
			Assert.That(() => new DefaultOption ("a={}", null), Throws.TypeOf<ArgumentException>());
			// Utils.AssertException (typeof(ArgumentException),
			// 		"Cannot provide key/value separators for Options taking 1 value(s).\nParameter name: prototype",
			// 		p, v => { new DefaultOption ("a={}", null); });
			Assert.That(() => new DefaultOption ("a=+-*/", null), Throws.TypeOf<ArgumentException>());
			// Utils.AssertException (typeof(ArgumentException),
			// 		"Cannot provide key/value separators for Options taking 1 value(s).\nParameter name: prototype",
			// 		p, v => { new DefaultOption ("a=+-*/", null); });
			Assert.That(() => new DefaultOption ("a", null, 0), Throws.Nothing);
			// Utils.AssertException (null, null,
			// 		p, v => { new DefaultOption ("a", null, 0); });
			Assert.That(() => new DefaultOption ("a", null, 0), Throws.Nothing);
			// Utils.AssertException (null, null,
			// 		p, v => { new DefaultOption ("a", null, 0); });
			DefaultOption d;
			string[] s;
			d = new DefaultOption ("a", null);
			Assert.AreEqual (d.GetValueSeparators ().Length, 0);
			// Utils.AssertException (null, null, 
			// 		p, v => {
			// 			var d = new DefaultOption ("a", null);
			// 			Assert.AreEqual (d.GetValueSeparators ().Length, 0);
			// 		});
			Assert.That(() => new DefaultOption ("t|<>=", null), Throws.Nothing);
			d = new DefaultOption ("a=", null, 1);
			s = d.GetValueSeparators ();
			Assert.AreEqual (s.Length, 0);
			// Utils.AssertException (null, null,
			// 		p, v => {
			// 			var d = new DefaultOption ("a=", null, 1);
			// 			string[] s = d.GetValueSeparators ();
			// 			Assert.AreEqual (s.Length, 0);
			// 		});
			d = new DefaultOption ("a=", null, 2);
			s = d.GetValueSeparators ();
			Assert.AreEqual (s.Length, 2);
			Assert.AreEqual (s [0], ":");
			Assert.AreEqual (s [1], "=");
			// Utils.AssertException (null, null,
			// 		p, v => {
			// 			var d = new DefaultOption ("a=", null, 2);
			// 			string[] s = d.GetValueSeparators ();
			// 			Assert.AreEqual (s.Length, 2);
			// 			Assert.AreEqual (s [0], ":");
			// 			Assert.AreEqual (s [1], "=");
			// 		});
			d = new DefaultOption ("a={}", null, 2);
			s = d.GetValueSeparators ();
			Assert.AreEqual (s.Length, 0);
			// Utils.AssertException (null, null,
			// 		p, v => {
			// 			var d = new DefaultOption ("a={}", null, 2);
			// 			string[] s = d.GetValueSeparators ();
			// 			Assert.AreEqual (s.Length, 0);
			// 		});
			d = new DefaultOption ("a={-->}{=>}", null, 2);
			s = d.GetValueSeparators ();
			Assert.AreEqual (s.Length, 2);
			Assert.AreEqual (s [0], "-->");
			Assert.AreEqual (s [1], "=>");
			// Utils.AssertException (null, null,
			// 		p, v => {
			// 			var d = new DefaultOption ("a={-->}{=>}", null, 2);
			// 			string[] s = d.GetValueSeparators ();
			// 			Assert.AreEqual (s.Length, 2);
			// 			Assert.AreEqual (s [0], "-->");
			// 			Assert.AreEqual (s [1], "=>");
			// 		});
			d = new DefaultOption ("a=+-*/", null, 2);
			s = d.GetValueSeparators ();
			Assert.AreEqual (s.Length, 4);
			Assert.AreEqual (s [0], "+");
			Assert.AreEqual (s [1], "-");
			Assert.AreEqual (s [2], "*");
			Assert.AreEqual (s [3], "/");
			// Utils.AssertException (null, null,
			// 		p, v => {
			// 			var d = new DefaultOption ("a=+-*/", null, 2);
			// 			string[] s = d.GetValueSeparators ();
			// 			Assert.AreEqual (s.Length, 4);
			// 			Assert.AreEqual (s [0], "+");
			// 			Assert.AreEqual (s [1], "-");
			// 			Assert.AreEqual (s [2], "*");
			// 			Assert.AreEqual (s [3], "/");
			// 		});
		}
	}
}

