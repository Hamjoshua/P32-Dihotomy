    using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PalMathy.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected Dictionary<BaseViewModel, string> nestedProperties = new Dictionary<BaseViewModel, string>();

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        //protected void Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        //{
        //    if (propertyName == null)
        //        throw new ArgumentNullException(nameof(propertyName));

        //    if (EqualityComparer<T>.Default.Equals(field, newValue)) return;
        //    field = newValue;
        //    OnPropertyChanged(propertyName);
        //}
        protected void Set<T>(ref T backingFiled, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingFiled, value)) return;
            if (backingFiled is BaseViewModel viewModel)
            {   // if old value is ViewModel, than we assume that it was subscribed,
                // so - unsubscribe it
                viewModel.PropertyChanged -= ChildViewModelChanged;
                nestedProperties.Remove(viewModel);
            }
            if (value is BaseViewModel valueViewModel)
            {
                // if new value is ViewModel, than we must subscribe it on PropertyChanged 
                // and add it into subscribe dictionary
                valueViewModel.PropertyChanged += ChildViewModelChanged;
                nestedProperties.Add(valueViewModel, propertyName);
            }
            backingFiled = value;
            OnPropertyChanged(propertyName);
        }

        private void ChildViewModelChanged(object? sender, PropertyChangedEventArgs e)
        {
            // this is child property name,
            // need to get parent property name from dictionary
            string propertyName = e.PropertyName;
            if (sender is BaseViewModel viewModel)
            {
                propertyName = nestedProperties[viewModel];
            }
            // Rise parent PropertyChanged with parent property name
            OnPropertyChanged(propertyName);
        }

        public void Dispose()
        {   // need to make sure that we unsubscibed
            foreach (BaseViewModel viewModel in nestedProperties.Keys)
            {
                viewModel.PropertyChanged -= ChildViewModelChanged;
                viewModel.Dispose();
            }
        }
    }
}
