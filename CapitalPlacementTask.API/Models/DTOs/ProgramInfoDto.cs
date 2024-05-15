using CapitalPlacementTask.API.Models.Entities;

namespace CapitalPlacementTask.API.Models.DTOs
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
