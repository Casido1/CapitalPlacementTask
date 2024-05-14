using Exercise.Models.Entities;

namespace Exercise.Models.DTOs
{
    public class CandidateToReturnDto : PersonalInformation
    {
        public Guid CandidateId { get; set; }

        public Guid ProgramInfoId { get; set; }

        public List<Answer> Answers { get; set; }

        public DateTime ApplicationDate { get; set; }
    }
}
