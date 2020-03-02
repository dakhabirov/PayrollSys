using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Linq;
using System.Windows;

namespace PayrollSys.BL.Model
{
    /// <summary>
    /// Подразделение.
    /// </summary>
    public class Subunit : INotifyPropertyChanged
    {
        static readonly MyDbContext myDbContext = new MyDbContext();

        string name;

        /// <summary>
        /// Идентификатор подразделения.
        /// </summary>
        public int SubunitId { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Коллекция сотрудников.
        /// </summary>
        public virtual ICollection<Worker> Workers { get; set; }

        /// <summary>
        /// Получить список подразделений.
        /// </summary>
        /// <returns>Список подразделений.</returns>
        public static List<Subunit> GetSubunits()
        {
            List<Subunit> subunits = new List<Subunit>(myDbContext.Subunits);
            return subunits;
        }

        /// <summary>
        /// Получить представление коллекции.
        /// </summary>
        /// <returns>Представление коллекции.</returns>
        private static ICollectionView GetCollectionView()
        {
            var subunits = GetSubunits(); // вся коллекция, без фильтра
            ICollectionView subunitsCollectionView;
            subunitsCollectionView = CollectionViewSource.GetDefaultView(subunits);   // представляем всю коллекцию
            return subunitsCollectionView;
        }

        /// <summary>
        /// Получить список наименований.
        /// </summary>
        /// <returns>Список наименований.</returns>
        public static List<string> GetNames()
        {
            using (var myDbContext = new MyDbContext())
            {
                List<string> Names = new List<string>();

                var query = from s in myDbContext.Subunits
                            select s;

                foreach (Subunit subunit in query)
                    Names.Add(subunit.Name);

                return Names;
            }
        }

        #region Добавление

        /// <summary>
        /// Добавить.
        /// </summary>
        /// <param name="name">Наименование.</param>
        public static void Insert(string name)
        {
            using (var myDbContext = new MyDbContext())
            {
                if (name != "")
                {
                    Subunit subunit = new Subunit
                    {
                        Name = name,
                    };

                    myDbContext.Subunits.Add(subunit);
                    myDbContext.SaveChanges();
                    MessageBox.Show($"Подразделение с наименованием {name} успешно добавлено в базу данных.", "Сообщение");
                }
                else
                {
                    MessageBox.Show("Проверьте корректность введенных данных!", "Ошибка");
                }
            }
        }

        #endregion

        #region Удаление.

        /// <summary>
        /// Удалить.
        /// </summary>
        /// <param name="subunitId">Идентификатор подразделения.</param>
        public static void Delete(int subunitId)
        {
            Subunit subunit = myDbContext.Subunits.Where(s => s.SubunitId == subunitId).FirstOrDefault();
            myDbContext.Subunits.Remove(subunit);
            myDbContext.SaveChanges();
        }

        #endregion

        /// <summary>
        /// Получить информацию о подразделении по его наименованию.
        /// </summary>
        /// <param name="name">Наименование.</param>
        /// <returns>Наименование.</returns>
        public static Subunit GetSubunitByName(string name)
        {
            using (var myDbContext = new MyDbContext())
            {
                Subunit subunit = new Subunit();

                var query = from s in myDbContext.Subunits
                            where s.Name == name
                            select s;

                foreach (Subunit s in query)
                    subunit = s;

                return subunit;
            }
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