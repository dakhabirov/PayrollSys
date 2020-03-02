using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollSys.BL.Model
{
    /// <summary>
    /// Календарь сотрудника.
    /// </summary>
    public class WorkerCalendar
    {
        static readonly MyDbContext myDbContext = new MyDbContext();

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int WorkerCalendarId { get; set; }

        /// <summary>
        /// Идентификатор сотрудника.
        /// </summary>
        public int WorkerId { get; set; }

        /// <summary>
        /// Идентификатор календаря.
        /// </summary>
        public int CalendarId { get; set; }

        /// <summary>
        /// Количество отработанных часов сотрудником.
        /// </summary>
        public int WorkedHours { get; set; }

        /// <summary>
        /// Сотрудник. 
        /// </summary>
        public virtual Worker Worker { get; set; }

        /// <summary>
        /// Календарь. 
        /// </summary>
        public virtual Calendar Calendar { get; set; }

        /// <summary>
        /// Получить календарь.
        /// </summary>
        /// <returns>Календарь.</returns>
        public static ICollection<WorkerCalendar> GetWorkerCalendar()
        {
            List<WorkerCalendar> workerCalendar = new List<WorkerCalendar>(myDbContext.WorkerCalendar);
            return workerCalendar;
        }

        #region Добавление

        /// <summary>
        /// Добавить.
        /// </summary>
        /// <param name="workerId">Идентификатор сотрудника.</param>
        /// <param name="calendarId">Идентификатор календаря.</param>
        /// <param name="workedHours">Отработанное время сотрудником.</param>
        public static void Insert(int workerId, int calendarId, int workedHours)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                WorkerCalendar workerCalendar = new WorkerCalendar
                {
                    WorkerId = workerId,
                    CalendarId = calendarId,
                    WorkedHours = workedHours,
                };

                myDbContext.WorkerCalendar.Add(workerCalendar);
                myDbContext.SaveChanges();
            }
        }

        #endregion

        #region Редактирование

        /// <summary>
        /// Редактировать.
        /// </summary>
        /// <param name="workerId">Идентификатор сотрудника.</param>
        /// <param name="month">Месяц.</param>
        /// <param name="workedHours">Количество отработанных часов.</param>
        public static void Update(int workerId, string month, int workedHours)
        {
            var query = from wc in myDbContext.WorkerCalendar
                        where wc.Calendar.Month == month && wc.Worker.WorkerId == workerId
                        select wc;

            foreach (WorkerCalendar workerCalendar in query)
            {
                workerCalendar.WorkedHours = workedHours;
            }

            myDbContext.SaveChanges();
        }

        #endregion

        #region Удаление.

        /// <summary>
        /// Удалить.
        /// </summary>
        /// <param name="workerCalendarId">Идентификатор.</param>
        public static void Delete(int workerCalendarId)
        {
            WorkerCalendar workerCalendar = myDbContext.WorkerCalendar.Where(wc => wc.WorkerCalendarId == workerCalendarId).FirstOrDefault();
            myDbContext.WorkerCalendar.Remove(workerCalendar);
            myDbContext.SaveChanges();
        }

        #endregion


        /// <summary>
        /// Фильтровать по месяцу календаря.
        /// </summary>
        /// <param name="month">Наименование месяца.</param>
        /// <param name="branchCity">Город филиала.</param>
        /// <returns>Коллекция сотрудников.</returns>
        public static List<WorkerCalendar> FilterByMonth(string month)
        {
            List<WorkerCalendar> workerCalendarList = new List<WorkerCalendar>();

            List<WorkerCalendar> query = (from wc in myDbContext.WorkerCalendar
                                          where wc.Calendar.Month == month
                                          select wc).ToList();

            foreach (WorkerCalendar wc in query)
            {
                if (workerCalendarList.Contains(wc))
                { }
                else
                    workerCalendarList.Add(wc);    // добавляем в список каждую запись только один раз
            }

            return workerCalendarList;
        }

        /// <summary>
        /// Фильтровать по месяцу календаря, городу филиала и наименованию подразделению.
        /// </summary>
        /// <param name="month">Наименование месяца.</param>
        /// <param name="branchCity">Город филиала.</param>
        /// <param name="subunitName">Наименование подразделения.</param>
        /// <returns>Коллекция сотрудников.</returns>
        public static List<WorkerCalendar> FilterByMonthBranchSubunit(string month, string branchCity, string subunitName)
        {
            List<WorkerCalendar> workerCalendarList = new List<WorkerCalendar>();

            List<WorkerCalendar> query = (from wc in myDbContext.WorkerCalendar
                                          where wc.Worker.Branch.City == branchCity && wc.Worker.Subunit.Name == subunitName && wc.Calendar.Month == month
                                          select wc).ToList();

            foreach (WorkerCalendar wc in query)
            {
                if (workerCalendarList.Contains(wc))
                { }
                else
                    workerCalendarList.Add(wc);    // добавляем в список каждую запись только один раз
            }

            return workerCalendarList;

        }

        /// <summary>
        /// Получить расчетный лист.
        /// </summary>
        public static List<string> GetPaySheet(string month, string branchCity, string subunitName)
        {
            string row;
            string fullname;
            int workedHours;
            double wage;
            List<string> list = new List<string>(); // пригодится для сортировки
            Dictionary<string, double> wcDictionary = new Dictionary<string, double>();
            List<WorkerCalendar> workerCalendarList = FilterByMonthBranchSubunit(month, branchCity, subunitName);

            // заполняем словарь
            foreach (WorkerCalendar wc in workerCalendarList)
            {
                fullname = wc.Worker.Fullname;
                month = wc.Calendar.Month;
                workedHours = wc.WorkedHours;
                wage = Worker.Payroll(wc.Worker.WorkerId, month, workedHours);
                wcDictionary.Add(fullname, wage);
            }

            // заполняем список
            foreach (KeyValuePair<string, double> keyValuePair in wcDictionary)
            {
                row = $"{keyValuePair.Key}: {keyValuePair.Value} руб";
                list.Add(row);
            }
            list.Sort();
            return list;
        }

        /// <summary>
        /// Получить список городов филиалов с указанием средней зарплаты в филиале.
        /// </summary>
        /// <param name="month">Наименование месяца.</param>
        /// <returns>Список городов филиалов со средней зарплатой.</returns>
        public static List<string> GetBranchesWithMidWage(string month)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                string row;
                double sumWage = 0;
                double midWage;
                List<string> list = new List<string>(); // пригодится для сортировки
                Dictionary<string, double> dictionary = new Dictionary<string, double>();
                List<WorkerCalendar> wcList = FilterByMonth(month);
                List<string> listCities = new List<string>();

                // находим все города
                var queryCities = from b in myDbContext.Branches
                                  select b.City;

                // заполняем список городов и общую сумму заработка среди всех сотрудников (в указанный месяц)
                foreach (string city in queryCities)
                {
                    var query = from wc in myDbContext.WorkerCalendar
                                where wc.Worker.Branch.City == city && wc.Calendar.Month == month
                                select wc;
                    foreach (WorkerCalendar wc in query)
                    {
                        sumWage = Worker.Payroll(wc.Worker.WorkerId, month, wc.WorkedHours);

                        if (!dictionary.ContainsKey(city))
                            dictionary.Add(city, sumWage);  // если указанный город филиала еще не внесен в список, то вносим
                                                            // город и заработок сотрудника в словарь
                        else dictionary[city] += sumWage;   // иначе добавляем в указанный город заработок сотрудника
                    }
                }

                // заполняем список
                foreach (KeyValuePair<string, double> keyValuePair in dictionary)
                {
                    // определяем среднюю зарплату в каждом филиале
                    midWage = keyValuePair.Value / (Branch.GetBranchByCity(keyValuePair.Key).Workers.Count);
                    midWage = Math.Round(midWage, 0); // ограничиваем количество знаков после запятой
                    row = $"{keyValuePair.Key}: {midWage} руб"; // формируем строку в виде 'Филиал: средняя зарплата'
                    list.Add(row);
                }
                list.Sort();
                return list;
            }
        }

        /// <summary>
        /// Получить список ФИО сотрудников, размер зарплаты которых больше указанного.
        /// </summary>
        /// <param name="month">Наименование месяца.</param>
        /// <param name="value">Значение X.</param>
        /// <returns>Список сотрудников.</returns>
        public static List<string> GetWorkersListOne(string month, double value)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                double wage;
                List<string> workerFullnames = new List<string>();

                try
                {
                    var query = from wc in myDbContext.WorkerCalendar
                                where wc.Calendar.Month == month
                                select wc;

                    foreach (WorkerCalendar wc in query)
                    {
                        wage = Worker.Payroll(wc.Worker.WorkerId, month, wc.WorkedHours);
                        if (wage > value)
                        {
                            workerFullnames.Add(wc.Worker.Fullname);
                        }
                    }
                    workerFullnames.Sort();
                }
                catch 
                { }

                return workerFullnames;
            }
        }

        /// <summary>
        /// Получить список ФИО сотрудников с фиксированный оплатой, которые отработали требуемые часы в указанный месяц.
        /// </summary>
        /// <param name="month">Наименование месяца.</param>
        /// <returns>Список сотрудников.</returns>
        public static List<string> GetWorkersListTwo(string month)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                List<string> workerFullnames = new List<string>();

                var query = from wc in myDbContext.WorkerCalendar
                            where wc.Calendar.Month == month
                            && wc.Worker.PaymentType == "Фиксированная ежемесячная оплата"
                            && wc.WorkedHours >= wc.Calendar.Hours
                            select wc;   // получаем список календарей сотрудников

                foreach (WorkerCalendar wc in query)
                {
                    workerFullnames.Add(wc.Worker.Fullname);
                }
                workerFullnames.Sort();

                return workerFullnames;
            }
        }

        /// <summary>
        /// Получить список определенного количества сотрудников с наибольшим размером зарплаты в указанном месяце.
        /// </summary>
        /// <param name="month">Наименование месяца.</param>
        /// <param name="count">Количество сотрудников.</param>
        /// <returns>Список сотрудников.</returns>
        public static List<string> GetWorkersRating(string month, int count)
        {
            string row;
            double wage = 0;
            List<string> list = new List<string>(); // пригодится для сортировки
            Dictionary<string, double> dictionary = new Dictionary<string, double>();
            List<string> filterList = new List<string>();
            List<WorkerCalendar> wcList = FilterByMonth(month);

            foreach (WorkerCalendar wc in wcList)
            {
                wage = Worker.Payroll(wc.Worker.WorkerId, wc.Calendar.Month, wc.WorkedHours);   // определяем зарплату для каждого сотрудника
                wage = Math.Round(wage, 1); // ограничиваем количество знаков после запятой

                if (!dictionary.ContainsKey(wc.Worker.Fullname))
                    dictionary.Add(wc.Worker.Fullname, wage);  // если указанный сотрудник еще не внесен в список, то вносим
                                                               // его ФИО и заработок за указанный месяц
            }

            // сортируем сотрудников по заработной плате
            // и заполняем общий список сотрудников
            foreach (KeyValuePair<string, double> keyValuePair in dictionary.OrderByDescending(value => value.Value))
            {
                row = $"{keyValuePair.Key}: {keyValuePair.Value} руб"; // формируем строку в виде 'Сотрудник: заработная плата'
                list.Add(row);
            }

            // отбираем из общего списка только указанное количество сотрудников
            for (int i = 0; i < count; i++)
            {
                filterList.Add(list.First());
                list.Remove(list.First());
            }

            return filterList;
        }
    }
}