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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void ResetGame();
        public static ResetGame resetGame;

        public int TimeInSeconds { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            //this.DataContext = this;
        }
        private void SizeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //SetGame(); //Done By data binding event?
        }
        private void SetGame()
        {
            resetGame.Invoke();
        }


        public static void EndGame(string result)
        {
            MessageBox.Show(result);
            //figure out win/loss ideas
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            SetGame();
        }

        private void BombCountSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            SetGame();
        }
    }
}
