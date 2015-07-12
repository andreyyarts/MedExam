using System;
using System.Windows;
using System.Windows.Interactivity;
using MedExam.Common.Windows;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace MedExam.Common.Interactivity
{
    /// <summary>
    ///     Shows a popup window in response to an <see cref="InteractionRequest{T}" /> being raised.
    /// </summary>
    public class PopupWindowAction : TriggerAction<FrameworkElement>
    {
        /// <summary>
        ///     The content of the child window to display as part of the popup.
        /// </summary>
        public static readonly DependencyProperty WindowContentProperty =
            DependencyProperty.Register(
                "WindowContent",
                typeof(FrameworkElement),
                typeof(PopupWindowAction),
                new PropertyMetadata(null));

        /// <summary>
        ///     Determines if the content should be shown in a modal window or not.
        /// </summary>
        public static readonly DependencyProperty IsModalProperty =
            DependencyProperty.Register(
                "IsModal",
                typeof(bool),
                typeof(PopupWindowAction),
                new PropertyMetadata(null));

        /// <summary>
        ///     Determines if the content should be initially shown centered over the view that raised the interaction request or
        ///     not.
        /// </summary>
        public static readonly DependencyProperty CenterOverAssociatedObjectProperty =
            DependencyProperty.Register(
                "CenterOverAssociatedObject",
                typeof(bool),
                typeof(PopupWindowAction),
                new PropertyMetadata(null));

        /// <summary>
        ///     Gets or sets the content of the window.
        /// </summary>
        public FrameworkElement WindowContent
        {
            get { return (FrameworkElement)GetValue(WindowContentProperty); }
            set { SetValue(WindowContentProperty, value); }
        }

        /// <summary>
        ///     Gets or sets if the window will be modal or not.
        /// </summary>
        public bool IsModal
        {
            get { return (bool)GetValue(IsModalProperty); }
            set { SetValue(IsModalProperty, value); }
        }

        /// <summary>
        ///     Gets or sets if the window will be initially shown centered over the view that raised the interaction request or
        ///     not.
        /// </summary>
        public bool CenterOverAssociatedObject
        {
            get { return (bool)GetValue(CenterOverAssociatedObjectProperty); }
            set { SetValue(CenterOverAssociatedObjectProperty, value); }
        }

        /// <summary>
        ///     Displays the child window and collects results for <see cref="IInteractionRequest" />.
        /// </summary>
        /// <param name="parameter">
        ///     The parameter to the action. If the action does not require a parameter, the parameter may be
        ///     set to a null reference.
        /// </param>
        protected override void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            if (args == null)
                return;

            if (WindowContent != null && WindowContent.Parent != null)
                return;

            var wrapperWindow = GetWindow(args.Context);
            wrapperWindow.SizeToContent = SizeToContent.WidthAndHeight;

            var callback = args.Callback;
            EventHandler handler = null;
            handler =
                (o, e) =>
                {
                    wrapperWindow.Closed -= handler;
                    wrapperWindow.Content = null;
                    if (callback != null) callback();
                };
            wrapperWindow.Closed += handler;

            if (AssociatedObject != null)
            {
                var viewCurrent = AssociatedObject;
                while ((viewCurrent as Window) == null)
                {
                    viewCurrent = (FrameworkElement)viewCurrent.Parent;
                }
                wrapperWindow.Owner = viewCurrent as Window;

                wrapperWindow.WindowStartupLocation = CenterOverAssociatedObject
                                                      ? WindowStartupLocation.CenterOwner
                                                      : WindowStartupLocation.CenterScreen;
            }

            if (IsModal)
            {
                wrapperWindow.ShowDialog();
            }
            else
            {
                wrapperWindow.Show();
            }
        }

        /// <summary>
        ///     Returns the window to display as part of the trigger action.
        /// </summary>
        /// <param name="notification">The notification to be set as a DataContext in the window.</param>
        /// <returns></returns>
        protected virtual Window GetWindow(Notification notification)
        {
            Window wrapperWindow;

            if (WindowContent != null)
            {
                wrapperWindow = new Window { DataContext = notification, Title = notification.Title };

                // If the WindowContent does not have its own DataContext, it will inherit this one.

                PrepareContentForWindow(notification, wrapperWindow);
            }
            else
            {
                wrapperWindow = CreateDefaultWindow(notification);
            }

            return wrapperWindow;
        }

        /// <summary>
        ///     Checks if the WindowContent or its DataContext implements <see cref="IInteractionRequestAware" />.
        ///     If so, it sets the corresponding value.
        ///     Also, if WindowContent does not have a RegionManager attached, it creates a new scoped RegionManager for it.
        /// </summary>
        /// <param name="notification">The notification to be set as a DataContext in the HostWindow.</param>
        /// <param name="wrapperWindow">The HostWindow</param>
        protected virtual void PrepareContentForWindow(Notification notification, Window wrapperWindow)
        {
            if (WindowContent == null)
                return;

            wrapperWindow.Content = WindowContent;

            var interactionAware = WindowContent as IInteractionRequestAware ??
                                   WindowContent.DataContext as IInteractionRequestAware;

            if (interactionAware != null)
            {
                interactionAware.Notification = notification;
                interactionAware.FinishInteraction = wrapperWindow.Close;
            }
        }

        /// <summary>
        ///     When no WindowContent is sent this method is used to create a default basic window to show
        ///     the corresponding <see cref="Notification" /> or <see cref="Confirmation" />.
        /// </summary>
        /// <param name="notification">The INotification or IConfirmation parameter to show.</param>
        /// <returns></returns>
        protected static Window CreateDefaultWindow(Notification notification)
        {
            Window window;

            if (notification is Confirmation)
            {
                window = new DefaultConfirmationWindow { Confirmation = (Confirmation)notification };
            }
            else
            {
                window = new DefaultNotificationWindow { Notification = notification };
            }

            return window;
        }
    }
}