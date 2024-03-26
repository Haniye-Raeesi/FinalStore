using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.InfraStructure.EfCore.Mappings
{
    public class ProductPictureMapping : IEntityTypeConfiguration<ProductPicture>
    {
        public void Configure(EntityTypeBuilder<ProductPicture> builder)
        {
            builder.ToTable("ProductPictures");
            builder.HasKey(x => x.Id);

            //This Causes Null Reference Error!
            //builder.Property(x => x.Picture).HasMaxLength(1000).IsRequired();
            //builder.Property(x => x.PictureAlt).HasMaxLength(500).IsRequired();
            //builder.Property(x => x.PictureTitle).HasMaxLength(500).IsRequired();

            builder.Property(x => x.Picture).HasMaxLength(1000);
            builder.Property(x => x.PictureAlt).HasMaxLength(500);
            builder.Property(x => x.PictureTitle).HasMaxLength(500);


            builder.HasOne(x => x.product)
                .WithMany(x => x.ProductPictures)
                .HasForeignKey(x => x.ProductId);
        }
    }
}
