using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace MedExam.Common
{
    public class ItemsNotification<T> : Notification
    {
        public ItemsNotification(T[] items)
        {
            Items = items;
        }

        public T[] Items { get; private set; }
    }
}