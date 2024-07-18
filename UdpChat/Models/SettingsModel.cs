using System;
using System.ComponentModel.DataAnnotations;

namespace UdpChat.Models
{
    internal class SettingsModel
    {

        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public byte[]? Avatar { get; set; } = null!;
        public string AvatarFileName { get; set; } = "";
        public bool IsAvatarAdded { get; set; }
        public string FolderPath { get; set; } = null!;
        public int Port { get; set; }

    }
}
