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

        public static RidesInformation GetAvailableTickets(string SearchTerm)
        {
            //Not gonna use the SearchTerm in this, as it has no fucntion.

            //Setup
            RidesInformation Fare = new RidesInformation();


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

                Fare.Rides.Add(NewRide);

                //Check for cheapest ride
                if(Fare.Cheapest == null || Fare.Cheapest.GetMinimumPrice() > NewRide.GetMinimumPrice())
                {
                    Fare.Cheapest = NewRide;
                }

                //Check for fastest ride
                if (Fare.Fastest == null || Fare.Fastest.GetRideTime() > NewRide.GetRideTime())
                {
                    Fare.Fastest = NewRide;
                }
            }

            return Fare;


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
