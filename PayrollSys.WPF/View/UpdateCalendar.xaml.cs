using PayrollSys.BL.Model;
using System;
using System.Windows;

namespace PayrollSys.WPF.View
{
    public partial class UpdateCalendarWindow : Window
    {
        public UpdateCalendarWindow()
        {
            InitializeComponent();
        }

        private void BEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Calendar.Update(lMonth.Content.ToString(), Convert.ToInt32(tbHours.Text));
                MessageBox.Show("Данные успешно обновлены.", "Сообщение");
                Close();
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
    }
}