using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UdpChat.Models;
using UdpChat.Models.MessageModels;
using UdpChat.ViewModels;

namespace UdpChat.Data.ServerTCP
{
    internal class TCPServer
    {
        public int Port { get; set; }
      //  public MessageModel ClientMessage { get; private set; }
        public string DirectoryPath { get; set; }

        public TCPServer(int port)
        {
            Port = port;
           // ClientMessage = new MessageModel();
            DirectoryPath = Directory.GetCurrentDirectory();
        }

        public async Task ServerStart(MainWindowViewModel ViewModel)
        {

            TcpListener listener = new(System.Net.IPAddress.Any, Port);
            try
            {
                listener.Start();
                while (true)
                {
                    //await Task.Run(async () => { 
                    //    TcpClient tcpClient = await listener.AcceptTcpClientAsync();
                    //    if (tcpClient != null)
                    //    {
                    //        //await Task.Run(async () => await ProcessTcpClientAsync(tcpClient, ClientMessage));
                    //        await ProcessTcpClientAsync(tcpClient, ClientMessage);
                    //    }
                    //});
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();
                    if (tcpClient != null)
                    {
                        //await Task.Run(async () => await ProcessTcpClientAsync(tcpClient, ClientMessage));
                        MessageModel newMessage =  await ProcessTcpClientAsync(tcpClient);
                        ViewModel.Message = newMessage;
                        //if (_contacts != null)
                        //{
                        //    var contact = _contacts.FirstOrDefault(e => e.Id == message.ContactId, null);
                        //    if (contact != null)
                        //    {
                        //        contact.Messages.Add(message);
                        //    }
                        //    else
                        //    {
                        //        var newContact = new ContactModel { Id = message.ContactId, Name = "unknown", IpAddress = message.IpAddress };
                        //        newContact.Messages.Add(message);
                        //        _contacts.Add(newContact);
                        //    }
                        //}
                    }
                }
            }
            finally
            {
                listener.Stop();
            }
        }



        private async Task<MessageModel> ProcessTcpClientAsync(TcpClient tcpClient)
        {
            MessageModel ClientMessage = null!;
            try
            {
                using NetworkStream stream = tcpClient.GetStream();
                using BinaryReader reader = new BinaryReader(stream, Encoding.UTF8);
                using BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, true);

                // Receive text
                String JsonText = reader.ReadString();

               
                if (JsonText != null) { ClientMessage = JsonSerializer.Deserialize<MessageModel>(JsonText) ?? ClientMessage; }

                if (ClientMessage.IsFileAdded && !String.IsNullOrEmpty(ClientMessage.FileName))
                {
                    string newFileName = FileNameTransformation(ClientMessage.FileName);
                    if (newFileName == string.Empty)
                    {
                        ClientMessage.FileName = null!;
                        ClientMessage.IsFileAdded = false;
                        ClientMessage.FileLength = 0;
                        return ClientMessage;
                    }

                    writer.Write(ClientMessage.IsFileAdded);

                    byte[] buffer = new byte[ClientMessage.FileLength];
                   
                    stream.Read(buffer);

                   

                    // reader.ReadBytes(message.FileLength);


                    String savedName = Path.Combine(DirectoryPath, newFileName);

                    using Stream filestream = System.IO.File.OpenWrite(savedName);
                    filestream.Write(buffer, 0, buffer.Length);
                    ClientMessage.FileName = savedName;
                    //stream.CopyTo(filestream);
                    return ClientMessage;
                }
                else
                {
                    writer.Write(ClientMessage.IsFileAdded);
                    return ClientMessage;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}", "Error");
            }
            return ClientMessage;
        }

