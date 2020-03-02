using System;
using System.Windows;
using PayrollSys.BL;
using PayrollSys.BL.Model;
using PayrollSys.BL.ViewModel;

namespace PayrollSys.WPF.View
{
    public partial class BranchesWindow : Window
    {
        private readonly MyDbContext myDbContext = new MyDbContext();

        public BranchesWindow()
        {
            InitializeComponent();

            DataContext = new BranchViewModel();
        }

        private void bInsert_Click(object sender, RoutedEventArgs e)
        {
            BranchWindow branchWindow = new BranchWindow();
            branchWindow.Show();
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgBranches.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dgBranches.SelectedItems.Count; i++)
                {
                    if (dgBranches.SelectedItems[i] is Branch branch)
                    {
                        MessageBoxResult result = MessageBox.Show($"Вы действительно хотите удалить филиал в городе {branch.City} из базы данных?", "Предупреждение", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            try
                            {
                                Branch.Delete(branch.BranchId);
                                MessageBox.Show($"Филиал в городе {branch.City} успешно удален из базы данных.", "Сообщение");
                                Refresh();
                            }
                            catch
                            { }
                        }
                    }
                }
            }
        }

        private void mRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// Обновить.
        /// </summary>
        private void Refresh()
        {
            dgBranches.ItemsSource = Branch.GetBranches();
            dgBranches.SelectedItems.Clear();
        }

        private void mExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}