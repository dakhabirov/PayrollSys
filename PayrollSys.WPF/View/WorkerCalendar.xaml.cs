using PayrollSys.BL.Model;
using System;
using System.Windows;
namespace PayrollSys.WPF.View
{
    public partial class WorkerCalendarWindow : Window
    {
        public WorkerCalendarWindow()
        {
            InitializeComponent();
        }

        private void BEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bEnter.Content is "Добавить")
                {
                    Insert();
                }
                else
                {
                    Update();
                }
            }
            catch
            {
                MessageBox.Show("Проверьте корректность введенных данных!", "Ошибка");
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
            int calendarId = Calendar.GetCalendarByMonth(cbMonth.Text).CalendarId;
            Worker worker = Worker.GetWorkerByFullname(tbFullname.Text);

            WorkerCalendar.Insert(worker.WorkerId, calendarId, Convert.ToInt32(tbHours.Text));
            Close();
            MessageBox.Show($"Данные сотрудника {worker.Fullname} успешно обновлены.", "Сообщение");
        }

        /// <summary>
        /// Добавить.
        /// </summary>
        private void Update()
        {
            Worker worker = Worker.GetWorkerByFullname(tbFullname.Text);
            WorkerCalendar.Update(Convert.ToInt32(lWorkerId.Content), cbMonth.Text, Convert.ToInt32(tbHours.Text));
            Close();
            MessageBox.Show($"Данные сотрудника {worker.Fullname} успешно обновлены.", "Сообщение");
        }
    }
}