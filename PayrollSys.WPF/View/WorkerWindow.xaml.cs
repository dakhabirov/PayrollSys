using System.Windows;
using System;
using PayrollSys.BL.Model;

namespace PayrollSys.WPF.View
{
    public partial class WorkerWindow : Window
    {
        public WorkerWindow()
        {
            InitializeComponent();

            cbBranch.ItemsSource = Branch.GetCities();
            cbSubunit.ItemsSource = Subunit.GetNames();
        }

        private void BEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bEnter.Content.ToString() == "Добавить")
                {
                    Insert();
                }
                else if (bEnter.Content.ToString() == "Сохранить")
                {
                    Update();
                }
            }
            catch
            {
                MessageBox.Show("Проверь корректность введеных данных!", "Ошибка.");
            }
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
            string fullname = tbFullname.Text;
            string position = tbPosition.Text;
            int branchId = Convert.ToInt32(Branch.GetBranchByCity(cbBranch.Text).BranchId);
            int subunitId = Convert.ToInt32(Subunit.GetSubunitByName(cbSubunit.Text).SubunitId);
            string paymentType = cbPaymentType.Text;
            int salary = Convert.ToInt32(tbSalary.Text);

            Worker.Insert(fullname, position, branchId, subunitId, paymentType, salary);
            Close();
            MessageBox.Show($"Сотрудник {fullname} успешно добавлен в базу данных.", "Сообщение");
        }

        /// <summary>
        /// Обновить.
        /// </summary>
        private void Update()
        {
            int workerId = Convert.ToInt32(tbWorkerId.Text);
            string fullname = tbFullname.Text;
            string position = tbPosition.Text;
            int branchId = Convert.ToInt32(Branch.GetBranchByCity(cbBranch.Text).BranchId);
            int subunitId = Convert.ToInt32(Subunit.GetSubunitByName(cbSubunit.Text).SubunitId);
            string paymentType = cbPaymentType.Text;
            int salary = Convert.ToInt32(tbSalary.Text);

            Worker.Update(workerId, fullname, position, branchId, subunitId, paymentType, salary);
            Close();
            MessageBox.Show($"Данные сотрудника {fullname} успешно обновлены.", "Сообщение");
        }
    }
}