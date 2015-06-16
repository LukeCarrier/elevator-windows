using System;
using System.ServiceModel;
using System.Threading;

namespace ElevatorServer
{
    class Program
    {
        static ManualResetEvent QuitEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            // Start the server
            var endpoints = new Uri[] {
                new Uri(ProcessElevator.PipeUri),
            };
            using (var host = new ServiceHost(typeof(ProcessElevator), endpoints)) {
                host.AddServiceEndpoint(typeof(ElevatorCommon.IProcessElevator),
                                        new NetNamedPipeBinding(), "PipeProcessElevator");

                try
                {
                    host.Open();
                }
                catch (System.ServiceModel.AddressAlreadyInUseException e)
                {
                    Console.Error.WriteLine(String.Format("{0}: {1}", e.GetType().ToString(), e.Message));
                    Environment.Exit(e.HResult);
                }

                Console.Out.WriteLine("Server is up");

                // Exit only when the user interrupts us with Ctrl+C
                Console.CancelKeyPress += (sender, arguments) =>
                {
                    Console.Out.WriteLine("Cleaning up...");
                    QuitEvent.Set();
                    arguments.Cancel = true;
                    host.Close();
                };
                QuitEvent.WaitOne();
            }
        }
    }
}
