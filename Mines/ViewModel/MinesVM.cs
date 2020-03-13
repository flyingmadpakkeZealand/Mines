using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Mines.Annotations;
using Mines.Exceptions;
using Mines.Model;
using Mines.View;

namespace Mines.ViewModel
{
    public class MinesVM
    {
        public static Node[,] gameMap;
        private int _tilesToWin;

        public static int xDimension, yDimension;
        private Random rand = new Random(DateTime.Now.Millisecond);
        private int _bombs;

        private ObservableCollection<GameMapVM> _clickableGameMap;
        public ObservableCollection<GameMapVM> ClickableGameMap
        {
            get { return _clickableGameMap; }
        }

        public static int XDimension
        {
            get { return xDimension; }
        }

        public static int YDimension
        {
            get { return yDimension; }
        }

        public MinesVM():this(SetupPageVM.Bombs, SetupPageVM.xDimension, SetupPageVM.yDimension) { }

        public MinesVM(int bombs, int x, int y)
        {
            _bombs = bombs;
            xDimension = x;
            yDimension = y;
            gameMap = new Node[MinesVM.xDimension, MinesVM.yDimension];
            SetupGameMap();
            _clickableGameMap = new ObservableCollection<GameMapVM>();
            for (int i = 0; i < yDimension; i++)
            {
                for (int j = 0; j < xDimension; j++)
                {
                    _clickableGameMap.Add(new GameMapVM(j,i));
                }
            }

            _tilesToWin = xDimension * yDimension - _bombs;

            currentInstance = this;
        }

        private void SetupGameMap()
        {
            Dictionary<int, List<int>> xyCords = new Dictionary<int, List<int>>();

            for (int i = 0; i < _bombs; i++)
            {
                AddCord(xyCords);
            }

            for (int i = 0; i < xDimension; i++)
            {
                for (int j = 0; j < yDimension; j++)
                {
                    if (gameMap[i,j] == null)
                    {
                        gameMap[i,j] = new ZeroNode(i,j);
                    }
                }
            }

            void AddCord(Dictionary<int, List<int>> xyCord)
            {
                int x = rand.Next(0, xDimension);
                int y = rand.Next(0, yDimension);
                if (xyCord.ContainsKey(x))
                {
                    if (xyCord[x].Contains(y))
                    {
                        AddCord(xyCord);
                    }
                    else
                    {
                        xyCord[x].Add(y);
                        gameMap[x,y] = new BombNode(x,y);
                    }
                }
                else
                {
                    xyCord.Add(x, new List<int>() { y });
                    gameMap[x,y] = new BombNode(x,y);
                }
            }
        }

        private static MinesVM currentInstance;
        public static void UpdateButton(int x, int y)
        {
            currentInstance._clickableGameMap[y * xDimension + x].RevealNode = true;
            currentInstance._tilesToWin--;
            if (currentInstance._tilesToWin == 0)
            {
                MessageDialogHelper.Show("You successfully found all the bombs!", "Win!");
            }
        }
    }

    public class GameMapVM : INotifyPropertyChanged
    {
        private int _xPosition, _yPosition;

        private bool _isMarked;

        public bool IsMarked
        {
            get { return _isMarked; }
            set
            {
                _isMarked = value;
                OnPropertyChanged(nameof(Signal));
            }
        }


        public GameMapVM(int xPosition, int yPosition)
        {
            _xPosition = xPosition;
            _yPosition = yPosition;
            _pressRevealCommand = new RelayCommand(ClickNode, NodeIsRevealed);
            _nodeIsRevealed = false;
            _self = this;
        }

        private RelayCommand _pressRevealCommand;

        public ICommand PressRevealCommand
        {
            get { return _pressRevealCommand; }
        }

        private void ClickNode()
        {
            try
            {
                MinesVM.gameMap[_xPosition, _yPosition].Reveal();
            }
            catch (LostGameException lge)
            {
                MessageDialogHelper.Show(lge.Message, "Game Over");
                MainPage.gamePageInstance.Frame.Navigate(typeof(SettupPage));
            }
        }

        private bool _nodeIsRevealed;

        public bool RevealNode
        {
            set
            {
                _nodeIsRevealed = value;
                _pressRevealCommand.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(Color));
                OnPropertyChanged(nameof(Signal));
            }
        }
        private bool NodeIsRevealed()
        {
            return !_nodeIsRevealed;
        }

        public Color Color
        {
            get { return _nodeIsRevealed ? Colors.GhostWhite : Colors.Green; }
        }

        public string Signal
        {
            get {
                if (_nodeIsRevealed)
                {
                    int theSignal = MinesVM.gameMap[_xPosition, _yPosition].Signal;
                    return theSignal != 0 ? Convert.ToString(theSignal) : "";
                }
                else
                {
                    return IsMarked ? "x" : "";
                }
            }
        }

        private GameMapVM _self;
        public GameMapVM Self
        {
            get { return _self; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
