using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZRO06A_HFT_2021221.Models
{
   [Table("cars")]
   public class Car
   {
      public Car() { }

      public Car(string modelName, int id, int price)
      {
         Model = modelName;
         BrandId = id;
         BasePrice = price;
      }

      public Car(string modelName, int id)
      {
         Model = modelName;
         BrandId = id;
      }

      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      // Part 1
      [Column("car_id", TypeName = "int")]
      // Part 2 auto key increment
      //[Column("car_id", TypeName = "int", Order = 0)]
      public int Id { get; set; }

      [MaxLength(100)] [Required] public string Model { get; set; }

      public int BasePrice { get; set; }

      // Proxy class
      // Lazy loading
      [NotMapped] public virtual Brand Brand { get; set; }
      [NotMapped] public virtual Order Order { get; set; }

      [ForeignKey(nameof(Brand))] public int BrandId { get; set; }
   }
}