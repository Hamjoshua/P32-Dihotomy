    using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PalMathy.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        protected void Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            if (EqualityComparer<T>.Default.Equals(field, newValue)) return;
            field = newValue;
            OnPropertyChanged(propertyName);
        }
    }
}
