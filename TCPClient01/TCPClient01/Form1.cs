using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace TCPClient01
{
    public partial class Form1 : Form
    {
        TcpClient mTcpClient;
        byte[] mRx;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            IPAddress ipa;
            int nPort;

            try
            {
                if (string.IsNullOrEmpty(tbServerIP.Text) || string.IsNullOrEmpty(tbServerPort.Text))
                {
                    MessageBox.Show("Please supply both IP Address and Port");
                    return;
                }

                if (!IPAddress.TryParse(tbServerIP.Text, out ipa))
                {
                    MessageBox.Show("Please supply a valid IP Address");
                    return;
                }

                if (!int.TryParse(tbServerPort.Text, out nPort))
                {
                    nPort = 23000;
                }

                mTcpClient = new TcpClient();
                mTcpClient.BeginConnect(ipa, nPort, onCompleteConnect, mTcpClient);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        void onCompleteConnect(IAsyncResult iar)
        {
            TcpClient tcpc;

            try
            {
                tcpc = (TcpClient)iar.AsyncState;
                tcpc.EndConnect(iar);
                mRx = new byte[512];
                tcpc.GetStream().BeginRead(mRx, 0, mRx.Length, onCompleteReadFromServerStream, tcpc);
                MessageBox.Show("Connected to the server!");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        void onCompleteReadFromServerStream(IAsyncResult iar)
        {
            TcpClient tcpc;
            int nCountBytesReceivedFromServer;
            string strReceived;
            try
            {
                tcpc = (TcpClient)iar.AsyncState;
                nCountBytesReceivedFromServer = tcpc.GetStream().EndRead(iar);

                if (nCountBytesReceivedFromServer == 0)
                {
                    MessageBox.Show("Connection broken....");
                    return;
                }
                strReceived = Encoding.ASCII.GetString(mRx, 0, nCountBytesReceivedFromServer);

                printLine(strReceived);

                mRx = new byte[512];

                tcpc.GetStream().BeginRead(mRx, 0, mRx.Length, onCompleteReadFromServerStream, tcpc);

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbSend_Click(object sender, EventArgs e)
        {
            byte[] tx;
            if (string.IsNullOrEmpty(tbPayload.Text)) return;

            try
            {
                tx = Encoding.ASCII.GetBytes(tbPayload.Text);

                if (mTcpClient != null && mTcpClient.Client != null)
                {
                    if (mTcpClient.Connected)
                    {
                        mTcpClient.GetStream().BeginWrite(tx, 0, tx.Length, onCompleteWriteToServer, mTcpClient);
                    }
                }
                else
                {
                    MessageBox.Show("Client not connected to server.");
                }

            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        public void printLine(string _strPrint)
        {
            tbConsole.Invoke(new Action<string>(doInvoke), _strPrint);
        }

        public void doInvoke(string _strPrint)
        {
            tbConsole.Text = _strPrint + Environment.NewLine + tbConsole.Text;
        }

        void onCompleteWriteToServer(IAsyncResult iar)
        {
            TcpClient tcpc;

            try
            {
                tcpc = (TcpClient)iar.AsyncState;
                tcpc.GetStream().EndWrite(iar);
                MessageBox.Show("Data sent to the server!");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
