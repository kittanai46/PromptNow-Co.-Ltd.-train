using System;
using System.Collections.Generic;
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
using WpfStaticIPApp.Services;

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

        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(BackButton);
            this.Close();
        }


        private async void NetConfig_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(NetConfig);
        }

        private async void LogExport_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(LogExport);
        }

        private async void ComPortChang_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(ComPortChang);
        }

        private async void Void_Icon_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(Void_Icon);
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

