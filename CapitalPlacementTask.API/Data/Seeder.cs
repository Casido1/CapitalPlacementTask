using CapitalPlacementTask.API.Models.Entities;

namespace CapitalPlacementTask.API.Data
{
    public static class Seeder
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<CosmosDbContext>());
            }
        }

        private static void SeedData(CosmosDbContext context)
        {
            if (context.Programs.Count() == 0)
            {
                Console.WriteLine("Seeding data...");

                var programId = Guid.NewGuid();
                var questionId1 = Guid.NewGuid();
                var questionId2 = Guid.NewGuid();
                var questionId3 = Guid.NewGuid();
                var questionId4 = Guid.NewGuid();

                context.Programs.Add(
                    new ProgramInfo
                    {
                        ProgramInfoId = programId,
                        Title = "Tech summit",
                        Description = "Tech summit",
                        Employer = new Employer
                        {
                            FirstName = "John",
                            LastName = "Paul",
                            Email = "john.paul@gmail.com",
                            PhoneNumber = "08136964004",
                            Nationality = "Nigerian",
                            CurrentResidence = "Nigeria",
                            IdNumber = "1234",
                            DateOfBirth = new DateTime(1992, 7, 5),
                            Gender = "Male",
                            EmployerId = Guid.NewGuid(),
                            ProgramInfoId = programId
                        },
                        Questions = new List<Question>
                        {
                            new Question { QuestionId = questionId1, ProgramInfoId = programId, QuestionType = "YesNo", Text = "Are you a software engineer?", Choices = new List<string>{"Yes", "No" } },
                            new Question { QuestionId = questionId2, ProgramInfoId = programId, QuestionType = "MultipleChoice", Text = "How many years of experience do you have?", Choices = new List<string>{"3", "4+", "6+"} },
                            new Question { QuestionId = questionId3, ProgramInfoId = programId, QuestionType = "Paragraph", Text = "In not more that 500 words, tell us about yourself" },
                            new Question { QuestionId = questionId4, ProgramInfoId = programId, QuestionType = "Date", Text = "In what year did you obtain your bachelors degree?", Choices = new List<string>{"3", "4+", "6+"} }
                        }
                    }
                );

                context.Candidates.Add(
                    new Candidate
                    {
                        FirstName = "ade",
                        LastName = "leke",
                        Email = "ade.leke@gmail.com",
                        PhoneNumber = "08567840336",
                        Nationality = "Nigerian",
                        CurrentResidence = "Nigeria",
                        IdNumber = "7697",
                        DateOfBirth = new DateTime(1995, 7, 5),
                        Gender = "Male",
                        CandidateId = Guid.NewGuid(),
                        ProgramInfoId = programId,
                        Answers = new List<Answer>
                        {
                            new Answer {QuestionId = questionId1, Text = "Yes"},
                            new Answer {QuestionId = questionId2, Text = "6+"},
                            new Answer {QuestionId = questionId3, Text = "I am amazing"},
                            new Answer {QuestionId = questionId4, Text = "2017"}
                        }

                    }
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("We have data!");
            }
        }
    }
}
