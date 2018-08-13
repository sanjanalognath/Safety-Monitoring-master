using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

class Program
{
    static void Main(string[] args)
    {
        DoorServer();
    }

    static void DoorServer()
    {
        Console.WriteLine("Door Server started...");

        TcpChannel tcpChannel = new TcpChannel(8700);
        ChannelServices.RegisterChannel(tcpChannel, false);

        Type commonInterfaceType = Type.GetType("DoorServer");

        RemotingConfiguration.RegisterWellKnownServiceType(commonInterfaceType,
        "DoorServerStatus", WellKnownObjectMode.SingleCall);

        System.Console.WriteLine("Press ENTER to quit\n");
        System.Console.ReadLine();

    }

}

public interface DoorServerInterface
{
    string GetDoorStatus(string stringToPrint);
}

public class DoorServer : MarshalByRefObject, DoorServerInterface
{
    public string GetDoorStatus(string stringToPrint)
    {
        Random rnd = new Random();
        String[] status = new string[] { "Open", "Closed but not locked", "Closed and locked" };
        int statusNo = rnd.Next(0, 3); 

        string returnStatus = status[statusNo];
        Console.WriteLine("Enquiry for {0}", stringToPrint);
        Console.WriteLine("Sending back status: {0}", returnStatus);

        return returnStatus;
    }
}
