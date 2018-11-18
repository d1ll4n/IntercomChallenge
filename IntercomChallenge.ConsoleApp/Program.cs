using IntercomChallenge.Models;
using IntercomChallenge.Models.Coordinates;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IntercomChallenge.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Please specify the path to the input file in the command line arguments.");
                return;
            }

            if (string.IsNullOrWhiteSpace(args[0]) || !File.Exists(args[0]))
            {
                Console.WriteLine("Input file not found.");
                return;
            }

            try
            {
                var lines = File.ReadAllLines(args[0]);
                var customers = new List<CustomerContract>();
                var calculator = new Calculator();

                var dublinOfficeLocation = new RadianCoordinate(Calculator.ConvertDegreesToRadians(53.339428), Calculator.ConvertDegreesToRadians(-6.257664));

                foreach (var line in lines)
                {
                    try
                    {
                        var customer = CustomerContract.Deserialize(line);

                        if (customer.IsValid())
                        {
                            customers.Add(customer);
                        }
                    }
                    catch (JsonSerializationException e)
                    {
                        continue;
                    }
                    catch (ArgumentNullException e)
                    {
                        continue;
                    }
                    catch (ArgumentException e)
                    {
                        continue;
                    }
                }

                var invitationManager = new InvitationManager(calculator, dublinOfficeLocation);
                var customersToInvite = invitationManager.DetermineCustomersToInvite(customers, 100d);

                Console.WriteLine("The following customers should be invited:");

                foreach (var customer in customersToInvite)
                {
                    Console.WriteLine($"{customer.Name} ({customer.UserId})");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"An unexpected error occurred: {e.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
