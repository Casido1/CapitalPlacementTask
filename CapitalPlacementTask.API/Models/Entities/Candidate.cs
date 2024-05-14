namespace Exercise.Models.Entities
{
    public class Candidate : PersonalInformation
    {
        public Guid CandidateId { get; set; }

        public Guid ProgramInfoId { get; set; }

        public List<Answer> Answers { get; set; }

        public DateTime ApplicationDate { get; set; } = DateTime.Now;
    }
}
