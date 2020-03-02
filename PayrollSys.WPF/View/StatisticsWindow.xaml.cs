using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using PayrollSys.BL.Model;
using PayrollSys.BL.ViewModel;

namespace PayrollSys.WPF.View
{
    /// <summary>
    /// Окно статистики.
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow()
        {
            InitializeComponent();

            DataContext = new WorkerCalendarViewModel();
            SortAscending();
        }

        private void BInsert_Click(object sender, RoutedEventArgs e)
        {
            WorkerCalendarWindow workerCalendarWindow = new WorkerCalendarWindow();
            workerCalendarWindow.bEnter.Content = "Добавить";
            workerCalendarWindow.cbMonth.IsEnabled = true;
            workerCalendarWindow.tbFullname.IsEnabled = true;
            workerCalendarWindow.Show();
        }

        private void BUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgStatistics.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dgStatistics.SelectedItems.Count; i++)
                {
                    if (dgStatistics.SelectedItems[i] is WorkerCalendar workerCalendar)
                    {
                        WorkerCalendarWindow workerCalendarWindow = new WorkerCalendarWindow();
                        workerCalendarWindow.bEnter.Content = "Сохранить";
                        workerCalendarWindow.lWorkerId.Content = ($"{workerCalendar.Worker.WorkerId}");
                        workerCalendarWindow.tbFullname.Text = ($"{workerCalendar.Worker.Fullname}");
                        workerCalendarWindow.cbMonth.Text = ($"{workerCalendar.Calendar.Month}");
                        workerCalendarWindow.cbMonth.IsEnabled = false;
                        workerCalendarWindow.tbFullname.IsEnabled = false;
                        workerCalendarWindow.Show();
                    }
                }
            }
            else
                MessageBox.Show("Сначала выберите запись из таблицы!", "Ошибка");
        }

        private void BDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgStatistics.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dgStatistics.SelectedItems.Count; i++)
                {
                    if (dgStatistics.SelectedItems[i] is WorkerCalendar workerCalendar)
                    {
                        MessageBoxResult result = MessageBox.Show($"Вы действительно хотите удалить статистику сотрудника {workerCalendar.Worker.Fullname} из базы данных?", "Предупреждение", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            try
                            {
                                WorkerCalendar.Delete(workerCalendar.WorkerCalendarId);
                                MessageBox.Show($"Статистика сотрудника успешно удалена из базы данных.", "Сообщение");
                                Refresh();
                            }
                            catch
                            { }
                        }
                    }
                }
            }
        }

        private void MPayroll_Click(object sender, RoutedEventArgs e)
        {
            double Wage;
            if (dgStatistics.SelectedItems.Count > 0)
            {
                for (int i = 0; i < dgStatistics.SelectedItems.Count; i++)
                {
                    if (dgStatistics.SelectedItems[i] is WorkerCalendar workerCalendar)
                    {
                        try
                        {
                            string month = workerCalendar.Calendar.Month;
                            int workerId = workerCalendar.Worker.WorkerId;
                            string fullname = workerCalendar.Worker.Fullname;
                            int workedHours = workerCalendar.WorkedHours;

                            if (workerCalendar.Worker.PaymentType == "Почасовая оплата (сдельная)")
                            {
                                Wage = Worker.Payroll(workerId, month, workedHours);
                            }
                            else
                            {
                                Wage = Worker.Payroll(workerId, month, workedHours);
                            }
                            MessageBox.Show($"Заработная плата за указанный месяц для сотрудника {fullname} равна {Wage} рублей.", "Сообщение");
                        }
                        catch
                        {
                            MessageBox.Show("Проверьте корректность введеных данных!", "Ошибка");
                        }
                    }
                }
            }
            else
                MessageBox.Show("Сначала выберите запись из списка!", "Ошибка");
        }

        private void MRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
            SortAscending();
        }

        private void MExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Обновить.
        /// </summary>
        private void Refresh()
        {
            try
            {
                dgStatistics.ItemsSource = WorkerCalendar.GetWorkerCalendar();
                dgStatistics.SelectedItems.Clear();
            }
            catch
            { }
        }

        /// <summary>
        /// Сортировать по возрастанию
        /// </summary>
        private void SortAscending()
        {
            dgStatistics.Items.SortDescriptions.Add(new SortDescription("Calendar.CalendarId", ListSortDirection.Ascending));
        }

        private void MGenPaySheet_Click(object sender, RoutedEventArgs e)
        {
            GetValueWindow genPaySheetWindow = new GetValueWindow();
            genPaySheetWindow.Show();
        }

        private void MGenWorkersListOne_Click(object sender, RoutedEventArgs e)
        {
            GetValue getValue = new GetValue();
            getValue.lValue.Content = "Размер зарплаты больше";
            getValue.lValue.Visibility = Visibility.Visible;
            getValue.tbValue.Visibility = Visibility.Visible;
            getValue.Show();
        }

        private void MGenWorkersListTwo_Click(object sender, RoutedEventArgs e)
        {
            GetValue getValue = new GetValue();
            getValue.lValue.Content = "MGenWorkersListTwo";
            getValue.lValue.Visibility = Visibility.Hidden;
            getValue.tbValue.Visibility = Visibility.Hidden;
            getValue.Show();
        }

        private void MGenMWorkersRating_Click(object sender, RoutedEventArgs e)
        {
            GetValue getValue = new GetValue();
            getValue.lValue.Content = "Количество сотрудников";
            getValue.lValue.Visibility = Visibility.Visible;
            getValue.tbValue.Visibility = Visibility.Visible;
            getValue.Show();
        }
    }
}