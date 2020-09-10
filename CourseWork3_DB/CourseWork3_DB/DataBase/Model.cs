using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CourseWork3_DB
{
    public class Model : IModelWithoutUser
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Type { get; set; }
        [Required] public string Category { get; set; }
        [Required] public string Status { get; set; }
        [Required] public string Path { get; set; }

        public Group Group { get; set; }
        public string GroupName => Group?.Name;
        [Required] public int UserId { get; set; }
        [Required] public User User { get; set; }
    }
}