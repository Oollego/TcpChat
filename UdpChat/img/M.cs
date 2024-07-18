using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpChat.img
{
    internal class M
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string IpAddress { get; set; } = null!;
        public string Text { get; set; } = null!;
        public long FileLength { get; set; } //zdelat tolko get;
        public bool IsFileAdded { get; set; }
        public string FileName { get; set; } = null!;
        public bool IsIncoming { get; set; }

        public Guid ContactId { get; set; }
        public C? Contact { get; set; }
    }
}
