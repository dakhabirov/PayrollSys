using System.Windows;

namespace PayrollSys.WPF.View
{
    public partial class ResultWindow : Window
    {
        public ResultWindow()
        {
            InitializeComponent();
        }

        private void BEnter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}