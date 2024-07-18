using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpChat.img
{
    internal class C
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Surname { get; set; }
        [Required]
        public string IpAddress { get; set; } = null!;
        public byte[]? Avatar { get; set; } = null!;
        public bool IsAvatarAdded { get; set; }
        public int NotReadedMessage { get; set; }
        public List<M> Messages { get; set; } = new();

    }
}
