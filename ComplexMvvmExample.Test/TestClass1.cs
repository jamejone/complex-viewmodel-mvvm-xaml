using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ComplexMvvmExample.ViewModel;

namespace ComplexMvvmExample.Test
{
    [TestFixture]
    public class TestClass1
    {
        [Test]
        public void TestCalculatedValueRaised()
        {
            var c3 = new Class3();
            var c2 = new Class2(c3);
            var c1 = new Class1(c2);

            var expecting = new string[] { Class1.CalculatedValue1PropertyName }.ToList(); //Expect these to change

            c1.PropertyChanged += (o, e) =>
            {
                expecting.Remove(e.PropertyName);
            };

            c3.Integer3 = 42; //Should immediately execute callback above

            Assert.AreEqual(0, expecting.Count);
        }
    }
}
