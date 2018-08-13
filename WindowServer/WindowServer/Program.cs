using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

class Program
{
    static void Main(string[] args)
    {
        WindowServer();
    }

    static void WindowServer()
    {
        Console.WriteLine("Window Server started...");

        TcpChannel tcpChannel = new TcpChannel(8800);
        ChannelServices.RegisterChannel(tcpChannel, false);

        Type commonInterfaceType = Type.GetType("WindowServer");

        RemotingConfiguration.RegisterWellKnownServiceType(commonInterfaceType,
        "WindowServerStatus", WellKnownObjectMode.SingleCall);

        System.Console.WriteLine("Press ENTER to quit\n");
        System.Console.ReadLine();

    }

}

public interface WindowServerInterface
{
    string GetWindowStatus(string stringToPrint);
}

public class WindowServer : MarshalByRefObject, WindowServerInterface
{
    public string GetWindowStatus(string stringToPrint)
    {
        Random rnd = new Random();
        String[] status = new string[] { "Open", "Half open", "Closed", "Closed and locked" };
        int statusNo = rnd.Next(0, 4);

        string returnStatus = status[statusNo];
        Console.WriteLine("Enquiry for {0}", stringToPrint);
        Console.WriteLine("Sending back status: {0}", returnStatus);

        return returnStatus;
    }
}
