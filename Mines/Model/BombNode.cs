using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mines.Exceptions;
using Mines.ViewModel;

namespace Mines.Model
{
    public class BombNode : ZeroNode
    {
        public BombNode(int xPosition, int yPosition) : base(xPosition, yPosition, 9)
        {
            VisitNeighbours(CreateNode);
        }

        public override void Reveal()
        {
            throw new LostGameException("You clicked on a bomb and lost the game ;(");
        }

        private void CreateNode(int[] index)
        {
            if (MinesVM.gameMap[_xPosition + index[0], _yPosition + index[1]] == null)
            {
                MinesVM.gameMap[_xPosition + index[0], _yPosition + index[1]] = new Node(_xPosition + index[0], _yPosition + index[1], 1);
            }
            else
            {
                MinesVM.gameMap[_xPosition + index[0], _yPosition + index[1]].Signal += 1;
            }
        }
    }
}
