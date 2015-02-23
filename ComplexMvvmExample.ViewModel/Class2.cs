using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ComplexMvvmExample.ViewModel
{
    public class Class2 : INotifyPropertyChanged
    {
        public const string DependentClassPropertyName = "DependentClass";
        private Class3 _dependentClass;
        public Class3 DependentClass
        {
            get { return _dependentClass; }
            set { SetProperty(ref _dependentClass, value, DependentClassPropertyName); }
        }

        public const string Integer2PropertyName = "Integer2";
        private int _integer2;
        public int Integer2
        {
            get { return _integer2; }
            set { SetProperty(ref _integer2, value, Integer2PropertyName); }
        }

        public const string CalculatedValue2PropertyName = "CalculatedValue2";
        public int CalculatedValue2
        {
            get
            {
                return Integer2 * DependentClass.Integer3;
            }
        }

        public Class2(Class3 o)
        {
            DependentClass = o;
            this.PropertyChanged += Class2CalculatedValueHandler;
            o.PropertyChanged += Class3CalculatedValueHandler;
        }

#region Wire up calculated properties
        protected void Class2CalculatedValueHandler(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case Integer2PropertyName:
                    {
                        OnPropertyChanged(CalculatedValue2PropertyName);
                        break;
                    }
            }
        }

        protected void Class3CalculatedValueHandler(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case Class3.Integer3PropertyName:
                    {
                        OnPropertyChanged(CalculatedValue2PropertyName);
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
                string.Format("Property changed: {0}.{1}", "Class2", propertyName));
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
