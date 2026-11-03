using Books.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.Data.Mappings;

public class BookMapping : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable("books");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Id).HasColumnType("uuid");
        builder.Property(x => x.Title).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Author).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Nationality).IsRequired().HasMaxLength(50);
        builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Year).IsRequired().HasMaxLength(10);
        builder.Property(x => x.Read).IsRequired().HasMaxLength(10);
    }
}