using Project_2023.Models;

namespace Project_2023.Data
{
    public class DataInitializer
    {
        public static void Initializer(CVContext context)
        {
            if (context.Skills.Any()) return;

            var skills = new Skill[]
            {
                new Skill{Name="C#"},
                new Skill{Name="C"},
                new Skill{Name="Java"},
                new Skill{Name="Python"},
                new Skill{Name="Assembly"},
                new Skill{Name="Flutter"}
            };

            context.Skills.AddRange(skills);
            context.SaveChanges();
        }
    }
}
