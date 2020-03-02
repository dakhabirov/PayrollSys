using System.Collections.Generic;
using PayrollSys.BL.Model;

namespace PayrollSys.BL.ViewModel
{
    public class WorkerCalendarViewModel
    {
        /// <summary>
        /// Коллекция записей.
        /// </summary>
        public ICollection<WorkerCalendar> WorkerCalendars { get; set; }

        public WorkerCalendarViewModel()
        {
            WorkerCalendars = WorkerCalendar.GetWorkerCalendar();
        }
    }
}
