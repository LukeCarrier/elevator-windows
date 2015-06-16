using System;
using System.ServiceModel;

namespace ElevatorCommon
{
    [ServiceContract(Namespace="https://github.com/LukeCarrier/elevator-windows/ProcessElevator")]
    public interface IProcessElevator
    {
        [OperationContract]
        int GetExitCode();

        [OperationContract]
        void Ping();

        [OperationContract]
        void Exit();
    }
}
