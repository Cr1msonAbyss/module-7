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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private char currentPlayer = 'X';
        private char[,] board = new char[3, 3];

        // Переменные для хранения очков
        private int xScore = 0;
        private int oScore = 0;

        public MainWindow()
        {
            InitializeComponent();
            UpdateScoreDisplay();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            // Проверка, что нажата игровая кнопка, а не NewGameButton
            if (button == null || button.Name == "NewGameButton")
                return;

            if (button.Content == null)
            {
                button.Content = currentPlayer;
                int row = Grid.GetRow(button);
                int col = Grid.GetColumn(button);
                board[row, col] = currentPlayer;

                if (CheckWin())
                {
                    MessageBox.Show($"{currentPlayer} победили!");

                    // Увеличение счёта победителя и обновление отображения счёта
                    if (currentPlayer == 'X')
                    {
                        xScore++;
                    }
                    else
                    {
                        oScore++;
                    }

                    UpdateScoreDisplay();
                    ResetGame();
                }
                else if (IsBoardFull())
                {
                    MessageBox.Show("Ничья!");
                    ResetGame();
                }

                // Смена текущего игрока
                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
            }
        }


        private bool CheckWin()
        {
            // Проверка строк
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] != '\0' && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                    return true;
            }

            // Проверка столбцов
            for (int i = 0; i < 3; i++)
            {
                if (board[0, i] != '\0' && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                    return true;
            }

            // Проверка диагоналей
            if (board[0, 0] != '\0' && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                return true;
            if (board[0, 2] != '\0' && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
                return true;

            return false;
        }

        private bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == '\0')
                        return false;
                }
            }
            return true;
        }

        private void ResetGame()
        {
            // Сбрасываем массив до пустого состояния
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = '\0';
                }
            }

            // Очистка кнопок игрового поля
            foreach (var control in MainGrid.Children)
            {
                if (control is Button button && button.Name != "NewGameButton")
                {
                    button.Content = null;
                }
            }

            // Начать с крестиков
            currentPlayer = 'X';
            MessageBox.Show("Крестики начинают");
        }


        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
            currentPlayer = 'O';
        }

        private void UpdateScoreDisplay()
        {
      
        }
    }
}
