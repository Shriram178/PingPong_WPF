using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace BounceBall.Models
{
    public class BallModel : INotifyPropertyChanged
    {
        private double _x;
        private double _y;
        private double _speedX;
        private double _speedY;
        private double _size;
        private Brush _color;

        public double X { get => _x; set { _x = value; OnPropertyChanged(); } }
        public double Y { get => _y; set { _y = value; OnPropertyChanged(); } }
        public double SpeedX { get => _speedX; set { _speedX = value; OnPropertyChanged(); } }
        public double SpeedY { get => _speedY; set { _speedY = value; OnPropertyChanged(); } }
        public double Size { get => _size; set { _size = value; OnPropertyChanged(); } }
        public Brush Color { get => _color; set { _color = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}
