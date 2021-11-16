using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using ZRO06A_HFT_2021221.Logic;
using ZRO06A_HFT_2021221.Models;
using ZRO06A_HFT_2021221.Repository;

namespace ZRO06A_HFT_2021221.Test
{
   [TestFixture]
   public class LogicTestWithMock
   {
      private readonly CarLogic carLogic;

      public LogicTestWithMock()
      {
         var mockCarRepository = new Mock<ICarRepository>();
         
         var fakeBrand = new Brand()
         {
            Name = "Audi"
         };

         mockCarRepository.Setup((t) => t.Create(It.IsAny<Car>()));
         mockCarRepository.Setup((t) => t.GetAll()).Returns(
            new List<Car>()
            {
               new()
               {
                  Model = "A3",
                  BasePrice = 2000,
                  Brand = fakeBrand
               },
               new()
               {
                  Model = "A5",
                  BasePrice = 3000,
                  Brand = fakeBrand
               }
            }.AsQueryable());

         mockCarRepository.Setup((t) => t.GetOne(It.IsAny<int>())).Returns<int>((id) =>
         {
            var l = new List<Car>()
            {
               new()
               {
                  Model = "A3",
                  BasePrice = 2000,
                  Brand = fakeBrand
               },
               new()
               {
                  Model = "A5",
                  BasePrice = 3000,
                  Brand = fakeBrand
               }
            };
            return l[id - 1];
         });

         carLogic = new CarLogic(mockCarRepository.Object);
      }

      [Test]
      public void CreateCarTestNull()
      {
         Assert.That(() => carLogic.Create(null), Throws.ArgumentNullException);
      }
      
      [TestCase(-5000, false)]
      [TestCase(5000, true)]
      public void CreateCarTestPrice(int price, bool result)
      {
         //ACT + ASSERT
         if (result)
         {
            Assert.That(() => carLogic.Create(new Car()
            {
               Model = "Astra",
               BasePrice = price
            }), Throws.Nothing);
         }
         else
         {
            Assert.That(() => carLogic.Create(new Car()
            {
               Model = "Astra",
               BasePrice = price
            }), Throws.Exception);
         }
      }
      
      [TestCase("Astra", true)]
      [TestCase("", false)]
      [TestCase(null, false)]
      public void CreateCarTestModel(string model, bool result)
      {

         //ACT + ASSERT
         if (result)
         {
            Assert.That(() => carLogic.Create(new Car()
            {
               Model = model,
               BasePrice = 5000
            }), Throws.Nothing);
         }
         else
         {
            Assert.That(() => carLogic.Create(new Car()
            {
               Model = model,
               BasePrice = 5000
            }), Throws.Exception);
         }

      }
      
      [Test]
      public void UpdateCarTestNull()
      {
         Assert.That(() => carLogic.Update(null), Throws.ArgumentNullException);
      }
      
      [Test]
      public void UpdateNullCarTest()
      {
         Assert.That(() => carLogic.Update(new Car()
         {
            Id = 999,
            Model = "Astra",
            BasePrice = 5000
         }), Throws.Exception);
      }

      [TestCase(-5000, false)]
      [TestCase(5000, true)]
      public void UpdateCarTestPrice(int price, bool result)
      {
         //ACT + ASSERT
         if (result)
         {
            Assert.That(() => carLogic.Update(new Car()
            {
               Id = 1,
               Model = "Astra",
               BasePrice = price
            }), Throws.Nothing);
         }
         else
         {
            Assert.That(() => carLogic.Update(new Car()
            {
               Id = 1,
               Model = "Astra",
               BasePrice = price
            }), Throws.Exception);
         }
      }
      
      [TestCase("Astra", true)]
      [TestCase("", false)]
      [TestCase(null, false)]
      public void UpdateCarTestModel(string model, bool result)
      {

         //ACT + ASSERT
         if (result)
         {
            Assert.That(() => carLogic.Update(new Car()
            {
               Id = 1,
               Model = model,
               BasePrice = 5000
            }), Throws.Nothing);
         }
         else
         {
            Assert.That(() => carLogic.Create(new Car()
            {
               Id = 1,
               Model = model,
               BasePrice = 5000
            }), Throws.Exception);
         }

      }
      
   }
}