using System;
using System.Windows;
using System.Windows.Controls;


namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string GenerateKey(string text, string key)
        {
            while (key.Length < text.Length)
                key += key;
            return key.Substring(0, text.Length);
        }

        private string Encrypt(string text, string key)
        {
            string result = "";
            key = GenerateKey(text, key);
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                {
                    char offset = char.IsUpper(text[i]) ? 'A' : 'a';
                    result += (char)((text[i] + key[i] - 2 * offset) % 26 + offset);
                }
                else
                {
                    result += text[i];
                }
            }
            return result;
        }

        private string Decrypt(string text, string key)
        {
            string result = "";
            key = GenerateKey(text, key);
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                {
                    char offset = char.IsUpper(text[i]) ? 'A' : 'a';
                    result += (char)((text[i] - key[i] + 26) % 26 + offset);
                }
                else
                {
                    result += text[i];
                }
            }
            return result;
        }

        private void OnExecuteButtonClick(object sender, RoutedEventArgs e)
        {
            string inputText = InputTextBox.Text;
            string key = KeyTextBox.Text;

            if (string.IsNullOrEmpty(inputText) || string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Будь ласка, введіть текст і ключ.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string result;
            if ((OperationComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() == "Шифрування")
            {
                result = Encrypt(inputText, key);
            }
            else
            {
                result = Decrypt(inputText, key);
            }

            OutputTextBox.Text = result;
        }
    }
}
