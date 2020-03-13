using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Mines.Annotations;

namespace Mines.ViewModel
{
    public class SetupPageVM : INotifyPropertyChanged
    {
        public static int xDimension;
        public int XDimension
        {
            get { return xDimension;}
            set
            {
                xDimension = value;
                OnPropertyChanged(nameof(MaxBombs));
            }
        }

        public static int yDimension;
        public int YDimension
        {
            get { return yDimension; }
            set
            {
                yDimension = value;
                OnPropertyChanged(nameof(MaxBombs));
            }
        }

        public static int Bombs { get; set; }

        public int MaxBombs
        {
            get { return XDimension * YDimension; }
        }

        public SetupPageVM()
        {
            XDimension = 10;
            YDimension = 10;
            Bombs = 20;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
