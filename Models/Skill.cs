namespace Project_2023.Models
{
    public class Skill
    {
        public int SkillId { get; set; }
        public string Name { get; set; }

        public ICollection<HasSkills> CVSkills { get; set; }
    }
}
