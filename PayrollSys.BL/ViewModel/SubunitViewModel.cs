using System.Collections.Generic;
using System.ComponentModel;
using PayrollSys.BL.Model;

namespace PayrollSys.BL.ViewModel
{
    public class SubunitViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Выбранное подразделение.
        /// </summary>
        Subunit selectedSubunit;

        /// <summary>
        /// Коллекция подразделений.
        /// </summary>
        public ICollection<Subunit> Subunits { get; set; }

        public SubunitViewModel()
        {
            Subunits = Subunit.GetSubunits();
        }

        /// <summary>
        /// Выбранное подразделение.
        /// </summary>
        public Subunit SelectedSubunit
        {
            get { return selectedSubunit; }
            set
            {
                selectedSubunit = value;
                OnPropertyChanged("SelectedSubunit");
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