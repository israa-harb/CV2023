namespace Project_2023.Models
{
    public class HasSkills
    {
        public int Id { get; set; }
        public int CVId { get; set; }
        public int SkillId { get; set; }

        public CV CV { get; set; }
        public Skill Skill { get; set; }
    }
}
