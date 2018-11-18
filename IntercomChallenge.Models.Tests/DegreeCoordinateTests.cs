using IntercomChallenge.Models.Coordinates;
using IntercomChallenge.Models.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntercomChallenge.Models.Tests
{
    [TestClass]
    public class DegreeCoordinateTests
    {
        /// <summary>
        /// Verifies that a valid coordinate object can be instantiated.
        /// </summary>
        [TestMethod]
        public void NewCoordinate_ValidCoordinate_InstantiatesCorrectly()
        {
            var latitude = 53.339428;
            var longitude = -6.257664;

            var coordinate = new DegreeCoordinate(latitude, longitude);

            Assert.IsNotNull(coordinate);
            Assert.AreEqual(latitude, coordinate.Latitude);
            Assert.AreEqual(longitude, coordinate.Longitude);
        }

        /// <summary>
        /// Verifies that a coordinate with latitude greater than 90 degrees cannot be instantiated.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCoordinateException))]
        public void NewCoordinate_LatitudeGreaterThan90Degrees_ThrowsInvalidCoordinateException()
        {
            var latitude = 90.000001;
            var longitude = -6.257664;

            var coordinate = new DegreeCoordinate(latitude, longitude);
        }

        /// <summary>
        /// Verifies that a coordinate with latitude less than -90 degrees cannot be instantiated.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCoordinateException))]
        public void NewCoordinate_LatitudeLessThanNegative90Degrees_ThrowsInvalidCoordinateException()
        {
            var latitude = -90.000001;
            var longitude = -6.257664;

            var coordinate = new DegreeCoordinate(latitude, longitude);
        }

        /// <summary>
        /// Verifies that a coordinate with longitude greater than 180 degrees cannot be instantiated.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCoordinateException))]
        public void NewCoordinate_LongitudeGreaterThan180Degrees_ThrowsInvalidCoordinateException()
        {
            var latitude = 53.339428;
            var longitude = 180.000001;

            var coordinate = new DegreeCoordinate(latitude, longitude);
        }

        /// <summary>
        /// Verifies that a coordinate with longitude less than -180 degrees cannot be instantiated.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCoordinateException))]
        public void NewCoordinate_LongitudeLessThanNegative180Degrees_ThrowsInvalidCoordinateException()
        {
            var latitude = 53.339428;
            var longitude = -180.000001;

            var coordinate = new DegreeCoordinate(latitude, longitude);
        }


    }
}
