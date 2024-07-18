using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UdpChat.img
{
    internal class S
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
