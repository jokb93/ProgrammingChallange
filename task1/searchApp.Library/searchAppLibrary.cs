using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using searchApp.Library.objects;
using System.Xml.Linq;

namespace searchApp.Library
{
    public class searchAppLibrary
    {
        static Ride Cheapest;
        static Ride Fastest;

        public static List<Ride> GetAvailableTickets(string SearchTerm)
        {
            //Not gonna use the SearchTerm in this, as it has no fucntion.

            //Setup
            List<Ride> Rides = new List<Ride>();


            //Read XML
            XElement SearchResult = XElement.Load("search.xml");

            //Parse objects from result
            foreach(XElement element in SearchResult.Elements("SearchResult"))
            {
                String ID = element.Element("ID").Value;
                List<Connection> Connections = ParseConnections(element.Element("Connections"));

                Ride NewRide = new Ride(
                    ID,
                    Connections
                    );

                Rides.Add(NewRide);

                //Check for cheapest ride
                if(Cheapest == null || Cheapest.GetMinimumPrice() > NewRide.GetMinimumPrice())
                {
                    Cheapest = NewRide;
                }

                //Check for fastest ride
                if (Fastest == null || Fastest.GetRideTime() > NewRide.GetRideTime())
                {
                    Fastest = NewRide;
                }
            }

            return Rides;


        }

        public static List<Connection> ParseConnections(XElement ConnectionXML)
        {
            List<Connection> Connections = new List<Connection>();

            foreach (XElement Connection in ConnectionXML.Elements("Connection"))
            {
                //Create objects before inputting
                string Start = Connection.Element("Start").Value.ToString();
                string Finish = Connection.Element("Finish").Value.ToString();
                DateTime DepartureTime = DateTime.Parse(Connection.Element("DepartureTime").Value.ToString());
                DateTime ArrivalTime = DateTime.Parse(Connection.Element("ArrivalTime").Value.ToString());
                string TrainName = Connection.Element("TrainName").Value.ToString();

                Connection NewConnection = new Connection(
                    Start,
                    Finish,
                    DepartureTime,
                    ArrivalTime,
                    TrainName,
                    ParseFares(Connection.Element("Fares"))
                );

                //Creatte Connection
                Connections.Add(NewConnection);
                
            }
            
            return Connections;
        }

        public static List<Fare> ParseFares(XElement FareXML)
        {
            List<Fare> Fares = new List<Fare>();

            foreach (XElement Fare in FareXML.Elements("Fare"))
            {


                string Name = Fare.Element("Name").Value.ToString();
                double Price = Convert.ToDouble(Fare.Element("Price").Element("Value").Value.ToString().Replace('.', ','));
                string Currency = Fare.Element("Price").Element("Currency").Value.ToString();

                Fare NewFare = new Fare(
                    Name,
                    Price,
                    Currency
                );

                Fares.Add(NewFare);
                
            }

            return Fares;
        }
    }
}
