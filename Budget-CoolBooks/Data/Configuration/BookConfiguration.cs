using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Budget_CoolBooks.Models;

namespace Budget_CoolBooks.Data.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            //Title-propery
            builder.Property(b => b.Title).HasMaxLength(100);

        }
     
    }
}
