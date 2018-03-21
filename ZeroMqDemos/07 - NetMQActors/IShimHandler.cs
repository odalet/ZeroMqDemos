using System.Threading;
using NetMQ.Sockets;

namespace NetMQActors
{
    /// <summary>
    /// Simple interface that all shims should implement
    /// </summary>
    public interface IShimHandler
    {
        void Run(PairSocket shim, object[] args, CancellationToken token);
    }
}
