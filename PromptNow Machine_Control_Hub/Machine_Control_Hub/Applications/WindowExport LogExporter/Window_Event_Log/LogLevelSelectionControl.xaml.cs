using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using SharedServices.Services;
using Window_Event_Log.Services;

namespace Window_Event_Log
{
    public partial class LogLevelSelectionControl : UserControl
    {
        private readonly LevelManager levelManager;
        private readonly string logType;
        private readonly IDateTimeService _dateTimeService;

        public LogLevelSelectionControl(string logType)
        {
            InitializeComponent();
            this.logType = logType;
            levelManager = new LevelManager();
            _dateTimeService = new DateTimeService();

            // Display the selected log type
            LogTypeText.Text = $"Selected Log: {logType}";

            // Set the default path
            PathTextBlock.Text = levelManager.GetDefaultPath();

            // Set the current date and time
            SetCurrentDate();

            // Set the initial status to PENDING
            levelManager.UpdateStatus(StatusTextBlock, "PENDING");
        }

        private void SetCurrentDate()
        {
            try
            {
                _dateTimeService.StartDateTimeUpdate(DateText, TimeText);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting date and time: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ToggleEventLevel(string level, Border border)
        {
            levelManager.ToggleEventSelection(level, border);
            levelManager.UpdateStatus(StatusTextBlock, "PENDING");
        }

        private void CriticalCheckBox_Click(object sender, RoutedEventArgs e)
        {
            ToggleEventLevel("Critical", CriticalBorder);
        }

        private void ErrorCheckBox_Click(object sender, RoutedEventArgs e)
        {
            ToggleEventLevel("Error", ErrorBorder);
        }

        private void WarningCheckBox_Click(object sender, RoutedEventArgs e)
        {
            ToggleEventLevel("Warning", WarningBorder);
        }

        private void InformationCheckBox_Click(object sender, RoutedEventArgs e)
        {
            ToggleEventLevel("Information", InformationBorder);
        }

        private void VerboseCheckBox_Click(object sender, RoutedEventArgs e)
        {
            ToggleEventLevel("Verbose", VerboseBorder);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            levelManager.UpdateStatus(StatusTextBlock, "PENDING");

            try
            {
                levelManager.SaveLevels();
                levelManager.UpdateStatus(StatusTextBlock, "SUCCESS", "Levels Saved Successfully");
            }
            catch (Exception ex)
            {
                levelManager.UpdateStatus(StatusTextBlock, "ERROR", ex.Message);
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            string outputPath = levelManager.ValidatePath(PathTextBlock.Text);
            if (outputPath != null)
            {
                levelManager.ExportLog(logType, outputPath, StatusTextBlock);
            }
            else
            {
                levelManager.UpdateStatus(StatusTextBlock, "ERROR", "Path not set");
            }
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            PathTextBlock.Text = levelManager.BrowsePath(PathTextBlock.Text);
            levelManager.UpdateStatus(StatusTextBlock, "PENDING");
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