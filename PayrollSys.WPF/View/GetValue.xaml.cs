using PayrollSys.BL.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace PayrollSys.WPF.View
{
    public partial class GetValue : Window
    {
        public GetValue()
        {
            InitializeComponent();
        }

        private void BEnter_Click(object sender, RoutedEventArgs e)
        {
            if (lValue.Content is "Размер зарплаты больше")
            {
                try
                {
                    ResultWindow resultWindow = new ResultWindow();
                    List<string> workerFullnames = WorkerCalendar.GetWorkersListOne(cbMonth.Text, Convert.ToDouble(tbValue.Text));
                    foreach (string s in workerFullnames)
                        resultWindow.ListBox.Items.Add(s);
                    resultWindow.tbOne.Text = $"Список сотрудников, размер зарплаты которых больше {tbValue.Text} руб";
                    resultWindow.Show();
                }
                catch
                {
                    MessageBox.Show("Проверьте корректность введенных данных!", "Ошибка");
                }
            }
            else if (lValue.Content is "MGenWorkersListTwo")
            {
                try
                {
                    ResultWindow resultWindow = new ResultWindow();
                    List<string> workerFullnames = WorkerCalendar.GetWorkersListTwo(cbMonth.Text);
                    foreach (string s in workerFullnames)
                        resultWindow.ListBox.Items.Add(s);
                    resultWindow.tbOne.Text = $"Список сотрудников с фиксированной месячной оплатой, которые отработали все требуемые часы согласно трудовому календарю";
                    resultWindow.Show();
                }
                catch
                {
                    MessageBox.Show("Проверьте корректность введенных данных!", "Ошибка");
                }
            }
            else if (lValue.Content is "MGenMidWageOnBranch")
            {
                try
                {
                    ResultWindow resultWindow = new ResultWindow();
                    List<string> branchesWithMidWage = WorkerCalendar.GetBranchesWithMidWage(cbMonth.Text);
                    foreach (string s in branchesWithMidWage)
                        resultWindow.ListBox.Items.Add(s);
                    resultWindow.tbOne.Text = $"Список филиалов с указанием средней заработной платы за {cbMonth.Text}";
                    resultWindow.Show();
                }
                catch
                {
                    MessageBox.Show("Проверьте корректность введенных данных!", "Ошибка");
                }
            }
            else if (lValue.Content is "Количество сотрудников")
            {
                try
                {
                    ResultWindow resultWindow = new ResultWindow();
                    List<string> workersRating = WorkerCalendar.GetWorkersRating(cbMonth.Text, Convert.ToInt32(tbValue.Text));
                    foreach (string s in workersRating)
                        resultWindow.ListBox.Items.Add(s);
                    resultWindow.tbOne.Text = $"Список {tbValue.Text} сотрудников с наибольшим размером зарплаты за {cbMonth.Text}.";
                    resultWindow.Show();
                }
                catch
                {
                    MessageBox.Show("Проверьте корректность введенных данных!", "Ошибка");
                }
            }
        }

        private void BCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
