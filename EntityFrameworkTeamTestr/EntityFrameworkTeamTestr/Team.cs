namespace EntityFrameworkTeamTestr
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Team")]
    public partial class Team
    {
        public Team()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
