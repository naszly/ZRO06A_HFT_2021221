using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZRO06A_HFT_2021221.Models
{
    [Table("brands")]
    public class Brand
    {
        // IEnumerable, ICollection, IList, IDictionary

        public Brand()
        {
            Cars = new HashSet<Car>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // Part 2
        //[Column(Order = 0)]
        public int Id { get; set; }

        [MaxLength(100)][Required] public string Name { get; set; }

        [NotMapped] public virtual ICollection<Car> Cars { get; set; }
    }
}