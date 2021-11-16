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
   public class BrandLogicTestWithMock
   {
      private readonly BrandLogic brandLogic;

      public BrandLogicTestWithMock()
      {
         var mockBrandRepository = new Mock<IBrandRepository>();

         mockBrandRepository.Setup((t) => t.Create(It.IsAny<Brand>()));
         mockBrandRepository.Setup((t) => t.GetAll()).Returns(
            new List<Brand>()
            {
               new()
               {
                  Id = 1,
                  Name = "Audi"
               },
               new()
               {
                  Id = 2,
                  Name = "Lada"
               }
            }.AsQueryable());
         
         mockBrandRepository.Setup((t) => t.GetOne(It.IsAny<int>())).Returns<int>((id) =>
         {
            return mockBrandRepository.Object.GetAll().SingleOrDefault(x => x.Id == id);
         });

         brandLogic = new BrandLogic(mockBrandRepository.Object);
      }

      [TestCase("Opel", true)]
      [TestCase("", false)]
      [TestCase(null, false)]
      public void CreateBrandTestModel(string name, bool result)
      {

         //ACT + ASSERT
         if (result)
         {
            Assert.That(() => brandLogic.Create(new Brand()
            { 
               Name = name
            }), Throws.Nothing);
         }
         else
         {
            Assert.That(() => brandLogic.Create(new Brand()
            {
               Name = name
            }), Throws.Exception);
         }

      }

      [Test]
      public void UpdateBrandTestNull()
      {
         Assert.That(() => brandLogic.Update(null), Throws.ArgumentNullException);
      }
      
      
      [TestCase("Opel", true)]
      [TestCase("", false)]
      [TestCase(null, false)]
      public void UpdateCarTestModel(string name, bool result)
      {
         ;
         //ACT + ASSERT
         if (result)
         {
            Assert.That(() => brandLogic.Update(new Brand()
            {
               Id = 1,
               Name = name
            }), Throws.Nothing);
         }
         else
         {
            Assert.That(() => brandLogic.Create(new Brand()
            {
               Id = 1,
               Name = name
            }), Throws.Exception);
         }

      }

   }
}