using Chat_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat_backend.Adapters.Database
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.Id).IsRequired().HasDefaultValueSql("gen_random_uuid()");
            builder.Property(user => user.Email).IsRequired(); 
            builder.Property(user => user.Password).IsRequired();
            builder.Property(user => user.Username).IsRequired().HasMaxLength(20); 
            builder.HasIndex(user => user.Email).IsUnique();

        }
    }
}
