using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkTeamTestr
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Saving changes");
            var context = new TeamContext();
            var user = context.Set<User>().Where(x => x.Id == 1).FirstOrDefault();

            var anotherUser = new User
            {
                Id = 2
            };
            context.Entry(anotherUser).State = System.Data.Entity.EntityState.Unchanged;

            var team = new Team()
            {
                Name = "Team Arvin",
                Users = new User[] 
                { 
                    user,
                    anotherUser
                }
            };
            context.Set<Team>().Add(team);
            context.SaveChanges();
            Console.WriteLine("Saved team. Id={0}, Name={1}", team.Id, team.Name);

            var savedTeam = context.Set<Team>().Where(x => x.Id == team.Id).FirstOrDefault();

            Console.WriteLine("Retreived save team. Id={0}, Name={1}", savedTeam.Id, savedTeam.Name);
            Console.ReadLine();

        }
    }
}
