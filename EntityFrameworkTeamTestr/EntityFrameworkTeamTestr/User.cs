namespace EntityFrameworkTeamTestr
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public User()
        {
            Teams = new HashSet<Team>();
        }

        public int Id { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }


        public virtual ICollection<Team> Teams { get; set; }
    }
}
