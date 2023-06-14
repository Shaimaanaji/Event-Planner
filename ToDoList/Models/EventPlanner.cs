using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class EventPlanner
    {
        //[Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }


         [Required]
        public string CreateDate { get; set; }


    }


}

//CREATE SEQUENCE event_seq
//  START WITH 1
//  INCREMENT BY 1
//  NOCACHE
//  NOCYCLE;
