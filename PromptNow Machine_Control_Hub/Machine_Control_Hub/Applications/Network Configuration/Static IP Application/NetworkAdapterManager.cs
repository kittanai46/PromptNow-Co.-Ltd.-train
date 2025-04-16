using System;
using System.Collections.Generic;
using System.Management;
using System.Security.Principal;

namespace PromptNow.NetworkAdapter
{
    public class NetworkAdapterManager
    {
        private static readonly NetworkAdapterManager instance = new NetworkAdapterManager();

        private NetworkAdapterManager() { }

        // ฟังก์ชันสำหรับเรียก Singleton Instance
        public static NetworkAdapterManager GetInstance()
        {
            return instance;
        }

        #region ฟังก์ชันสำหรับดึงรายชื่อ Network Adapters
        public bool GetNetworkAdapters(out List<string> adapterNames, out string error)
        {
            adapterNames = new List<string>();
            error = null;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                    "SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionID IS NOT NULL");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    string connectionID = queryObj["NetConnectionID"]?.ToString();
                    if (!string.IsNullOrEmpty(connectionID))
                    {
                        adapterNames.Add(connectionID);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        #endregion

        #region ฟังก์ชันดึงรายละเอียด IP ของ Adapter
        public bool GetAdapterProfile(string connectionID, out string ipAddress, out string subnetMask,
                                      out string gateway, out string preferredDNS, out string alternateDNS, out string error)
        {
            return GetAdapterProfileWithDescription(connectionID, out ipAddress, out subnetMask, out gateway,
                                                    out preferredDNS, out alternateDNS, out _, out error);
        }
        #endregion

        #region ฟังก์ชันดึงรายละเอียด IP พร้อมคำอธิบาย (Description)
        public bool GetAdapterProfileWithDescription(string connectionID, out string ipAddress, out string subnetMask,
                                                     out string gateway, out string preferredDNS, out string alternateDNS,
                                                     out string description, out string error)
        {
            error = null;
            ipAddress = subnetMask = gateway = preferredDNS = alternateDNS = description = "";

            try
            {
                // ดึง Index และ Description ของ Adapter
                if (!GetAdapterIndex(connectionID, out string adapterIndex, out description))
                {
                    error = "Adapter not found.";
                    return false;
                }

                // ดึงข้อมูล IP Configuration
                ManagementObjectSearcher configSearcher = new ManagementObjectSearcher(
                    $"SELECT * FROM Win32_NetworkAdapterConfiguration WHERE Index = {adapterIndex} AND IPEnabled = true");

                foreach (ManagementObject queryObj in configSearcher.Get())
                {
                    ipAddress = queryObj["IPAddress"] is string[] ip && ip.Length > 0 ? ip[0] : "";
                    subnetMask = queryObj["IPSubnet"] is string[] subnet && subnet.Length > 0 ? subnet[0] : "";
                    gateway = queryObj["DefaultIPGateway"] is string[] gw && gw.Length > 0 ? gw[0] : "";
                    preferredDNS = queryObj["DNSServerSearchOrder"] is string[] dns && dns.Length > 0 ? dns[0] : "";
                    alternateDNS = queryObj["DNSServerSearchOrder"] is string[] dnsAlt && dnsAlt.Length > 1 ? dnsAlt[1] : "";

                    return true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            return false;
        }
        #endregion

        #region ฟังก์ชันการตั้งค่า Static IP
        public bool SetIPAddress(string connectionID, string ipAddress, string subnetMask, string gateway, out string error)
        {
            error = null;

            try
            {
                // ดึง Index ของ Adapter
                if (!GetAdapterIndex(connectionID, out string adapterIndex, out _))
                {
                    error = "Adapter not found";
                    return false;
                }

                // ค้นหา adapter configuration ด้วย Index
                var searcher = new ManagementObjectSearcher(
                    $"SELECT * FROM Win32_NetworkAdapterConfiguration WHERE Index = {adapterIndex}");

                foreach (ManagementObject obj in searcher.Get())
                {
                    try
                    {
                        // ตั้งค่า IP Address และ Subnet Mask
                        var newIP = obj.GetMethodParameters("EnableStatic");
                        newIP["IPAddress"] = new string[] { ipAddress };
                        newIP["SubnetMask"] = new string[] { subnetMask };
                        obj.InvokeMethod("EnableStatic", newIP, null);

                        // ตั้งค่า Gateway ถ้ามีการระบุ
                        if (!string.IsNullOrEmpty(gateway))
                        {
                            var newGateway = obj.GetMethodParameters("SetGateways");
                            newGateway["DefaultIPGateway"] = new string[] { gateway };
                            newGateway["GatewayCostMetric"] = new int[] { 1 };
                            obj.InvokeMethod("SetGateways", newGateway, null);
                        }

                        return true;
                    }
                    catch (Exception methodEx)
                    {
                        error = $"Error applying network settings: {methodEx.Message}";
                        return false;
                    }
                }

                error = "Network adapter configuration not found";
                return false;
            }
            catch (Exception ex)
            {
                error = $"System error: {ex.Message}";
                return false;
            }
        }

        public bool SetDNS(string connectionID, string preferredDNS, string alternateDNS, out string error)
        {
            error = null;

            try
            {
                // ดึง Index ของ Adapter
                if (!GetAdapterIndex(connectionID, out string adapterIndex, out _))
                {
                    error = "Adapter not found";
                    return false;
                }

                // ค้นหา adapter configuration ด้วย Index
                var searcher = new ManagementObjectSearcher(
                    $"SELECT * FROM Win32_NetworkAdapterConfiguration WHERE Index = {adapterIndex}");

                foreach (ManagementObject obj in searcher.Get())
                {
                    try
                    {
                        // สร้าง array สำหรับเก็บค่า DNS servers
                        List<string> dnsServers = new List<string>();
                        if (!string.IsNullOrWhiteSpace(preferredDNS))
                        {
                            dnsServers.Add(preferredDNS);
                        }
                        if (!string.IsNullOrWhiteSpace(alternateDNS))
                        {
                            dnsServers.Add(alternateDNS);
                        }

                        // ถ้าไม่มี DNS servers ให้เคลียร์ค่า DNS
                        if (dnsServers.Count == 0)
                        {
                            obj.InvokeMethod("SetDNSServerSearchOrder", new object[] { null });
                            return true;
                        }

                        // ตั้งค่า DNS servers
                        var newDNS = obj.GetMethodParameters("SetDNSServerSearchOrder");
                        newDNS["DNSServerSearchOrder"] = dnsServers.ToArray();
                        obj.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);

                        return true;
                    }
                    catch (Exception methodEx)
                    {
                        error = $"Error setting DNS: {methodEx.Message}";
                        return false;
                    }
                }

                error = "Network adapter configuration not found";
                return false;
            }
            catch (Exception ex)
            {
                error = $"System error: {ex.Message}";
                return false;
            }
        }
        #endregion

        // เพิ่มเมธอดตรวจสอบ Admin privileges
        private bool IsRunningAsAdmin()
        {
            try
            {
                using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
                {
                    WindowsPrincipal principal = new WindowsPrincipal(identity);
                    return principal.IsInRole(WindowsBuiltInRole.Administrator);
                }
            }
            catch
            {
                return false;
            }
        }


        public bool GetNetworkAdaptersWithDescriptions(out List<string> adapterDetails, out string error)
        {
            adapterDetails = new List<string>();
            error = null;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                    "SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionID IS NOT NULL");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    string connectionID = queryObj["NetConnectionID"]?.ToString();
                    string description = queryObj["Description"]?.ToString();

                    if (!string.IsNullOrEmpty(connectionID) && !string.IsNullOrEmpty(description))
                    {
                        adapterDetails.Add($"{connectionID} : {description}");
                    }
                }

                if (adapterDetails.Count == 0)
                {
                    error = "No active network adapters found.";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                error = $"Error fetching network adapters: {ex.Message}";
                return false;
            }
        }


