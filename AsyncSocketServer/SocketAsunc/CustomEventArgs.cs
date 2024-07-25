using System;

namespace SocketAsync
{
    // Ensure the ClientConnectedEventArgs class is public
    public class ClientConnectedEventArgs : EventArgs
    {
        public string NewClient { get; set; }

        public ClientConnectedEventArgs(string _newClient)
        {
            NewClient = _newClient;
        }
    }

    public class TextReceivedEventArgs : EventArgs
    {
        public string ClientWhoSentText { get; set; }
        public string TextReceived { get; set; }


        public TextReceivedEventArgs(string _clientWhoSentText, string _textReceived)
        {
            TextReceived = _textReceived;
        }
    }
}
