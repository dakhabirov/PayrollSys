using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PayrollSys.BL.Model
{
    /// <summary>
    /// Связующая (промежуточная) таблица.
    /// </summary>
    public class BranchSubunit
    {
        static readonly MyDbContext myDbContext = new MyDbContext();
        readonly ValueController valueController = new ValueController();

        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int BranchSubunitId { get; set; }

        /// <summary>
        /// Идентификатор филиала.
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// Идентификатор подразделения.
        /// </summary>
        public int SubunitId { get; set; }

        /// <summary>
        /// Филиал. 
        /// </summary>
        public virtual Branch Branch { get; set; }

        /// <summary>
        /// Подразделение. 
        /// </summary>
        public virtual Subunit Subunit { get; set; }

        /// <summary>
        /// Получить связующую (промежуточную) таблицу.
        /// </summary>
        /// <returns>Связующая (промежуточная) таблица.</returns>
        public static ICollection<BranchSubunit> GetBranchSubunit()
        {
            List<BranchSubunit> branchSubunit = new List<BranchSubunit>(myDbContext.BranchSubunit);
            return branchSubunit;
        }

        /// <summary>
        /// Получить представление коллекции.
        /// </summary>
        /// <returns>Представление коллекции.</returns>
        private static ICollectionView GetCollectionView()
        {
            var workers = GetBranchSubunit(); // вся коллекция, без фильтра
            ICollectionView branchSubunitCollectionView;
            branchSubunitCollectionView = CollectionViewSource.GetDefaultView(workers);   // представляем всю коллекцию
            return branchSubunitCollectionView;
        }

        #region Добавление

        /// <summary>
        /// Добавить.
        /// </summary>
        /// <param name="branchId">Идентификатор филиала.</param>
        /// <param name="subunitId">Идентификатор подразделения.</param>
        public void Insert(int branchId, int subunitId)
        {
            BranchSubunit branchSubunit = new BranchSubunit
            {
                BranchId = branchId,
                SubunitId = subunitId,
            };

            // проверяем на уникальность
            if (valueController.CheckDuplicateBranchSubunit(branchSubunit) == true)
            {
                MessageBox.Show($"Данное подразделение уже имеется в филиале!", "Ошибка");
            }
            else
            {
                myDbContext.BranchSubunit.Add(branchSubunit);
                myDbContext.SaveChanges();
            }
        }

        #endregion

        #region Удаление.

        /// <summary>
        /// Удалить.
        /// </summary>
        /// <param name="branchSubunitId">Идентификатор.</param>
        public static void Delete(int branchSubunitId)
        {
            BranchSubunit branchSubunit = myDbContext.BranchSubunit.Where(bs => bs.BranchSubunitId == branchSubunitId).FirstOrDefault();
            myDbContext.BranchSubunit.Remove(branchSubunit);
            myDbContext.SaveChanges();
        }

        #endregion
    }
}