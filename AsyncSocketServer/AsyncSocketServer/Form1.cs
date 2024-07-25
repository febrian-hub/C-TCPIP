using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SocketAsync;

namespace AsyncSocketServer
{
    
    public partial class Form1 : Form
    {
        SocketServer mServer;
        public Form1()
        {
            InitializeComponent();
            mServer = new SocketServer();
            mServer.RaiseClientConnectedEvent += HandleClientConnected;
            mServer.RaiseTextReceivedEvent += HandleTextReceived;
        }

        private void btnAcceptIncomingConnection_Click(object sender, EventArgs e)
        {
            mServer.StartListeningForIncomingConnection();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSendToAll_Click(object sender, EventArgs e)
        {
            mServer.SendToAll(txtMessage.Text.Trim());
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            mServer.StopServer();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mServer.StopServer();
        }

        void HandleClientConnected(object sender, ClientConnectedEventArgs ccea)
        {
            txtConsole.AppendText(string.Format("(0) - New Client Connected: {1} {2}",
                DateTime.Now, ccea.NewClient, Environment.NewLine));
        }

        void HandleTextReceived(object sender, TextReceivedEventArgs trea)
        {
            txtConsole.AppendText(
                string.Format(
                "(0) - Received from {3}: {1} {2}",
                DateTime.Now,
                trea.TextReceived, 
                Environment.NewLine,
                trea.ClientWhoSentText));
        }
    }
}
