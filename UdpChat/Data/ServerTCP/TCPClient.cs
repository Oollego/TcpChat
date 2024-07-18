using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UdpChat.Models;

namespace UdpChat.Data.ServerTCP
{
    internal class TCPClient
    {
        public string FileName { get; set; } = null!;
        public MessageModel SendingMessage { get; set; }
        public TCPClient(MessageModel message)
        {
            SendingMessage = message;
        }

        // Methods
        public async Task SendMessageAsync(string ipAddress, int port)
        {
            try
            {
                SendingMessage.FileName = FileName;
                using TcpClient client = new TcpClient();

                client.Connect(ipAddress, port);

                using NetworkStream stream = client.GetStream();
                using BinaryReader reader = new BinaryReader(stream, Encoding.UTF8);
                using BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, true);

                byte[] buffer = new byte[0];
                String savedName;

                if (SendingMessage.IsFileAdded)
                {
                    String dir = Directory.GetCurrentDirectory();
                    savedName = Path.Combine(dir, FileName);
                    buffer = await File.ReadAllBytesAsync(savedName);
                    SendingMessage.FileLength = buffer.Length;
                }

                String jsonMessage = JsonSerializer.Serialize<MessageModel>(SendingMessage);
             
                writer.Write(jsonMessage);
               
                bool isClientReadyAcceptFile = reader.ReadBoolean();

                if (isClientReadyAcceptFile)
                {
                  
                    await Task.Run(() => writer.Write(buffer));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}", "Error");
            }
        }
    }
}
