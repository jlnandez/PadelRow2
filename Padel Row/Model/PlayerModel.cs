using System.ComponentModel;

namespace Padel_Row.Model
{
    public class PlayerModel : INotifyPropertyChanged
    {
        private int _score;
        public string Name { get; set; }
        public int Score
        {
            get => _score;
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged(nameof(Score)); // Notifica que la propiedad ha cambiado
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
