using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CourseWork3_DB
{
   public class Group : IShortGroup
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Model> Models { get; set; }
        
    }
}
