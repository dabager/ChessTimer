using System;
using System.Windows;

namespace ChessTimer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Bir hata oluştu!" + Environment.NewLine + Environment.NewLine + "Hata detayının screenshot'ını alıp yollamalısınız." + Environment.NewLine + e.Exception.ToString());
            e.Handled = true;
        }
    }
}
