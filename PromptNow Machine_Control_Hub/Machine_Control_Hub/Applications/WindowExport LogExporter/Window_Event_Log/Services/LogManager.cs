using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LogManager
{
    public class LogManager
    {
        // รายชื่อ Windows Logs ที่ต้องการ
        private readonly List<string> _windowsLogs = new List<string>
        {
            "Application",
            "Security",
            "System",
            "Setup",
            "Forwarded Events"
        };

        /// <summary>
        /// ดึงรายชื่อ Windows Logs ทั้งหมด
        /// </summary>
        /// <returns>รายชื่อ Windows Logs</returns>
        public List<string> GetWindowsLogs()
        {
            try
            {
                // ดึง Event Logs ทั้งหมดจากระบบ
                var eventLogs = EventLog.GetEventLogs();

                // กรองเฉพาะ Logs ที่อยู่ในรายการที่กำหนด
                var existingLogs = eventLogs
                    .Where(log => _windowsLogs.Contains(log.Log))
                    .Select(log => log.LogDisplayName)
                    .ToList();

                // เพิ่มชื่อ Logs ที่ไม่มีในระบบ
                foreach (var log in _windowsLogs)
                {
                    if (!existingLogs.Contains(log))
                    {
                        existingLogs.Add(log); // เพิ่ม Log ที่ขาดหาย
                    }
                }

                return existingLogs;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching Windows logs: {ex.Message}");
            }
        }

        /// <summary>
        /// ดึงรายการ Event Logs สำหรับ Log ที่เลือก
        /// </summary>
        /// <param name="logName">ชื่อ Log</param>
        /// <returns>รายการ Event Logs</returns>
        public List<string> GetLogDetails(string logName)
        {
            try
            {
                var logDetails = new List<string>();

                // ตรวจสอบว่า Log มีอยู่ในระบบหรือไม่
                if (!_windowsLogs.Contains(logName))
                {
                    throw new Exception($"Log '{logName}' is not a valid Windows Log.");
                }

                // ดึงข้อมูลจาก EventLog
                using (var eventLog = new EventLog(logName))
                {
                    logDetails = eventLog.Entries.Cast<EventLogEntry>()
                        .OrderByDescending(entry => entry.TimeGenerated) // เรียงลำดับจากล่าสุด
                        .Take(100) // จำกัดจำนวนบันทึก (100 รายการล่าสุด)
                        .Select(entry =>
                            $"{entry.TimeGenerated} [{entry.EntryType}] {entry.Source}: {entry.Message}")
                        .ToList();
                }

                return logDetails;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching details for log '{logName}': {ex.Message}");
            }
        }
    }
}
