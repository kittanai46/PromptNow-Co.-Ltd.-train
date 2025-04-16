using PromptNow.NetworkAdapter;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SharedServices.Services;

namespace WpfStaticIPApp
{
    public partial class DHCPConfirmation : Window
    {


        private readonly string adapterName;
        private NetworkAdapterManager networkManager;
        private DateTimeService dateTimeService;




        // สร้าง event เพื่อแจ้ง AdapterDetailWindow
        public event Action<string, bool> OnStatusChanged; // เพิ่ม bool เพื่อส่งสถานะสำเร็จหรือไม่




        public DHCPConfirmation(string adapterName)
        {
            InitializeComponent();
            this.adapterName = adapterName;

            networkManager = NetworkAdapterManager.GetInstance();
            dateTimeService = new DateTimeService();
            dateTimeService.StartDateTimeUpdate(DateText, TimeText);
        }

        
        private async void YESButton_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(YES);
            try
            {
                string error;

                // ขั้นที่ 1: Set DHCP
                bool success = networkManager.SetDHCP(adapterName, out error);

                if (success)
                {
                    // ขั้นที่ 2: Clear DNS settings (set to automatic)
                    success = networkManager.SetDNS(adapterName, "", "", out error);

                    if (success)
                    {
                        // ขั้นที่ 3: ดึงข้อมูล DNS ที่ได้รับจาก DHCP
                        string ipAddress, subnetMask, gateway, preferredDNS, alternateDNS;
                        success = networkManager.GetAdapterProfile(
                            adapterName,
                            out ipAddress,
                            out subnetMask,
                            out gateway,
                            out preferredDNS,
                            out alternateDNS,
                            out error
                        );

                        if (success)
                        {
                            string dnsMessage = "";
                            if (!string.IsNullOrEmpty(preferredDNS))
                            {
                                dnsMessage += $"Preferred DNS: {preferredDNS}";
                                if (!string.IsNullOrEmpty(alternateDNS))
                                {
                                    dnsMessage += $", Alternate DNS: {alternateDNS}";
                                }
                            }
                            else
                            {
                                dnsMessage = "Waiting for DHCP to assign DNS servers";
                            }

                            OnStatusChanged?.Invoke($"DHCP Configuration Successful. {dnsMessage}", true);
                        }
                        else
                        {
                            OnStatusChanged?.Invoke($"DHCP enabled but couldn't fetch DNS info: {error}", true);
                        }
                    }
                    else
                    {
                        OnStatusChanged?.Invoke($"DHCP enabled but failed to set DNS to automatic: {error}", false);
                    }
                }
                else
                {
                    OnStatusChanged?.Invoke($"Failed to enable DHCP: {error}", false);
                }

                DialogResult = success;
                this.Close();
            }
            catch (Exception ex)
            {
                OnStatusChanged?.Invoke($"Unexpected error: {ex.Message}", false);
                DialogResult = false;
                this.Close();
            }
        }
        // เมื่อคลิกปุ่ม Cancel
        private async void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(CANCEL);
            OnStatusChanged?.Invoke("DHCP configuration cancelled by user.", false);
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
