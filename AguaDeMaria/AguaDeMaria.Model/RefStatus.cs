namespace AguaDeMaria.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RefStatus")]
    public partial class RefStatus
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        [StringLength(100)]
        public string StatusName { get; set; }
    }
}
