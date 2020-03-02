using System.Collections.Generic;
using PayrollSys.BL.Model;

namespace PayrollSys.BL.ViewModel
{
    public class WorkerViewModel
    {
        /// <summary>
        /// Коллекция сотрудников.
        /// </summary>
        public List<Worker> Workers { get; set; }

        public WorkerViewModel()
        {
            Workers = Worker.GetWorkers();
        }
    }
}