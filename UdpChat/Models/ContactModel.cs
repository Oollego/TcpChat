using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using UdpChat.Data.Entities;

namespace UdpChat.Models
{
   
    internal class ContactModel: INotifyPropertyChanged
    {
        [Required]
        public Guid Id { get; set; }

        string _name = null!;
        public string Name
        {
            get { return _name; }
            set { if (_name != value) { _name = value; INotifyPropertyChanged(); } }
        }

        string _surname = null!;
        public string Surname
        {
            get { return _surname; }
            set { if (_surname != value) { _surname = value; INotifyPropertyChanged(); } }
        }
       
        string _ipAddress = null!;
        [Required]
        public string IpAddress
        {
            get { return _ipAddress; }
            set { if (_ipAddress != value) { _ipAddress = value; INotifyPropertyChanged(); } }
        }

        byte[]? _avatar = null!;
        public byte[]? Avatar 
        {
            get { return _avatar; } 
            set { if (_avatar != value) { _avatar = value; INotifyPropertyChanged(); } } 
        }

        public bool IsAvatarAdded { get; set; }

        int _notReadedMessage = 0;
        public int NotReadedMessage
        {
            get { return _notReadedMessage; }
            set { if (_notReadedMessage != value) { _notReadedMessage = value; INotifyPropertyChanged(); } }
        }

        public ObservableCollection<MessageModel> Messages { get; set; } = new ();

        void INotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        //public static implicit operator EntityContact (ContactModel contact)
        //{
        //    EntityContact result = new();

        //    result.Id = contact.Id;
        //    result.Name = contact.Name;
        //    result.Surname = contact.Surname;
        //    result.IpAddress = contact.IpAddress;
        //    result.Avatar = contact.Avatar;
        //    result.IsAvatarAdded = contact.IsAvatarAdded;
        //    //result.Messages = contact.Messages.ToList();

        //    return result;
        //} 
    }


  
   
}
