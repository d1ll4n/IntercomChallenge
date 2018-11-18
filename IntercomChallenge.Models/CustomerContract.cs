using IntercomChallenge.Models.Coordinates;
using IntercomChallenge.Models.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntercomChallenge.Models
{
    /// <summary>
    /// Represents an Intercom customer.
    /// </summary>
    public class CustomerContract
    {
        [JsonProperty(PropertyName = "user_id", Required = Required.Always)]
        public int UserId { get; set; }

        [JsonProperty(PropertyName = "name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "latitude", Required = Required.Always)]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude", Required = Required.Always)]
        public double Longitude { get; set; }

        /// <summary>
        /// Deserializes a customer from a JSON string.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static CustomerContract Deserialize(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
            {
                throw new ArgumentException($"{nameof(json)} may not be null or empty.");
            }

            return JsonConvert.DeserializeObject<CustomerContract>(json);
        }

        /// <summary>
        /// Determines if a customer instance is valid.
        /// </summary>
        /// <returns>True if the customer has a name and a valid location on Earth, else false.</returns>
        public bool IsValid()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Name))
                {
                    return false;
                }

                var position = new DegreeCoordinate(Latitude, Longitude);

                return true;
            }
            catch(InvalidCoordinateException e)
            {
                return false;
            }
        }
    }
}
