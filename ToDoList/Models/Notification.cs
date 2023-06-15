using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Notification
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
  
        [Required]
        public string Location { get; set; }


        [Required]
        public bool IsRead { get; set; }
    }
}
