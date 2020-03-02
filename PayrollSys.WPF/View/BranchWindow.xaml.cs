using System.Windows;
using PayrollSys.BL;
using PayrollSys.BL.Model;

namespace PayrollSys.WPF.View
{
    public partial class BranchWindow : Window
    {
        readonly MyDbContext myDbContext = new MyDbContext();
        readonly ValueController valueController = new ValueController();

        public BranchWindow()
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
        /// <param name="name">Наименование.</param>
        public void Insert()
        {
            try
            {
                Branch branch = new Branch();
                branch.Insert(tbCity.Text);
                Close();
            }
            catch
            {
                MessageBox.Show("Проверьте корректность введенных данных!", "Ошибка");
            }
        }
    }
}