using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Data;
using System.Linq;
using System;

namespace PayrollSys.BL.Model
{
    /// <summary>
    /// Сотрудник.
    /// </summary>
    public class Worker : INotifyPropertyChanged
    {
        string position;
        int branchId;
        int subunitId;
        string paymentType;
        double salary;
        static readonly MyDbContext myDbContext = new MyDbContext();

        /// <summary>
        /// Идентификатор сотрудника.
        /// </summary>
        public int WorkerId { get; set; }

        /// <summary>
        /// Полное имя.
        /// </summary>
        [MaxLength(100)]
        public string Fullname { get; set; }

        /// <summary>
        /// Должность.
        /// </summary>
        [MaxLength(50)]
        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                OnPropertyChanged("Position");
            }
        }

        /// <summary>
        /// Идентификатор филиала.
        /// </summary>
        public int BranchId
        {
            get { return branchId; }
            set
            {
                branchId = value;
                OnPropertyChanged("BranchId");
            }
        }

        /// <summary>
        /// Идентификатор подразделения.
        /// </summary>
        public int SubunitId
        {
            get { return subunitId; }
            set
            {
                subunitId = value;
                OnPropertyChanged("SubunitId");
            }
        }

        /// <summary>
        /// Тип оплаты труда.
        /// </summary>
        [MaxLength(50)]
        public string PaymentType
        {
            get { return paymentType; }
            set
            {
                paymentType = value;
                OnPropertyChanged("PaymentType");
            }
        }

        /// <summary>
        /// Размер оплаты.
        /// </summary>
        public double Salary
        {
            get { return salary; }
            set
            {
                salary = value;
                OnPropertyChanged("Salary");
            }
        }

        /// <summary>
        /// Филиал.
        /// </summary>
        public virtual Branch Branch { get; set; }

        /// <summary>
        /// Подразделение.
        /// </summary>
        public virtual Subunit Subunit { get; set; }

        /// <summary>
        /// Реализация интерфейса INotifyPropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Получить коллекцию сотрудников.
        /// </summary>
        /// <returns>Коллекция сотрудников.</returns>
        public static List<Worker> GetWorkers()
        {
            List<Worker> workers = new List<Worker>(myDbContext.Workers);
            return workers;
        }

        /// <summary>
        /// Получить коллекцию сотрудников.
        /// </summary>
        /// <param name="branchId">Полное имя.</param>
        public static int GetWorkersCountByBranchId(int branchId)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                int count = 0;

                var query = from w in myDbContext.Workers
                            where w.BranchId == branchId
                            select w;
                foreach (Worker worker in query)
                    count++;

                return count;
            }
        }

        #region Добавление

        /// <summary>
        /// Добавить.
        /// </summary>
        /// <param name="fullname">Полное имя.</param>
        /// <param name="position">Должность.</param>
        /// <param name="branchId">Идентификатор филиала.</param>
        /// <param name="subunitId">Идентификатор подразделения.</param>
        /// <param name="paymentType">Тип оплаты труда.</param>
        /// <param name="salary">Размер оплаты.</param>
        public static void Insert(string fullname, string position, int branchId, int subunitId, string paymentType, int salary)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                Worker worker = new Worker
                {
                    Fullname = fullname,
                    Position = position,
                    BranchId = branchId,
                    SubunitId = subunitId,
                    PaymentType = paymentType,
                    Salary = salary
                };

                myDbContext.Workers.Add(worker);
                myDbContext.SaveChanges();
            }
        }

        #endregion

        #region Редактирование

        /// <summary>
        /// Редактировать.
        /// </summary>
        /// <param name="fullname">Полное имя.</param>
        /// <param name="position">Должность.</param>
        /// <param name="branchId">Идентификатор филиала.</param>
        /// <param name="subunitId">Идентификатор подразделения.</param>
        /// <param name="paymentType">Тип оплаты труда.</param>
        /// <param name="salary">Размер оплаты.</param>
        public static void Update(int workerId, string fullname, string position, int branchId, int subunitId, string paymentType, int salary)
        {
            var query = from w in myDbContext.Workers
                        where w.WorkerId == workerId
                        select w;

            foreach (Worker worker in query)
            {
                worker.Fullname = fullname;
                worker.Position = position;
                worker.BranchId = branchId;
                worker.SubunitId = subunitId;
                worker.PaymentType = paymentType;
                worker.Salary = salary;
            }
            myDbContext.SaveChanges();
        }

        #endregion

        #region Удаление

        /// <summary>
        /// Удалить.
        /// </summary>
        /// <param name="workerId">Идентификатор сотрудника.</param>
        public static void Delete(int workerId)
        {
            Worker worker = myDbContext.Workers.Where(w => w.WorkerId == workerId).FirstOrDefault();
            myDbContext.Workers.Remove(worker);
            myDbContext.SaveChanges();
        }

        #endregion

        #region Фильтр

        /// <summary>
        /// Получить сотрудника по его идентификатору.
        /// </summary>
        /// <param name="workerId">Идентификатор сотрудника.</param>
        /// <returns>Сотрудник.</returns>
        public static Worker GetWorkerByWorkerId(int workerId)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                Worker worker = new Worker();

                var query = from w in myDbContext.Workers
                            where w.WorkerId == workerId
                            select w;

                foreach (Worker w in query)
                    worker = w;

                return worker;
            }
        }

        /// <summary>
        /// Получить сотрудника по его полному имени.
        /// </summary>
        /// <param name="fullname">Полное имя.</param>
        /// <returns>Сотрудник.</returns>
        public static Worker GetWorkerByFullname(string fullname)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                Worker worker = new Worker();

                var query = from w in myDbContext.Workers
                            where w.Fullname == fullname
                            select w;

                foreach (Worker w in query)
                    worker = w;

                return worker;
            }
        }

        #endregion

        /// <summary>
        /// Расчитать заработную плату.
        /// </summary>
        /// <param name="workerId">Идентификатор сотрудника.</param>
        /// <param name="month">Наименование месяца.</param>
        /// <param name="workedHours">План. часы по трудовому календарю.</param>
        /// <returns>Заработная плата.</returns>
        public static double Payroll(int workerId, string month, int workedHours)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                int hours = Calendar.GetCalendarByMonth(month).Hours;
                double wage;

                Worker worker = new Worker();
                var query = from w in myDbContext.Workers
                            where w.WorkerId == workerId
                            select w;
                foreach (Worker w in query)
                    worker = w;

                if (worker.PaymentType == "Почасовая оплата (сдельная)")
                {
                    wage = worker.Salary * workedHours;
                }
                else
                {
                    wage = worker.Salary / hours * workedHours;
                }

                wage = Math.Round(wage, 2); // ограничиваем количество знаков после запятой
                return wage;
            }
        }

        /// <summary>
        /// Проверка на изменение значений.
        /// </summary>
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}