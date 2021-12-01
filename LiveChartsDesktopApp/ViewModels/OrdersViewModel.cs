using LiveCharts;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LiveChartsDesktopApp.ViewModels
{
    public class OrdersViewModel : INotifyPropertyChanged
    {
        private SeriesCollection cancelledOrdersByProvince;
        public SeriesCollection CancelledOrdersByProvince
        {
            get { return cancelledOrdersByProvince; }
            set
            {
                cancelledOrdersByProvince = value;
                OnPropertyChanged();
            }
        }

        private string[] labelsCancelledOrderByProvince;
        public string[] LabelsCancelledOrderByProvince
        {
            get { return labelsCancelledOrderByProvince; }
            set
            {
                labelsCancelledOrderByProvince = value;
                OnPropertyChanged();
            }
        }
        public Func<double, string> Formatter { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
