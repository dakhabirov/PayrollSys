using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Threading.Tasks;

namespace PayrollSys.BL.Model
{
    /// <summary>
    /// Инициализатор случайных значений для базы данных.
    /// </summary>
    public class Initializer
    {
        readonly MyDbContext myDbContext = new MyDbContext();
        readonly Random rnd = new Random();
        readonly ValueController valueController = new ValueController();

        #region Заполнение таблиц.

        #region Заполнить таблицу сотрудников.

        /// <summary>
        /// Заполнить таблицу сотрудников.
        /// </summary>
        /// <param name="count">Количество записей.</param>
        public async System.Threading.Tasks.Task FillWorkersAsync(int count)
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                var workers = myDbContext.Workers;    // таблица сотрудников

                while (count > 0)
                {
                    Worker worker = await RandomWorkerAsync();  // получаем случайного сотрудника
                    int workerId = worker.WorkerId;    // идентификатор этого сотрудника

                    // проверяем данные сотрудника на уникальность
                    if (valueController.CheckDuplicateWorker(worker) == true)
                    {
                        MessageBox.Show($"Пользователь с идентификатором {workerId} уже зарегистрирован!");
                    }
                    else
                    {
                        workers.Add(worker);
                        myDbContext.SaveChanges();
                    }
                    count--;
                }
            }
        }

        #endregion

        #region Заполнить таблицу филиалов.

        /// <summary>
        /// Заполнить таблицу филиалов.
        /// </summary>
        public async Task FillBranchesAsync()
        {
            var branches = myDbContext.Branches;  // список всех филиалов из базы данных
            Branch branch = new Branch();

            string pathCities = @"..\..\..\Resourses\Branch\Cities.txt";
            string readedCity;

            try
            {
                using (StreamReader sr = new StreamReader(pathCities, System.Text.Encoding.UTF8))
                {
                    while ((readedCity = await sr.ReadLineAsync()) != null)
                    {
                        branch.City = readedCity;

                        // проверяем город на уникальность
                        if (valueController.CheckDuplicateBranch(branch) == true)
                        {
                            MessageBox.Show($"Филиал в городе {readedCity} уже зарегистрирован!");
                        }
                        else
                        {
                            branches.Add(branch);
                            myDbContext.SaveChanges();
                        }
                    }
                    readedCity = null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion

        #region Заполнить таблицу подразделений.

        /// <summary>
        /// Заполнить таблицу подразделений.
        /// </summary>
        public async Task FillSubunitsAsync()
        {
            var subunits = myDbContext.Subunits;  // список всех подразделений из базы данных
            Subunit subunit = new Subunit();

            string pathNames = @"..\..\..\Resourses\Subunit\Names.txt";
            string readedName;

            try
            {
                using (StreamReader sr = new StreamReader(pathNames, System.Text.Encoding.UTF8))
                {
                    while ((readedName = await sr.ReadLineAsync()) != null)
                    {
                        subunit.Name = readedName;

                        // проверяем наименование на уникальность
                        if (valueController.CheckDuplicateSubunit(subunit) == true)
                        {
                            MessageBox.Show($"Подразделение с наименованием {readedName} уже зарегистрировано!");
                        }
                        else
                        {
                            subunits.Add(subunit);
                            myDbContext.SaveChanges();
                        }
                    }
                    readedName = null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion



        #endregion

        #region Генерация записей.

        #region Сгенерировать сотрудника.

        /// <summary>
        /// Сгенерировать сотрудника.
        /// </summary>
        /// <returns>Сотрудник.</returns>
        protected async System.Threading.Tasks.Task<Worker> RandomWorkerAsync()
        {
            Worker worker = new Worker();

            string pathFirstnames = @"..\..\..\Resourses\Worker\Firstnames.txt";
            string pathSurnames = @"..\..\..\Resourses\Worker\Surnames.txt";
            string pathPatronymics = @"..\..\..\Resourses\Worker\Patronymics.txt";
            string pathPositions = @"..\..\..\Resourses\Worker\Positions.txt";
            string pathSalaries = @"..\..\..\Resourses\Worker\Salaries.txt";
            string pathPaymentTypes = @"..\..\..\Resourses\Worker\PaymentTypes.txt";

            string line;
            List<string> firstnames = new List<string>(); // список имен
            List<string> surnames = new List<string>(); // список фамилий
            List<string> patronymics = new List<string>(); // список отчеств
            List<string> positions = new List<string>(); // список должностей
            List<int> branchIds = new List<int>(); // список филиалов
            List<int> subunitIds = new List<int>(); // список подразделений
            List<string> paymentTypes = new List<string>(); // список типов оплаты труда
            List<string> salaries = new List<string>(); // список размеров оплаты

            try
            {
                // заполняем список филиалов
                foreach (Branch branch in myDbContext.Branches)
                    branchIds.Add(branch.BranchId);
                // заполняем список подразделений
                foreach (Subunit subunit in myDbContext.Subunits)
                    subunitIds.Add(subunit.SubunitId);

                using (StreamReader sr = new StreamReader(pathSurnames, System.Text.Encoding.Default))
                {
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        surnames.Add(line);
                    }
                    line = null;
                }

                using (StreamReader sr = new StreamReader(pathFirstnames, System.Text.Encoding.Default))
                {
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        firstnames.Add(line);
                    }
                    line = null;
                }

                using (StreamReader sr = new StreamReader(pathPatronymics, System.Text.Encoding.Default))
                {
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        patronymics.Add(line);
                    }
                    line = null;
                }

                using (StreamReader sr = new StreamReader(pathPositions, System.Text.Encoding.UTF8))
                {
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        positions.Add(line);
                    }
                    line = null;
                }

                using (StreamReader sr = new StreamReader(pathPaymentTypes, System.Text.Encoding.UTF8))
                {
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        paymentTypes.Add(line);
                    }
                    line = null;
                }

                using (StreamReader sr = new StreamReader(pathSalaries, System.Text.Encoding.Default))
                {
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        salaries.Add(line);
                    }
                    line = null;
                }

                string rndSurname = surnames[rnd.Next(surnames.Count)]; // определяем случайную фамилию
                string rndFirstname = firstnames[rnd.Next(firstnames.Count)];   // определяем случайное имя
                string rndPatronymic = patronymics[rnd.Next(patronymics.Count)];    // определяем случайное отчество
                string rndPosition = positions[rnd.Next(positions.Count)];    // определяем случайную должность
                int rndBranchId = branchIds[rnd.Next(branchIds.Count)]; // определяем случайный филиал
                int rndSubunitId = subunitIds[rnd.Next(subunitIds.Count)]; // определяем случайное подразделение
                string rndPaymentType = paymentTypes[rnd.Next(paymentTypes.Count)]; // определяем случайный тип оплаты труда
                int rndSalary = Convert.ToInt32(salaries[rnd.Next(salaries.Count)]);    // определяем случайный размер оплаты

                worker.Fullname = string.Format("{0} {1} {2}", rndSurname, rndFirstname, rndPatronymic); // назначаем полное имя
                worker.Position = rndPosition;  // назначаем случайную должность
                worker.BranchId = rndBranchId;  // назначаем случайный филиал
                worker.SubunitId = rndSubunitId;  // назначаем случайное подразделение
                worker.PaymentType = rndPaymentType;  // назначаем случайный тип оплаты труда
                worker.Salary = rndSalary;  // назначаем случайный размер оплаты
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return worker;
        }

        #endregion.

        #region Сгенерировать филиал.

        /// <summary>
        /// Сгенерировать филиал.
        /// </summary>
        /// <returns>Филиал.</returns>
        protected async System.Threading.Tasks.Task<Branch> RandomBranchAsync()
        {
            Branch branch = new Branch();

            string pathCities = @"..\..\..\Resourses\Branch\Cities.txt";

            string line;
            List<string> list = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(pathCities, System.Text.Encoding.Default))
                {
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        list.Add(line);
                    }
                    branch.City = list[rnd.Next(list.Count)]; // назначаем случайный город
                    line = null;
                    list.Clear();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return branch;
        }

        #endregion

        #region Сгенерировать подразделение.

        /// <summary>
        /// Сгенерировать подразделение.
        /// </summary>
        /// <returns>Подразделение.</returns>
        protected async System.Threading.Tasks.Task<Subunit> RandomSubunitAsync()
        {
            Subunit subunit = new Subunit();

            string pathSubunits = @"..\..\..\Resourses\Subunit\Names.txt";

            string line;
            List<string> names = new List<string>();
            List<int> branchIds = new List<int>(); // список филиалов

            try
            {
                // заполняем список филиалов
                foreach (Branch branch in myDbContext.Branches)
                    branchIds.Add(branch.BranchId);

                using (StreamReader sr = new StreamReader(pathSubunits, System.Text.Encoding.Default))
                {
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        names.Add(line);
                    }
                    line = null;
                }

                string rndName = names[rnd.Next(names.Count)]; // определяем случайное наименование
                subunit.Name = names[rnd.Next(names.Count)]; // назначаем случайное наименование
                names.Clear();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return subunit;
        }

        #endregion

        #endregion

        #region Заполнить календарь сотрудников.

        /// <summary>
        /// Заполнить календарь сотрудников.
        /// </summary>
        public void FillWorkerCalendar()
        {
            using (MyDbContext myDbContext = new MyDbContext())
            {
                List<int> workerIds = new List<int>(); // список сотрудников
                List<int> calendarIds = new List<int>(); // список календарей

                try
                {
                    // заполняем список сотрудников
                    foreach (Worker worker in myDbContext.Workers)
                        workerIds.Add(worker.WorkerId);
                    // заполняем список календарей
                    foreach (Calendar calendar in myDbContext.Calendar)
                        calendarIds.Add(calendar.CalendarId);

                    for (int i = 0; i < calendarIds.Count; i++)
                        for (int j = 0; j < workerIds.Count; j++)
                            WorkerCalendar.Insert(workerIds[j], calendarIds[i], 150);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        #endregion.

        #region Заполнить связующую (промежуточную) таблицу филиалов и подразделений.

        /// <summary>
        /// Заполнить связующую (промежуточную) таблицу филиалов и подразделений.
        /// </summary>
        public void FillBranchSubunit()
        {
            BranchSubunit branchSubunit = new BranchSubunit();
            List<int> branchIds = new List<int>(); // список филиалов
            List<int> subunitIds = new List<int>(); // список подразделений

            try
            {
                // заполняем список филиалов
                foreach (Branch branch in myDbContext.Branches)
                    branchIds.Add(branch.BranchId);
                // заполняем список подразделений
                foreach (Subunit subunit in myDbContext.Subunits)
                    subunitIds.Add(subunit.SubunitId);

                for (int i = 0; i < subunitIds.Count; i++)
                    for (int j = 0; j < branchIds.Count; j++)
                        branchSubunit.Insert(branchIds[j], subunitIds[i]);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #endregion.
    }
}