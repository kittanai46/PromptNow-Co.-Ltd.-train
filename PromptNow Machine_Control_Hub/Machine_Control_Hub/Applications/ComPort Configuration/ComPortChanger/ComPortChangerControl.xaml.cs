using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ComPortChanger.Models;
using ComPortChanger.Services;
using SharedServices.Services;

namespace ComPortChanger
{
    public partial class ComPortChangerControl : UserControl
    {
        public ObservableCollection<PortInfo> PortsList { get; set; }
        private readonly IComPortService _comPortService;
        private readonly IPortUIService _portUIService;
        private readonly IDateTimeService _dateTimeService;
        private List<PortInfo> previousPorts;




        public event EventHandler OnBackClicked;
        public event EventHandler<PortChangeEventArgs> OnPortChangeComplete;
        

        public ComPortChangerControl()
        {
           
            InitializeComponent();
            _comPortService = new ComPortService();
            _portUIService = new PortUIService();
            _dateTimeService = new SharedServices.Services.DateTimeService();
            PortsList = new ObservableCollection<PortInfo>();

            _portUIService.SetFrameBackground(FrameContainer);
            ConfirmButton.Click += ApplyChanges_Click;
            BackButton.Click += BackButton_Click;

            LoadPorts();
            SetCurrentDate();
        }

        private void LoadPorts()
        {
            PortsList.Clear();
            var ports = _comPortService.GetAllPorts();
            foreach (var port in ports)
            {
                PortsList.Add(port);
            }

            PortBoxesContainer.Children.Clear();

            foreach (var port in PortsList)
            {
                _portUIService.CreatePortBox(port, PortBoxesContainer);
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
                MessageBox.Show($"Error setting date and time: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ApplyChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // เก็บข้อมูล ports ก่อนการเปลี่ยนแปลง
                var previousPorts = new List<PortInfo>(_comPortService.GetAllPorts());
                await ButtonBlink(ConfirmButtonGrid);

                foreach (var port in PortsList)
                {
                    if (port.CurrentComNumber != port.SelectedComNumber)
                    {
                        try
                        {
                            _comPortService.ChangeComPortNumber(port);
                        }
                        catch (Exception portEx)
                        {
                            // ซ่อนหน้าปัจจุบัน
                            var currentWindow = Window.GetWindow(this);
                            currentWindow?.Hide();

                            // สร้างและแสดง StatusWindow พร้อมข้อความ error
                            var errorStatusWindow = new StatusWindow(previousPorts, portEx.Message);
                            errorStatusWindow.Show();
                            return;
                        }
                    }
                }

                // ซ่อนหน้าปัจจุบัน
                var window = Window.GetWindow(this);
                window?.Hide();

                // สร้างและแสดง StatusWindow สำหรับกรณีสำเร็จ
                var successStatusWindow = new StatusWindow(previousPorts);
                successStatusWindow.Show();
            }
            catch (Exception ex)
            {
                // ซ่อนหน้าปัจจุบัน
                var window = Window.GetWindow(this);
                window?.Hide();

                // สร้างและแสดง StatusWindow พร้อมข้อความ error ทั่วไป
                var errorStatusWindow = new StatusWindow(previousPorts, $"General error: {ex.Message}");
                errorStatusWindow.Show();
            }
        }
        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
             await ButtonBlink(BackButtonGrid);

                var currentWindow = Window.GetWindow(this);
                if (currentWindow != null)
                {
                    currentWindow.Close();
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

    public class PortChangeEventArgs : EventArgs
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public List<PortInfo> PreviousPorts { get; set; }
    }
}