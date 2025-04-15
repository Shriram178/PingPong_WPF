using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BounceBall.Models
{
    public class PaddleModel : INotifyPropertyChanged
    {
        private double _x;
        private double _width;

        public double X { get => _x; set { _x = value; OnPropertyChanged(); } }
        public double Width { get => _width; set { _width = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}
