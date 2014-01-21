﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using AddressLibrary;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.Collections;
using Packet;
using System.Runtime.Serialization.Formatters.Binary;

namespace PolicyDirectory {

    public partial class PolicyDirectory : Form {
        
        public static List<int> speedList;

        delegate void SetTextCallback(string text);

        private static Address myAddr = new Address(0,0,2);

        private class userData {
            public string userName;
            public Address userAddr;
            public int userCap;
            public bool canReq;

            public userData(string userName, Address userAddr, int userCap, bool canReq) {
                this.userName = userName;
                this.userAddr = userAddr;
                this.userCap = userCap;
                this.canReq = canReq;
            }
        }

        //dane chmury
        private IPAddress cloudAddress;        //Adres na którym chmura nasłuchuje
        private Int32 cloudPort;           //port chmury
        private IPEndPoint cloudEndPoint;
        private Socket cloudSocket;

        private Thread receiveThread;     //wątek służący do odbierania połączeń
        private Thread sendThread;        // analogicznie - do wysyłania

        private Queue _whatToSendQueue;
        private Queue whatToSendQueue;

        //strumienie
        private NetworkStream networkStream;
        //lista klientów
        private List<userData> userList;

        private bool isDebug;

        public bool isConnectedToCloud { get; private set; } // czy połączony z chmurą?


        public PolicyDirectory() {
            userList = new List<userData>();
            isConnectedToCloud = false;
            isDebug = true;
             _whatToSendQueue = new Queue();
            whatToSendQueue = Queue.Synchronized(_whatToSendQueue);
            InitializeComponent();

            speedList = new List<int>();
            speedList.Add(2);
            speedList.Add(6);
            speedList.Add(10);
            BindingSource bs = new BindingSource();
            bs.DataSource = speedList;
            clientSpeedBox.DataSource = bs;
        }
        /// <summary>
        /// metoda wywołana po wciśnięciu "połącz z chmurą"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void conToCloudButton_Click(object sender, EventArgs e) {
            if (!isConnectedToCloud) {
                    if (IPAddress.TryParse(cloudIPTextBox.Text, out cloudAddress)) {
                        SetText("IP ustawiono jako " + cloudAddress.ToString());
                    } else {
                        SetText("Błąd podczas ustawiania IP chmury (zły format?)");
                    }
                    if (Int32.TryParse(cloudPortTextBox.Text, out cloudPort)) {
                        SetText("Port chmury ustawiony jako " + cloudPort.ToString());
                    } else {
                        SetText("Błąd podczas ustawiania portu chmury (zły format?)");
                    }

                    cloudSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    cloudEndPoint = new IPEndPoint(cloudAddress, cloudPort);
                    try {
                        cloudSocket.Connect(cloudEndPoint);
                        isConnectedToCloud = true;
                        networkStream = new NetworkStream(cloudSocket);
                        //writer = new StreamWriter(networkStream);
                        //reader = new StreamReader(networkStream);
                        List<String> _welcArr = new List<String>();
                        _welcArr.Add("HELLO");
                        SPacket welcomePacket = new SPacket(myAddr.ToString(), new Address(0, 0, 0).ToString(), _welcArr);
                        whatToSendQueue.Enqueue(welcomePacket);
                        //whatToSendQueue.Enqueue("HELLO " + myAddr);
                        receiveThread = new Thread(this.receiver);
                        receiveThread.IsBackground = true;
                        receiveThread.Start();
                        sendThread = new Thread(this.sender);
                        sendThread.IsBackground = true;
                        sendThread.Start();
                        conToCloudButton.Text = "Rozłącz";
                        SetText("Połączono!");
                    } catch (SocketException) {
                        isConnectedToCloud = false;
                        SetText("Błąd podczas łączenia się z chmurą");
                        SetText("Złe IP lub port? Chmura nie działa?");
                    }
            } else {
                isConnectedToCloud = false;
                conToCloudButton.Text = "Połącz";
                SetText("Rozłączono!");
                if (cloudSocket != null) cloudSocket.Close();
            }
        }

        public void sender() {
            while (isConnectedToCloud) {
                //jeśli coś jest w kolejce - zdejmij i wyślij
                if (whatToSendQueue.Count != 0) {
                    SPacket _pck = (SPacket)whatToSendQueue.Dequeue();
                    BinaryFormatter bformatter = new BinaryFormatter();
                    bformatter.Serialize(networkStream, _pck);
                    networkStream.Flush();
                    String[] _argsToShow = _pck.getParames().ToArray();
                    String argsToShow = "";
                    foreach (String str in _argsToShow) {
                        argsToShow += str + " ";
                    }
                    if (isDebug) SetText("Wysłano: " + _pck.getSrc() + ":" + _pck.getDest() + ":" + argsToShow);
                }
            }
        }

