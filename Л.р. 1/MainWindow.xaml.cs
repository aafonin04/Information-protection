using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Л.р._1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private bool IsCorrectData()
        {
            if (String.IsNullOrEmpty(KeyTextBox.Text) || String.IsNullOrEmpty(InputTextBox.Text))
            {
                MessageBox.Show("Пустые данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                KeyTextBox.Clear();
                InputTextBox.Clear();
                return false;
            }
            if (!(IsRussianAlphabetSymbols(KeyTextBox.Text) && IsRussianAlphabetSymbols(InputTextBox.Text)))
            {
                MessageBox.Show("Текст должен содержать буквы только русского алфавита", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                KeyTextBox.Clear();
                InputTextBox.Clear();
                return false;
            }
            return true;
        }
        private bool IsRussianAlphabetSymbols(string inputString)
        {
            foreach (char c in inputString.ToUpper())
            {
                if (Regex.IsMatch(inputString, "^[A-Z]+$"))
                {
                    return false;
                }
            }
            return true;
        }
        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsCorrectData())
            {
                string key = KeyTextBox.Text.ToUpper();
                string text = InputTextBox.Text.ToUpper();
                string encryptedText = TresimusCipher.Encrypt(text.ToUpper(), key.ToUpper());
                OutputTextBox.Text = encryptedText;
                DisplayTable(TresimusCipher.GenerateTable(key));
            }
        }
        private void KeyButtonClear_Clear(object sender, RoutedEventArgs e)
        {
            KeyTextBox.Clear();
        }
        private void StringButtonClear_Click(object sender, RoutedEventArgs e)
        {
            InputTextBox.Clear();
        }
        private void DisplayTable(char[,] table)
        {
            TableGrid.Children.Clear();
            TableGrid.RowDefinitions.Clear();
            TableGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < table.GetLength(0); i++)
            {
                TableGrid.RowDefinitions.Add(new RowDefinition());
            }
            for (int j = 0; j < table.GetLength(1); j++)
            {
                TableGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < table.GetLength(0); i++)
            {
                for (int j = 0; j < table.GetLength(1); j++)
                {
                    TextBlock textBlock = new TextBlock
                    {
                        Text = table[i, j].ToString(),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        FontSize = 16,
                        Margin = new Thickness(5)
                    };
                    Grid.SetRow(textBlock, i);
                    Grid.SetColumn(textBlock, j);
                    TableGrid.Children.Add(textBlock);
                }
            }
        }
        private void AllClearButton_Click(object sender, RoutedEventArgs e)
        {
            KeyTextBox.Clear();
            InputTextBox.Clear();
            OutputTextBox.Clear();
            TableGrid.Children.Clear();
            TableGrid.RowDefinitions.Clear();
            TableGrid.ColumnDefinitions.Clear();
        }
    }
}
