using Microsoft.EntityFrameworkCore;
using StayPilot.Domain.Entities;

namespace StayPilot.Infrastructure.Context
{
    public class StayPilotDbContext : DbContext
    {
        public StayPilotDbContext(DbContextOptions<StayPilotDbContext> options) : base(options)
        {
        }

        public DbSet<HeroSection> HeroSections { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<AiConversationLog> AiConversationLogs { get; set; }
        public DbSet<ApiRequestLog> ApiRequestLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HeroSection>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(x => x.Subtitle)
                    .HasMaxLength(250);

                entity.Property(x => x.Description)
                    .HasMaxLength(700);

                entity.Property(x => x.BackgroundImageUrl)
                    .HasMaxLength(500);

                entity.Property(x => x.PrimaryButtonText)
                    .HasMaxLength(80);

                entity.Property(x => x.PrimaryButtonUrl)
                    .HasMaxLength(250);

                entity.Property(x => x.SecondaryButtonText)
                    .HasMaxLength(80);

                entity.Property(x => x.SecondaryButtonUrl)
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<Destination>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.CityName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.CountryName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.Description)
                    .HasMaxLength(700);

                entity.Property(x => x.ImageUrl)
                    .HasMaxLength(500);

                entity.Property(x => x.HighlightText)
                    .HasMaxLength(200);

                entity.Property(x => x.AverageHotelPrice)
                    .HasPrecision(18, 2);
            });

            modelBuilder.Entity<BlogPost>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(180);

                entity.Property(x => x.Slug)
                    .IsRequired()
                    .HasMaxLength(220);

                entity.HasIndex(x => x.Slug)
                    .IsUnique();

                entity.Property(x => x.Summary)
                    .HasMaxLength(500);

                entity.Property(x => x.CoverImageUrl)
                    .HasMaxLength(500);

                entity.Property(x => x.AuthorName)
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<ContactMessage>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.FullName)
                    .IsRequired()
                    .HasMaxLength(120);

                entity.Property(x => x.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(x => x.PhoneNumber)
                    .HasMaxLength(30);

                entity.Property(x => x.Subject)
                    .IsRequired()
                    .HasMaxLength(180);

                entity.Property(x => x.Message)
                    .IsRequired()
                    .HasMaxLength(2000);
            });

            modelBuilder.Entity<AiConversationLog>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.UserQuestion)
                    .IsRequired()
                    .HasMaxLength(1500);

                entity.Property(x => x.AiAnswer)
                    .IsRequired();

                entity.Property(x => x.UserIpAddress)
                    .HasMaxLength(80);

                entity.Property(x => x.UserAgent)
                    .HasMaxLength(500);

                entity.Property(x => x.ModelName)
                    .HasMaxLength(100);

                entity.Property(x => x.ErrorMessage)
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<ApiRequestLog>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.ProviderName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(x => x.Endpoint)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(x => x.RequestPath)
                    .HasMaxLength(500);

                entity.Property(x => x.ErrorMessage)
                    .HasMaxLength(1000);
            });
        }
    }
}