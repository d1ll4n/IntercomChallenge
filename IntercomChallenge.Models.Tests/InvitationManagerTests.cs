using IntercomChallenge.Models.Coordinates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntercomChallenge.Models.Tests
{
    [TestClass]
    public class InvitationManagerTests
    {
        /// <summary>
        /// Verifies that an invitation manager cannot be instatiated without a calculator.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NewInvitationManager_NullCalculator_ThrowsArgumentNullException()
        {
            var invitationManager = new InvitationManager(null, new RadianCoordinate(1, 1));
        }

        /// <summary>
        /// Verifies that an invitation manager cannot be instantiated without a venue location.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NewInvitationManager_NullVenue_ThrowsArgumentNullException()
        {
            var invitationManager = new InvitationManager(new Calculator(), null);
        }

        /// <summary>
        /// Verifies that an invitation manager can be instantiated with a calculator and valid coordinate. 
        /// </summary>
        [TestMethod]
        public void NewInvitationManager_ValidVenue_InstantiatesInvitationManager()
        {
            var venueLocation = new RadianCoordinate(Calculator.ConvertDegreesToRadians(53.339428), Calculator.ConvertDegreesToRadians(-6.257664));
            var calculator = new Calculator();

            var invitationManager = new InvitationManager(calculator, venueLocation);

            Assert.IsNotNull(invitationManager);
        }

        /// <summary>
        /// Verifies that when the list of customers is null, a ArgumentNullException is thrown.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DetermineCustomersToInvite_NullCustomers_ThrowsArgumentNullException()
        {
            var venueLocation = new RadianCoordinate(Calculator.ConvertDegreesToRadians(53.339428), Calculator.ConvertDegreesToRadians(-6.257664));
            var calculator = new Calculator();
            var invitationManager = new InvitationManager(calculator, venueLocation);

            var customersToInvite = invitationManager.DetermineCustomersToInvite(null, 100);
        }

        /// <summary>
        /// Verifies that we cannot try to invite customers who are a negative distance away from the office.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DetermineCustomersToInvite_NegativeDistanceToVenue_ThrowsArgumentException()
        {
            var venueLocation = new RadianCoordinate(Calculator.ConvertDegreesToRadians(53.339428), Calculator.ConvertDegreesToRadians(-6.257664));
            var calculator = new Calculator();
            var customers = new List<CustomerContract>();
            var invitationManager = new InvitationManager(calculator, venueLocation);

            var customersToInvite = invitationManager.DetermineCustomersToInvite(customers, -100);
        }

        /// <summary>
        /// Verifies that all customers close enough to the office are invited.
        /// </summary>
        [TestMethod]
        public void DetermineCustomersToInvite_AllCustomersWithinDistance_InvitesAllCustomers()
        {
            var venue = new RadianCoordinate(Calculator.ConvertDegreesToRadians(53.339428), Calculator.ConvertDegreesToRadians(-6.257664));
            var calculator = new Calculator();
            var customers = new List<CustomerContract>
            {
                new CustomerContract{ Name = "1", UserId = 1, Latitude = 53.3334849, Longitude = -6.2612991 },
                new CustomerContract{ Name = "3", UserId = 3, Latitude = 53.3334593, Longitude = -6.2646894 },
                new CustomerContract{ Name = "2", UserId = 2, Latitude = 53.3334593, Longitude = -6.2646894 },
            };

            var invitationManager = new InvitationManager(calculator, venue);
            var customersToInvite = invitationManager.DetermineCustomersToInvite(customers, 10);

            Assert.AreEqual(customers.Count, customersToInvite.Count());
            Assert.IsTrue(customersToInvite.ElementAt(0).UserId < customersToInvite.ElementAt(1).UserId);
            Assert.IsTrue(customersToInvite.ElementAt(1).UserId < customersToInvite.ElementAt(2).UserId);
        }

        /// <summary>
        /// Verifies that no customers too far from the office are invited.
        /// </summary>
        [TestMethod]
        public void DetermineCustomersToInvite_NoCustomersWithinDistance_InvitesNobody()
        {
            var venue = new RadianCoordinate(Calculator.ConvertDegreesToRadians(53.339428), Calculator.ConvertDegreesToRadians(-6.257664));
            var calculator = new Calculator();
            var customers = new List<CustomerContract>
            {
                new CustomerContract{ Name = "1", UserId = 1, Latitude = 53.2487055, Longitude = -6.5992854 },
                new CustomerContract{ Name = "3", UserId = 3, Latitude = 53.5061796, Longitude = -6.4699798 },
                new CustomerContract{ Name = "2", UserId = 2, Latitude = 53.1970187, Longitude = -6.1308245 },
            };

            var invitationManager = new InvitationManager(calculator, venue);
            var customersToInvite = invitationManager.DetermineCustomersToInvite(customers, 10);

            Assert.AreEqual(0, customersToInvite.Count());
        }

        /// <summary>
        /// Verifies that customers too far from the office are filtered out.
        /// </summary>
        [TestMethod]
        public void DetermineCustomersToInvite_ThreeCustomersWithinDistance_InvitesThreeCustomers()
        {
            var venue = new RadianCoordinate(Calculator.ConvertDegreesToRadians(53.339428), Calculator.ConvertDegreesToRadians(-6.257664));
            var calculator = new Calculator();
            var customers = new List<CustomerContract>
            {
                new CustomerContract{ Name = "1", UserId = 1, Latitude = 53.3334849, Longitude = -6.2612991 },
                new CustomerContract{ Name = "3", UserId = 3, Latitude = 53.3334593, Longitude = -6.2646894 },
                new CustomerContract{ Name = "2", UserId = 2, Latitude = 53.3334593, Longitude = -6.2646894 },
                new CustomerContract{ Name = "5", UserId = 5, Latitude = 53.2487055, Longitude = -6.5992854 },
                new CustomerContract{ Name = "4", UserId = 4, Latitude = 53.5061796, Longitude = -6.4699798 },
                new CustomerContract{ Name = "6", UserId = 6, Latitude = 53.1970187, Longitude = -6.1308245 },
            };

            var invitationManager = new InvitationManager(calculator, venue);
            var customersToInvite = invitationManager.DetermineCustomersToInvite(customers, 10);

            Assert.AreEqual(3, customersToInvite.Count());
            Assert.IsTrue(customersToInvite.ElementAt(0).UserId < customersToInvite.ElementAt(1).UserId);
            Assert.IsTrue(customersToInvite.ElementAt(1).UserId < customersToInvite.ElementAt(2).UserId);
        }
    }
}
