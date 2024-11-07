using System.Windows;

namespace WpfApp3
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Відкриття вікна для введення пароля
            PasswordWindow passwordWindow = new PasswordWindow();
            passwordWindow.Show();
        }
    }
}
