using System.Windows.Controls;

namespace SharedServices.Services
{
    public interface IDateTimeService
    {
        void StartDateTimeUpdate(TextBlock dateTextBlock, TextBlock timeTextBlock);
        void StopDateTimeUpdate();
    }
}