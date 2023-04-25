using Budget_CoolBooks.Models;

namespace Budget_CoolBooks.ViewModels
{
    public class CommentViewModel
    {
        public Review Review { get; set; }
        public Comment Comment { get; set; }    
        public int BookId { get; set; }

    }

}
