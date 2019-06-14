using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProducts.Models;
using System;
using System.Threading.Tasks;

namespace MyProducts.Tests.Models
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public async Task TestReverse()
        {
            MyArrayCalc calc = new MyArrayCalc();
            int[] a = new int []{ 1, 2, 3, 4, 5, 6 };

            int len = a.Length;
            int[] b = await calc.Reverse(a);

            for(int i=0;i<a.Length;i++)
             {
                 Assert.AreEqual(b[i], a[len-i-1]);
             }
        }

        [TestMethod]
        public async Task TestDelete()
        {
            MyArrayCalc calc = new MyArrayCalc();
            int[] a = new int[] { 1, 2, 3, 4, 5, 6 };

            //abnormal input
            try
            {
               await calc.Delete(-1, a);
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e,typeof(ArgumentException));
            }

            //normal input
            int len = a.Length;
            int[] b = await calc.Delete(0, a);
            for(int i=0;i<len-1;i++)
            {
                Assert.AreEqual(b[i], a[i + 1]);
            }

            b = await calc.Delete(2, a);
            Assert.AreEqual(b[2], 4);

            b = await calc.Delete(len-1, a);
            Assert.AreEqual(b[4], 5);
        }

    }
}
