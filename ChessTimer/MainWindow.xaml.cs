using System;
using System.Windows;

namespace ChessTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = (this.DataContext as Model);
            vm.bitti += Vm_bitti;
        }

        private void Vm_bitti(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString());
            sifirla();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as Model;
            vm.Sirabirdemi = !vm.Sirabirdemi;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as Model;
            vm.basla();
            btn_basla.IsEnabled = false;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            sifirla();
        }

        private void sifirla()
        {
            var vm = this.DataContext as Model;
            vm.dur();
            vm.init();
            btn_basla.IsEnabled = true;
        }
    }
}
