using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sweeper04
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ZCell : UserControl
    {
        public int KeyNumber { get; set; }
        public Tuple GridPositon { get; set; }
        public bool IsBomb { get; set; }
        public bool IsFlag { get; set; }
        public bool Visited { get; set; }

        public bool LeftMouseDown { get; set; }
        public bool RightMouseDown { get; set; }
        public bool chording { get; set; }



        public string CellLabel
        {
            get {return KeyNumber == 0 ? " " : $"{KeyNumber}";}
        }





        public ZCell()
        {
            InitializeComponent();
            DataContext = this;
        }
        public override string ToString()
        {
            return $"{GridPositon} " + base.ToString();
        }
        private void CellButton_Click(object sender, RoutedEventArgs e)
        {
            RevealCell();
        }
        private void RevealCell()
        {
            if (Visited)
                return;

            Visited = true;

            if (IsBomb)
            {
                CellButton.Background = Brushes.DarkRed;
                MainWindow.EndGame("Lose");
                return;
            }

            CellButton.Background = Brushes.DarkGray;
            KeyNumber = (NeighborBombCount() == 0) ? CallNeighbors() : NeighborBombCount();
            CellButton.Content = CellLabel; //FIX data bind this
            Minefield2.RemainingCells.Remove(this);
            Minefield2.CheckWin();//add listener to change on remaining cells?
        }

        private int NeighborBombCount()
        {
            int count = 0;
            foreach (ZCell cell in Neighbors())
            {
                if (cell.IsBomb)
                    count++;
            }
            return count;
        }
        //combine these two
        private int NeighborFlagCount()
        {
            int count = 0;
            foreach (ZCell cell in Neighbors())
            {
                if (cell.IsFlag)
                    count++;
            }
            return count;
        }
        private int CallNeighbors()
        {
            foreach (ZCell cell in Neighbors())
            {
                if (!cell.IsFlag)
                    cell.RevealCell();
            }
            return 0;
        }

        private List<ZCell> Neighbors()
        {
            List<ZCell> localList = new List<ZCell>();
            for (int y = GridPositon.y - 1; y < GridPositon.y + 2; y++)
            {
                if (y < 0 || y > Minefield2.CellArray.GetLength(1) - 1)
                    continue;
                for (int x = GridPositon.x - 1; x < GridPositon.x + 2; x++)
                {
                    if (x < 0 || x > Minefield2.CellArray.GetLength(0) - 1)
                        continue;
                    if (!(Minefield2.CellArray[x, y] == this))
                    {
                        localList.Add(Minefield2.CellArray[x, y]);
                    }
                }
            }
            return localList;
        }

        private void CellButton_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (LeftMouseDown)
            {
                chording = true;
            }
        }
        private void CellButton_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (chording)
            {
                DoChording();
                chording = false;
                return;
            }

            if (Visited)
                return;

            IsFlag = !IsFlag;
            if (IsFlag)
            {
                Minefield2.FlaggedCells.Add(this);
            }
            else
            {
                Minefield2.FlaggedCells.Remove(this);
            }
            CellButton.Background = IsFlag ? Brushes.Red : Brushes.LightGray;

            //cell style prefab change?
        }

        private void DoChording()
        {
            if (NeighborFlagCount() == NeighborBombCount())
            {
                CallNeighbors();
            }
        }

        private void CellButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                LeftMouseDown = true;
            if (e.ChangedButton == MouseButton.Right)
                RightMouseDown = true;
        }

        private void CellButton_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                LeftMouseDown = false;
            if (e.ChangedButton == MouseButton.Right)
                RightMouseDown = false;
        }
    }
}
