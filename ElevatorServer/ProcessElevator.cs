using System;

using ElevatorCommon;

namespace ElevatorServer
{
    class ProcessElevator : ElevatorCommon.IProcessElevator
    {
        public const string PipeUri = "net.pipe://localhost/ProcessElevator";

        public int GetExitCode()
        {
            return 1;
        }

        public void Exit()
        {
            Environment.Exit(0);
        }

        public void Ping()
        {
        }
    }
}
