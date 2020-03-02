using System.Collections.Generic;
using System.ComponentModel;
using PayrollSys.BL.Model;

namespace PayrollSys.BL.ViewModel
{
    public class CalendarViewModel
    {
        /// <summary>
        /// Коллекция записей.
        /// </summary>
        public ICollection<Calendar> Calendars { get; set; }

        public CalendarViewModel()
        {
            Calendars = Calendar.GetCalendar();
        }
    }
}