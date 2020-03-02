using System;
using System.ComponentModel;
using System.Windows;
using PayrollSys.BL.Model;
using PayrollSys.BL.ViewModel;

namespace PayrollSys.WPF.View
{
    public partial class BranchesWindow : Window
    {
        public BranchesWindow()
        {
            InitializeComponent();
            DataContext = new BranchViewModel();
            SortAscending();
        }

        private void BInsert_Click(object sender, RoutedEventArgs e)
        {
            BranchWindow branchWindow = new BranchWindow();
            branchWindow.Show();
        }


        private void BUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgBranches.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dgBranches.SelectedItems.Count; i++)
                {
                    if (dgBranches.SelectedItems[i] is Branch branch)
                    {
                        UpdateBranchWindow updateBranchWindow = new UpdateBranchWindow();
                        updateBranchWindow.lBranch.Content = $"{branch.City}";
                        updateBranchWindow.Show();
                    }
                    else
                    {
                        MessageBox.Show("Выберите запись для редактирования!", "Ошибка");
                    }
                }
            }
        }

        private void BDelete_Click(object sender, RoutedEventArgs e)
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

        private void MRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            SortAscending();
        }

        /// <summary>
        /// Обновить.
        /// </summary>
        private void Refresh()
        {
            try
            {
                dgBranches.ItemsSource = Branch.GetBranches();
                dgBranches.SelectedItems.Clear();
            }
            catch
            { }
        }

        /// <summary>
        /// Сортировать по возрастанию.
        /// </summary>
        private void SortAscending()
        {
            dgBranches.Items.SortDescriptions.Add(new SortDescription("City", ListSortDirection.Ascending));
        }

        private void MToCount_Click(object sender, RoutedEventArgs e)
        {
            if (dgBranches.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dgBranches.SelectedItems.Count; i++)
                {
                    if (dgBranches.SelectedItems[i] is Branch branch)
                    {
                        try
                        {
                            int count = Worker.GetWorkersCountByBranchId(branch.BranchId);
                            MessageBox.Show($"Количество сотрудников в филиале города {branch.City}: {count}", "Сообщение");
                        }
                        catch
                        { }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите запись из таблицы!", "Ошибка");
            }
        }

        private void MGenMidWageOnBranch_Click(object sender, RoutedEventArgs e)
        {
            GetValue getValue = new GetValue();
            getValue.lValue.Content = "MGenMidWageOnBranch";
            getValue.lValue.Visibility = Visibility.Hidden;
            getValue.tbValue.Visibility = Visibility.Hidden;
            getValue.Show();
        }

        private void MGenListWithWorkersCount(object sender, RoutedEventArgs e)
        {

        }

        private void MExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}