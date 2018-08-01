using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace searchApp.Library.objects
{

    public class RidesInformation
    {
        //Props
        public Ride Cheapest
        {
            get
            {
                return cheapest;
            }
            set
            {
                cheapest = value;
            }
        }

        public Ride Fastest
        {
            get
            {
                return fastest;
            }
            set
            {
                fastest = value;
            }
        }

        public List<Ride> Rides
        {
            get
            {
                return rides;
            }
        }

        //private vars
        Ride cheapest;
        Ride fastest;
        List<Ride> rides = new List<Ride>();

        //Methods
        public void SetRides(List<Ride> Rides)
        {
            rides = Rides;
        }
    }

    public class Ride
    {
        
        //Props
        public string ID
        {
            get
            {
                return id;
            }
        }

        public List<Connection> Connections
        {
            get
            {
                return connections;
            }
        }

        //Private vars
        String id;
        List<Connection> connections;

        //Methods
        public Ride(string ID, List<Connection> Connections)
        {
            this.id = ID;
            this.connections = Connections;
        }

        public double GetRideTime()
        {
            return (Connections.Last<Connection>().ArrivalTime - Connections[0].DepartureTime).TotalMinutes;
        }

        public double GetMinimumPrice()
        {
            double AccumulatedPrice = 0;
            foreach (Connection Connection in Connections)
            {
                AccumulatedPrice += Connection.GetLowestPrice();
            }

            return AccumulatedPrice;
        }

        public string GetID()
        {
            return ID;
        }

        public List<Connection> GetConnections()
        {
            return Connections;
        }

    }

    public class Connection
    {

        //Props
        public string Start
        {
            get
            {
                return start;
            }
        }

        public string Finish
        {
            get
            {
                return finish;
            }
        }

        public DateTime DepartureTime
        {
            get
            {
                return departureTime;
            }
        }

        public DateTime ArrivalTime
        {
            get
            {
                return arrivalTime;
            }
        }

        public string TrainName
        {
            get
            {
                return trainName;
            }
        }

        public List<Fare> Fares
        {
            get
            {
                return fares;
            }
        }


        //Private vars
        string start;
        string finish;
        DateTime departureTime;
        DateTime arrivalTime;
        string trainName;
        List<Fare> fares;


        //Methods
        public Connection(string Start, string Finish, DateTime DepartureTime, DateTime ArrivalTime, string TrainName, List<Fare> Fares)
        {
            this.start = Start;
            this.finish = Finish;
            this.departureTime = DepartureTime;
            this.arrivalTime = ArrivalTime;
            this.trainName = TrainName;
            this.fares = Fares;
        }
        
        public double GetDuration()
        {
            return (ArrivalTime - DepartureTime).TotalMinutes;
        }

        public double GetLowestPrice()
        {
            return Fares[0].Price;
        }

    }

    public class Fare
    {

        //Props
        public string Name
        {
            get
            {
                return name;
            }
        }

        public double Price
        {
            get
            {
                return price;
            }
        }

        public string Currency
        {
            get
            {
                return currency;
            }
        }

        //Private vars
        string name;
        double price;
        string currency;

        //Methods
        public Fare(string Name, double Price, string Currency)
        {
            this.name = Name;
            this.price = Price;
            this.currency = Currency;
        }

    }
}
