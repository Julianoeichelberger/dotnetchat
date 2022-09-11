using Chat.Server.Ports.Commands;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using RestSharp;
using System;
using System.Globalization;
using System.IO;


namespace Chat.Server.Adapters.Commands
{
    internal class StockRecord
    {
        [Index(0)]
        public string Symbol { get; set; }

        [Index(1)]
        public DateTime Date { get; set; }

        [Index(2)]
        public TimeOnly Time { get; set; }

        [Index(3)]
        public double Open { get; set; }

        [Index(4)]
        public double High { get; set; }

        [Index(5)]
        public double Low { get; set; }

        [Index(6)]
        public double Close { get; set; }

        [Index(7)]
        public int Volume { get; set; }
    }


    public class StockCommand : IStockCommand
    {
        private readonly string _url = "https://stooq.com/q/l/?s={0}&f=sd2t2ohlcv&h&e=csv";

        private readonly string _command;

        public StockCommand(string command)
        {
            _command = command;
        }

        public string Execute()
        {
            Uri baseUrl = new Uri(string.Format(_url, _command));

            RestClient client = new RestClient(baseUrl);
            client.AddDefaultHeader("Content-Type", "text/plain");
            RestRequest request = new RestRequest();

            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                try
                {
                    var reader = new StringReader(response.Content);
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Read();
                        var record = csv.GetRecord<StockRecord>();
                        return string.Format("{0} quote is ${1} per share", record.Symbol, record.Close);
                    }
                }
                catch (Exception e)
                {
                    return e.Message;
                }

            }
            else
            {
                return response.ErrorMessage;
            }
        }
    }
}
