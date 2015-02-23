using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexMvvmExample.ViewModel
{
    public class Class3 : INotifyPropertyChanged
    {
        public const string Integer3PropertyName = "Integer3";
        public int _integer3;
        public int Integer3
        {
            get { return _integer3; }
            set { SetProperty(ref _integer3, value, Integer3PropertyName); }
        }

#region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void SetProperty<U>(
            ref U backingStore,
            U value,
            string propertyName,
            Action onChanged = null)
        {
            if (EqualityComparer<U>.Default.Equals(backingStore, value))
                return;

            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
            System.Diagnostics.Debug.Write(
                string.Format("Property changed: {0}.{1}", "Class3", propertyName));
        }

        protected void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
#endregion INotifyPropertyChanged
    }
}
