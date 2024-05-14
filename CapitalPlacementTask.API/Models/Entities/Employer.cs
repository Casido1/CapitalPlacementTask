namespace Exercise.Models.Entities
{
    public class Employer : PersonalInformation
    {
        public Guid EmployerId { get; set; }

        public Guid ProgramInfoId { get; set; }
    }
}
