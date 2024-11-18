using AudioProcessing.Domain.Chats;
using AudioProcessing.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AudioProcessing.Infrastructure.Domain.Chats
{
    public class ChatConfigyration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(c => c.ChatTitel);

            builder.Property(c => c.UserId).IsRequired();

            builder.Property(c => c.CreatedDate).IsRequired();


            builder.OwnsMany(c => c.ChatResponces, b =>
            {
                b.HasKey(x => x.Id);

                b.ToTable("ChatResponces");

                b.WithOwner().HasForeignKey(x => x.ChatId);

                b.Property(x => x.AudioId).IsRequired(false);
                b.Property(x => x.Promt);
                b.Property(x => x.Content);
                b.Property(x => x.CreatedAt).IsRequired();

                b.OwnsOne(x => x.PromtType, b =>
                {
                    b.Property(c => c.Value).HasColumnName("PromtType");
                });


                //b.ComplexProperty(o => o.PromtType, b =>
                //{
                //    b.IsRequired();
                //    b.Property(a => a.Value).HasColumnName("TreatmentStageStatus");
                //});

                // b.Property(e => e.PromtType)
                //.HasConversion(
                //    v => v.Value,
                //    v => new PromtType(v)
                //)
                //.IsRequired();
            });

          
        }
    }
}
