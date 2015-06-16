using System;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading;

using ElevatorCommon;

namespace ElevatorClient
{
    class Program
    {
        private const string ElevatorServerFileName = "ElevatorServer.exe";

        private const string ProcessElevatorPipeUri = "net.pipe://localhost/ProcessElevator/";

        private const int ProcessElevatorWaitTimeout = 10;

        private Process ServerProcess;

        static void Main(string[] args)
        {
            new Program().Run();
        }

        public Program()
        {
            ServerProcess = new Process();
        }

        public void Run()
        {
            // Try to locate the server binary.
            var elevatorServerPath = ElevatorCommon.PathUtils.FindOnPath(ElevatorServerFileName);
            if (string.IsNullOrEmpty(elevatorServerPath))
            {
                Environment.Exit(ExitCodes.ElevatorServerLaunchFailed);
            }

            // Launch the server process which actually does the elevation.
            Console.Out.WriteLine("Server launching");
            var processStartInfo = new ProcessStartInfo()
            {
                FileName = elevatorServerPath,
                Verb = "runas",
            };
            ServerProcess.StartInfo = processStartInfo;
            if (ServerProcess.Start())
            {
                Console.Out.WriteLine("Server launched");
            }
            else
            {
                Console.Error.WriteLine("Failed to launch server");
                Environment.Exit(ElevatorCommon.ExitCodes.ElevatorServerLaunchFailed);
            }

            // Open a pipe to the process we just opened.
            var pipeFactory = new ChannelFactory<ElevatorCommon.IProcessElevator>(
                    new NetNamedPipeBinding(), new EndpointAddress(ProcessElevatorPipeUri));
            var elevatorServer = pipeFactory.CreateChannel();

            var result = RetryableUtil.InvokeWithRetry(elevatorServer.GetExitCode);

            Console.Out.WriteLine("Got response from server of: " + result.ToString());
        }
    }
}
