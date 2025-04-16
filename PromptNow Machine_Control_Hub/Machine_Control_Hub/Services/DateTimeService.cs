using System;
using System.Windows.Controls;
using System.Windows.Threading;

namespace WpfStaticIPApp.Services
{
    public class DateTimeService
    {
        private DispatcherTimer timer;
        private DateTime lastDate;

        // ฟังก์ชันสำหรับเริ่มต้นการแสดงเวลา
        public void StartDateTimeUpdate(TextBlock dateTextBlock, TextBlock timeTextBlock)
        {
            if (timer != null) return; // ป้องกันการสร้าง Timer ซ้ำ

            // ฟอร์แมตวันที่
            lastDate = DateTime.Now;
            dateTextBlock.Text = lastDate.ToString("dd MMM yyyy", new System.Globalization.CultureInfo("en-US"));

            // ฟอร์แมตเวลา
            timeTextBlock.Text = lastDate.ToString("HH:mm:ss");

            // ตั้ง Timer สำหรับอัปเดตเวลา
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) // อัปเดตทุก 1 วินาที
            };

            timer.Tick += (s, e) =>
            {
                DateTime now = DateTime.Now;
                timeTextBlock.Text = now.ToString("HH:mm:ss");

                // ตรวจสอบและอัปเดตวันที่หากเปลี่ยนวัน
                if (now.Date != lastDate.Date)
                {
                    dateTextBlock.Text = now.ToString("dd MMM yyyy", new System.Globalization.CultureInfo("en-US"));
                    lastDate = now;
                }
            };

            timer.Start();
        }

        // ฟังก์ชันหยุดการอัปเดตเวลา
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
