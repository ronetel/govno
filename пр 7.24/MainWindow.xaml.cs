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
            string ip = ip_server.Text;
            string name = name_client.Text;
            ClientWindow clientWindow = new ClientWindow(ip,name);
            clientWindow.Show();
            Close();
        }

        private void Server_but_Click(object sender, RoutedEventArgs e)
        {
            ServerWindow serverWindow = new ServerWindow();
            serverWindow.Show();
            Close();
        }
    }
}