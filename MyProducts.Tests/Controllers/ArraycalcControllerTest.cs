using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MyProducts.Controllers;
using MyProducts.Models;

namespace MyProducts.Tests.Controllers
{
    [TestClass]
    public class ArraycalcControllerTest
    {
        [TestMethod]
        public async Task TestGetReverse()
        {
            var mock = new Mock<IArrayCalc>();
            mock.Setup(o => o.Reverse(It.IsAny<int[]>())).Returns(()=>Task.FromResult(new int[] { 6,5,4,3,2,1}));

            var ctrl = new ArraycalcController(mock.Object);

            int[] nums = new int[6] { 1, 2, 3, 4, 5, 6 };
            var actionResult = await ctrl.Get(nums);

            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            var valueResult = okResult.Value as int[];
            int len = nums.Length;
            for (int i=0;i<len;i++)
            {
                Assert.AreEqual(valueResult[i],nums[len-i-1]);
            }
        }

        [TestMethod]
        public async Task TestGetDelete()
        {
            var mock = new Mock<IArrayCalc>();

            int[] nums = new int[6] { 1, 2, 3, 4, 5, 6 };

            //Test position == -1
            mock.Setup(o => o.Delete(It.Is<int>(x => x == -1), It.Is<int[]>(a=>a==nums)))
                .Throws<ArgumentException>();
            var ctrl = new ArraycalcController(mock.Object);
            var actionResult = await ctrl.Get(-1,nums);
            var badRequestResult = actionResult.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            var valueResult = badRequestResult.Value as String;
            Assert.AreEqual(valueResult, "Invalid Parameters");
            
            //Test position > length
            mock.Setup(o => o.Delete(It.Is<int>(x => x > nums.Length), It.Is<int[]>(a => a == nums)))
               .Throws<ArgumentException>();
            ctrl = new ArraycalcController(mock.Object);
            actionResult = await ctrl.Get(-1, nums);
            badRequestResult = actionResult.Result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            valueResult = badRequestResult.Value as String;
            Assert.AreEqual(valueResult, "Invalid Parameters");

            //Test Normal position
            var resultExpected = new int[] { 2, 3, 4, 5, 6 };
            mock.Setup(o => o.Delete(It.Is<int>(p=>p==0), It.Is<int[]>(a => a == nums)))
               .Returns(() => Task.FromResult(resultExpected));
            ctrl = new ArraycalcController(mock.Object);
            actionResult = await ctrl.Get(0, nums);
            var okResult = actionResult.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var valResult = okResult.Value as int[];
            for(int i=0;i<nums.Length-1;i++)
            {
                valResult[i] = resultExpected[i];
            }
        }
    }
}
