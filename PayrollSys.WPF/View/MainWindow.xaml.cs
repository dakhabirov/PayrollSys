using System;
using System.Windows;
using PayrollSys.BL.Model;
using PayrollSys.WPF.View;

namespace PayrollSys.WPF
{
    /// <summary>
    /// Главное окно.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BWorkers_Click(object sender, RoutedEventArgs e)
        {
            WorkersWindow workersWindow = new WorkersWindow();
            workersWindow.Show();
        }

        private void BBranches_Click(object sender, RoutedEventArgs e)
        {
            BranchesWindow branchesWindow = new BranchesWindow();
            branchesWindow.Show();
        }

        private void BSubunits_Click(object sender, RoutedEventArgs e)
        {
            SubunitsWindow subunitsWindow = new SubunitsWindow();
            subunitsWindow.Show();
        }

        private void BStatistics_Click(object sender, RoutedEventArgs e)
        {
            StatisticsWindow statisticsWindow = new StatisticsWindow();
            statisticsWindow.Show();
        }

        private void BCalendar_Click(object sender, RoutedEventArgs e)
        {
            CalendarWindow calendarWindow = new CalendarWindow();
            calendarWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Инициализировать случайные записи.
        /// </summary>
        private async void MInitialize_ClickAsync(object sender, RoutedEventArgs e)
        {
            Initializer randomInitializer = new Initializer();
            await randomInitializer.FillBranchesAsync();
            await randomInitializer.FillSubunitsAsync();
            await randomInitializer.FillWorkersAsync(30);
            randomInitializer.FillWorkerCalendar();
            randomInitializer.FillBranchSubunit();
            MessageBox.Show("Инициализация записей базы данных прошла успешно.");
        }

        private void MExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}