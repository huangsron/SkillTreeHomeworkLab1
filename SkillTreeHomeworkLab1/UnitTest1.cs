using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SkillTreeHomeworkLab1
{
    /// <summary>
    /// order model
    /// </summary>
    internal class OrderModel
    {
        public int Id { get; set; }

        public int Cost { get; set; }

        public int Revenue { get; set; }

        public int SellPrice { get; set; }
    }

    internal interface ISum
    {
        int Invoke(IEnumerable<OrderModel> collection);
    }

    internal class GroupCount
    {
        private readonly int _count;
        private readonly ISum _calculator;

        public GroupCount(int count, ISum calculator)
        {
            _count = count;
            _calculator = calculator;
        }

        public IEnumerable<int> Invoke(IEnumerable<OrderModel> collection)
        {
            var total = collection.Count();
            var skip = 0;

            var result = new List<int>();

            while (total >= 0)
            {
                // add to result
                result.Add(_calculator.Invoke(collection.Skip(skip).Take(_count)));

                total -= _count;
                skip += _count;
            }

            return result;
        }
    }

    /// <summary>
    /// calculator cost by count
    /// </summary>
    internal class SumCount : ISum
    {
        public int Invoke(IEnumerable<OrderModel> collection)
        {
            return collection.Sum(e => e.Cost);
        }
    }

    /// <summary>
    /// calculator cost by count
    /// </summary>
    internal class SumRevenue : ISum
    {
        public int Invoke(IEnumerable<OrderModel> collection)
        {
            return collection.Sum(e => e.Revenue);
        }
    }

    [TestClass]
    public class UnitTest1
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
        public void Test_sum_revenue_groupby_count()
        {
            var expected = new int[] { 50, 66, 60 };
            var grouptCount = 4;
            var target = new GroupCount(grouptCount, new SumRevenue());

            var actual = target.Invoke(_datasource);

            CollectionAssert.AreEqual(expected, actual.ToList());
        }

        [TestMethod]
        public void Test_sum_cost_groupby_count()
        {
            var expected = new int[] { 6, 15, 24, 21 };
            var grouptCount = 3;
            var target = new GroupCount(grouptCount, new SumCount());

            var actual = target.Invoke(_datasource);

            CollectionAssert.AreEqual(expected, actual.ToList());
        }
    }
}