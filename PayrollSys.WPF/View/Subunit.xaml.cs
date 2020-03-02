using System.Windows;
using PayrollSys.BL.Model;

namespace PayrollSys.WPF.View
{
    public partial class SubunitWindow : Window
    {
        public SubunitWindow()
        {
            InitializeComponent();
        }

        private void BEnter_Click(object sender, RoutedEventArgs e)
        {
            Insert();
        }

        private void BCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Добавить.
        /// </summary>
        private void Insert()
        {
            try
            {
                string name = tbName.Text;
                Subunit.Insert(name);
                Close();
            }
            catch
            {
                MessageBox.Show("Проверьте корректность введенных данных!", "Ошибка");
            }
        }
    }
}