using System.Collections.Generic;
using ComPortChanger.Models;

namespace ComPortChanger.Services
{
    public interface IComPortService
    {
        List<PortInfo> GetAllPorts();
        void ChangeComPortNumber(PortInfo port);
    }
}