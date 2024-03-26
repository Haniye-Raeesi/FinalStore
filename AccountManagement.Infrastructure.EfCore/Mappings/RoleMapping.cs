using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.InfraStructure.EfCore.Mappings
{
    class RoleMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.HasMany(x => x.Accounts).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
            builder.OwnsMany(x => x.Permissions, ModelBuilder =>
            {
                ModelBuilder.HasKey(x => x.Id);
                ModelBuilder.ToTable("RolePermissions");
                ModelBuilder.Ignore(x => x.Name);
                ModelBuilder.WithOwner(x => x.Role).HasForeignKey(x=>x.RoleId);
            });


        }

    }
}
