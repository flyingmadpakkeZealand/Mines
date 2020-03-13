using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mines.ViewModel;

namespace Mines.Model
{
    public class ZeroNode : Node
    {

        protected bool _xLeftExist, _xRightExist, _yBelowExist, _yAboveExist;

        public ZeroNode(int xPosition, int yPosition) : base(xPosition, yPosition, 0)
        {
            InitializeBools();
        }

        protected ZeroNode(int xPosition, int yPosition, int signal) : base(xPosition, yPosition, signal)
        {
            InitializeBools();
        }

        public override void Reveal()
        {
            IsRevealed = true;
            //for (int i = -1 + (_xLeftExist ? 0 : 1); i <= 1 + (_xRightExist ? 0 : -1); i++)
            //{
            //    for (int j = -1 + (_yBelowExist ? 0 : 1); j <= 1 + (_yAboveExist ? 0 : -1); j++)
            //    {
            //        if (!MinesVM.gameMap[_xPosition + i, _yPosition + j].IsRevealed)
            //        {
            //            MinesVM.gameMap[_xPosition + i, _yPosition + j].Reveal();
            //        }
            //    }
            //}

            VisitNeighbours(DoReveal);
            MinesVM.UpdateButton(_xPosition,_yPosition);
        }

        private void DoReveal(int[] index)
        {
            if (!MinesVM.gameMap[_xPosition + index[0], _yPosition + index[1]].IsRevealed)
            {
                MinesVM.gameMap[_xPosition + index[0], _yPosition + index[1]].Reveal();
            }
        }

        private void InitializeBools()
        {
            _xLeftExist = _xPosition > 0;
            _xRightExist = _xPosition < MinesVM.xDimension - 1;
            _yBelowExist = _yPosition > 0;
            _yAboveExist = _yPosition < MinesVM.yDimension - 1;
        }

        protected void VisitNeighbours(Action<int[]> action)
        {
            int[] ints = new int[2];
            for (int i = -1 + (_xLeftExist ? 0 : 1); i <= 1 + (_xRightExist ? 0 : -1); i++)
            {
                for (int j = -1 + (_yBelowExist ? 0 : 1); j <= 1 + (_yAboveExist ? 0 : -1); j++)
                {
                    ints[0] = i;
                    ints[1] = j;
                    action(ints);
                }
            }
        }
    }
}
