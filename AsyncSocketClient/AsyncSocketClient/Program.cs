using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocketAsync;

namespace UdemyAsyncSocketClient
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketClient client = new SocketClient();
            client.RaiseTextReceivedEvent += HandleTextReceived;
            Console.WriteLine("*** Welcome to Socket Client ***");
            Console.WriteLine("Please Type a Valid Server IP Address and Press Enter: ");

            string strIPAddress = Console.ReadLine();

            Console.WriteLine("Please Supply a Valid Port Number 0 - 65535 and Press Enter: ");
            string strPortInput = Console.ReadLine();

            if(strIPAddress.StartsWith("<HOST>"))
            {
                strIPAddress = strIPAddress.Replace("<HOST>", string.Empty);
                strIPAddress = Convert.ToString(SocketClient.ResolveHostNameToIPAddress(strIPAddress));
            }

            if (!client.SetServerIPAddress(strIPAddress) ||
                    !client.SetPortNumber(strPortInput))
            {
                Console.WriteLine(
                    string.Format(
                        "Wrong IP Address or port number supplied - {0} - {1} - Press a key to exit",
                        strIPAddress,
                    strPortInput));
                Console.ReadKey();
                return;
            }

            client.ConnectToServer();

            string strInputUser = null;

            do
            {
                strInputUser = Console.ReadLine();

                if (strInputUser.Trim() != "<EXIT>")
                {
                    client.SendToServer(strInputUser);
                }
                if (string.IsNullOrEmpty(strIPAddress))
                {
                    Console.WriteLine("No ip address suplied...");
                }
                else if (strInputUser.Equals("<EXIT>"))
                {
                    client.CloseAndDisconnect();
                }
               

            } while (strInputUser != "<EXIT>");



        }
        private static void HandleTextReceived(object sender, TextReceivedEventArgs trea) 
        {
            Console.WriteLine(
                string.Format(
                    "{0} - Received: {1}{2}",
                    DateTime.Now,
                    trea.TextReceived,
                    Environment.NewLine));
        }
    }
}
