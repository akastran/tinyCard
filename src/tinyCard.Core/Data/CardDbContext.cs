﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyCard.Core.Model;

namespace tinyCard.Core.Data
{
    public class CardDbContext : DbContext
    {
        public CardDbContext(
            DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Card>()
                .ToTable("Card");

            modelBuilder.Entity<Card>()
                .HasKey(c => c.CardNumber);

            modelBuilder.Entity<Limit>()
                .ToTable("Limit");

            modelBuilder.Entity<Limit>()
                .HasIndex(c => c.LimitId)
                .IsUnique();
        }
    }
}
