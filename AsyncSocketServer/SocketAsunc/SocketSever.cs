using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketAsync
{
    public class SocketServer
    {
        IPAddress mIP;
        int mPort;
        TcpListener mTCPListener;

        public event EventHandler<ClientConnectedEventArgs> RaiseClientConnectedEvent;
        public EventHandler<TextReceivedEventArgs> RaiseTextReceivedEvent;
        List<TcpClient> mClients;

        public bool KeepRunning { get; set; }

        public SocketServer()
        {
            mClients = new List<TcpClient>();
        }

        protected virtual void OnRaiseClientConnectedEvent(ClientConnectedEventArgs e)
        {
            EventHandler<ClientConnectedEventArgs> handler = RaiseClientConnectedEvent;
            handler?.Invoke(this, e);
        }

        protected virtual void OnRaiseTextReceivedEvent(TextReceivedEventArgs trea)
        {
            EventHandler<TextReceivedEventArgs> handler = RaiseTextReceivedEvent;
            handler?.Invoke(this, trea);
        }


        public async Task StartListeningForIncomingConnection(IPAddress ipaddr = null, int port = 23000)
        {
            if (ipaddr == null)
            {
                ipaddr = IPAddress.Any;
            }

            if (port <= 0)
            {
                port = 23000;
            }

            mIP = ipaddr;
            mPort = port;

            Debug.WriteLine($"IP Address: {mIP} - Port: {mPort}");

            mTCPListener = new TcpListener(mIP, mPort);

            try
            {
                mTCPListener.Start();

                KeepRunning = true;

                while (KeepRunning)
                {
                    var returnedByAccept = await mTCPListener.AcceptTcpClientAsync();

                    mClients.Add(returnedByAccept);

                    Debug.WriteLine($"Client connected successfully, count: {mClients.Count} - {returnedByAccept.Client.RemoteEndPoint}");

                    OnRaiseClientConnectedEvent(new ClientConnectedEventArgs(returnedByAccept.Client.RemoteEndPoint.ToString()));

                    _ = TakeCareOfTCPClient(returnedByAccept); // Discard the Task intentionally to run asynchronously

                    ClientConnectedEventArgs eaClientConnected;
                    eaClientConnected = new ClientConnectedEventArgs(
                        returnedByAccept.Client.RemoteEndPoint.ToString()
                        );
                    OnRaiseClientConnectedEvent(eaClientConnected);
                }
            }
            catch (Exception excp)
            {
                Debug.WriteLine(excp.ToString());
            }
        }

        public void StopServer()
        {
            try
            {
                if (mTCPListener != null)
                {
                    mTCPListener.Stop();
                }

                foreach (TcpClient c in mClients)
                {
                    c.Close();
                }

                mClients.Clear();
            }
            catch (Exception excp)
            {
                Debug.WriteLine(excp.ToString());
            }
        }

        private async Task TakeCareOfTCPClient(TcpClient paramClient)
        {
            NetworkStream stream = null;
            StreamReader reader = null;

            try
            {
                stream = paramClient.GetStream();
                reader = new StreamReader(stream);

                char[] buff = new char[64];

                while (KeepRunning)
                {
                    Debug.WriteLine("*** Ready to read");

                    int nRet = await reader.ReadAsync(buff, 0, buff.Length);

                    Debug.WriteLine("Returned: " + nRet);

                    if (nRet == 0)
                    {
                        RemoveClient(paramClient);

                        Debug.WriteLine("Socket disconnected");
                        break;
                    }

                    string receivedText = new string(buff, 0, nRet);

                    System.Diagnostics.Debug.WriteLine("*** RECEIVED: " + receivedText);

                    OnRaiseTextReceivedEvent(new TextReceivedEventArgs(
                        paramClient.Client.RemoteEndPoint.ToString(),
                        receivedText
                        ));

                    Array.Clear(buff, 0, buff.Length);
                }
            }
            catch (Exception excp)
            {
                RemoveClient(paramClient);
                Debug.WriteLine(excp.ToString());
            }
        }

        private void RemoveClient(TcpClient paramClient)
        {
            if (mClients.Contains(paramClient))
            {
                mClients.Remove(paramClient);
                Debug.WriteLine($"Client removed, count: {mClients.Count}");
            }
        }

        public async Task SendToAll(string leMessage)
        {
            if (string.IsNullOrEmpty(leMessage))
            {
                return;
            }

            try
            {
                byte[] buffMessage = Encoding.ASCII.GetBytes(leMessage);

                foreach (TcpClient c in mClients)
                {
                    await c.GetStream().WriteAsync(buffMessage, 0, buffMessage.Length);
                }
            }
            catch (Exception excp)
            {
                Debug.WriteLine(excp.ToString());
            }
        }
    }
}
