using System;
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

        [Column(TypeName = "Date")][Required] public DateTime Date { get; set; }

        [Required] public int Price { get; set; }

        // Proxy class
        // Lazy loading
        [NotMapped] public virtual Car Car { get; set; }

        [NotMapped] public virtual Customer Customer { get; set; }


        [ForeignKey(nameof(Car))] public int CarId { get; set; }

        [ForeignKey(nameof(Customer))] public int CustomerId { get; set; }

        public Order GetCopy()
        {
            Order copy = new Order()
            {
                Id = Id,
                Date = Date,
                Price = Price,
                CarId = CarId,
                CustomerId = CustomerId,
            };
            return copy;
        }
    }
}