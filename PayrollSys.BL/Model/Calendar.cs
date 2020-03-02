using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;

namespace PayrollSys.BL.Model
{
    /// <summary>
    /// Трудовой календарь.
    /// </summary>
    public class Calendar : INotifyPropertyChanged
    {
        static readonly MyDbContext myDbContext = new MyDbContext();

        string months;
        int hours;

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int CalendarId { get; set; }

        /// <summary>
        /// Название месяца.
        /// </summary>
        public string Month
        {
            get { return months; }
            set
            {
                months = value;
                OnPropertyChanged("Months");
            }
        }

        /// <summary>
        /// Часы.
        /// </summary>
        public int Hours
        {
            get { return hours; }
            set
            {
                hours = value;
                OnPropertyChanged("Hours");
            }
        }

        /// <summary>
        /// Коллекция календарей сотрудников.
        /// </summary>
        public virtual ICollection<WorkerCalendar> WorkerCalendars { get; set; }

        /// <summary>
        /// Получить календарь.
        /// </summary>
        /// <returns>Календарь.</returns>
        public static ICollection<Calendar> GetCalendar()
        {
            List<Calendar> calendar = new List<Calendar>(myDbContext.Calendar);
            return calendar;
        }

        #region Добавление

        /// <summary>
        /// Добавить.
        /// </summary>
        /// <param name="month">Месяц.</param>
        /// <param name="hours">Часы.</param>
        public static void Insert(string month, int hours)
        {
            Calendar calendar = new Calendar
            {
                Month = month,
                Hours = hours,
            };

            myDbContext.Calendar.Add(calendar);
            myDbContext.SaveChanges();
        }

        #endregion

        #region Редактирование

        /// <summary>
        /// Редактировать.
        /// </summary>
        /// <param name="month">Месяц.</param>
        /// <param name="hours">Часы.</param>
        public static void Update(string month, int hours)
        {
            var query = from c in myDbContext.Calendar
                        where c.Month == month
                        select c;

            foreach (Calendar calendar in query)
            {
                calendar.Hours = hours;
            }

            myDbContext.SaveChanges();
        }

        #endregion

        /// <summary>
        /// Получить календарь по названию месяца.
        /// </summary>
        /// <param name="month">Название месяца.</param>
        /// <returns>Календарь.</returns>
        public static Calendar GetCalendarByMonth(string month)
        {
            Calendar calendar = new Calendar();
            var query = from c in myDbContext.Calendar
                        where c.Month == month
                        select c;
            foreach (Calendar c in query)
                calendar = c;
            return calendar;
        }

        /// <summary>
        /// Реализация интерфейса INotifyPropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Проверка на изменение значений.
        /// </summary>
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}