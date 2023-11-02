using Chat_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat_backend.Adapters.Database
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(message => message.Id).IsRequired().HasDefaultValueSql("gen_random_uuid()"); 
            builder.Property(message => message.Content).IsRequired();
            builder.Property(message => message.UserId).IsRequired(); 
            builder.Property(message => message.ChatId).IsRequired();
        }
    }
}
