using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using searchApp.Library;
using searchApp.Library.objects;

namespace ConsoleFrontEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            //Interchangable front-end
            RidesInformation RidesInformation = searchAppLibrary.GetAvailableTickets("FakeSearch");


            //Simple presentation
            foreach(Ride Ride in RidesInformation.Rides)
            {
                int i = 1;
                Console.WriteLine("____________________________________");
                Console.WriteLine();
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Information");
                Console.WriteLine("------------------------------------");
                if(Ride == RidesInformation.Cheapest)
                {
                    Console.WriteLine("Special information: Cheapest Fare");
                }
                if (Ride == RidesInformation.Fastest)
                {
                    Console.WriteLine("Special information: Fastest Fare");
                }
                Console.WriteLine("ID: "+Ride.GetID());
                Console.WriteLine("Amount of Train changes: " + (Ride.GetConnections().Count - 1));
                Console.WriteLine("Lowest possible price: " + Ride.GetMinimumPrice() + " GBP");
                Console.WriteLine("Ride total time: " + Ride.GetRideTime() + " Min");
                Console.WriteLine();
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Connections");
                foreach(Connection Connection in Ride.GetConnections())
                {
                    Console.WriteLine("------------------------------------");
                    Console.WriteLine("Start: " + Connection.Start);
                    Console.WriteLine("Finish: " + Connection.Finish);
                    Console.WriteLine("Depature: " + Connection.DepartureTime.ToString());
                    Console.WriteLine("Arrival: " + Connection.ArrivalTime.ToString());
                    Console.WriteLine("Train name: " + Connection.TrainName);
                    Console.WriteLine("Ride time: " + Connection.GetDuration() + " Min");
                    Console.WriteLine();
                    Console.WriteLine("     -------------------------------");
                    Console.WriteLine("     Fares");
                    Console.WriteLine("     -------------------------------");
                    foreach(Fare Fare in Connection.Fares)
                    {
                        Console.WriteLine("     Ticket type: " + Fare.Name);
                        Console.WriteLine("     Ticket price: " + Fare.Price+ " "+ Fare.Currency);
                        Console.WriteLine("     -------------------------------");
                    }
                    if (Ride.GetConnections().Last() != Connection)
                    {
                        Console.WriteLine("------------------------------------");
                        Console.WriteLine("Wait time: " + (Ride.GetConnections()[i].DepartureTime - Connection.ArrivalTime).TotalMinutes);
                    }
                    i++;

                }
                Console.WriteLine("____________________________________");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
