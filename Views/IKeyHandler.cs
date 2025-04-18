using System.Windows.Input;

namespace BounceBall.Views
{
    public interface IKeyHandler
    {
        void OnKeyDown(KeyEventArgs e);
        void OnKeyUp(KeyEventArgs e);
    }
}

