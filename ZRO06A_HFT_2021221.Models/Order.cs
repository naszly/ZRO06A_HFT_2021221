using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZRO06A_HFT_2021221.Models
{
   [Table("orders")]
   public class Order
   {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      [MaxLength(100)] [Required] public string Name { get; set; }

      // Proxy class
      // Lazy loading
      [NotMapped] public virtual Car Car { get; set; }

      [ForeignKey(nameof(Car))] public int CarId { get; set; }

      public Order() { }
   }
}