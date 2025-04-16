using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PromptNow.NetworkAdapter;
using WpfStaticIPApp.Dialogs;
using SharedServices.Services;
using System.Runtime.ConstrainedExecution;
using WpfStaticIPApp;

namespace NetworkAdapter
{
    public partial class AdapterDetailControl : UserControl
    {
        private readonly IDateTimeService _dateTimeService;
        private readonly NetworkAdapterManager networkAdapterManager;
        private readonly string connectionID;
        private readonly string description;
        private TextBox activeTextBox;

        public AdapterDetailControl(string connectionID, string description)
        {
            InitializeComponent();
            this.connectionID = connectionID;
            this.description = description;
            networkAdapterManager = NetworkAdapterManager.GetInstance();
            _dateTimeService = new DateTimeService();

            AdapterNameText.Text = $"{connectionID} : {description}";
            _dateTimeService.StartDateTimeUpdate(DateText, TimeText);
            LoadAdapterData();
        }

        private void LoadAdapterData()
        {
            try
            {
                string error;
                string ipAddress, subnetMask, gateway, preferredDNS, alternateDNS;

                bool result = networkAdapterManager.GetAdapterProfile(connectionID,
                    out ipAddress, out subnetMask, out gateway, out preferredDNS, out alternateDNS, out error);

                if (result)
                {
                    IPAddressTextBox.Text = ipAddress ?? "N/A";
                    SubnetMaskTextBox.Text = subnetMask ?? "N/A";
                    GatewayTextBox.Text = gateway ?? "N/A";
                    PreferredDNSTextBox.Text = preferredDNS ?? "N/A";
                    AlternateDNSTextBox.Text = alternateDNS ?? "N/A";
                }
                else
                {
                    MessageBox.Show($"Error fetching data: {error}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(IPAddressTextBox.Text) || !System.Net.IPAddress.TryParse(IPAddressTextBox.Text, out _))
            {
                UpdateStatus("Invalid IP Address. Please enter a valid IP address.", Colors.Red);
                return false;
            }

            if (string.IsNullOrWhiteSpace(SubnetMaskTextBox.Text) || !System.Net.IPAddress.TryParse(SubnetMaskTextBox.Text, out _))
            {
                UpdateStatus("Invalid Subnet Mask. Please enter a valid subnet mask.", Colors.Red);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(GatewayTextBox.Text) && !System.Net.IPAddress.TryParse(GatewayTextBox.Text, out _))
            {
                UpdateStatus("Invalid Gateway. Please enter a valid gateway.", Colors.Red);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(PreferredDNSTextBox.Text) && !System.Net.IPAddress.TryParse(PreferredDNSTextBox.Text, out _))
            {
                UpdateStatus("Invalid Preferred DNS. Please enter a valid DNS address.", Colors.Red);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(AlternateDNSTextBox.Text) && !System.Net.IPAddress.TryParse(AlternateDNSTextBox.Text, out _))
            {
                UpdateStatus("Invalid Alternate DNS. Please enter a valid DNS address.", Colors.Red);
                return false;
            }

            UpdateStatus("All inputs are valid.", Colors.Green);
            return true;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(SAVE);
            var currentWindow = Window.GetWindow(this);
            currentWindow?.Hide();

            if (!ValidateInput())
            {
                currentWindow?.Show();
                return;
            }

            UpdateStatus("Saving...", Colors.Yellow);

            SaveConfirmation confirmationDialog = new SaveConfirmation(
                adapterName: connectionID,
                ipAddress: IPAddressTextBox.Text,
                subnetMask: SubnetMaskTextBox.Text,
                gateway: GatewayTextBox.Text,
                preferredDNS: PreferredDNSTextBox.Text,
                alternateDNS: AlternateDNSTextBox.Text
            );

            bool? result = confirmationDialog.ShowDialog();

            if (result == true)
            {
                string error = string.Empty;

                try
                {
                    bool successIP = networkAdapterManager.SetIPAddress(
                        connectionID,
                        IPAddressTextBox.Text,
                        SubnetMaskTextBox.Text,
                        GatewayTextBox.Text,
                        out error);

                    if (!successIP)
                    {
                        UpdateStatus($"Failed to set IP Address: {error}", Colors.Red);
                        currentWindow?.Show();
                        return;
                    }

                    bool successDNS = networkAdapterManager.SetDNS(
                        connectionID,
                        PreferredDNSTextBox.Text,
                        AlternateDNSTextBox.Text,
                        out error);

                    if (!successDNS)
                    {
                        UpdateStatus($"Failed to set DNS: {error}", Colors.Red);
                        currentWindow?.Show();
                        return;
                    }

                    UpdateStatus("Network settings saved successfully", Colors.Green);
                    LoadAdapterData();
                }
                catch (Exception ex)
                {
                    UpdateStatus($"Error: {ex.Message}", Colors.Red);
                    currentWindow?.Show();
                    return;
                }
            }
            else
            {
                UpdateStatus("Save operation cancelled", Colors.Red);
            }

            currentWindow?.Show();
        }

        private async void DHCPButton_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(DHCP);
            var currentWindow = Window.GetWindow(this);
            currentWindow?.Hide();

            var dhcpWindow = new DHCPConfirmation(connectionID);
            bool? result = dhcpWindow.ShowDialog();

            if (result == true)
            {
                LoadAdapterData();
                UpdateStatus("DHCP settings applied successfully", Colors.Green);
            }
            else
            {
                UpdateStatus("DHCP operation cancelled", Colors.Red);
            }

            currentWindow?.Show();
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(REFRESH);
            UpdateStatus("Refreshing...", Colors.Yellow);

            IPAddressTextBox.Text = string.Empty;
            SubnetMaskTextBox.Text = string.Empty;
            GatewayTextBox.Text = string.Empty;
            PreferredDNSTextBox.Text = string.Empty;
            AlternateDNSTextBox.Text = string.Empty;

            await Task.Delay(2000);
            LoadAdapterData();
            UpdateStatus("Refreshed successfully", Colors.Cyan);
        }

        private async void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            await ButtonBlink(CLEAR);
            IPAddressTextBox.Clear();
            SubnetMaskTextBox.Clear();
            GatewayTextBox.Clear();
            PreferredDNSTextBox.Clear();
            AlternateDNSTextBox.Clear();

            UpdateStatus("Cleared successfully", Colors.Yellow);
        }

        private void UpdateStatus(string message, Color color)
        {
            if (StatusMessageRun != null)
            {
                StatusMessageRun.Text = message;
                StatusMessageRun.Foreground = new SolidColorBrush(color);
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                activeTextBox = textBox;
                KeyboardGrid.Visibility = Visibility.Visible;
                ButtonPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void CloseKeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            KeyboardGrid.Visibility = Visibility.Collapsed;
            ButtonPanel.Visibility = Visibility.Visible;
            activeTextBox = null;
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if (activeTextBox != null && activeTextBox.Text.Length > 0)
            {
                activeTextBox.Text = activeTextBox.Text.Substring(0, activeTextBox.Text.Length - 1);
            }
        }

        private void KeyboardButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && activeTextBox != null && button.Tag != null)
            {
                string value = button.Tag.ToString();
                activeTextBox.Text += value;
            }
        }

