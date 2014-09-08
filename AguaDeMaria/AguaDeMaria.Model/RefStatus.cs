namespace AguaDeMaria.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
