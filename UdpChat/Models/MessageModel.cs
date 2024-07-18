using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UdpChat.img;
using UdpChat.Models.MessageModels;

namespace UdpChat.Models
{
    internal class MessageModel
    {

        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string IpAddress { get; set; } = null!;
        public string Text { get; set; } = null!;
        public long FileLength { get; set; }
        public bool IsFileAdded { get; set; } = false;
        public string? FileName { get; set; }
        public bool IsIncoming { get; set; } = false;

        //For Entity
        public Guid ContactId { get; set; }
        public ContactModel? Contact { get; set; }
    }
}