        private void KeyboardClearButton_Click(object sender, RoutedEventArgs e)
        {
            activeTextBox?.Clear();
        }

        private void KeyboardRefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (activeTextBox != null)
            {
                try
                {
                    string refreshedValue = GetRefreshedValue(activeTextBox.Name);
                    activeTextBox.Text = refreshedValue;
                    UpdateStatus($"Value refreshed for {activeTextBox.Name}", Colors.Cyan);
                }
                catch (Exception ex)
                {
                    UpdateStatus($"Error refreshing value: {ex.Message}", Colors.Red);
                }
            }
            else
            {
                UpdateStatus("No active field to refresh!", Colors.Red);
            }
        }

        private string GetRefreshedValue(string fieldName)
        {
            switch (fieldName)
            {
                case "IPAddressTextBox":
                    return networkAdapterManager.GetIPAddress(connectionID) ?? "N/A";
                case "SubnetMaskTextBox":
                    return networkAdapterManager.GetSubnetMask(connectionID) ?? "N/A";
                case "GatewayTextBox":
                    return networkAdapterManager.GetGateway(connectionID) ?? "N/A";
                case "PreferredDNSTextBox":
                    return networkAdapterManager.GetPreferredDNS(connectionID) ?? "N/A";
                case "AlternateDNSTextBox":
                    return networkAdapterManager.GetAlternateDNS(connectionID) ?? "N/A";
                default:
                    return string.Empty;
            }
        }

        private void KeyboardNavigateUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (activeTextBox != null)
            {
                var textBoxes = new List<TextBox> { IPAddressTextBox, SubnetMaskTextBox, GatewayTextBox, PreferredDNSTextBox, AlternateDNSTextBox };
                int index = textBoxes.IndexOf(activeTextBox);
                if (index > 0)
                {
                    textBoxes[index - 1].Focus();
                }
            }
        }

        private void KeyboardNavigateDownButton_Click(object sender, RoutedEventArgs e)
        {
            if (activeTextBox != null)
            {
                var textBoxes = new List<TextBox> { IPAddressTextBox, SubnetMaskTextBox, GatewayTextBox, PreferredDNSTextBox, AlternateDNSTextBox };
                int index = textBoxes.IndexOf(activeTextBox);
                if (index < textBoxes.Count - 1)
                {
                    textBoxes[index + 1].Focus();
                }
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