        public void SetText(string text) {
            // InvokeRequired required compares the thread ID of the 
            // calling thread to the thread ID of the creating thread. 
            // If these threads are different, it returns true. 
            if (this.log.InvokeRequired) {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            } else {
                try {
                    this.log.AppendText(text + "\n");
                } catch { }
            }
        }
        /// <summary>
        /// wątek odbierający wiadomości z chmury
        /// </summary>
        public void receiver() {
            while (isConnectedToCloud) {
                BinaryFormatter bf = new BinaryFormatter();
                try {
                    SPacket receivedPacket = (Packet.SPacket)bf.Deserialize(networkStream);
                    if (isDebug) SetText("Odczytano:\n" + receivedPacket.ToString());
                    List<String> _msgList = receivedPacket.getParames();
                    Address _senderAddr;
                    if (Address.TryParse(receivedPacket.getSrc(), out _senderAddr)) {
                        //gdy logowanie się
                        if (_msgList[0] == "LOGIN") {
                            try {
                                string usr = _msgList[1];
                                Address usrAddr = _senderAddr;
                                bool tempIsOk = true;
                                userData tempUser = null;
                                foreach (userData ud in userList) {
                                    if (ud.userName == usr || ud.userAddr.ToString() == _senderAddr.ToString()) {
                                        tempIsOk = false;
                                        tempUser = ud;
                                    }
                                }
                                if (tempIsOk) {
                                    userList.Add(new userData(usr, usrAddr, 6, true));
                                    List<string> temp = new List<string>();
                                    foreach (userData ud in userList) {
                                        temp.Add(ud.userName);
                                    }
                                    BindingSource bs = new BindingSource();
                                    bs.DataSource = temp;
                                    this.Invoke((MethodInvoker)delegate() {
                                        selectedClientBox.DataSource = bs;
                                    });
                                    SPacket pck = new SPacket(myAddr.ToString(), _senderAddr.ToString(), "OK");
                                    whatToSendQueue.Enqueue(pck);
                                    this.Invoke((MethodInvoker)delegate() {
                                        selectedClientBox_SelectedIndexChanged();
                                    });
                                } else {
                                    SPacket pck = new SPacket(myAddr.ToString(), _senderAddr.ToString(), "NAME_OR_ADDR_TAKEN_BY " + tempUser.userName + " WITH_ADDR " + tempUser.userAddr);
                                    whatToSendQueue.Enqueue(pck);
                                }
                            } catch {
                                SPacket pck = new SPacket(myAddr.ToString(), _senderAddr.ToString(), "ERROR");
                                whatToSendQueue.Enqueue(pck);
                            }
                        } else if (_msgList[0] == "REQ_CLIENTS") {
                            List<string> clients = new List<string>();
                            clients.Add("CLIENTS");
                            String callerName = String.Empty; 
                            foreach (userData ud in userList) {
                                if (ud.userAddr == _senderAddr) {
                                    callerName = ud.userName;
                                }
                            }
                            foreach (userData ud in userList) {
                                if (ud.userName != callerName) {
                                    clients.Add(ud.userName);
                                }
                            }
                            SPacket pck = new SPacket(myAddr.ToString(), _senderAddr.ToString(), clients);
                            whatToSendQueue.Enqueue(pck);
                        } else if (_msgList[0] == "REQ_CALL") {
                            try {
                                bool canCall = false;
                                foreach (userData ud in userList) {
                                    if (ud.userAddr.ToString() == _senderAddr.ToString()) {
                                        if (ud.canReq && int.Parse(_msgList[2]) <= ud.userCap) canCall = true;
                                    }
                                }
                                List<string> response = new List<string>();
                                if (canCall) {
                                    foreach (userData ud in userList) {
                                        if (ud.userName != _msgList[1]) {
                                            response.Add("YES");
                                            response.Add(ud.userAddr.ToString());
                                        }
                                    }
                                } else response.Add("NO");
                                SPacket pck = new SPacket(myAddr.ToString(), _senderAddr.ToString(), response);
                                whatToSendQueue.Enqueue(pck);
                            } catch {
                                SPacket pck = new SPacket(myAddr.ToString(), _senderAddr.ToString(), "ERROR");
                                whatToSendQueue.Enqueue(pck);
                            }
                        }
                    }
                } catch {
                    SetText("Coś poszło nie tak");
                }
            }
        }
        private void selectedClientBox_SelectedIndexChanged() {
            foreach (userData ud in userList) {
                if (ud.userName == (string)selectedClientBox.SelectedItem) {
                    addrLabel.Text = ud.userAddr.ToString();
                    canReqCheckBox.CheckState = ud.canReq ? CheckState.Checked : CheckState.Unchecked;
                    switch (ud.userCap) {
                        case (2):
                            clientSpeedBox.SelectedIndex = 0;
                            break;
                        case (6):
                            clientSpeedBox.SelectedIndex = 1;
                            break;
                        case (10):
                            clientSpeedBox.SelectedIndex = 2;
                            break;
                        default:
                            clientSpeedBox.SelectedIndex = 0;
                            break;
                    }
                }
            }
        }
        private void selectedClientBox_SelectedIndexChanged(object sender, EventArgs e) {
            selectedClientBox_SelectedIndexChanged();
        }

        private void canReqCheckBox_CheckedChanged(object sender, EventArgs e) {
            foreach (userData ud in userList) {
                if (ud.userName == (string)selectedClientBox.SelectedItem) {
                    if (canReqCheckBox.CheckState == CheckState.Checked) {
                        ud.canReq = true;
                    } else {
                        ud.canReq = false;
                    }
                }
            }
        }

        private void clientSpeedBox_SelectedIndexChanged(object sender, EventArgs e) {
            foreach (userData ud in userList) {
                if (ud.userName == (string)selectedClientBox.SelectedItem) {
                    switch (clientSpeedBox.SelectedIndex) {
                        case (0):
                            ud.userCap = 2;
                            break;
                        case (1):
                            ud.userCap = 6;
                            break;
                        case (2):
                            ud.userCap = 10;
                            break;
                        default:
                            ud.userCap = 2;
                            break;
                    }
                }
            }
        }


    }
}