using System;
using System.IO;
using UdpChat.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace UdpChat.Data.Entities
{
    internal class DataContext : DbContext
    {

        public DbSet<SettingsModel> Settings { get; set; } = null!;
        public DbSet<ContactModel> Contacts { get; set; } = null!;
        public DbSet<MessageModel> Messages { get; set; } = null!;

        
        public DataContext(DbContextOptions options) : base(options)
        {
           
           
                Database.Migrate();
           
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SettingsModel>().HasData(new SettingsModel
            {
                Id = Guid.NewGuid(),
                Name = "UserName",
                Surname = "",
                FolderPath = Directory.GetCurrentDirectory() + "\\Downloads",
                IsAvatarAdded = false,
                Port = 10000
            });

            //modelBuilder.Entity<EntityContact>().HasData(new EntityContact
            //{

            //    Id = Guid.NewGuid(),
            //    Name = "UserName",
            //    Surname = "",
            //    IpAddress = "127.0.0.1",
            //    IsAvatarAdded = false,

            //});
        }
    }
}
