using CommentManagement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentManagement.Infrastructure.EfCore.Mappings
{
    public class CommentMapping : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x=>x.Email).HasMaxLength(100).IsRequired();
            builder.Property(x=>x.Message).HasMaxLength(1000).IsRequired();
        }
    }
}
