using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;



namespace EntityFrameworkTeamTestr
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Saving changes");
            var context = new TeamContext();
            var user = context.Set<User>().Where(x => x.Id == 1).FirstOrDefault();
            var userX = context.Set<User>().Where(x => x.Id == 2).FirstOrDefault();

            var anotherUser = new User
            {
                Id = 2
            };
            //context.Entry(anotherUser).State = System.Data.Entity.EntityState.Unchanged;
            anotherUser = context.AttachOrGetLocal(anotherUser);
            //context.Set<User>().Attach(anotherUser);

            var team = new Team()
            {
                Name = "Team Arvin",
                Users = new List<User> 
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
            Console.WriteLine("Now clearing the users");

            context.RemoveRelationship(savedTeam, user, x => x.Users);

            context.SaveChanges();
            Console.WriteLine("Done. Now check the users table.");

            Console.ReadLine();

        }

    }

    public static class MyExtensions
    {
        public static void RemoveRelationship<TParent, TChild>(this DbContext context, TParent parent, TChild child,
                    Expression<Func<TParent, object>> childCollectionFromParent) 
                    where TParent: class
                    where TChild: class
        {
            var objectContext = ((IObjectContextAdapter)context).ObjectContext;
            context.Set<TParent>().Attach(parent);
            context.Set<TChild>().Attach(child);
            objectContext.ObjectStateManager.ChangeRelationshipState(parent, child, childCollectionFromParent, EntityState.Deleted);
        }

        public static T AttachOrGetLocal<T>(this DbContext context, T entity)
            where T : class, IEntity 
        {
            var local = context.Set<T>().Local.FirstOrDefault(e => e.Id == entity.Id);
            if (local == null)
            {
                context.Set<T>().Attach(entity);
                local = entity;
            }
            return local;
        }

    }

}
