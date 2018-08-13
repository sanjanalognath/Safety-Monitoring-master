using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

class SensorClient
{
    static int input;
    static DoorServerInterface doorServerInterface;
    static WindowServerInterface windowServerInterface;
    static FloorServerInterface floorServerInterface;

    public static void Main()
    {
        TcpChannel tcpChannel = new TcpChannel();
        ChannelServices.RegisterChannel(tcpChannel, false);

        Type doorRequiredType = typeof(DoorServerInterface);
        doorServerInterface = (DoorServerInterface)Activator.GetObject(doorRequiredType,
        "tcp://localhost:8700/DoorServerStatus");

        Type windowRequiredType = typeof(WindowServerInterface);
        windowServerInterface = (WindowServerInterface)Activator.GetObject(windowRequiredType,
        "tcp://localhost:8800/WindowServerStatus");

        Type floorRequiredType = typeof(FloorServerInterface);
        floorServerInterface = (FloorServerInterface)Activator.GetObject(floorRequiredType,
        "tcp://localhost:8900/FloorServerStatus");

        Console.WriteLine("Sensor Client\n");

        menu();


    }

    public static void menu() {
        Boolean validInput = false;
       
        while (!validInput)
        {
            Console.WriteLine("\nPlease entry your option:");
            Console.WriteLine("1. Receptionist Room - Entry Door");
            Console.WriteLine("2. Receptionist Room - Front Floor");
            Console.WriteLine("3. Receptionist Room - Back Floor");
            Console.WriteLine("4. Very Important Person room - Entry Door");
            Console.WriteLine("5. Very Important Person room - Window 1");
            Console.WriteLine("6. Very Important Person room - Window 2");
            Console.WriteLine("7. Very Important Person room - Front Floor");
            Console.WriteLine("8. Very Important Person room - Back Floor");
            Console.WriteLine("9. Exit\n");

            String inputString = Console.ReadLine();

            if (Int32.TryParse(inputString, out input))
            {
                if (input < 1 || input > 9)
                {
                    Console.WriteLine("Please provide valid input");
                }
                else
                {
                    validInput = true;
                }
            }
            else
            {
                Console.WriteLine("Please provide valid input");
            }
        }

        switch (input)
        {
            case 1:
                Console.WriteLine("Receptionist Room - Entry Door Status : " + doorServerInterface.GetDoorStatus("Receptionist Room - Entry Door"));
                break;
            case 2:
                Console.WriteLine("Receptionist Room - Front Floor Weight : " + floorServerInterface.GetFloorStatus("Receptionist Room - Front Floor"));
                break;
            case 3:
                Console.WriteLine("Receptionist Room - Back Floor Weight : " + floorServerInterface.GetFloorStatus("Receptionist Room - Back Floor"));
                break;
            case 4:
                Console.WriteLine("Very Important Person room - Entry Door Status : " + doorServerInterface.GetDoorStatus("Very Important Person room - Entry Door"));
                break;
            case 5:
                Console.WriteLine("Very Important Person room - Window 1 Status : " + windowServerInterface.GetWindowStatus("Very Important Person room - Window 1"));
                break;
            case 6:
                Console.WriteLine("Very Important Person room - Window 2 Status : " + windowServerInterface.GetWindowStatus("Very Important Person room - Window 2"));
                break;
            case 7:
                Console.WriteLine("Very Important Person room - Front Floor Weight : " + floorServerInterface.GetFloorStatus("Very Important Person room - Front Floor"));
                break;
            case 8:
                Console.WriteLine("Very Important Person room - Back Floor Weight : " + floorServerInterface.GetFloorStatus("Very Important Person room - Back Floor"));
                break;
            case 9:
                Console.WriteLine("Thank you !");
                break;
            default:
                break;
        }

        if (input > 0 && input < 9)
        {
            menu();
        }
    }


}
