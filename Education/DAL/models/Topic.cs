﻿using Education.DAL.CustomValidation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Education.DAL.models
{
    public class Topic
    {
        public int Id { get; set; }
        [MinLength(3)]
        public string Name { get; set; }
        [EnumDataType(typeof(TypeAllowedValues), ErrorMessage = "Invalid Input")]
        public string Type { get; set; } /// hardcoded


        public DateTime Date { get; set; }

        public bool IsDeleted { set; get; }

        [ForeignKey("instructor")]
        public int InstructorId { set; get; }

        public virtual Instructor? instructor { set; get; }

    }
}
