using Exercise.Models.Entities;

namespace Exercise.Models.DTOs
{
    public class CandidateDto : PersonalInformation
    {
        public Guid CandidateId { get; set; }

        public Guid ProgramInfoId { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
