using System.Collections.Generic;
using System.Data.Entity;

namespace PayrollSys.BL.Model
{
    class DbContextInitializer : DropCreateDatabaseIfModelChanges<MyDbContext>
    {
        /// <summary>
        /// Инициализация базы данных.
        /// </summary>
        /// <param name="dbContext">База данных.</param>
        protected override void Seed(MyDbContext dbContext)
        {
            // Заполняем календарь
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            dictionary.Add("Январь", 150);
            dictionary.Add("Февраль", 150);
            dictionary.Add("Март", 150);
            dictionary.Add("Апрель", 150);
            dictionary.Add("Май", 150);
            dictionary.Add("Июнь", 150);
            dictionary.Add("Июль", 150);
            dictionary.Add("Август", 150);
            dictionary.Add("Сентябрь", 150);
            dictionary.Add("Октябрь", 150);
            dictionary.Add("Ноябрь", 150);
            dictionary.Add("Декабрь", 150);
            foreach (KeyValuePair<string, int> kvp in dictionary)
            {
                Calendar.Insert(kvp.Key, kvp.Value);
            }
            dbContext.SaveChanges();
        }
    }
}