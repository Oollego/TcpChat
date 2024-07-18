using System;
using System.Net;
using System.Windows;


namespace UdpChat.View.Windows
{
    /// <summary>
    /// Interaction logic for AddContactWindow.xaml
    /// </summary>
    public partial class AddContactWindow : Window
    {
        public string PersonName { get; set; } = null!;
        public string PersonSurname { get; set; } = null!;
        public string PersonIpAddress { get; set; } = null!;
        public bool IsCancel { get; private set; }
        public AddContactWindow()
        {
            InitializeComponent();

            IsCancel = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            PersonName = TextBoxName.Text;
            PersonSurname = TextBoxSurname.Text;
            try
            {
                PersonIpAddress = TextBoxIpAddress.Text;
            }
            catch (Exception)
            {
                throw new Exception("Ip address in incorrect");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IsCancel = true;
        }

       
    }
}
