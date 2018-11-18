using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntercomChallenge.Models.Tests
{
    [TestClass]
    public class CustomerContractTests
    {
        /// <summary>
        /// Verifies that a valid JSON string is correctly deserialized to a customer object.
        /// </summary>
        [TestMethod]
        public void Deserialize_ValidJson_InstantiatesNewObjectFromJson()
        {
            var json = "{\"latitude\": \"52.986375\", \"user_id\": 12, \"name\": \"Christina McArdle\", \"longitude\": \"-6.043701\"}";

            var customer = CustomerContract.Deserialize(json);

            Assert.IsNotNull(customer);
            Assert.AreEqual(52.986375, customer.Latitude);
            Assert.AreEqual(12, customer.UserId);
            Assert.AreEqual("Christina McArdle", customer.Name);
            Assert.AreEqual(-6.043701, customer.Longitude);
        }

        /// <summary>
        /// Verifies that a JSON string missing the latitude property does not instantiate a customer object.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void Deserialize_JsonMissingLatitude_ThrowsJsonSerializationException()
        {
            var json = "{\"user_id\": 12, \"name\": \"Christina McArdle\", \"longitude\": \"-6.043701\"}";

            var customer = CustomerContract.Deserialize(json);
        }

        /// <summary>
        /// Verifies that a JSON string missing the longitude property does not instantiate a customer object.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void Deserialize_JsonMissingLongitude_ThrowsJsonSerializationException()
        {
            var json = "{\"latitude\": \"52.986375\", \"user_id\": 12, \"name\": \"Christina McArdle\"}";

            var customer = CustomerContract.Deserialize(json);
        }

        /// <summary>
        /// Verifies that a JSON string missing the user id property does not instantiate a customer object.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void Deserialize_JsonMissingUserId_ThrowsJsonSerializationException()
        {
            var json = "{\"latitude\": \"52.986375\", \"name\": \"Christina McArdle\", \"longitude\": \"-6.043701\"}";

            var customer = CustomerContract.Deserialize(json);
        }

        /// <summary>
        /// Verifies that a JSON string missing the name property does not instantiate a customer object.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(JsonSerializationException))]
        public void Deserialize_JsonMissingName_ThrowsJsonSerializationException()
        {
            var json = "{\"latitude\": \"52.986375\", \"user_id\": 12, \"longitude\": \"-6.043701\"}";

            var customer = CustomerContract.Deserialize(json);
        }

        /// <summary>
        /// Verifies that an empty JSON string does not instantiate a customer object.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Deserialize_EmptyJson_ThrowsArgumentException()
        {
            var json = string.Empty;

            var customer = CustomerContract.Deserialize(json);
        }

        /// <summary>
        /// Verifies that a null JSON string missing does not instantiate a customer object.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Deserialize_NullJson_ThrowsArgumentExcption()
        {
            string json = null;

            var customer = CustomerContract.Deserialize(json);
        }

        /// <summary>
        /// Verifies that a customer with a name and a valid poistion on Earth is valid.
        /// </summary>
        [TestMethod]
        public void IsValid_ValidCustomer_ReturnsTrue()
        {
            var customer = new CustomerContract { Name = "Bob", UserId = 1, Latitude = 58, Longitude = 6 };
            Assert.IsTrue(customer.IsValid());
        }

        /// <summary>
        /// Verifies that a customer without a name is not valid.
        /// </summary>
        [TestMethod]
        public void IsValid_NullName_ReturnsFalse()
        {
            var customer = new CustomerContract { Name = null, UserId = 1, Latitude = 58, Longitude = 6 };
            Assert.IsFalse(customer.IsValid());
        }

        /// <summary>
        /// Verifies that a customer without a name is not valid.
        /// </summary>
        [TestMethod]
        public void IsValid_EmptyName_ReturnsFalse()
        {
            var customer = new CustomerContract { Name = string.Empty, UserId = 1, Latitude = 58, Longitude = 6 };
            Assert.IsFalse(customer.IsValid());
        }

        /// <summary>
        /// Verifies that a customer in an invalid position on earth is not valid.
        /// </summary>
        [TestMethod]
        public void IsValid_InvalidCoordinate_ReturnsFalse()
        {
            var customer = new CustomerContract { Name = "Bob", UserId = 1, Latitude = 91, Longitude = 6 };
            Assert.IsFalse(customer.IsValid());
        }
    }
}
