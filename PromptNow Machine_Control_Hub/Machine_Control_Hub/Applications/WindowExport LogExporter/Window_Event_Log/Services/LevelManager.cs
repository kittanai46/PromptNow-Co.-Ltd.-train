using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;

namespace Window_Event_Log.Services
{
    public class LevelManager
    {
        private readonly List<string> selectedLevels;
        private readonly string defaultPath = "PLEASE SELECT YOUR PATH";

        public LevelManager()
        {
            selectedLevels = new List<string>();
        }

        public void UpdateLevelSelection(string level, bool isSelected)
        {
            if (isSelected && !selectedLevels.Contains(level))
            {
                selectedLevels.Add(level);
            }
            else if (!isSelected && selectedLevels.Contains(level))
            {
                selectedLevels.Remove(level);
            }
        }

        public void SaveLevels()
        {
            if (selectedLevels.Count == 0)
            {
                throw new Exception("No levels selected to save.");
            }
        }

        public List<string> GetSelectedLevels()
        {
            return new List<string>(selectedLevels);
        }

        public string GetDefaultPath()
        {
            return $"EXPORT PATH: {defaultPath}";
        }

        public string BrowsePath(string currentPath)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.Description = "Select the folder to save the logs:";
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return $"Export Path: {dialog.SelectedPath}";
                }
            }
            return currentPath;
        }

        public string ValidatePath(string pathText)
        {
            if (string.IsNullOrWhiteSpace(pathText))
            {
                return null;
            }

            string outputPath = pathText.Replace("Export Path: ", "").Trim();
            if (string.IsNullOrWhiteSpace(outputPath) || !Directory.Exists(outputPath))
            {
                return null;
            }

            return outputPath;
        }

        public void ExportLog(string logType, string outputPath, TextBlock statusTextBlock)
        {
            try
            {
                if (selectedLevels.Count == 0)
                {
                    UpdateStatus(statusTextBlock, "ERROR", "No levels selected");
                    return;
                }

                if (!EventLog.Exists(logType))
                {
                    UpdateStatus(statusTextBlock, "ERROR", $"Log type '{logType}' does not exist");
                    return;
                }

                string fileName = $"{logType}_ExportedLog_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                string filePath = Path.Combine(outputPath, fileName);

                using (var writer = new StreamWriter(filePath))
                {
                    writer.WriteLine("Timestamp,Level,Source,Message"); // Header
                    EventLog eventLog = new EventLog(logType);

                    foreach (EventLogEntry entry in eventLog.Entries)
                    {
                        if (selectedLevels.Contains(entry.EntryType.ToString()))
                        {
                            string logMessage = entry.Message.Replace(",", ";"); // Handle commas in message
                            writer.WriteLine($"{entry.TimeGenerated.ToString("o", CultureInfo.InvariantCulture)},{entry.EntryType},{entry.Source},{logMessage}");
                        }
                    }
                }

                UpdateStatus(statusTextBlock, "SUCCESS", filePath);
            }
            catch (Exception ex)
            {
                UpdateStatus(statusTextBlock, "ERROR", ex.Message);
            }
        }

        public void UpdateStatus(TextBlock statusTextBlock, string status, string additionalInfo = "")
        {
            switch (status.ToUpper())
            {
                case "SUCCESS":
                    statusTextBlock.Text = $"SUCCESS: {additionalInfo}";
                    statusTextBlock.Foreground = new SolidColorBrush(Colors.Green);
                    break;

                case "ERROR":
                    statusTextBlock.Text = $"FAILED: {additionalInfo}";
                    statusTextBlock.Foreground = new SolidColorBrush(Colors.Red);
                    break;

                case "PENDING":
                    statusTextBlock.Text = "IN PROGRESS...";
                    statusTextBlock.Foreground = new SolidColorBrush(Color.FromRgb(255, 136, 0));
                    break;

                default:
                    statusTextBlock.Text = status.ToUpper();
                    statusTextBlock.Foreground = new SolidColorBrush(Colors.White);
                    break;
            }
        }

        public void ToggleEventSelection(string level, Border border)
        {
            bool isSelected = selectedLevels.Contains(level);

            if (isSelected)
            {
                selectedLevels.Remove(level);
                border.BorderBrush = new SolidColorBrush(Colors.Transparent);
            }
            else
            {
                selectedLevels.Add(level);
                border.BorderBrush = new SolidColorBrush(Colors.White);
            }
        }
    }
}