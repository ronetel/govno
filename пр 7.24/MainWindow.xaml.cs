using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace пр_7._24
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void But_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ip_server.Text) && !string.IsNullOrEmpty(name_client.Text))
            {
                string ip = ip_server.Text;
                string name = name_client.Text;
                ClientWindow clientWindow = new ClientWindow(ip, name);
                clientWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("эу, ну ка все поля заполни (имя и айпи)");
            }
        }

        private void Server_but_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(name_client.Text))
            {
                string name = name_client.Text; 
                ServerWindow serverWindow = new ServerWindow(name);
                serverWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("хоть ты и сервер но введи имя пжпжпжпжпж");
            }
        }
    }
}