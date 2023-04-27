using Budget_CoolBooks.Models;

namespace Budget_CoolBooks.ViewModels
{
    public class ModeratorViewModel
    {
        public List<Review> Reviews { get; set; }
        public Review Review { get; set; }
        public List<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
        public List<Reply> Replies { get; set; }
        public Reply Reply { get; set; }
        public Quote Quote { get; set; }
        public List<Quote> QuotesToApprove { get; set; }
    }
}
