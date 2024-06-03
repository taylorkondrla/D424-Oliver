using System;
namespace c971_oliver.Models
{
    public class Note
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Content { get; set; }

        public Note()
        {

        }
    }
}