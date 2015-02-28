namespace AguaDeMaria.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Inventory")]
    public partial class Inventory
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TransactionId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(11)]
        public string TransactionType { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string RefNumber { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime RefDate { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerId { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(100)]
        public string CustomerName { get; set; }

        public int? Slim { get; set; }

        public int? Round { get; set; }
    }
}
