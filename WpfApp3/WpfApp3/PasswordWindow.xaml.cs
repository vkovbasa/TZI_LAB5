using System;
using System.IO;
using System.Windows;

namespace WpfApp3
{
    public partial class PasswordWindow : Window
    {
        private const string CorrectPassword = "2299673"; 
        private const string CounterFilePath = "install_counter.txt";
        private const int MaxCopies = 3;

        public PasswordWindow()
        {
            InitializeComponent();

            // Перевірка ліміту копій при запуску
            if (!CheckInstallationLimit())
            {
                MessageBox.Show("Досягнуто ліміт копій програми. Програма не може бути запущена.", "Ліміт копій",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(0); // Завершення програми без виклику Application.Current
            }
        }

        private bool CheckInstallationLimit()
        {
            int installationCount = 0;

            // Перевіряємо, чи існує файл лічильника і зчитуємо значення
            if (File.Exists(CounterFilePath))
            {
                try
                {
                    string countText = File.ReadAllText(CounterFilePath);
                    installationCount = int.Parse(countText);
                }
                catch (Exception)
                {
                    installationCount = 0; // У разі помилки зчитування встановлюємо значення за замовчуванням
                }
            }

            // Збільшуємо лічильник інсталяцій
            installationCount++;

            // Перевірка, чи не перевищено максимальний ліміт інсталяцій
            if (installationCount > MaxCopies)
            {
                return false;
            }

            // Записуємо нове значення лічильника у файл
            File.WriteAllText(CounterFilePath, installationCount.ToString());
            return true;
        }

        private void OnConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password == CorrectPassword)
            {
                // Відкриваємо основне вікно, якщо пароль правильний
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                // Виводимо повідомлення про помилку, якщо пароль неправильний
                ErrorTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
