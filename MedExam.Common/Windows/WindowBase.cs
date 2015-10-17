using System;
using System.Windows;
using System.Windows.Controls;
using MedExam.Common.Local;

namespace MedExam.Common.Windows
{
    public class WindowBase : Window
    {
        protected WindowBase()
        {
            Initialized += WindowInitialized;
            Closed += WindowClosed;
        }

        public static void WindowInitialized(object sender, EventArgs eventArgs)
        {
            var window = sender as Window;
            if (window == null)
                return;

            window.Name = GetWindowName(window);
            if (!CacheViewsRepository.Exists(window.Name))
            {
                SetLayoutDefault(window);
                return;
            }

            var settings = CacheViewsRepository.Load(window.Name);

            window.Top = settings.Top;
            window.Left = settings.Left;
            window.Width = settings.Width;
            window.Height = settings.Height;
            window.WindowState = settings.State;
        }

        private static void SetLayoutDefault(Window window)
        {
            window.SizeToContent = window.Owner != null
                                    ? SizeToContent.WidthAndHeight
                                    : SizeToContent.Manual;

            window.WindowStartupLocation = window.Owner != null
                ? WindowStartupLocation.CenterOwner
                : WindowStartupLocation.CenterScreen;
        }

        private static string GetWindowName(ContentControl window)
        {
            return window.GetType().Name + window.Content.GetType().Name;
        }

        public static void WindowClosed(object sender, EventArgs e)
        {
            var window = sender as Window;
            if (window == null)
                return;

            var settings = new ViewSetting
            {
                Top = window.Top,
                Left = window.Left,
                Width = window.Width,
                Height = window.Height,
                State = window.WindowState
            };

            CacheViewsRepository.Save(settings, window.Name);
        }
    }
}
