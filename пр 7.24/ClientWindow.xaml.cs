using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace пр_7._24
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        DateTime _date = DateTime.Now;
        Socket socket;
        List<Socket> clients = new List<Socket>();
        public ClientWindow(string ip, string name)
        {
            InitializeComponent();
            socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ip, 8888);
            SendMassage($"#{name}");
            ReceiveName();
        }
        
        private void out_Click(object sender, RoutedEventArgs e)
        {
            SendMassage("/disconnect");
        }

        private void go_Click(object sender, RoutedEventArgs e)
        {
            SendMassage(message.Text);
        }
        private async void SendMassage(string massage)
        {
            byte[] bytes = Encoding.UTF8.GetBytes($" [{_date}] {massage}");
            await socket.SendAsync(bytes);
            if (massage == "/disconnect")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }
        private async void ReceiveName()
        {
            while (true)
            {   
                var prikol = new byte[65536];
                await socket.ReceiveAsync(prikol);
                string name = Encoding.UTF8.GetString(prikol);
                if (name.Contains('#'))
                {
                    string jopa = name.Split("#")[1];
                    clients_list.Items.Add(jopa);
                }
                else
                {
                    dialoge.Items.Add(name);
                }
            }
        }
        private async void ReceiveMessage()
        {
            while (true)
            {
                var krestik = new byte[65535];
                await socket.ReceiveAsync(krestik);
                string massage = Encoding.UTF8.GetString(krestik);
                dialoge.Items.Add(massage);

            }
        }
    }
}
