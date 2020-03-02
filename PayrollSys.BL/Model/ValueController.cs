namespace PayrollSys.BL.Model
{
    public class ValueController
    {
        readonly MyDbContext MyDbContext = new MyDbContext();

        #region Проверить уникальность записей в базе данных.

        #region Проверить данные сотрудника.

        /// <summary>
        /// Проверить данные сотрудника на уникальность.
        /// </summary>
        /// <param name="worker">Сотрудник.</param>
        public bool CheckDuplicateWorker(Worker worker)
        {
            var workers = MyDbContext.Workers;    // таблица сотрудников
            var checker = false;

            try
            {
                foreach (Worker w in workers)
                {
                    if (w.WorkerId == worker.WorkerId)
                    {
                        checker = true;
                    }
                }
            }
            catch
            { }

            return checker;
        }

        #endregion

        #region Проверить данные филиала.

        /// <summary>
        /// Проверить данные филиала на уникальность.
        /// </summary>
        /// <param name="branch">Филиал.</param>
        public bool CheckDuplicateBranch(Branch branch)
        {
            var branches = MyDbContext.Branches;  // таблица филиалов
            var checker = false;

            try
            {
                foreach (Branch b in branches)
                {
                    if (b.City == branch.City)
                    {
                        checker = true;
                    }
                }
            }
            catch
            { }

            return checker;
        }

        #endregion

        #region Проверить данные подразделения.

        /// <summary>
        /// Проверить данные подразделения на уникальность.
        /// </summary>
        /// <param name="subunit">Подразделение.</param>
        public bool CheckDuplicateSubunit(Subunit subunit)
        {
            var subunits = MyDbContext.Subunits;  // таблица подразделений
            var checker = false;

            try
            {
                foreach (Subunit s in subunits)
                {
                    if (s.Name == subunit.Name)
                    {
                        checker = true;
                    }
                }
            }
            catch
            { }

            return checker;
        }

        #endregion

        #region Проверить данные связующий (промежуточной) таблицы филиала и подразделения.

        /// <summary>
        /// Проверить данные связующий (промежуточной) таблицы филиала и подразделения.
        /// </summary>
        /// <param name="branchSubunit">Промежуточная (связующая) таблица.</param>
        public bool CheckDuplicateBranchSubunit(BranchSubunit branchSubunit)
        {
            var branchSubunits = MyDbContext.BranchSubunit;
            var checker = false;

            try
            {
                foreach (BranchSubunit bs in branchSubunits)
                {
                    if (bs.BranchId == branchSubunit.BranchId && bs.SubunitId == branchSubunit.SubunitId)
                    {
                        checker = true;
                    }
                }
            }
            catch
            { }

            return checker;
        }

        #endregion

        #region Проверить данные календаря сотрудника.

        /// <summary>
        /// Проверить данные календаря сотрудника на уникальность.
        /// </summary>
        /// <param name="workerCalendar">Календарь сотрудника.</param>
        public bool CheckDuplicateWorkerCalendar(WorkerCalendar workerCalendar)
        {
            var workerCalendars = MyDbContext.WorkerCalendar;
            var checker = false;

            try
            {
                foreach (WorkerCalendar wc in workerCalendars)
                {
                    if (wc.WorkerId == workerCalendar.WorkerId && wc.CalendarId == workerCalendar.CalendarId)
                    {
                        checker = true;
                    }
                }
            }
            catch
            { }

            return checker;
        }

        #endregion

        #endregion
    }
}