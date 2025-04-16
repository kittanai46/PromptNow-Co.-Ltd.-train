using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ComPortChanger.Models;
using ComPortChanger.Services;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;
using SharedServices.Services;

namespace ComPortChanger
{
    public partial class StatusWindow : Window
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly IComPortService _comPortService;
        private List<PortInfo> _currentPorts;

        public StatusWindow(List<PortInfo> previousPorts = null, string errorMessage = null)
        {
            InitializeComponent();
            _comPortService = new ComPortService();
            _currentPorts = new List<PortInfo>();
            _dateTimeService = new DateTimeService();

            // ตั้งค่าพื้นหลัง Frame
            FrameContainer.Background = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/ComPortChanger;component/Assets/FrameStatus.png")),
                Stretch = Stretch.Fill
            };

            SetCurrentDate();
            LoadCurrentPorts();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                UpdateStatus(errorMessage, "#FF0000");
            }
            else if (previousPorts != null && previousPorts.Any())
            {
                CheckForChanges(previousPorts);
            }
            else
            {
                UpdateStatus("The system could not detect the port", "#CACACA");
            }
        }

            private void SetCurrentDate()
            {
                try
                {
                    _dateTimeService.StartDateTimeUpdate(DateText, TimeText);
                }
                catch (Exception ex)
                {
                    UpdateStatus($"Time service error: {ex.Message}", "#FF0000");
                }
            }

            private void LoadCurrentPorts()
            {
            try
            {
                _currentPorts = _comPortService.GetAllPorts();
                foreach (var port in _currentPorts)
                {
                    AddPortNameText(port);
                }

                if (!_currentPorts.Any())
                {
                    UpdateStatus("NO COM Ports Found", "#FFFF00");
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error loading ports: {ex.Message}", "#FF0000");
            }
        }
        
        private void AddPortNameText(PortInfo port)
        {
            TextBlock portName = new TextBlock
            {
                Text = port.Name,
                FontSize = 25,
                FontFamily = new FontFamily("Noto Sans"),
                Foreground = (Brush)new BrushConverter().ConvertFrom("#CACACA"),
                TextAlignment = TextAlignment.Left,
                Margin = new Thickness(60, 15, 0, 10),
                TextWrapping = TextWrapping.Wrap
            };

            PortNamesContainer.Children.Add(portName);
        }

        private void CheckForChanges(List<PortInfo> previousPorts)
        {
            try
            {
                bool hasChanges = false;
                var changes = new List<string>();

                foreach (var currentPort in _currentPorts)
                {
                    var previousPort = previousPorts.FirstOrDefault(p => p.DeviceID == currentPort.DeviceID);
                    if (previousPort != null && previousPort.CurrentComNumber != currentPort.CurrentComNumber)
                    {
                        hasChanges = true;
                     
                    }
                }

                if (hasChanges)
                {
                    UpdateStatus($"COM Port number change was successful.", "#00FF00");
                }
                else
                {
                    UpdateStatus("The COM Port number remains unchanged.", "#FFFF00");
                }
            }
            catch (Exception ex)
            {
                UpdateStatus($"Error checking changes: {ex.Message}", "#FF0000");
            }
        }

        private void UpdateStatus(string message, string color)
        {
            try
            {
                if (StatusText != null)
                {
                    StatusText.Text = message;
                    var converter = new BrushConverter();
                    StatusText.Foreground = (Brush)converter.ConvertFrom(color);
                    StatusText.UpdateLayout();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating status: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void Ok_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(OkButtonGrid);
            var comPortWindow = Application.Current.Windows
                .OfType<Window>()
                 .FirstOrDefault(w => w.Content is ComPortChangerControl);

                if (comPortWindow != null)
                {
                    comPortWindow.Show();
                }
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