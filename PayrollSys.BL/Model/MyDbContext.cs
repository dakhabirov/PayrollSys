using System.Data.Entity;
using PayrollSys.BL.Model;

namespace PayrollSys.BL
{
    /// <summary>
    /// Контекст базы данных.
    /// </summary>
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("PayrollSys")
        {
            // Инициализирует базу данных и указывает EF, что если модель изменилась, то воссоздать новую
            Database.SetInitializer(new DbContextInitializer());
        }

        /// <summary>
        /// Сотрудники.
        /// </summary>
        public DbSet<Worker> Workers { get; set; }

        /// <summary>
        /// Филиалы.
        /// </summary>
        public DbSet<Branch> Branches { get; set; }

        /// <summary>
        /// Подразделения.
        /// </summary>
        public DbSet<Subunit> Subunits { get; set; }

        /// <summary>
        /// Календарь.
        /// </summary>
        public DbSet<Calendar> Calendar { get; set; }

        /// <summary>
        /// Календарь сотрудников.
        /// </summary>
        public DbSet<WorkerCalendar> WorkerCalendar { get; set; }

        /// <summary>
        /// Связующая (промежуточная) таблицу.
        /// </summary>
        public DbSet<BranchSubunit> BranchSubunit { get; set; }
    }
}