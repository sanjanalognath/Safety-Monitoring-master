using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

class Program
{
    static void Main(string[] args)
    {
        FloorServer();
    }

    static void FloorServer()
    {
        Console.WriteLine("Floor Server started...");

        TcpChannel tcpChannel = new TcpChannel(8900);
        ChannelServices.RegisterChannel(tcpChannel, false);

        Type commonInterfaceType = Type.GetType("FloorServer");

        RemotingConfiguration.RegisterWellKnownServiceType(commonInterfaceType,
        "FloorServerStatus", WellKnownObjectMode.SingleCall);

        System.Console.WriteLine("Press ENTER to quit\n");
        System.Console.ReadLine();

    }

}

public interface FloorServerInterface
{
    string GetFloorStatus(string stringToPrint);
}

public class FloorServer : MarshalByRefObject, FloorServerInterface
{
    public string GetFloorStatus(string stringToPrint)
    {
        Random rnd = new Random();
       
        string returnStatus = NextDouble(rnd, 1, 100).ToString("0.###");

        Console.WriteLine("Enquiry for {0}", stringToPrint);
        Console.WriteLine("Sending back weight: {0}", returnStatus);

        return returnStatus;
    }

    double NextDouble(Random rnd, double min, double max)
    {
        return min + (rnd.NextDouble() * (max - min));
    }
}
