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
    public class ProgramServiceFacts
    {
        private readonly ProgramService _sut;
        private readonly Mock<IProgramRepository> _programRepoMock = new Mock<IProgramRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        public ProgramServiceFacts()
        {
            _sut = new ProgramService(_programRepoMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetProgramById_ShouldReturnProgram_WhenProgramExists()
        {
            // Arrange
            var programIdToAdd = Guid.NewGuid();

            var programToAdd = new ProgramInfo
            {
                ProgramInfoId = programIdToAdd,
                Title = "Test Title",
                Description = "Test Description",
                Employer = new Employer
                {
                    FirstName = "first",
                    LastName = "second",
                    Email = "first.second@gmail.com",
                    PhoneNumber = "08136964004",
                    Nationality = "Nigerian",
                    CurrentResidence = "Nigeria",
                    IdNumber = "1234",
                    DateOfBirth = new DateTime(1992, 7, 5),
                    Gender = "Male",
                    EmployerId = Guid.NewGuid(),
                    ProgramInfoId = programIdToAdd
                },
                Questions = new List<Question>
                {
                    new Question
                    {
                        QuestionId = Guid.NewGuid(),
                        ProgramInfoId = programIdToAdd,
                        QuestionType = "YesNo",
                        Text = "Are you a software engineer?",
                        Choices = new List<string> { "Yes", "No" },
                        MaxNumberOfChoices = 1
                    }
                }
            };

            var programDto = new ProgramInfoDto
            {
                ProgramInfoId = programIdToAdd,
                Title = "Test Title",
                Description = "Test Description",
                Employer = new Employer
                {
                    FirstName = "first",
                    LastName = "second",
                    Email = "first.second@gmail.com",
                    PhoneNumber = "08136964004",
                    Nationality = "Nigerian",
                    CurrentResidence = "Nigeria",
                    IdNumber = "1234",
                    DateOfBirth = new DateTime(1992, 7, 5),
                    Gender = "Male",
                    EmployerId = Guid.NewGuid(),
                    ProgramInfoId = programIdToAdd
                },
                Questions = new List<Question>
                {
                    new Question
                    {
                        QuestionId = Guid.NewGuid(),
                        ProgramInfoId = programIdToAdd,
                        QuestionType = "YesNo",
                        Text = "Are you a software engineer?",
                        Choices = new List<string> { "Yes", "No" },
                        MaxNumberOfChoices = 1
                    }
                }
            };

            _mapperMock.Setup(mapper => mapper.Map<ProgramInfoDto>(programToAdd))
                      .Returns(programDto);
            _programRepoMock.Setup(x => x.GetById(programIdToAdd)).ReturnsAsync(programToAdd);

            // Act
            var program = await _sut.GetProgramById(programIdToAdd);

            // Assert
            program.Data.ProgramInfoId.Should().Be(programIdToAdd);
        }

        [Fact]
        public async Task GetProgramById_ShouldReturnNull_WhenProgramDoesNotExists()
        {
            // Arrange
            var programIdToAdd = Guid.NewGuid();

            _programRepoMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(() => null);

            // Act
            var program = await _sut.GetProgramById(programIdToAdd);

            // Assert
            program.Status.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenProgramDoesNotExist()
        {
            // Arrange
            var programIdToDelete = Guid.NewGuid();

            _programRepoMock.Setup(x => x.DeleteById(It.IsAny<Guid>())).ReturnsAsync(() => false);

            // Act
            var program = await _sut.Delete(programIdToDelete);

            // Assert
            program.Status.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenProgramIsSuccessfullyDeleted()
        {
            // Arrange
            var programIdToDelete = Guid.NewGuid();

            _programRepoMock.Setup(x => x.DeleteById(It.IsAny<Guid>())).ReturnsAsync(() => true);
            _programRepoMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(() => true);

            // Act
            var program = await _sut.Delete(programIdToDelete);

            // Assert
            program.Status.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_ShouldReturnBadRequest_WhenProgramFailedToDelete()
        {
            // Arrange
            var programIdToDelete = Guid.NewGuid();
            var errorMessage = "Delete operation failed";

            _programRepoMock.Setup(x => x.DeleteById(It.IsAny<Guid>())).ReturnsAsync(() => true);
            _programRepoMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(() => false);

            // Act
            var program = await _sut.Delete(programIdToDelete);

            // Assert
            program.Status.Should().Be(HttpStatusCode.BadRequest);
            program.ErrorMessage.Should().Be(errorMessage);
        }

        [Fact]
        public async Task Create_ShouldCreateProgram()
        {
            // Arrange
            var programIdToAdd = Guid.NewGuid();

            var programToAdd = new ProgramInfo
            {
                ProgramInfoId = programIdToAdd,
                Title = "Test Title",
                Description = "Test Description",
                Employer = new Employer
                {
                    FirstName = "first",
                    LastName = "second",
                    Email = "first.second@gmail.com",
                    PhoneNumber = "08136964004",
                    Nationality = "Nigerian",
                    CurrentResidence = "Nigeria",
                    IdNumber = "1234",
                    DateOfBirth = new DateTime(1992, 7, 5),
                    Gender = "Male",
                    EmployerId = Guid.NewGuid(),
                    ProgramInfoId = programIdToAdd
                },
                Questions = new List<Question>
                {
                    new Question
                    {
                        QuestionId = Guid.NewGuid(),
                        ProgramInfoId = programIdToAdd,
                        QuestionType = "YesNo",
                        Text = "Are you a software engineer?",
                        Choices = new List<string> { "Yes", "No" },
                        MaxNumberOfChoices = 1
                    }
                }
            };

            var programDto = new ProgramInfoDto
            {
                ProgramInfoId = programIdToAdd,
                Title = "Test Title",
                Description = "Test Description",
                Employer = new Employer
                {
                    FirstName = "first",
                    LastName = "second",
                    Email = "first.second@gmail.com",
                    PhoneNumber = "08136964004",
                    Nationality = "Nigerian",
                    CurrentResidence = "Nigeria",
                    IdNumber = "1234",
                    DateOfBirth = new DateTime(1992, 7, 5),
                    Gender = "Male",
                    EmployerId = Guid.NewGuid(),
                    ProgramInfoId = programIdToAdd
                },
                Questions = new List<Question>
                {
                    new Question
                    {
                        QuestionId = Guid.NewGuid(),
                        ProgramInfoId = programIdToAdd,
                        QuestionType = "YesNo",
                        Text = "Are you a software engineer?",
                        Choices = new List<string> { "Yes", "No" },
                        MaxNumberOfChoices = 1
                    }
                }
            };

            _mapperMock.Setup(mapper => mapper.Map<ProgramInfo>(programDto))
                      .Returns(programToAdd);
            _programRepoMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(() => true);

            // Act
            var program = await _sut.Create(programDto);

            // Assert
            program.Data.Id.Should().Be(programIdToAdd);
            program.Status.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_ShouldFailToCreateProgram()
        {
            // Arrange
            var programIdToAdd = Guid.NewGuid();

            var programToAdd = new ProgramInfo
            {
                ProgramInfoId = programIdToAdd,
                Title = "Test Title",
                Description = "Test Description",
                Employer = new Employer
                {
                    FirstName = "first",
                    LastName = "second",
                    Email = "first.second@gmail.com",
                    PhoneNumber = "08136964004",
                    Nationality = "Nigerian",
                    CurrentResidence = "Nigeria",
                    IdNumber = "1234",
                    DateOfBirth = new DateTime(1992, 7, 5),
                    Gender = "Male",
                    EmployerId = Guid.NewGuid(),
                    ProgramInfoId = programIdToAdd
                },
                Questions = new List<Question>
                {
                    new Question
                    {
                        QuestionId = Guid.NewGuid(),
                        ProgramInfoId = programIdToAdd,
                        QuestionType = "YesNo",
                        Text = "Are you a software engineer?",
                        Choices = new List<string> { "Yes", "No" },
                        MaxNumberOfChoices = 1
                    }
                }
            };

            var programDto = new ProgramInfoDto
            {
                ProgramInfoId = programIdToAdd,
                Title = "Test Title",
                Description = "Test Description",
                Employer = new Employer
                {
                    FirstName = "first",
                    LastName = "second",
                    Email = "first.second@gmail.com",
                    PhoneNumber = "08136964004",
                    Nationality = "Nigerian",
                    CurrentResidence = "Nigeria",
                    IdNumber = "1234",
                    DateOfBirth = new DateTime(1992, 7, 5),
                    Gender = "Male",
                    EmployerId = Guid.NewGuid(),
                    ProgramInfoId = programIdToAdd
                },
                Questions = new List<Question>
                {
                    new Question
                    {
                        QuestionId = Guid.NewGuid(),
                        ProgramInfoId = programIdToAdd,
                        QuestionType = "YesNo",
                        Text = "Are you a software engineer?",
                        Choices = new List<string> { "Yes", "No" },
                        MaxNumberOfChoices = 1
                    }
                }
            };

            _mapperMock.Setup(mapper => mapper.Map<ProgramInfo>(programDto))
                      .Returns(programToAdd);
            _programRepoMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(() => false);

            // Act
            var program = await _sut.Create(programDto);

            // Assert
            program.Status.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenProgramDoesNotExist()
        {
            // Arrange
            var programIdToAdd = Guid.NewGuid();

            var programDto = new ProgramInfoDto
            {
                ProgramInfoId = programIdToAdd,
                Title = "Test Title",
                Description = "Test Description",
                Employer = new Employer
                {
                    FirstName = "first",
                    LastName = "second",
                    Email = "first.second@gmail.com",
                    PhoneNumber = "08136964004",
                    Nationality = "Nigerian",
                    CurrentResidence = "Nigeria",
                    IdNumber = "1234",
                    DateOfBirth = new DateTime(1992, 7, 5),
                    Gender = "Male",
                    EmployerId = Guid.NewGuid(),
                    ProgramInfoId = programIdToAdd
                },
                Questions = new List<Question>
                {
                    new Question
                    {
                        QuestionId = Guid.NewGuid(),
                        ProgramInfoId = programIdToAdd,
                        QuestionType = "YesNo",
                        Text = "Are you a software engineer?",
                        Choices = new List<string> { "Yes", "No" },
                        MaxNumberOfChoices = 1
                    }
                }
            };

            _programRepoMock.Setup(x => x.GetById(It.IsAny<Guid>())).ReturnsAsync(() => null);

            // Act
            var program = await _sut.Update(programDto);

            // Assert
            program.Status.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
