using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SharedServices.Services;

namespace WpfStaticIPApp.Dialogs
{
    public partial class SaveConfirmation : Window
    {
        private DateTimeService dateTimeService;
        private string adapterName;
        private string ipAddress;
        private string subnetMask;
        private string gateway;
        private string preferredDNS;
        private string alternateDNS;


        // Constructor รับค่าจากหน้า AdapterDetailWindow
        public SaveConfirmation(string adapterName, string ipAddress, string subnetMask,
                                string gateway, string preferredDNS, string alternateDNS)
        {
            InitializeComponent();
            dateTimeService = new DateTimeService();
            dateTimeService.StartDateTimeUpdate(DateText, TimeText);

            // รับข้อมูลทั้งหมดที่ส่งมา
            this.adapterName = adapterName;
            this.ipAddress = ipAddress;
            this.subnetMask = subnetMask;
            this.gateway = gateway;
            this.preferredDNS = preferredDNS;
            this.alternateDNS = alternateDNS;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(Save);
            this.DialogResult = true;
            this.Close(); 
        }

        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(Cancel);
            this.DialogResult = false;
            this.Close();
        }

        private Task ButtonBlink(Grid buttonGrid)
        {
            return Task.Run(async () => {
                Dispatcher.Invoke(() =>
                {
                    buttonGrid.Opacity = 0.5;

                });
                await Task.Delay(200);
                Dispatcher.Invoke(() =>
                {
                    buttonGrid.Opacity = 1;

                });
            });
        }
    }
}
