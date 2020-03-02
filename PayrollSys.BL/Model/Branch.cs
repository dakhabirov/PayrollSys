using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Linq;
using System.Windows;

namespace PayrollSys.BL.Model
{
    /// <summary>
    /// Отдел.
    /// </summary>
    public class Branch : INotifyPropertyChanged
    {
        static readonly MyDbContext myDbContext = new MyDbContext();
        readonly ValueController valueController = new ValueController();

        string city;

        /// <summary>
        /// Идентификатор филиала.
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// Город.
        /// </summary>
        public string City
        {
            get { return city; }
            set
            {
                city = value;
                OnPropertyChanged("City");
            }
        }

        /// <summary>
        /// Коллекция сотрудников.
        /// </summary>
        public virtual ICollection<Worker> Workers { get; set; }

        /// <summary>
        /// Коллекция подразделений.
        /// </summary>
        public virtual ICollection<BranchSubunit> BranchSubunits { get; set; }

        /// <summary>
        /// Получить список филиалов.
        /// </summary>
        /// <returns>Список филиалов.</returns>
        public static List<Branch> GetBranches()
        {
            List<Branch> branches = new List<Branch>(myDbContext.Branches);
            return branches;
        }

        /// <summary>
        /// Получить список городов филиалов.
        /// </summary>
        /// <returns>Список городов филиалов.</returns>
        public static List<string> GetCities()
        {
            List<string> Cities = new List<string>();

            var query = from b in myDbContext.Branches
                        select b;

            foreach (Branch branch in query)
                Cities.Add(branch.City);

            return Cities;
        }

        #region Добавление

        /// <summary>
        /// Добавить.
        /// </summary>
        /// <param name="city">Город.</param>
        public void Insert(string city)
        {
            if (city != "")
            {
                Branch branch = new Branch
                {
                    City = city,
                };
                // проверяем на уникальность
                if (valueController.CheckDuplicateBranch(branch) == true)
                {
                    MessageBox.Show($"Филиал в городе {city} уже зарегистрирован!");
                }
                else
                {
                    myDbContext.Branches.Add(branch);
                    myDbContext.SaveChanges();
                    MessageBox.Show($"Филиал в городе {city} успешно добавлен в базу данных.", "Сообщение");
                }
            }
            else
            {
                MessageBox.Show("Проверьте корректность введенных данных!", "Ошибка");
            }
        }

        #endregion

        #region Удаление.

        /// <summary>
        /// Удалить.
        /// </summary>
        /// <param name="branchId">Идентификатор филиала.</param>
        public static void Delete(int branchId)
        {
            Branch branch = myDbContext.Branches.Where(b => b.BranchId == branchId).FirstOrDefault();
            myDbContext.Branches.Remove(branch);
            myDbContext.SaveChanges();
        }

        #endregion

        /// <summary>
        /// Получить информацию о филиале по его городу.
        /// </summary>
        /// <param name="city">Город.</param>
        /// <returns>Город.</returns>
        public static Branch GetBranchByCity(string city)
        {
            Branch branch = new Branch();
            try
            {
                var query = from b in myDbContext.Branches
                            where b.City == city
                            select b;

                foreach (Branch b in query)
                    branch = b;
            }
            catch 
            { }
            return branch;
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