using IntercomChallenge.Models.Coordinates;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntercomChallenge.Models
{
    public class Calculator
    {
        /// <summary>
        /// The mean radius of the Earth.
        /// </summary>
        public static readonly double EARTH_RADIUS_KM = 6371;

        /// <summary>
        /// Calculates the central angle between two points on the surface of the Earth.
        /// This method has been made virtual to allow it to be overridden in sub-classes with other formulas for calculating the central angle.
        /// </summary>
        /// <returns>The central angle</returns>
        public virtual double CalculateCentralAngle(RadianCoordinate point1, RadianCoordinate point2)
        {
            double centralAngle = Math.Acos(Math.Sin(point1.Latitude) * Math.Sin(point2.Latitude) + Math.Cos(point1.Latitude) * Math.Cos(point2.Latitude) * Math.Cos(point1.Longitude - point2.Longitude));
            return centralAngle;
        }

        /// <summary>
        /// Calculates the the lenght of an arc on the surface of a shere between two points.
        /// </summary>
        /// <returns>The length of the arc.</returns>
        public double CalculateArcLength(RadianCoordinate point1, RadianCoordinate point2, double radius)
        {
            return radius * CalculateCentralAngle(point1, point2);
        }

        /// <summary>
        /// Converts a given value in degrees to radians.
        /// </summary>
        /// <returns>The value in radians.</returns>
        public static double ConvertDegreesToRadians(double degrees)
        {
            return degrees * (Math.PI / 180d);
        }
    }
}
