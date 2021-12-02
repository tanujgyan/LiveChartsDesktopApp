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

        private SeriesCollection draftOrdersByProvince;
        public SeriesCollection DraftOrdersByProvince
        {
            get { return draftOrdersByProvince; }
            set
            {
                draftOrdersByProvince = value;
                OnPropertyChanged();
            }
        }

        private string[] labelsDraftOrderByProvince;
        public string[] LabelsDraftOrderByProvince
        {
            get { return labelsDraftOrderByProvince; }
            set
            {
                labelsDraftOrderByProvince = value;
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
