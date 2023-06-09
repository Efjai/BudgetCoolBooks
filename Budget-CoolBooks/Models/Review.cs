﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget_CoolBooks.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public double Rating { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
        public int Flag { get; set; }

        //Nested properties
        public Book Book { get; set; }
        public User User { get; set; }
        
    }
}
