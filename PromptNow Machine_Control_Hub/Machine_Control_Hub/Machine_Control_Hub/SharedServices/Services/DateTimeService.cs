using System;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Globalization;

namespace SharedServices.Services
{
    public class DateTimeService : IDateTimeService
    {
        private DispatcherTimer timer;
        private DateTime lastDate;

        public void StartDateTimeUpdate(TextBlock dateTextBlock, TextBlock timeTextBlock)
        {
            if (timer != null) return;

            lastDate = DateTime.Now;
            dateTextBlock.Text = lastDate.ToString("dd MMM yyyy", new CultureInfo("en-US"));
            timeTextBlock.Text = lastDate.ToString("HH:mm:ss");

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += (s, e) =>
            {
                DateTime now = DateTime.Now;
                timeTextBlock.Text = now.ToString("HH:mm:ss");

                if (now.Date != lastDate.Date)
                {
                    dateTextBlock.Text = now.ToString("dd MMM yyyy", new CultureInfo("en-US"));
                    lastDate = now;
                }
            };

            timer.Start();
        }

        public void StopDateTimeUpdate()
        {
            if (timer != null)
            {
                timer.Stop();
                timer = null;
            }
        }
    }
}