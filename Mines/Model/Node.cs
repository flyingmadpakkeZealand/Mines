using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mines.ViewModel;

namespace Mines.Model
{
    public class Node
    {
        public bool IsRevealed { get; set; }
        public int Signal { get; set; }
        protected int _xPosition, _yPosition;

        public Node(int xPosition, int yPosition, int signal)
        {
            _xPosition = xPosition;
            _yPosition = yPosition;
            IsRevealed = false;
            Signal = signal;
        }

        public virtual void Reveal()
        {
            IsRevealed = true;
            MinesVM.UpdateButton(_xPosition,_yPosition);
        }
    }
}
