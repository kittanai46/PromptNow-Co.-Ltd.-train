using PromptNow.NetworkAdapter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SharedServices.Services;

namespace NetworkAdapter
{
    public partial class NetworkAdapterControl : UserControl
    {
        public NetworkAdapterManager NetworkAdapterManager { get; private set; }
        private readonly IDateTimeService _dateTimeService;
        private Dictionary<string, string> adapterDescriptions;

        public NetworkAdapterControl()
        {
            InitializeComponent();

            NetworkAdapterManager = NetworkAdapterManager.GetInstance();
            _dateTimeService = new DateTimeService();
            adapterDescriptions = new Dictionary<string, string>();

            LoadNetworkAdapters();
            SetCurrentDate();

            AdapterGrid.AddHandler(Button.ClickEvent, new RoutedEventHandler(Adapter_Click));
        }

        private void LoadNetworkAdapters()
        {
            if (!NetworkAdapterManager.GetInstance().GetNetworkAdaptersWithDescriptions(out List<string> adapterDetails, out string error))
            {
                MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            AdapterGrid.Items.Clear();
            adapterDescriptions.Clear();

            foreach (var adapterDetail in adapterDetails)
            {
                string[] details = adapterDetail.Split(new[] { " : " }, StringSplitOptions.None);
                string connectionID = details[0];
                string description = details.Length > 1 ? details[1] : string.Empty;

                adapterDescriptions[connectionID] = description;
                AdapterGrid.Items.Add(connectionID);
            }
        }

        private void Adapter_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement element && element.DataContext is string connectionID)
            {
                string description = adapterDescriptions.ContainsKey(connectionID) ? adapterDescriptions[connectionID] : string.Empty;

                var currentWindow = Window.GetWindow(this);
                if (currentWindow != null)
                {
                    currentWindow.Hide();

                    var window = new Window
                    {
                        Title = "Adapter Detail",
                        Height = 720,
                        Width = 540,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        Content = new AdapterDetailControl(connectionID, description)
                    };

                    window.Closed += (s, args) => currentWindow.Show();
                    window.Show();
                }
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

        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(BACK);
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.Close();
            }
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