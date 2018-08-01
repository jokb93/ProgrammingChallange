using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace searchApp.Library.objects
{

    public class Ride
    {

        String ID;
        List<Connection> Connections;

        public Ride(string ID, List<Connection> Connections)
        {
            this.ID = ID;
            this.Connections = Connections;
        }

        public double GetRideTime()
        {
            return (Connections.Last<Connection>().GetArrival() - Connections[0].GetDepature()).TotalMinutes;
        }

        public double GetMinimumPrice()
        {
            double AccumulatedPrice = 0;
            foreach (Connection Connection in Connections)
            {
                AccumulatedPrice += Connection.getLowestPrice();
            }

            return AccumulatedPrice;
        }

    }

    public class Connection
    {
        string Start;
        string Finish;
        DateTime DepartureTime;
        DateTime ArrivalTime;
        string TrainName;
        List<Fare> Fares;

        public Connection(string Start, string Finish, DateTime DepartureTime, DateTime ArrivalTime, string TrainName, List<Fare> Fares)
        {
            this.Start = Start;
            this.Finish = Finish;
            this.DepartureTime = DepartureTime;
            this.ArrivalTime = ArrivalTime;
            this.TrainName = TrainName;
            this.Fares = Fares;
        }

        public DateTime GetDepature()
        {
            return DepartureTime;
        }

        public DateTime GetArrival()
        {
            return ArrivalTime;
        }

        public double getLowestPrice()
        {
            return Fares[0].GetPrice();
        }

    }

    public class Fare
    {
        string Name;
        double Price;
        string Currency;

        public Fare(string Name, double Price, string Currency)
        {
            this.Name = Name;
            this.Price = Price;
            this.Currency = Currency;
        }

        public double GetPrice()
        {
            return Price;
        }
    }
}
