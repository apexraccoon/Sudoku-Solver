using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sudoku_Solver_Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[] Board = new int[82];
        public MainWindow()
        {
            InitializeComponent();
        }
        void Button_Clicked(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Content.ToString() == " ")
            {
                button.Content = 0;
            }
            if (Int32.Parse(button.Content.ToString()) < 9)
            {
                
                button.Content = Int32.Parse(button.Content.ToString()) + 1;
            }
            else
            {
                button.Content = " ";
            }
        }
        
        void Solve_Algo(object sender,RoutedEventArgs e)
        {
            for (int i = 1; i < 82; i++)
            {
                Button button = ((Button)this.FindName("Btn" + i));
                //find button and put it inside button_found
                //check if it is space put zero else put number value
                if (button.Content.ToString() == " ")
                {
                    Board[i] = 0;
                }
                else
                {
                    //store value in board
                    Board[i] = Int32.Parse(button.Content.ToString());
                }
            }
            solver solve = new solver();
            //solve board
            solve.solve_board(Board,1,1);
            for (int i = 1; i < 82; i++)
            {
                Button button = ((Button)this.FindName("Btn" + i));
                //put new board value inside that button
                button.Content = Board[i];
            }
        }
        void Clear_Board(object sender, RoutedEventArgs e)
        {
            for (int i = 1; i < 82; i++)
            {
                Button button = ((Button)this.FindName("Btn" + i));
                button.Content = " ";
                Board[i] = 0;
            }
        }
        void Settings(object sender, RoutedEventArgs e)
        {
            //Hide Board and buttons
            for (int i = 1; i < 82; i++)
            {
                Button button = ((Button)this.FindName("Btn" + i));
                button.Visibility = Visibility.Hidden;
            }
            Button Button_solve = ((Button)this.FindName("BtnSolve"));
            Button Button_Clear = ((Button)this.FindName("BtnClear"));
            Button Button_Back = ((Button)this.FindName("BtnBack"));
            Button Button_ChangeBackground = ((Button)this.FindName("BtnBackgroundChange"));
            TextBox ChangeBackground = ((TextBox)this.FindName("BackgroundImage"));
            Button_solve.Visibility = Visibility.Hidden;
            Button_Clear.Visibility = Visibility.Hidden;
            ((Button)sender).Visibility = Visibility.Hidden;
            //Show Back button and settings stuff
            Button_Back.Visibility = Visibility.Visible;
            ChangeBackground.Visibility = Visibility.Visible;
            Button_ChangeBackground.Visibility = Visibility.Visible;
        }
        void Back(object sender, RoutedEventArgs e)
        {
            //show board and buttons
            for (int i = 1; i < 82; i++)
            {
                Button button = ((Button)this.FindName("Btn" + i));
                button.Visibility = Visibility.Visible;
            }
            Button Button_solve = ((Button)this.FindName("BtnSolve"));
            Button Button_Clear = ((Button)this.FindName("BtnClear"));
            Button Button_Setting = ((Button)this.FindName("BtnSettings"));
            Button Button_ChangeBackground = ((Button)this.FindName("BtnBackgroundChange"));
            TextBox ChangeBackground = ((TextBox)this.FindName("BackgroundImage"));
            Button_solve.Visibility = Visibility.Visible;
            Button_Clear.Visibility = Visibility.Visible;
            Button_Setting.Visibility = Visibility.Visible;
            //hide settings options
            ((Button)sender).Visibility = Visibility.Hidden;
            Button_ChangeBackground.Visibility = Visibility.Hidden;
            ChangeBackground.Visibility = Visibility.Hidden;
        }
        void ChangeBackground(object sender,RoutedEventArgs e)
        {
            Canvas canvas = (Canvas)this.FindName("MainCanvas");
            TextBox ChangeBackground = ((TextBox)this.FindName("BackgroundImage"));
            ImageBrush background_image = (ImageBrush)canvas.FindName("BackgroundCanvas");
            string filepath = ChangeBackground.Text;
            filepath = filepath.Trim('"');
            background_image.ImageSource = new BitmapImage(new Uri(filepath, UriKind.Absolute));
            this.Background = background_image;
        }
    }
}