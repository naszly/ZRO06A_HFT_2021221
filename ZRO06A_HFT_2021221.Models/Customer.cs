using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZRO06A_HFT_2021221.Models
{
   [Table("customers")]
   public class Customer
   {
      public Customer()
      {
         Orders = new HashSet<Order>();
      }

      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public int Id { get; set; }

      [MaxLength(100)] [Required] public string Name { get; set; }

      [NotMapped] public virtual ICollection<Order> Orders { get; set; }
   }
}