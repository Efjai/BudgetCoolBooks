﻿using Microsoft.AspNetCore.Http.HttpResults;

namespace Budget_CoolBooks.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }

        public List<BookGenre> BookGenre { get; } = new();
    }
}
