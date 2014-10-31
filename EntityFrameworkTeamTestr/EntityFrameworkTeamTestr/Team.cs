namespace EntityFrameworkTeamTestr
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    [Table("Team")]
    public partial class Team : IEntity
    {
        public Team()
        {
            Users = new HashSet<User>().ToList();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public IList<User> Users { get; set; }
    }
}
