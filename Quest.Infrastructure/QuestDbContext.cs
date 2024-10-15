using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quest.Domain.Models;

namespace Quest.Infrastructure
{
    public class QuestDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; } // Игроки
        public DbSet<Domain.Models.Quest> Quests { get; set; } // Квесты
        public DbSet<PlayerQuest> PlayerQuests { get; set; } // Квесты игроков

        public QuestDbContext(DbContextOptions<QuestDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфигурация сущности Player
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(p => p.Level)
                    .IsRequired();
            });

            // Конфигурация сущности Quest
            modelBuilder.Entity<Domain.Models.Quest>(entity =>
            {
                entity.HasKey(q => q.Id);
                entity.Property(q => q.Title)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(q => q.Description)
                    .IsRequired()
                    .HasMaxLength(1000);
                entity.Property(q => q.Requirements)
                    .HasMaxLength(500);
                entity.Property(q => q.Rewards)
                    .HasMaxLength(500);
                entity.Property(q => q.MinPlayerLevel)
                    .IsRequired();
            });

            // Конфигурация сущности PlayerQuest
            modelBuilder.Entity<PlayerQuest>(entity =>
            {
                entity.HasKey(pq => pq.Id);

                entity.HasOne(pq => pq.Player)
                    .WithMany(p => p.PlayerQuests)
                    .HasForeignKey(pq => pq.PlayerId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(pq => pq.Quest)
                    .WithMany(q => q.PlayerQuests)
                    .HasForeignKey(pq => pq.QuestId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(pq => pq.Status)
                    .IsRequired();
                entity.Property(pq => pq.Progress)
                    .HasMaxLength(1000);

                entity.Property(pq => pq.AcceptedAt)
                    .IsRequired();
                entity.Property(pq => pq.CompletedAt);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}