using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BounceBall.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            // Retrieve the active view (UserControl) and forward KeyDown
            if (GetActiveView() is IKeyHandler keyHandler)
            {
                keyHandler.OnKeyDown(e);
            }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            // Retrieve the active view (UserControl) and forward KeyUp
            if (GetActiveView() is IKeyHandler keyHandler)
            {
                keyHandler.OnKeyUp(e);
            }
        }

        private object GetActiveView()
        {
            // Find the ContentPresenter within the ContentControl
            var contentPresenter = FindVisualChild<ContentPresenter>(ActiveItem);

            if (contentPresenter != null)
            {
                // Retrieve the view (UserControl) displayed by the ContentPresenter
                return VisualTreeHelper.GetChild(contentPresenter, 0);
            }

            return null;
        }

        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            // Recursively search for a child of type T in the visual tree
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T tChild)
                {
                    return tChild;
                }

                var result = FindVisualChild<T>(child);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
