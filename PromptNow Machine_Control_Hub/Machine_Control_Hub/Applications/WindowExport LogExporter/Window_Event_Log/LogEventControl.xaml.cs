using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SharedServices.Services;

namespace Window_Event_Log
{
    public partial class LogEventControl : UserControl
    {
        private readonly IDateTimeService _dateTimeService;

        public LogEventControl()
        {
            InitializeComponent();
            _dateTimeService = new DateTimeService();
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

        private void NavigateToLogLevelWindow(string logType)
        {
            var currentWindow = Window.GetWindow(this);
            if (currentWindow != null)
            {
                currentWindow.Hide();

                var window = new Window
                {
                    Title = "Log Level Selection",
                    Height = 720,
                    Width = 540,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Content = new LogLevelSelectionControl(logType)
                };

                window.Closed += (s, args) => currentWindow.Show();
                window.Show();
            }
        }

        // Event handlers เหมือนเดิมทั้งหมด แต่ปรับให้ทำงานกับ parent window
        private void ApplicationLog_Click(object sender, RoutedEventArgs e)
        {
            NavigateToLogLevelWindow("Application");
        }

        private void SystemLog_Click(object sender, RoutedEventArgs e)
        {
            NavigateToLogLevelWindow("System");
        }

        private void SecurityLog_Click(object sender, RoutedEventArgs e)
        {
            NavigateToLogLevelWindow("Security");
        }

        private void SetupLog_Click(object sender, RoutedEventArgs e)
        {
            NavigateToLogLevelWindow("Setup");
        }

        private void ForwardedEventsLog_Click(object sender, RoutedEventArgs e)
        {
            NavigateToLogLevelWindow("Forwarded Events");
        }

        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(BackButtonGrid);
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.Close();
            }
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