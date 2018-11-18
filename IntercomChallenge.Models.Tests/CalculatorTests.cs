using IntercomChallenge.Models.Coordinates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntercomChallenge.Models.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        const double TOLERANCE = 0.0000001;

        /// <summary>
        /// Verifies that 0 degrees is correctly converted to 0 radians.
        /// </summary>
        [TestMethod]
        public void ConvertDegreesToRadians_0Degrees_ShouldBe0()
        {
            var expectedRadians = 0;

            var actualRadians = Calculator.ConvertDegreesToRadians(0);

            Assert.AreEqual(expectedRadians, actualRadians);
        }

        /// <summary>
        /// Verfies that 90 degrees is correctly converted to Pi / 2 radians.
        /// </summary>
        [TestMethod]
        public void ConvertDegreesToRadians_90Degrees_ShouldBeHalfPi()
        {
            var expectedRadians = Math.PI / 2;

            var actualRadians = Calculator.ConvertDegreesToRadians(90);

            Assert.IsTrue(Math.Abs(expectedRadians - actualRadians) < TOLERANCE);
        }

        /// <summary>
        /// Verifies that the the arc length between two of the same point is zero.
        /// </summary>
        [TestMethod]
        public void CalculateCentralAngle_SamePoint_ShouldBeZero()
        {
            var point1 = new RadianCoordinate(0.930115210104, -0.1093636224733);
            var point2 = new RadianCoordinate(0.930115210104, -0.1093636224733);
            var calculator = new Calculator();

            var centralAngle = calculator.CalculateCentralAngle(point1, point2);

            Assert.IsTrue(Math.Abs(centralAngle) < TOLERANCE);
        }

        /// <summary>
        /// Verifies that the arc length between the same point is zero.
        /// </summary>
        [TestMethod]
        public void CalculateArcLength_SamePoint_ShouldBeZero()
        {
            var point1 = new RadianCoordinate(0.930115210104, -0.1093636224733);
            var point2 = new RadianCoordinate(0.930115210104, -0.1093636224733);
            var calculator = new Calculator();

            var arcLength = calculator.CalculateArcLength(point1, point2, Calculator.EARTH_RADIUS_KM);

            Assert.IsTrue(Math.Abs(arcLength) < TOLERANCE);
        }

        /// <summary>
        /// Verifies that two points on the Earth are 5.33908km apart as confirmed by: https://keisan.casio.com/exec/system/1224587128
        /// </summary>
        [TestMethod]
        public void CalculateArcLength_PointsOnEarth()
        {
            var point1 = new RadianCoordinate(0.930115210104, -0.10936362247);
            var point2 = new RadianCoordinate(0.930948639728, -0.10921684028);
            var calculator = new Calculator();

            var expectedArcLength = 5.339089;

            var actualArcLength = calculator.CalculateArcLength(point1, point2, 6371);

            Assert.IsTrue(Math.Abs(actualArcLength - expectedArcLength) < TOLERANCE);
        }
    }
}
