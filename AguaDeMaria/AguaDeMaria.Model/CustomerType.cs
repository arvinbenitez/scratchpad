namespace AguaDeMaria.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CustomerType")]
    public partial class CustomerType
    {
        public CustomerType()
        {
            Customers = new HashSet<Customer>();
        }

        public int CustomerTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerTypeName { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}
