using IntercomChallenge.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntercomChallenge.Models.Coordinates
{
    /// <summary>
    /// Represents a co-ordinate on the surface of the Earth with the latitude and longitude given in radians.
    /// </summary>
    public class RadianCoordinate : CoordinateBase
    {
        const double MAX_LATITUDE = 90 * Math.PI / 180;
        const double MIN_LATITUDE = -90 * Math.PI / 180;

        const double MAX_LONGITUDE = 180 * Math.PI / 180;
        const double MIN_LONGITUDE = -180 * Math.PI / 180;

        public RadianCoordinate(double latitude, double longitude)
        {
            if (latitude > MAX_LATITUDE || latitude < MIN_LATITUDE)
            {
                throw new InvalidCoordinateException($"Latitude may not be greater than {MAX_LATITUDE} radians of less than {MIN_LATITUDE} radians.");
            }

            if (longitude > MAX_LONGITUDE || longitude < MIN_LONGITUDE)
            {
                throw new InvalidCoordinateException($"Longitude may not be greater than {MAX_LONGITUDE} radians or less than {MIN_LONGITUDE} radians.");
            }

            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
