using IntercomChallenge.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntercomChallenge.Models.Coordinates
{
    /// <summary>
    /// Represents a co-ordinate on the surface of the Earth with the latitude and longitude given in degrees.
    /// </summary>
    public class DegreeCoordinate : CoordinateBase
    {
        const double MAX_LATITUDE = 90;
        const double MIN_LATITUDE = -90;

        const double MAX_LONGITUDE = 180;
        const double MIN_LONGITUDE = -180;

        public DegreeCoordinate(double latitude, double longitude)
        {
            if (latitude > MAX_LATITUDE || latitude < MIN_LATITUDE)
            {
                throw new InvalidCoordinateException($"Latitude may not be greater than {MAX_LATITUDE} degrees of less than {MIN_LATITUDE} degrees.");
            }

            if (longitude > MAX_LONGITUDE || longitude < MIN_LONGITUDE)
            {
                throw new InvalidCoordinateException($"Longitude may not be greater than {MAX_LONGITUDE} degrees or less than {MIN_LONGITUDE} degrees.");
            }

            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
