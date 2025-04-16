using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using ComPortChanger.Models;

namespace ComPortChanger.Services
{
    public class ComPortService : IComPortService
    {
        public List<PortInfo> GetAllPorts()
        {
            var ports = new List<PortInfo>();

            // Add real ports from system
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%(COM%)'"))
            {
                foreach (ManagementObject port in searcher.Get())
                {
                    string name = port["Name"].ToString();
                    int currentComNumber = ExtractComNumber(name);

                    var portInfo = new PortInfo
                    {
                        Name = name,
                        Description = port["Description"]?.ToString() ?? "",
                        DeviceID = port["DeviceID"].ToString(),
                        OriginalName = name,
                        CurrentComNumber = currentComNumber,
                        AvailableComNumbers = Enumerable.Range(1, 30).ToList(),
                        SelectedComNumber = currentComNumber
                    };

                    ports.Add(portInfo);
                }
            }
            return ports;
        }



        private int ExtractComNumber(string portName)
        {
            var comIndex = portName.IndexOf("(COM");
            if (comIndex >= 0)
            {
                var numStr = portName.Substring(comIndex + 4).TrimEnd(')');
                if (int.TryParse(numStr, out int number))
                {
                    return number;
                }
            }
            return 1;
        }

        public void ChangeComPortNumber(PortInfo port)
        {
            UpdateDeviceParameters(port);
            UpdateFriendlyName(port);
        }

        private void UpdateDeviceParameters(PortInfo port)
        {
            string deviceParamsKeyPath = $@"SYSTEM\CurrentControlSet\Enum\{port.DeviceID}\Device Parameters";
            using (var deviceParamsKey = Registry.LocalMachine.OpenSubKey(deviceParamsKeyPath, true))
            {
                if (deviceParamsKey == null)
                    throw new Exception($"Registry key not found: {deviceParamsKeyPath}");

                deviceParamsKey.SetValue("PortName", $"COM{port.SelectedComNumber}", RegistryValueKind.String);
            }
        }

        private void UpdateFriendlyName(PortInfo port)
        {
            string friendlyNameKeyPath = $@"SYSTEM\CurrentControlSet\Enum\{port.DeviceID}";
            using (var friendlyNameKey = Registry.LocalMachine.OpenSubKey(friendlyNameKeyPath, true))
            {
                if (friendlyNameKey == null)
                    throw new Exception($"Registry key not found: {friendlyNameKeyPath}");

                string currentFriendlyName = friendlyNameKey.GetValue("FriendlyName")?.ToString() ?? port.OriginalName;
                string newFriendlyName = Regex.Replace(
                    currentFriendlyName,
                    @"\(COM\d+\)",
                    $"(COM{port.SelectedComNumber})"
                );
                friendlyNameKey.SetValue("FriendlyName", newFriendlyName, RegistryValueKind.String);
            }
        }
    }
}