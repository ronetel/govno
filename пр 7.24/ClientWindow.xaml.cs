﻿using System;
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
        private CancellationTokenSource cts;
        Socket socket;
        string Name { get; set; }
        public ClientWindow(string ip, string name)
        {
            this.Name = name;
            InitializeComponent();
            cts = new CancellationTokenSource();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(ip, 8888);
            SendMassage($"#{name}");
            ReceiveName(cts.Token);

            this.Closing += ClientWindow_Closing;
        }

        private void out_Click(object sender, RoutedEventArgs e)
        {
            SendMassage("/disconnect");
            Dispatcher.Invoke(() =>
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            });
        }

        private void go_Click(object sender, RoutedEventArgs e)
        {
            SendMassage(message.Text);
        }
        private async void SendMassage(string massage)
        {
            DateTime date = DateTime.Now;
            byte[] bytes = Encoding.UTF8.GetBytes($"[{date}] [{Name}] - {massage}");

            await socket.SendAsync(bytes, SocketFlags.None);
            if (massage == "/disconnect")
            {
                cts.Cancel();
            }
        }
        private void ClientWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (socket.Connected)
            {
                SendMassage("/disconnect");

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }
        private async Task ReceiveName(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var bytes = new byte[65536];
                await socket.ReceiveAsync(bytes);
                string mess = Encoding.UTF8.GetString(bytes);
                if (mess.Contains('#'))
                {
                    string[] names = mess.Split('#');
                    foreach (string name in names)
                    {
                        if (!string.IsNullOrEmpty(name))
                        {
                            clients_list.Items.Add(name);
                        }
                    }
                }
                else
                {
                    dialoge.Items.Add(mess);
                }
            }
        }

    }
}