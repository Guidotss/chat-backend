using Chat_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat_backend.Adapters.Database
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.Property(chat => chat.Id).IsRequired().HasDefaultValueSql("gen_random_uuid()");
            builder.Property(chat => chat.Name).IsRequired();
        }
    }
}
