using IntercomChallenge.Models.Coordinates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntercomChallenge.Models
{
    /// <summary>
    /// A class to manage the distibution of invitations to customers.
    /// </summary>
    public class InvitationManager
    {
        RadianCoordinate _venue;
        Calculator _calculator;

        /// <summary>
        /// Creates a new instance of the InvitationManager class.
        /// </summary>
        /// <param name="calculator">A calculator instance.</param>
        /// <param name="venue">The location of the venue.</param>
        public InvitationManager(Calculator calculator, RadianCoordinate venue)
        {
            if(calculator is null)
            {
                throw new ArgumentNullException($"{nameof(calculator)} may not be null.");
            }

            if (venue is null)
            {
                throw new ArgumentNullException($"{nameof(venue)} may not be null.");
            }

            _calculator = calculator;
            _venue = venue;
        } 
       
        /// <summary>
        /// Determines which customers to invite for drinks.
        /// </summary>
        /// <param name="customers">A list of all customers.</param>
        /// <param name="distanceFromVenue">The maximum distance customers must be within to attend.</param>
        /// <returns>A list of customers to invite for food and drinks.</returns>
        public IEnumerable<CustomerContract> DetermineCustomersToInvite(IEnumerable<CustomerContract> customers, double distanceFromVenue)
        {
            if(customers is null)
            {
                throw new ArgumentNullException($"{nameof(customers)} may not be null.");
            }

            if(distanceFromVenue < 0)
            {
                throw new ArgumentException($"{nameof(distanceFromVenue)} may not be negative.");
            }

            var customersToInvite = new List<CustomerContract>();

            foreach(var customer in customers)
            {
                var customerLocation = new RadianCoordinate(Calculator.ConvertDegreesToRadians(customer.Latitude), Calculator.ConvertDegreesToRadians(customer.Longitude));
                var distanceFromOffice = _calculator.CalculateArcLength(_venue, customerLocation, Calculator.EARTH_RADIUS_KM);

                if (distanceFromOffice < distanceFromVenue)
                {
                    customersToInvite.Add(customer);
                }
            }

            return customersToInvite.OrderBy(c => c.UserId);
            
        }
    }
}
