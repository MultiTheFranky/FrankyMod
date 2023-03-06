using System;
namespace FR4.Prometheus
{
    public class PrometheusData
    {
        public String key { get; set; }
        public String description { get; set; }
        public Double value { get; set; }

        public PrometheusData(String key, String description, Double value)
        {
            this.key = key;
            this.description = description;
            this.value = value;
        }
    }
}