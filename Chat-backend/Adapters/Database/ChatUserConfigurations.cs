using Chat_backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat_backend.Adapters.Database
{
    public class ChatUserConfigurations : IEntityTypeConfiguration<ChatUser>
    {
        public void Configure(EntityTypeBuilder<ChatUser> builder)
        {
            builder.HasKey(builder => new { builder.ChatsId, builder.UsersId }); 

            builder.HasOne(chatUser => chatUser.Chat)
                .WithMany(chat => chat.ChatUser)
                .HasForeignKey(chatUser => chatUser.ChatsId);

            builder.HasOne(chatUser => chatUser.User)
                .WithMany(user => user.ChatUser)
                .HasForeignKey(chatUser => chatUser.UsersId);

        }
    }
}
