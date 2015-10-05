using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Destination
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupId { get; set; }
        public string Country { get; set; }

        public static IList<Destination> Destinations(int destinationCount)
        {
            Random random = new Random();
            var destinations = new List<Destination>();
            for (int i = 0; i < destinationCount; i++)
            {
                destinations.Add(new Destination
                {
                    Id = i + 1,
                    Name = "Destination " + random.Next(1, 1000).ToString("0000"),
                    GroupId = random.Next(1, 5),
                    Country = "Country " + random.Next(1, 5).ToString()
                });
            }
            return destinations;
        }
    }

    class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }

        public static IList<Group> Groups()
        {
            var groups = new List<Group>();
            Random random = new Random();
            for (int i = 0; i < 3; i++)
            {
                groups.Add(new Group
                {
                    Id = i + 1,
                    Name = "Group " + (i + 1).ToString(),
                    Order = random.Next(1, 10)
                });
            }
            return groups;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var destinations = Destination.Destinations(100000);

            var groups = Group.Groups();
            Console.WriteLine("Groups");
            foreach (var group in groups)
            {
                Console.WriteLine("GroupId={0},Name={1},SortOrder={2}", group.Id, group.Name, group.Order);
            }

            //Console.WriteLine("Unsorted");
            //foreach (var group in destinations)
            //{
            //    Console.WriteLine("Id={0},Name={1},Country={2},GroupId={3}", group.Id, group.Name, group.Country, group.GroupId);
            //}

            var query = from d in destinations
                        join g in groups on d.GroupId equals g.Id into sortedDestinations
                        from dg in sortedDestinations.DefaultIfEmpty()
                        let sortOrder = dg == null ? int.MaxValue : dg.Order
                        orderby d.Country, sortOrder, d.Name
                        select new { Destination = d, Group = dg };
            Console.WriteLine("Enter sort option: 1. By Country 2. By Group");
            string option = Console.ReadLine();

            //var sortOptions = SortOptions.Name;
            //if (option == "1")
            //{
            //    sortOptions = sortOptions | SortOptions.Country | SortOptions.Group;
            //}
            //else
            //{
            //    sortOptions = sortOptions | SortOptions.Group;
            //}

            var startTime = DateTime.Now;
            //var orderedQuery = query.OrderBy(x => 0);

            //if (sortOptions.HasFlag(SortOptions.Country))
            //{
            //    orderedQuery = orderedQuery.ThenBy(x => x.Destination.Country);
            //}
            //if (sortOptions.HasFlag(SortOptions.Group))
            //{
            //    orderedQuery = orderedQuery.ThenBy(x => x.Group == null ? int.MaxValue : x.Group.Order);
            //}
            //orderedQuery = orderedQuery.ThenBy(x => x.Destination.Name);

            //var sortedGroup = orderedQuery.Select(d => d.Destination).ToList();
            var sortedGroup = query.Select(d => d.Destination).ToList();
            var amountOfTime = DateTime.Now.Subtract(startTime);
            Console.WriteLine("Completed Sorting in: {0} ms", amountOfTime.TotalMilliseconds);
            Console.WriteLine("Sorted");
            //foreach (var group in sortedGroup)
            //{
            //    Console.WriteLine("Id={0},Name={1},Country={2},GroupId={3}", group.Id, group.Name, group.Country, group.GroupId);
            //}
            Console.ReadLine();
        }
    }

    [Flags]
    public enum SortOptions
    {
        Country = 1,
        Group = 2,
        Name = 4
    }
}
