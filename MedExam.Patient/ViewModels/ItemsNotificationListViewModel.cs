using MedExam.Common;

namespace MedExam.Patient.ViewModels
{
    public class ItemsNotificationListViewModel : ItemsNotification<long>
    {
        public ItemsNotificationListViewModel(long[] items) : base(items)
        {
        }
    }
}