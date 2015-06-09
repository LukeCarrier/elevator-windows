using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator
{
    class Program
    {
        static void Main(string[] args)
        {

            // We require an image, else we have nothing to do
            if (args.Length < 1)
            {
                System.Console.Error.WriteLine(Elevator.Strings.MissingImageName);
                Environment.Exit(ExitCodes.MissingImageName);
            }

            // Initialise some stuff
            var argsList = args.ToList();
            var process = new System.Diagnostics.Process();
            var outputBuffer = new StringBuilder();

            // Proxy all of our arguments to the image
            process.StartInfo.FileName = argsList.First();
            if (argsList.Count > 2)
            {
                process.StartInfo.Arguments = String.Join(" ", argsList.GetRange(1, argsList.Count - 1));
            }

            // These options don't fit in with our manual approach
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.ErrorDialog = false;

            // We're going to redirect our streams by ourselves
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;

            // Register a handler for the output event
            process.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(StandardOutput);

            // Launch the process
            if (!process.Start()) {
                System.Console.Error.WriteLine(Strings.ProcessStartFailed);
                Environment.Exit(ExitCodes.ProcessStartFailed);
            }

            // Begin handling output
            process.BeginOutputReadLine();

            // TODO: handle input here, maybe along the lines of this:
            //   https://msdn.microsoft.com/en-us/library/system.diagnostics.datareceivedeventhandler%28v=vs.110%29.aspx

            // Close the process, bubbling its exit status
            process.WaitForExit();
            var exitCode = process.ExitCode;
            process.Close();
            Environment.Exit(exitCode);
        }

        protected static void StandardOutput(object sendingProcess, DataReceivedEventArgs outLine) {
            System.Console.Out.WriteLine(outLine.Data);
        }
    }
}
