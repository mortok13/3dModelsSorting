using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CourseWork3_DB
{
    public class User
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Password { get; set; }

        public List<Model> Models { get; set; }
        public List<Group> Groups { get; set; }
    }
}