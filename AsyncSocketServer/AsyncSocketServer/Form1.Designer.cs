namespace AsyncSocketServer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAcceptIncomingConnection = new System.Windows.Forms.Button();
            this.btnSendToAll = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnStopServer = new System.Windows.Forms.Button();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnAcceptIncomingConnection
            // 
            this.btnAcceptIncomingConnection.Location = new System.Drawing.Point(21, 277);
            this.btnAcceptIncomingConnection.Name = "btnAcceptIncomingConnection";
            this.btnAcceptIncomingConnection.Size = new System.Drawing.Size(235, 33);
            this.btnAcceptIncomingConnection.TabIndex = 0;
            this.btnAcceptIncomingConnection.Text = "Accept Incpoming Connection";
            this.btnAcceptIncomingConnection.UseVisualStyleBackColor = true;
            this.btnAcceptIncomingConnection.Click += new System.EventHandler(this.btnAcceptIncomingConnection_Click);
            // 
            // btnSendToAll
            // 
            this.btnSendToAll.Location = new System.Drawing.Point(278, 277);
            this.btnSendToAll.Name = "btnSendToAll";
            this.btnSendToAll.Size = new System.Drawing.Size(75, 33);
            this.btnSendToAll.TabIndex = 1;
            this.btnSendToAll.Text = "&Send All";
            this.btnSendToAll.UseVisualStyleBackColor = true;
            this.btnSendToAll.Click += new System.EventHandler(this.btnSendToAll_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(372, 277);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "message";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(442, 277);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(163, 22);
            this.txtMessage.TabIndex = 3;
            this.txtMessage.Text = "Message for Client";
            // 
            // btnStopServer
            // 
            this.btnStopServer.Location = new System.Drawing.Point(21, 316);
            this.btnStopServer.Name = "btnStopServer";
            this.btnStopServer.Size = new System.Drawing.Size(131, 33);
            this.btnStopServer.TabIndex = 4;
            this.btnStopServer.Text = "S&top Server";
            this.btnStopServer.UseVisualStyleBackColor = true;
            this.btnStopServer.Click += new System.EventHandler(this.btnStopServer_Click);
            // 
            // txtConsole
            // 
            this.txtConsole.Location = new System.Drawing.Point(21, 31);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.Size = new System.Drawing.Size(605, 220);
            this.txtConsole.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 389);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.btnStopServer);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSendToAll);
            this.Controls.Add(this.btnAcceptIncomingConnection);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAcceptIncomingConnection;
        private System.Windows.Forms.Button btnSendToAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnStopServer;
        private System.Windows.Forms.TextBox txtConsole;
    }
}

