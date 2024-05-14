namespace Exercise.Models.Entities
{
    public class Question
    {
        public Guid QuestionId { get; set; }

        public Guid ProgramInfoId { get; set; }

        public string QuestionType { get; set; }

        public string Text { get; set; }

        public List<string> Choices { get; set; }

        public int? MaxNumberOfChoices { get; set; }
    }
}
