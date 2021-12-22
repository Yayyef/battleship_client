using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace src
{
    public class Client
    {
        private Socket clientSocket;
        private byte[] data = new byte[1024];
        private static int port = 1000;



        public void ConnectServer()
        {

            try
            {

                // Establish the remote endpoint
                // for the socket. This example
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = new IPAddress(0x972ba8c0);
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, port);

                // Creation TCP/IP Socket using
                // Socket Class Constructor
                clientSocket = new Socket(ipAddr.AddressFamily,
                           SocketType.Stream, ProtocolType.Tcp);

                try
                {

                    // Connect Socket to the remote
                    // endpoint using method Connect()
                    clientSocket.Connect(localEndPoint);

                    // We print EndPoint information
                    // that we are connected
                    Console.WriteLine("You're successfully connected now! -> {0} ",
                                  clientSocket.RemoteEndPoint.ToString());

                }

                // Manage of Socket's Exceptions
                catch (ArgumentNullException ane)
                {

                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }

                catch (SocketException se)
                {

                    Console.WriteLine("SocketException : {0}", se.ToString());
                }

                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }
            }

            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
        }
   





        public string GetPosition(int[] outPos)
        {
            int received = clientSocket.Receive(data);
            string msg= Encoding.ASCII.GetString(data,0,received);
            while (clientSocket.Available > 0)
            {
                received = clientSocket.Receive(data);
                msg += Encoding.ASCII.GetString(data,0,received);
            }
                if (!GetCoord(msg, outPos))
                {
                    Console.WriteLine("Bad input from client. Aborting...");
                    return "";
                }
            
            return msg;
        }

        public void SendResponse(string message)
        {
            if (clientSocket == null)
            {
                Console.WriteLine("Start the server before sending positions stupid");
                return;
            }
            data = Encoding.ASCII.GetBytes(message);
            clientSocket.Send(data);
        }

        public string GetResponse()
        {
            if (clientSocket == null)
            {
                Console.WriteLine("Start the server before sending positions stupid");
                return "";
            }

            int received = clientSocket.Receive(data);
            string msg = Encoding.ASCII.GetString(data,0,received);
            while (clientSocket.Available > 0)
            {
                received = clientSocket.Receive(data);
                msg += Encoding.ASCII.GetString(data,0,received);
            }


            return msg;

        }
        ~Client()
        {

            if (clientSocket != null)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
        }

        public bool GetCoord(string position, int[] coord)
        {
            if (position.Length < 2)
                return false;
            coord[0] = position[0] - 'a';
            coord[1] = Convert.ToInt32(position.Substring(1)) - 1;
            if (coord[0] < 0 || coord[0] > 9 || coord[1] < 0 || coord[1] > 9)
                return false;
            return true;
        }

    }
}

