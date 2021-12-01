using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveChartsDesktopApp.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace LiveChartsDesktopApp.Models
{
    public class OrderService
    {
        HubConnection connection = null;
        HttpClient client = new HttpClient();
        DashboardModel dashboardModel = new DashboardModel();
        List<ObservableValue> CancelledOrdersByProvinceList = new List<ObservableValue>();
        OrdersViewModel orderViewModel;
        public List<string> LabelsCancelledOrderByProvinceList { get; set; } = new List<string>();
        public OrderService(OrdersViewModel ordersViewModel)
        {
            orderViewModel = ordersViewModel;
        }
        public async Task StartConnection()
        {
            connection = new HubConnectionBuilder()
                 .WithUrl("http://localhost:7071/api")
                 .Build();
            await connection.StartAsync();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
            connection.On<string>("target",
                                (value) =>
                                {
                                    Dispatcher.CurrentDispatcher.BeginInvoke((Action)(() =>
                                    {
                                        dashboardModel = System.Text.Json.JsonSerializer.Deserialize<DashboardModel>(value);
                                        MapToChartModelCancelledByProvince(dashboardModel);
                                    }));
                                });
            _ = GetDataInitial();


        }
        public async Task GetDataInitial()
        {
            try
            {
                client.BaseAddress = new Uri("http://localhost:7071/api/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetStringAsync("DashboardLoadFunction");
                dashboardModel = System.Text.Json.JsonSerializer.Deserialize<DashboardModel>(response);
                MapToChartModelCancelledByProvince(dashboardModel);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }
        private ChartValues<ObservableValue> GetChartValuesForCancelledOrderByProvince(List<ChartModel> chartModels)
        {
            ChartValues<ObservableValue> chartValues = new ChartValues<ObservableValue>();
            CancelledOrdersByProvinceList = new List<ObservableValue>();
            LabelsCancelledOrderByProvinceList = new List<string>();
            int i = 0;
            for (int k = 0; k < chartModels.Count; k++)
            {
                CancelledOrdersByProvinceList.Add(new ObservableValue(0));
            }
            foreach (var cm in chartModels)
            {
                CancelledOrdersByProvinceList[i].Value = cm.Data;
                LabelsCancelledOrderByProvinceList.Add(cm.Label);
                chartValues.Add(CancelledOrdersByProvinceList[i]);
                i++;
            }
            return chartValues;


        }
        private void UpdateChartDataCancelledOrderByProvince(List<ChartModel> chartModels)
        {
            orderViewModel.CancelledOrdersByProvince = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Registration By Province",
                    Values = GetChartValuesForCancelledOrderByProvince(chartModels),

                }
            };


            orderViewModel.LabelsCancelledOrderByProvince = LabelsCancelledOrderByProvinceList.ToArray();
            orderViewModel.Formatter = value => value.ToString("N");


        }
        private void MapToChartModelCancelledByProvince(DashboardModel data)
        {
            List<ChartModel> chartModels = new List<ChartModel>();
            foreach (var d in data.CancelledOrdersByProvince)
            {
                chartModels.Add(new ChartModel { Data = Convert.ToInt32(d.Value), Label = d.Key });
            }
            UpdateChartDataCancelledOrderByProvince(chartModels);
        }
    }
}
