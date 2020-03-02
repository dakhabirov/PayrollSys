using System;
using System.Windows;
using PayrollSys.BL;
using PayrollSys.BL.Model;
using PayrollSys.BL.ViewModel;

namespace PayrollSys.WPF.View
{
    public partial class WorkersWindow : Window
    {
        public WorkersWindow()
        {
            InitializeComponent();

            DataContext = new WorkerViewModel();
        }

        private void BInsert_Click(object sender, RoutedEventArgs e)
        {
            WorkerWindow branchWindow = new WorkerWindow();
            branchWindow.bEnter.Content = "Добавить";
            branchWindow.Show();
        }

        private void BUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Worker worker = (Worker)dgWorkers.SelectedItem;

                WorkerWindow workerWindow = new WorkerWindow();

                workerWindow.tbWorkerId.Text = worker.WorkerId.ToString();
                try { workerWindow.tbFullname.Text = worker.Fullname; } catch { }
                try { workerWindow.tbPosition.Text = worker.Position; } catch { }
                try { workerWindow.cbBranch.Text = worker.Branch.City; } catch { }
                try { workerWindow.cbSubunit.Text = worker.Subunit.Name; } catch { }
                try { workerWindow.cbPaymentType.Text = worker.PaymentType; } catch { }
                try { workerWindow.tbSalary.Text = worker.Salary.ToString(); } catch { }
                workerWindow.bEnter.Content = "Сохранить";

                workerWindow.Show();
            }
            catch
            { }
        }

        private void BDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgWorkers.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dgWorkers.SelectedItems.Count; i++)
                {
                    if (dgWorkers.SelectedItems[i] is Worker worker)
                    {
                        MessageBoxResult result = MessageBox.Show($"Вы действительно хотите удалить сотрудника {worker.Fullname} из базы данных?", "Предупреждение", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            try
                            {
                                Worker.Delete(worker.WorkerId);
                                MessageBox.Show($"Сотрудник {worker.Fullname} успешно удален из базы данных.", "Сообщение");
                                Refresh();
                            }
                            catch
                            { }
                        }
                    }
                }
            }
        }

        private void MRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Обновить.
        /// </summary>
        private void Refresh()
        {
            try
            {
                dgWorkers.ItemsSource = Worker.GetWorkers();
                dgWorkers.SelectedItems.Clear();
            }
            catch
            { }
        }

        private void MExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}