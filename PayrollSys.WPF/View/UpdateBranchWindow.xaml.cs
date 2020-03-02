using System.Windows;
using PayrollSys.BL;
using PayrollSys.BL.Model;
using System.Linq;

namespace PayrollSys.WPF.View
{
    /// <summary>
    /// Окно редактирования филиала
    /// </summary>
    public partial class UpdateBranchWindow : Window
    {
        public UpdateBranchWindow()
        {
            InitializeComponent();

            cbSubunit.ItemsSource = Subunit.GetNames();
        }

        private void BEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BranchSubunit branchSubunit = new BranchSubunit();
                Branch branch = Branch.GetBranchByCity(lBranch.Content.ToString());
                Subunit subunit = Subunit.GetSubunitByName(cbSubunit.Text);
                branchSubunit.Insert(branch.BranchId, subunit.SubunitId);
                Close();
            }
            catch
            {
                MessageBox.Show("Проверьте корректность введенных данных!", "Ошибка");
            }
        }

        private void BDelete_Click(object sender, RoutedEventArgs e)
        {
            using (var myDbContext = new MyDbContext())
            {
                try
                {
                    var query = from bs in myDbContext.BranchSubunit
                                where bs.Branch.City == lBranch.Content.ToString() && bs.Subunit.Name == cbSubunit.Text
                                select bs;

                    foreach (BranchSubunit bs in query)
                    {
                        try
                        {
                            BranchSubunit.Delete(bs.BranchSubunitId);
                            MessageBox.Show($"Подразделение успешно удалено из филиала.", "Сообщение");
                            Close();
                        }
                        catch
                        { }
                    }
                }
                catch
                {
                    MessageBox.Show("Проверьте корректность введенных данных!", "Ошибка");
                }
            }
        }
    }
}