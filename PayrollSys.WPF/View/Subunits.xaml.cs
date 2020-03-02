using System;
using System.ComponentModel;
using System.Windows;
using PayrollSys.BL.Model;
using PayrollSys.BL.ViewModel;

namespace PayrollSys.WPF.View
{
    public partial class SubunitsWindow : Window
    {
        public SubunitsWindow()
        {
            InitializeComponent();

            DataContext = new SubunitViewModel();
            SortAscending();
        }

        private void BInsert_Click(object sender, RoutedEventArgs e)
        {
            SubunitWindow subunitWindow = new SubunitWindow();
            subunitWindow.Show();
        }

        private void BDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgSubunits.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dgSubunits.SelectedItems.Count; i++)
                {
                    if (dgSubunits.SelectedItems[i] is Subunit subunit)
                    {
                        MessageBoxResult result = MessageBox.Show($"Вы действительно хотите удалить подразделение '{subunit.Name}' из базы данных?", "Предупреждение", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            try
                            {
                                Subunit.Delete(subunit.SubunitId);
                                MessageBox.Show($"Подразделение '{subunit.Name}' успешно удалено из базы данных.", "Сообщение");
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
                dgSubunits.ItemsSource = Subunit.GetSubunits();
                dgSubunits.SelectedItems.Clear();
            }
            catch
            { }
        }

        /// <summary>
        /// Сортировать по возрастанию
        /// </summary>
        private void SortAscending()
        {
            dgSubunits.Items.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        private void MExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}