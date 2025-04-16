using System.Windows.Controls;
using ComPortChanger.Models;

namespace ComPortChanger.Services
{
    public interface IPortUIService
    {
        void CreatePortBox(PortInfo port, StackPanel container);
        void SetFrameBackground(Grid frameContainer);
    }
}