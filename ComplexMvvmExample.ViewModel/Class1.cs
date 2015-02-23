using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ComplexMvvmExample.ViewModel
{
    public class Class1 : INotifyPropertyChanged
    {
        public const string DependentClassPropertyName = "DependentClass";
        private Class2 _dependentClass;
        public Class2 DependentClass
        {
            get { return _dependentClass; }
            set { SetProperty(ref _dependentClass, value, DependentClassPropertyName); }
        }

        public const string Integer1PropertyName = "Integer1";
        private int _integer1;
        public int Integer1 
        {
            get { return _integer1; }
            set { SetProperty(ref _integer1, value, Integer1PropertyName); }
        }

        public const string CalculatedValue1PropertyName = "CalculatedValue1";
        public int CalculatedValue1
        {
            get
            {
                return Integer1 * DependentClass.CalculatedValue2;
            }
        }

        public Class1(Class2 o)
        {
            DependentClass = o;
            this.PropertyChanged += Class1CalculatedValueHandler;
            o.PropertyChanged += Class2CalculatedValueHandler;
        }

#region Wire up calculated properties
        protected void Class1CalculatedValueHandler(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case Integer1PropertyName:
                    {
                        OnPropertyChanged(CalculatedValue1PropertyName);
                        break;
                    }
            }
        }

        protected void Class2CalculatedValueHandler(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case Class2.CalculatedValue2PropertyName:
                    {
                        OnPropertyChanged(CalculatedValue1PropertyName);
                        break;
                    }
            }
        }
#endregion

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
                string.Format("Property changed: {0}.{1}", "Class1", propertyName));
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
