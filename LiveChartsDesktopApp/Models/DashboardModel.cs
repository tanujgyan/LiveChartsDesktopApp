using System.Collections.Generic;

namespace LiveChartsDesktopApp.Models
{
    public class DashboardModel
    {
        public Dictionary<string, string> CompletedOrdersByProvince { get; set; }

        public Dictionary<string, string> DraftOrdersByProvince { get; set; }

        public Dictionary<string, string> CancelledOrdersByProvince { get; set; }
    }
}
