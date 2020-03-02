using System.Collections.Generic;
using System.ComponentModel;
using PayrollSys.BL.Model;

namespace PayrollSys.BL.ViewModel
{
    public class BranchViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Выбранный филиал.
        /// </summary>
        Branch selectedBranch;

        /// <summary>
        /// Коллекция филиалов.
        /// </summary>
        public ICollection<Branch> Branches { get; set; }

        public BranchViewModel()
        {
            Branches = Branch.GetBranches();
        }

        /// <summary>
        /// Выбранный филиал.
        /// </summary>
        public Branch SelectedBranch
        {
            get { return selectedBranch; }
            set
            {
                selectedBranch = value;
                OnPropertyChanged("SelectedBranch");
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