using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using ZRO06A_HFT_2021221.Logic;
using ZRO06A_HFT_2021221.Models;
using ZRO06A_HFT_2021221.Repository;

namespace ZRO06A_HFT_2021221.Test
{
   internal class CustomerLogicTestWithMock
   {
      private readonly CustomerLogic customerLogic;

      public CustomerLogicTestWithMock()
      {
         var mockCustomerRepository = new Mock<ICustomerRepository>();

         mockCustomerRepository.Setup(t => t.Create(It.IsAny<Customer>()));
         mockCustomerRepository.Setup(t => t.GetAll()).Returns(
            new List<Customer>
            {
               new()
               {
                  Id = 1,
                  Name = "Sam Winchester",
                  Orders = new List<Order>
                  {
                     new() { Id = 1, Price = 1000, CustomerId = 1, Date = new DateTime(2005, 09, 15) },
                     new() { Id = 2, Price = 3000, CustomerId = 1, Date = new DateTime(2007, 10, 5) },
                     new() { Id = 3, Price = 2000, CustomerId = 1, Date = new DateTime(2006, 09, 29) }
                  }
               },
               new()
               {
                  Id = 2,
                  Name = "Dean Winchester",
                  Orders = new List<Order>
                  {
                     new() { Id = 4, Price = 5000, CustomerId = 2, Date = new DateTime(2009, 09, 11) },
                     new() { Id = 5, Price = 4000, CustomerId = 2, Date = new DateTime(2008, 09, 19) }
                  }
               }
            }.AsQueryable());

         mockCustomerRepository.Setup(t => t.GetOne(It.IsAny<int>())).Returns<int>(id =>
         {
            return mockCustomerRepository.Object.GetAll().SingleOrDefault(x => x.Id == id);
         });

         customerLogic = new CustomerLogic(mockCustomerRepository.Object);
      }

      [TestCase(1, 6000)]
      [TestCase(2, 9000)]
      public void CustomerPaidSumTest(int id, int sum)
      {
         Assert.That(customerLogic.GetPaidSum(id), Is.EqualTo(sum));
      }

      [TestCase(1, 2)]
      [TestCase(2, 4)]
      public void CustomerLastOrderTest(int id, int orderId)
      {
         Assert.That(customerLogic.GetLastOrder(id).Id, Is.EqualTo(orderId));
      }

      [TestCase("John Winchester", true)]
      [TestCase("", false)]
      [TestCase(null, false)]
      public void CreateCustomerTestName(string name, bool result)
      {
         //ACT + ASSERT
         if (result)
            Assert.That(() => customerLogic.Create(new Customer
            {
               Name = name
            }), Throws.Nothing);
         else
            Assert.That(() => customerLogic.Create(new Customer
            {
               Name = name
            }), Throws.Exception);
      }

      [Test]
      public void UpdateCustomerTestNull()
      {
         Assert.That(() => customerLogic.Update(null), Throws.ArgumentNullException);
      }

      [Test]
      public void UpdateNullCustomerTest()
      {
         Assert.That(() => customerLogic.Update(new Customer
         {
            Id = 999,
            Name = "Marry Winchester"
         }), Throws.Exception);
      }

      [TestCase("John Winchester", true)]
      [TestCase("", false)]
      [TestCase(null, false)]
      public void UpdateCustomerTestName(string name, bool result)
      {
         //ACT + ASSERT
         if (result)
            Assert.That(() => customerLogic.Update(new Customer
            {
               Id = 1,
               Name = name
            }), Throws.Nothing);
         else
            Assert.That(() => customerLogic.Update(new Customer
            {
               Id = 1,
               Name = name
            }), Throws.Exception);
      }
   }
}