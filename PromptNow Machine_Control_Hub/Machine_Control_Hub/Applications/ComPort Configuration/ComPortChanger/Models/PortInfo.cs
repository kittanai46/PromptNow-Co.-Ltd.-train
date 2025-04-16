using System;
using System.Collections.Generic;

namespace ComPortChanger.Models
{
    public class PortInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DeviceID { get; set; }
        public string OriginalName { get; set; }
        public int CurrentComNumber { get; set; }
        public int SelectedComNumber { get; set; }
        public List<int> AvailableComNumbers { get; set; }
    }
}