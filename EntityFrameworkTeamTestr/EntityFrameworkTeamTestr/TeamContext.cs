namespace EntityFrameworkTeamTestr
{

    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class TeamContext : DbContext
    {
        public TeamContext()
            : base("name=TeamContext")
        {
        }

        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Team>()
                .HasMany(t => t.Users)
                .WithMany(u => u.Teams)
                .Map(m =>
                    {
                        m.MapLeftKey("TeamId");
                        m.MapRightKey("UserId");
                        m.ToTable("TeamUser");
                    }
                );
        }
    }
}
