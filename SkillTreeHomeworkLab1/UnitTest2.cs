using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SkillTreeHomeworkLab1
{
    internal class PageGroupCount
    {
        private readonly int _count;

        public PageGroupCount(int count)
        {
            _count = count;
        }

        public IEnumerable<int> SumCost(IEnumerable<OrderModel> collection)
        {
            var totalCount = collection.Count();
            var skip = 0;

            var result = new List<int>();

            while (totalCount >= 0)
            {
                // add to result
                var sum = collection.Skip(skip).Take(_count).Sum(e => e.Cost);

                result.Add(sum);

                totalCount -= _count;
                skip += _count;
            }

            return result;
        }

        public IEnumerable<int> SumRevenue(IEnumerable<OrderModel> collection)
        {
            var totalCount = collection.Count();
            var skip = 0;

            var result = new List<int>();

            while (totalCount >= 0)
            {
                // add to result
                var sum = collection.Skip(skip).Take(_count).Sum(e => e.Revenue);

                result.Add(sum);

                totalCount -= _count;
                skip += _count;
            }

            return result;
        }
    }

    /// <summary>
    /// 去除商業邏輯測試
    /// </summary>
    [TestClass]
    public class PageGroupCountTest
    {
        private static List<OrderModel> _datasource;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Console.WriteLine("ClassInit " + context.TestName);

            _datasource = new List<OrderModel>()
            {
                new OrderModel() {Id = 1,Cost = 1,Revenue = 11,SellPrice = 21}   ,
                new OrderModel() {Id = 2,Cost = 2,Revenue = 12,SellPrice = 22}   ,
                new OrderModel() {Id = 3,Cost = 3,Revenue = 13,SellPrice = 23}   ,
                new OrderModel() {Id = 4,Cost = 4,Revenue = 14,SellPrice = 24}   ,
                new OrderModel() {Id = 5,Cost = 5,Revenue = 15,SellPrice = 25}   ,
                new OrderModel() {Id = 6,Cost = 6,Revenue = 16,SellPrice = 26}   ,
                new OrderModel() {Id = 7,Cost = 7,Revenue = 17,SellPrice = 27}   ,
                new OrderModel() {Id = 8,Cost = 8,Revenue = 18,SellPrice = 28}   ,
                new OrderModel() {Id = 9,Cost = 9,Revenue = 19,SellPrice = 29}   ,
                new OrderModel() {Id = 10,Cost = 10,Revenue = 20,SellPrice = 30}   ,
                new OrderModel() {Id = 11,Cost = 11,Revenue = 21,SellPrice = 31}
            };
        }

        [TestMethod]
        public void Test_PageGroupCount_sum_revenue_groupby_count()
        {
            //建立以筆數來計算 revenue
            var grouptCount = 4;
            var target = new PageGroupCount(grouptCount);

            var expected = new int[] { 50, 66, 60 };
            var actual = target.SumRevenue(_datasource);

            CollectionAssert.AreEqual(expected, actual.ToList());
        }

        [TestMethod]
        public void Test_PageGroupCount_sum_cost_groupby_count()
        {
            var grouptCount = 3;
            //建立以筆數來計算 cost
            var target = new PageGroupCount(grouptCount);

            var expected = new int[] { 6, 15, 24, 21 };
            var actual = target.SumCost(_datasource);

            CollectionAssert.AreEqual(expected, actual.ToList());
        }
    }
}