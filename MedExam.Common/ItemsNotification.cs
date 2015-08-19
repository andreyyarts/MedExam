using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace MedExam.Common
{
    public class ItemsNotification<T> : Notification
    {
        protected ItemsNotification(T[] items)
        {
            Items = items;
        }

        private T[] Items { get; set; }
    }
}