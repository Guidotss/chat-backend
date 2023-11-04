using Chat_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat_backend.Adapters.Database
{
    public class ChatUserConfigurations : IEntityTypeConfiguration<ChatUser>
    {
        public void Configure(EntityTypeBuilder<ChatUser> builder)
        {
            builder.HasKey(builder => new { builder.ChatId, builder.UserId }); 
            builder.Property(builder => builder.ChatId).IsRequired();
            builder.Property(builder => builder.UserId).IsRequired();

        }
    }
}
