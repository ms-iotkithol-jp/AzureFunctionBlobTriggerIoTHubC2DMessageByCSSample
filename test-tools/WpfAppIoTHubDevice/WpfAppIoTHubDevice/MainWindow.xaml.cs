using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppIoTHubDevice
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            tbConnectionString.Text = "HostName=egohtaiwan20180320.azure-devices.net;DeviceId=win-test;SharedAccessKey=su7LXaKhAbMYNLsjbYA7/X/uswvy3LhH6c1W3Z7JrpQ=";
        }

        Microsoft.Azure.Devices.Client.DeviceClient deviceClient = null;
        private async void buttonOpen_Click(object sender, RoutedEventArgs e)
        {
            deviceClient = Microsoft.Azure.Devices.Client.DeviceClient.CreateFromConnectionString(tbConnectionString.Text, Microsoft.Azure.Devices.Client.TransportType.Amqp);
            try
            {
                
                await deviceClient.OpenAsync();
                tbMessage.Text = "Connected";
                ReceiveC2DMessages();
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Exception : " + ex.Message);
            }
        }

        private async Task ReceiveC2DMessages()
        {
            while (true)
            {
                var message = await deviceClient.ReceiveAsync();
                tbMessage.Text = System.Text.Encoding.UTF8.GetString(message.GetBytes());
                await deviceClient.CompleteAsync(message.LockToken);
            }
        }
    }
}
