using System;
using System.Collections.ObjectModel;
using SQLite;

namespace c971_oliver.Models
{
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public Term()
        {

        }
    }
}