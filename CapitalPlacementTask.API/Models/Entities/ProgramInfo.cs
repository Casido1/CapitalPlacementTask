﻿using System.ComponentModel.DataAnnotations;

namespace Exercise.Models.Entities
{
    public class ProgramInfo
    {
        [Key]
        public Guid ProgramInfoId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Employer Employer { get; set; }

        public List<Question> Questions { get; set; }
    }
}