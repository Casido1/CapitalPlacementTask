using AutoMapper;
using CapitalPlacementTask.API.Data.Repository.Interface;
using CapitalPlacementTask.API.Models.DTOs;
using CapitalPlacementTask.API.Models.Entities;
using CapitalPlacementTask.API.Services.Implementation;
using FluentAssertions;
using Moq;
using System.Net;

namespace CapitalPlacementTask.Test
{
    public class CandidateServiceFacts
    {
        private readonly CandidateService _sut;
        private readonly Mock<ICandidateRepository> _candidateRepoMock = new Mock<ICandidateRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        public CandidateServiceFacts()
        {
            _sut = new CandidateService(_candidateRepoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenCandidateDoesNotExist()
        {
            // Arrange
            var candidateIdToDelete = Guid.NewGuid();

            _candidateRepoMock.Setup(x => x.DeleteById(It.IsAny<Guid>())).ReturnsAsync(() => false);

            // Act
            var candidate = await _sut.Delete(candidateIdToDelete);

            // Assert
            candidate.Status.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenCandidateIsSuccessfullyDeleted()
        {
            // Arrange
            var candidateIdToDelete = Guid.NewGuid();

            _candidateRepoMock.Setup(x => x.DeleteById(It.IsAny<Guid>())).ReturnsAsync(() => true);
            _candidateRepoMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(() => true);

            // Act
            var candidate = await _sut.Delete(candidateIdToDelete);

            // Assert
            candidate.Status.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ShouldReturnBadRequest_WhenCandidateFailedToDelete()
        {
            // Arrange
            var candidateIdToDelete = Guid.NewGuid();
            var errorMessage = "Delete operation failed";

            _candidateRepoMock.Setup(x => x.DeleteById(It.IsAny<Guid>())).ReturnsAsync(() => true);
            _candidateRepoMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(() => false);

            // Act
            var candidate = await _sut.Delete(candidateIdToDelete);

            // Assert
            candidate.Status.Should().Be(HttpStatusCode.BadRequest);
            candidate.ErrorMessage.Should().Be(errorMessage);
        }

        [Fact]
        public async Task Create_ShouldCreateCandidate()
        {
            // Arrange
            var programIdToAdd = Guid.NewGuid();
            var CandidateIdToAdd = Guid.NewGuid();
            var questionId = Guid.NewGuid();

            var candidateToAdd = new Candidate
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
                CandidateId = CandidateIdToAdd,
                ProgramInfoId = programIdToAdd,
                Answers = new List<Answer>
                        {
                            new Answer {QuestionId = questionId, Text = "Yes"}
                        }
            };

            var candidateDto = new CandidateDto
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
                CandidateId = CandidateIdToAdd,
                ProgramInfoId = programIdToAdd,
                Answers = new List<Answer>
                        {
                            new Answer {QuestionId = questionId, Text = "Yes"}
                        }
            };

            _mapperMock.Setup(mapper => mapper.Map<Candidate>(candidateDto))
                      .Returns(candidateToAdd);
            _candidateRepoMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(() => true);

            // Act
            var candidate = await _sut.Create(candidateDto);

            // Assert
            candidate.Data.Id.Should().Be(CandidateIdToAdd);
            candidate.Status.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_ShouldFailToCreateCandidate()
        {
            // Arrange
            var programIdToAdd = Guid.NewGuid();
            var CandidateIdToAdd = Guid.NewGuid();
            var questionId = Guid.NewGuid();

            var candidateToAdd = new Candidate
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
                CandidateId = CandidateIdToAdd,
                ProgramInfoId = programIdToAdd,
                Answers = new List<Answer>
                        {
                            new Answer {QuestionId = questionId, Text = "Yes"}
                        }
            };

            var candidateDto = new CandidateDto
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
                CandidateId = CandidateIdToAdd,
                ProgramInfoId = programIdToAdd,
                Answers = new List<Answer>
                        {
                            new Answer {QuestionId = questionId, Text = "Yes"}
                        }
            };

            _mapperMock.Setup(mapper => mapper.Map<Candidate>(candidateDto))
                      .Returns(candidateToAdd);
            _candidateRepoMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(() => false);

            // Act
            var candidate = await _sut.Create(candidateDto);

            // Assert
            candidate.Status.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
