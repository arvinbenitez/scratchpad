namespace EntityFrameworkTeamTestr
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("User")]
    public partial class User : IEntity
    {
        public User()
        {
            Teams = new HashSet<Team>().ToList();
        }

        public int Id { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }


        public  IList<Team> Teams { get; set; }
    }
}
