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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Sweeper04
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Minefield2 : UserControl
    {
        public static readonly DependencyProperty DimensionsDependentProperty =
            DependencyProperty.Register("FieldDimensions",
                typeof(Tuple),
                typeof(UserControl),
                new PropertyMetadata(OnFieldDimensionsChanged));

        public Tuple FieldDimensions
        {
            get => (Tuple)GetValue(DimensionsDependentProperty);
            set => SetValue(DimensionsDependentProperty, value);
        }
        private static void OnFieldDimensionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as Minefield2).SetField();
        }

        public static readonly DependencyProperty BombCountDependentProp =
            DependencyProperty.Register("MinePercent",
        typeof(int),
        typeof(UserControl),
        new PropertyMetadata(OnBombCountChanged));

        public int MinePercent
        {
            get => (int)GetValue(BombCountDependentProp);
            set => SetValue(BombCountDependentProp, value);
        }
        private static void OnBombCountChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //(d as Minefield2).SetField();
        }



        public static ZCell[,] CellArray { get; set; }


        public int BombCount
        {
            get { return (int)MathF.Round(CellArray.Length * ((float)MinePercent / 100f)); }
        }
        public int FlagCount
        {
            get { return BombCount - FlaggedCells.Count(); }
        }


        public static ObservableCollection<Button> CellButtons { get; } = new ObservableCollection<Button>();

        public static List<ZCell> RemainingCells { get; set; } = new List<ZCell>();
        public static List<ZCell> MinedCells { get; set; } = new List<ZCell>();
        public static List<ZCell> FlaggedCells { get; set; } = new List<ZCell>();

        //What does this do? part of observable collection updating on change
        public readonly object _sync = new object();

        public Minefield2()
        {
            InitializeComponent();
            DataContext = this;
            BindingOperations.EnableCollectionSynchronization(CellButtons, _sync);
            MainWindow.resetGame += SetField;
        }

        private void SetField()
        {
            CellArray = BuildArray();
            MinedCells = SetMines();
        }
        private ZCell[,] BuildArray()
        {
            ZCell[,] LocalArray = new ZCell[FieldDimensions.x, FieldDimensions.y];
            CellButtons.Clear();
            RemainingCells.Clear();
            for (int y = 0; y < FieldDimensions.y; y++)
            {
                for (int x = 0; x < FieldDimensions.x; x++)
                {
                    LocalArray[x, y] = new ZCell();
                    LocalArray[x, y].GridPositon = new Tuple(x,y);
                    RemainingCells.Add(LocalArray[x, y]);
                    CellButtons.Add(LocalArray[x, y].CellButton);
                }
            }
            return LocalArray;
        }
        private List<ZCell> SetMines()
        {
            MinedCells.Clear();
            List<ZCell> localList = new List<ZCell>();
            Random RNG = new Random();
            for (int i = 0; i < BombCount; i++)
            {
                ZCell setCell = RemainingCells[RNG.Next(RemainingCells.Count)];
                setCell.IsBomb = true;
                localList.Add(setCell);
                RemainingCells.Remove(setCell);
            }
            return localList;
        }
        public static void CheckWin()
        {
            if (RemainingCells.Count == 0)
                MainWindow.EndGame("Win");
        }
    }
}