        #region ฟังก์ชันเปิดใช้งาน DHCP
        public bool SetDHCP(string connectionID, out string error)
        {
            error = null;
            try
            {
                if (!IsRunningAsAdmin())
                {
                    error = "Requires administrator privileges";
                    return false;
                }

                // ดึง Index ของ Adapter
                if (!GetAdapterIndex(connectionID, out string adapterIndex, out _))
                {
                    error = "Adapter not found";
                    return false;
                }

                var searcher = new ManagementObjectSearcher(
                    $"SELECT * FROM Win32_NetworkAdapterConfiguration WHERE Index = {adapterIndex}");

                foreach (ManagementObject obj in searcher.Get())
                {
                    try
                    {
                        // ลบ Routes โดยใช้ WMI
                        var routeSearcher = new ManagementObjectSearcher(
                            $"SELECT * FROM Win32_IP4RouteTable WHERE InterfaceIndex = {adapterIndex}");

                        foreach (ManagementObject route in routeSearcher.Get())
                        {
                            route.Delete(); // ลบเส้นทาง Route
                        }

                        // เปิด DHCP
                        obj.InvokeMethod("EnableDHCP", null);

                        // ตั้งค่า DNS เป็นอัตโนมัติ
                        obj.InvokeMethod("SetDNSServerSearchOrder", new object[] { null });

                        return true;
                    }
                    catch (Exception methodEx)
                    {
                        error = $"Error enabling DHCP: {methodEx.Message}";
                        return false;
                    }
                }

                error = "Network adapter configuration not found";
                return false;
            }
            catch (Exception ex)
            {
                error = $"System error: {ex.Message}";
                return false;
            }
        }
        #endregion

        #region ฟังก์ชันช่วยค้นหา Adapter Index
        private bool GetAdapterIndex(string connectionID, out string adapterIndex, out string description)
        {
            adapterIndex = null;
            description = null;

            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(
                    $"SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionID = '{connectionID}'");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    adapterIndex = queryObj["Index"]?.ToString();
                    description = queryObj["Description"]?.ToString();
                    return true;
                }

                // หากไม่พบ Adapter
                description = $"Adapter with connection ID '{connectionID}' not found.";
                return false;
            }
            catch (Exception ex)
            {
                description = ex.Message; // เก็บ Error Message ไว้ใน description
                return false;
            }
        }
        #endregion

        public string GetIPAddress(string connectionID)
        {
            GetAdapterProfile(connectionID, out string ipAddress, out _, out _, out _, out _, out _);
            return ipAddress;
        }

        public string GetSubnetMask(string connectionID)
        {
            GetAdapterProfile(connectionID, out _, out string subnetMask, out _, out _, out _, out _);
            return subnetMask;
        }

        public string GetGateway(string connectionID)
        {
            GetAdapterProfile(connectionID, out _, out _, out string gateway, out _, out _, out _);
            return gateway;
        }

        public string GetPreferredDNS(string connectionID)
        {
            GetAdapterProfile(connectionID, out _, out _, out _, out string preferredDNS, out _, out _);
            return preferredDNS;
        }

        public string GetAlternateDNS(string connectionID)
        {
            GetAdapterProfile(connectionID, out _, out _, out _, out _, out string alternateDNS, out _);
            return alternateDNS;
        }





    }

}