        private string FileNameTransformation(string fileName)
        {
            if (fileName == null) return string.Empty;

            int dotPosition = fileName.LastIndexOf('.');
            if (dotPosition == -1)
            {
                return string.Empty;
            }

            String ext = fileName.Substring(dotPosition);
            string result = Guid.NewGuid() + ext;

            return result;
        }
    }

    //public async Task ServerStart(ObservableCollection<ContactModel> _contacts)
    //{

    //    TcpListener listener = new(System.Net.IPAddress.Any, Port);
    //    try
    //    {
    //        listener.Start();
    //        while (true)
    //        {
    //            //await Task.Run(async () => { 
    //            //    TcpClient tcpClient = await listener.AcceptTcpClientAsync();
    //            //    if (tcpClient != null)
    //            //    {
    //            //        //await Task.Run(async () => await ProcessTcpClientAsync(tcpClient, ClientMessage));
    //            //        await ProcessTcpClientAsync(tcpClient, ClientMessage);
    //            //    }
    //            //});
    //            TcpClient tcpClient = await listener.AcceptTcpClientAsync();
    //            if (tcpClient != null)
    //            {
    //                //await Task.Run(async () => await ProcessTcpClientAsync(tcpClient, ClientMessage));
    //                MessageModel message = await ProcessTcpClientAsync(tcpClient);
    //                if (_contacts != null)
    //                {
    //                    var contact = _contacts.FirstOrDefault(e => e.Id == message.ContactId, null);
    //                    if (contact != null)
    //                    {
    //                        contact.Messages.Add(message);
    //                    }
    //                    else
    //                    {
    //                        var newContact = new ContactModel { Id = message.ContactId, Name = "unknown", IpAddress = message.IpAddress };
    //                        newContact.Messages.Add(message);
    //                        _contacts.Add(newContact);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    finally
    //    {
    //        listener.Stop();
    //    }
    //}
    //internal class TCPServer
    //{
    //    public int Port { get; set; }
    //    //  public MessageModel ClientMessage { get; private set; }
    //    public string DirectoryPath { get; set; }

    //    public TCPServer(int port)
    //    {
    //        Port = port;
    //        // ClientMessage = new MessageModel();
    //        DirectoryPath = Directory.GetCurrentDirectory();
    //    }

    //    public async Task ServerStart(MessageModel ClientMessage)
    //    {

    //        TcpListener listener = new(System.Net.IPAddress.Any, Port);
    //        try
    //        {
    //            listener.Start();
    //            while (true)
    //            {
    //                TcpClient tcpClient = await listener.AcceptTcpClientAsync();
    //                if (tcpClient != null)
    //                {
    //                    //await Task.Run(async () => await ProcessTcpClientAsync(tcpClient, ClientMessage));
    //                    await ProcessTcpClientAsync(tcpClient, ClientMessage);
    //                }
    //            }
    //        }
    //        finally
    //        {
    //            listener.Stop();
    //        }
    //    }



    //    private async Task ProcessTcpClientAsync(TcpClient tcpClient, MessageModel ClientMessage)
    //    {

    //        try
    //        {
    //            using NetworkStream stream = tcpClient.GetStream();
    //            using BinaryReader reader = new BinaryReader(stream, Encoding.UTF8);
    //            using BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, true);

    //            // Receive text
    //            String JsonText = reader.ReadString();


    //            if (JsonText != null) { ClientMessage = JsonSerializer.Deserialize<MessageModel>(JsonText) ?? ClientMessage; }

    //            if (ClientMessage.IsFileAdded == true && !String.IsNullOrEmpty(ClientMessage.FileName))
    //            {
    //                string newFileName = FileNameTransformation(ClientMessage.FileName);
    //                if (newFileName == string.Empty)
    //                {
    //                    ClientMessage.FileName = null!;
    //                    ClientMessage.IsFileAdded = false;
    //                    ClientMessage.FileLength = 0;
    //                    return;
    //                }

    //                writer.Write(ClientMessage.IsFileAdded);

    //                byte[] buffer = new byte[ClientMessage.FileLength];
    //                await stream.ReadAsync(buffer);

    //                // reader.ReadBytes(message.FileLength);


    //                String savedName = Path.Combine(DirectoryPath, newFileName);

    //                using Stream filestream = System.IO.File.OpenWrite(savedName);
    //                await stream.CopyToAsync(filestream);
    //            }
    //            else
    //            {
    //                writer.Write(ClientMessage.IsFileAdded);
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine($"Error sending message: {ex.Message}", "Error");
    //        }

    //    }

    //    private string FileNameTransformation(string fileName)
    //    {
    //        if (fileName == null) return string.Empty;

    //        int dotPosition = fileName.LastIndexOf('.');
    //        if (dotPosition == -1)
    //        {
    //            return string.Empty;
    //        }

    //        String ext = fileName.Substring(dotPosition);
    //        string result = Guid.NewGuid() + ext;

    //        return result;
    //    }
    //}
}
