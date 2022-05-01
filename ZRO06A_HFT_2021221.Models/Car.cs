using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZRO06A_HFT_2021221.Models
{
   [Table("cars")]
   public class Car
   {
      public Car()
      {
         Orders = new HashSet<Order>();
      }

      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      [MaxLength(100)] [Required] public string Model { get; set; }

      public int BasePrice { get; set; }

      // Proxy class
      // Lazy loading
      [NotMapped] public virtual Brand Brand { get; set; }

      [NotMapped] public virtual ICollection<Order> Orders { get; set; }

      [ForeignKey(nameof(Brand))] public int BrandId { get; set; }
   }
}