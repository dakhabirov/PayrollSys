using PayrollSys.BL.Model;
using System.Collections.Generic;
using System.Windows;

namespace PayrollSys.WPF.View
{
    public partial class GetValueWindow : Window
    {
        public GetValueWindow()
        {
            InitializeComponent();

            cbBranch.ItemsSource = Branch.GetCities();
            cbSubunit.ItemsSource = Subunit.GetNames();
        }

        private void BEnter_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = WorkerCalendar.GetPaySheet(cbMonth.Text, cbBranch.Text, cbSubunit.Text);
            ResultWindow resultWindow = new ResultWindow();
            foreach (string s in list)
                resultWindow.ListBox.Items.Add(s);
            resultWindow.tbOne.Text = $"Месяц: {cbMonth.Text}";
            resultWindow.tbTwo.Text = $"Филиал: {cbBranch.Text}";
            resultWindow.tbThree.Text = $"Подразделение: {cbSubunit.Text}";
            resultWindow.Show();
        }

        private void BCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}