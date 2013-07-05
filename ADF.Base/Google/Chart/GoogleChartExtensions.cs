namespace Adf.Base.Google.Chart
{
    public static class GoogleChartExtensions
    {
        public static GoogleChart Pie(this GoogleChart chart)
        {
            chart.ChartType = "chartType: 'PieChart'";

            return chart;
        }

        public static GoogleChart Legend(this GoogleChart chart, string legend)
        {
            chart.Legend = string.Format("'legend': '{0}'", legend);

            return chart;
        }

        public static GoogleChart Data(this GoogleChart chart, GoogleData data)
        {
            chart.Data = string.Format("dataTable: {0}", data.Format());

            return chart;
        }

        public static GoogleChart WithArea(this GoogleChart chart, int left = 20, int top = 20, int width = 0, int height = 0)
        {
            chart.Area = "'chartArea': '{ " + string.Format("left: {0}, top: {1}, width: {2}, height: {3} ", left, top, width, height) + "}'";

            return chart;
        }

        public static string Format(this GoogleChart chart, string id)
        {
            chart.ID = id;

            var s = string.Format("{{{0}, {1}, options: {{ {2}, {3} }}, containerId: '{4}' }}", chart.ChartType, chart.Data, chart.Legend, chart.Area, chart.ID);

            return s;
        }
    }
}