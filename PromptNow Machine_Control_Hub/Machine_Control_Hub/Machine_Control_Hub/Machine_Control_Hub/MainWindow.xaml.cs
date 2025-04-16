using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SharedServices.Services;
using ComPortChanger;
using Window_Event_Log;
using NetworkAdapter;


namespace Machine_Control_Hub
{
    public partial class MainWindow : Window
    {
        private readonly DateTimeService _dateTimeService;
        public MainWindow()
        {
            _dateTimeService = new DateTimeService();
            InitializeComponent();
            SetCurrentDate();
        }
        private void SetCurrentDate()
        {
            try
            {
                _dateTimeService.StartDateTimeUpdate(DateText, TimeText);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting date and time: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void NetConfig_Click(object sender, RoutedEventArgs e)
            {
                await ButtonBlink(NetConfig);
                this.Hide();

                var window = new Window
                {
                    Title = "Network Adapter Configuration",
                    Height = 720,
                    Width = 540,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Content = new NetworkAdapter.NetworkAdapterControl()
                };

                window.Closed += (s, args) => this.Show();
                window.Show();
            }

        private async void LogExport_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(LogExport);

            // ซ่อนหน้าปัจจุบัน
            this.Hide();

            // สร้างและแสดง Window สำหรับ LogEventControl
            var window = new Window
            {
                Title = "Windows Logs Viewer",
                Height = 720,
                Width = 540,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Content = new LogEventControl()
            };

            window.Closed += (s, args) => this.Show();
            window.Show();
        }

        private async void ComPortChang_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(ComPortChang);
            this.Hide();
            var window = new Window
            {
                Title = "COM Port Number Changer",
                Height = 720,
                Width = 540,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Content = new ComPortChangerControl()
            };
            window.Closed += (s, args) => this.Show();
            window.Show();
        }

        private async void Void_Icon_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(Void_Icon);
        }

        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(BackButton);
            this.Close();
        }


        private Task ButtonBlink(Grid buttonGrid)
        {
            if (buttonGrid == null) return Task.CompletedTask;

            return Task.Run(async () =>
            {
                try
                {
                    await Dispatcher.InvokeAsync(() =>
                    {
                        buttonGrid.Opacity = 0.5;
                    });
                    await Task.Delay(200);
                    await Dispatcher.InvokeAsync(() =>
                    {
                        buttonGrid.Opacity = 1;
                        buttonGrid.Background = Brushes.Transparent;
                    });
                }
                catch (Exception)
                {
                    // Handle or log error if needed
                }
            });
        }
    }
}

