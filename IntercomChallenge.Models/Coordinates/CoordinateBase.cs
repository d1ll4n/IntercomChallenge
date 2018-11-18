using System;

namespace IntercomChallenge.Models.Coordinates
{
    /// <summary>
    /// Represents a co-ordinate on the surface of the Earth.
    /// </summary>
    public abstract class CoordinateBase
    {
        public double Latitude { get; protected set; }

        public double Longitude { get; protected set; }
    }
}
