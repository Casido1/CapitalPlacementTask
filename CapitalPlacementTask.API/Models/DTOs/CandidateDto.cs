using CapitalPlacementTask.API.Models.Entities;

namespace CapitalPlacementTask.API.Models.DTOs
{
    public class CandidateDto : PersonalInformation
    {
        public Guid CandidateId { get; set; }

        public Guid ProgramInfoId { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
