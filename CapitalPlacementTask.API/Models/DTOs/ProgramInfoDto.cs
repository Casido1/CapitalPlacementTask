using Exercise.Models.Entities;

namespace Exercise.Models.DTOs
{
    public class ProgramInfoDto
    {
        public Guid ProgramInfoId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Employer Employer { get; set; }

        public List<Question> Questions { get; set; }
    }
}
