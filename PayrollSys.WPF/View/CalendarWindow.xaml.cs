using PayrollSys.BL.Model;
using PayrollSys.BL.ViewModel;
using System;
using System.Windows;

namespace PayrollSys.WPF.View
{
    public partial class CalendarWindow : Window
    {
        public CalendarWindow()
        {
            InitializeComponent();

            DataContext = new CalendarViewModel();
        }

        private void BUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgCalendar.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dgCalendar.SelectedItems.Count; i++)
                {
                    if (dgCalendar.SelectedItems[i] is Calendar calendar)
                    {
                        UpdateCalendarWindow updateCalendarWindow = new UpdateCalendarWindow();
                        updateCalendarWindow.lMonth.Content = ($"{calendar.Month}");
                        updateCalendarWindow.Show();
                    }
                }
            }
            else
                MessageBox.Show("Сначала выберите запись из таблицы!", "Ошибка");
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
                dgCalendar.ItemsSource = Calendar.GetCalendar();
                dgCalendar.SelectedItems.Clear();
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