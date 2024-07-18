using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UdpChat.Data;

namespace UdpChat.Models.MessageModels
{
    internal class IMessage
    {
        public Guid GuidId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string IpAddress { get; set; } = null!;
    }
}
