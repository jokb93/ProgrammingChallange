using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using NUnit.Framework;
using searchApp.Library;
using searchApp.Library.objects;

namespace searchApp.Library.Test
{

    [TestFixture]
    public class searchAppLibraryTest
    {

        [Test]
        public void Check_For_Results_Test()
        {
            //Setup
            int MinimumInput = 1;

            //Action
            RidesInformation Fare = searchAppLibrary.GetAvailableTickets("FakeSearch");

            //Assert
            Assert.GreaterOrEqual(Fare.Rides.Count, MinimumInput);
        }

        [Test]
        public void Parse_Connection_Test()
        {

            //Setup
            int MinimumInput = 1;
            XElement TestConnections = XElement.Parse("<Connections><Connection><Start>LondonStPancrasInternational</Start><Finish>ParisNord</Finish><DepartureTime>2015-07-11T09:23:00+01:00</DepartureTime><ArrivalTime>2015-07-11T12:41:00+02:00</ArrivalTime><TrainName>Eurostar</TrainName><Fares><Fare><Name>StandardClass</Name><Price><Currency>GBP</Currency><Value>79.00</Value></Price></Fare><Fare><Name>StandardPremier</Name><Price><Currency>GBP</Currency><Value>159.00</Value></Price></Fare></Fares></Connection><Connection><Start>ParisLyon</Start><Finish>BarcelonaSants</Finish><DepartureTime>2015-07-11T13:56:00+02:00</DepartureTime><ArrivalTime>2015-07-11T20:17:00+02:00</ArrivalTime><TrainName>TGV2N2</TrainName><Fares><Fare><Name>StandardClass</Name><Price><Currency>GBP</Currency><Value>50.00</Value></Price></Fare><Fare><Name>FirstClass</Name><Price><Currency>GBP</Currency><Value>75.00</Value></Price></Fare></Fares></Connection></Connections>");

            //Action
            List<objects.Connection> Connections = searchAppLibrary.ParseConnections(TestConnections);

            //Assert
            Assert.GreaterOrEqual(Connections.Count, MinimumInput);
        }

        [Test]
        public void Parse_Fares_Test()
        {

            //Setup
            int MinimumInput = 1;
            XElement TestFares = XElement.Parse("<Fares><Fare><Name>StandardClass</Name><Price><Currency>GBP</Currency><Value>50.00</Value></Price></Fare><Fare><Name>FirstClass</Name><Price><Currency>GBP</Currency><Value>75.00</Value></Price></Fare></Fares>");

            //Action
            List<objects.Fare> Fares = searchAppLibrary.ParseFares(TestFares);

            //Assert
            Assert.GreaterOrEqual(Fares.Count, MinimumInput);
        }

        [Test]
        public void Get_Duration_Test()
        {

            //Setup
            double ExpectedOutput = 15;
            Connection TestConnection = new Connection("TestStart", "TestEnd", DateTime.Now, DateTime.Now.AddMinutes(15), "TestTain", new List<Fare>());

            //Action
            double TestDuration = TestConnection.GetDuration();

            //Assert
            Assert.AreEqual(ExpectedOutput, TestDuration);
        }


    }
}