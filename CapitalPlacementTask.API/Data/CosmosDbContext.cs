using CapitalPlacementTask.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CapitalPlacementTask.API.Data
{
    public class CosmosDbContext : DbContext
    {
        public CosmosDbContext() { }

        public CosmosDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ProgramInfo> Programs { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Employer> Employers { get; set; }

        public DbSet<Candidate> Candidates { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasAutoscaleThroughput(1000);

            modelBuilder.HasDefaultContainer("Programs");

            modelBuilder.Entity<ProgramInfo>()
                .HasNoDiscriminator()
                .HasPartitionKey(x => x.Title)
                .HasKey(x => x.ProgramInfoId);

            modelBuilder.Entity<Employer>()
                .HasNoDiscriminator()
                .ToContainer("Employers")
                .HasPartitionKey(x => x.ProgramInfoId)
                .HasKey(x => x.EmployerId);

            modelBuilder.Entity<Candidate>()
                .HasNoDiscriminator()
                .ToContainer("Candidates")
                .HasPartitionKey(x => x.ProgramInfoId)
                .HasKey(x => x.CandidateId);

            modelBuilder.Entity<Question>()
                .HasNoDiscriminator()
                .ToContainer("Questions")
                .HasPartitionKey(x => x.ProgramInfoId)
                .HasKey(x => x.QuestionId);

        }
    }
}
