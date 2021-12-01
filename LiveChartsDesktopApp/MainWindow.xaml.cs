using LiveChartsDesktopApp.Models;
using LiveChartsDesktopApp.ViewModels;
using System;
using System.Windows;

namespace LiveChartsDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OrderService orderService;
        OrdersViewModel orderViewModel;
        public MainWindow()
        {
            try
            {

                InitializeComponent();
                orderViewModel = new OrdersViewModel();
                orderService = new OrderService(orderViewModel);
                orderService.StartConnection();
                this.DataContext = orderViewModel;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
