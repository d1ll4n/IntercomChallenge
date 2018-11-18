using IntercomChallenge.Models.Coordinates;
using IntercomChallenge.Models.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntercomChallenge.Models.Tests
{
    [TestClass]
    public class RadianCoordinateTests
    {
        /// <summary>
        /// Verifies that a radian coordinate can be instantiated with valid a valid latitude and longitude.
        /// </summary>
        [TestMethod]
        public void NewCoordinate_ValidCoordinate_InstantiatesCorrectly()
        {
            var latitude = 0.930948;
            var longitude = -0.109216;

            var coordinate = new RadianCoordinate(latitude, longitude);

            Assert.IsNotNull(coordinate);
            Assert.AreEqual(latitude, coordinate.Latitude);
            Assert.AreEqual(longitude, coordinate.Longitude);
        }

        /// <summary>
        /// Verifies that a radian co-ordinate cannot be instantiated when the latitude is too greater than P/2 radians.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCoordinateException))]
        public void NewCoordinate_LatitudeTooLarge_ThrowsInvalidCoordinateException()
        {
            var latitude = 1.5709;
            var longitude = 0;

            var coordinate = new RadianCoordinate(latitude, longitude);
        }

        /// <summary>
        /// Verifies that a radian co-ordinate cannot be instantiated when the latitude is less than - Pi/2 radians.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCoordinateException))]
        public void NewCoordinate_LatitudeTooSmall_ThrowsInvalidCoordinateException()
        {
            var latitude = -1.5709;
            var longitude = 0;

            var coordinate = new RadianCoordinate(latitude, longitude);
        }

        /// <summary>
        /// Verifies that a radian coordinate cannot be instantated with a longitude greater than Pi radians.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCoordinateException))]
        public void NewCoordinate_LongitudeToLarge_ThrowsInvalidCoordinateException()
        {
            var latitude = 0;
            var longitude = 3.1416;

            var coordinate = new RadianCoordinate(latitude, longitude);
        }

        /// <summary>
        /// Verifies that a radian coordinate cannot be instantated with a longitude less than -Pi radians.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCoordinateException))]
        public void NewCoordinate_LongitudeTooSmall_ThrowsInvalidCoordinateException()
        {
            var latitude = 0;
            var longitude = -3.1416;

            var coordinate = new RadianCoordinate(latitude, longitude);
        }


    }
}
