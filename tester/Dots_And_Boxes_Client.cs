using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace tester
{
    public partial class Dots_And_Boxes_Client : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;


        private Thread outputThread; // Thread for receiving data from server
        private TcpClient connection; // client to establish connection      
        private NetworkStream stream; // network data stream                 
        private BinaryWriter writer; // facilitates writing to the stream    
        private BinaryReader reader; // facilitates reading from the stream  
        private int myMark; // player's mark on the board                   
        private bool myTurn; // is it this player's turn
        private bool done = false; // true when game is over

        public int p1, p2, player;


        private readonly bool[,] lin = new bool[13, 10];
        private readonly int[,] crs = new int[6, 9];
        readonly Dictionary<string, Label> m = new Dictionary<string, Label>();
        readonly Dictionary<string, Panel> pnl = new Dictionary<string, Panel>();
        public Dots_And_Boxes_Client()
        {
            InitializeComponent();
            Fm();
        }
        private void Fm()
        {
            pnl["000"] = pane000;
            pnl["001"] = pane001;
            pnl["002"] = pane002;
            pnl["003"] = pane003;
            pnl["004"] = pane004;
            pnl["005"] = pane005;
            pnl["006"] = pane006;
            pnl["007"] = pane007;
            pnl["008"] = pane008;
            pnl["010"] = pane010;
            pnl["011"] = pane011;
            pnl["012"] = pane012;
            pnl["013"] = pane013;
            pnl["014"] = pane014;
            pnl["015"] = pane015;
            pnl["016"] = pane016;
            pnl["017"] = pane017;
            pnl["018"] = pane018;
            pnl["019"] = pane019;
            pnl["020"] = pane020;
            pnl["021"] = pane021;
            pnl["022"] = pane022;
            pnl["023"] = pane023;
            pnl["024"] = pane024;
            pnl["025"] = pane025;
            pnl["026"] = pane026;
            pnl["027"] = pane027;
            pnl["028"] = pane028;
            pnl["030"] = pane030;
            pnl["031"] = pane031;
            pnl["032"] = pane032;
            pnl["033"] = pane033;
            pnl["034"] = pane034;
            pnl["035"] = pane035;
            pnl["036"] = pane036;
            pnl["037"] = pane037;
            pnl["038"] = pane038;
            pnl["039"] = pane039;
            pnl["040"] = pane040;
            pnl["041"] = pane041;
            pnl["042"] = pane042;
            pnl["043"] = pane043;
            pnl["044"] = pane044;
            pnl["045"] = pane045;
            pnl["046"] = pane046;
            pnl["047"] = pane047;
            pnl["048"] = pane048;
            pnl["050"] = pane050;
            pnl["051"] = pane051;
            pnl["052"] = pane052;
            pnl["053"] = pane053;
            pnl["054"] = pane054;
            pnl["055"] = pane055;
            pnl["056"] = pane056;
            pnl["057"] = pane057;
            pnl["058"] = pane058;
            pnl["059"] = pane059;
            pnl["060"] = pane060;
            pnl["061"] = pane061;
            pnl["062"] = pane062;
            pnl["063"] = pane063;
            pnl["064"] = pane064;
            pnl["065"] = pane065;
            pnl["066"] = pane066;
            pnl["067"] = pane067;
            pnl["068"] = pane068;
            pnl["070"] = pane070;
            pnl["071"] = pane071;
            pnl["072"] = pane072;
            pnl["073"] = pane073;
            pnl["074"] = pane074;
            pnl["075"] = pane075;
            pnl["076"] = pane076;
            pnl["077"] = pane077;
            pnl["078"] = pane078;
            pnl["079"] = pane079;
            pnl["080"] = pane080;
            pnl["081"] = pane081;
            pnl["082"] = pane082;
            pnl["083"] = pane083;
            pnl["084"] = pane084;
            pnl["085"] = pane085;
            pnl["086"] = pane086;
            pnl["087"] = pane087;
            pnl["088"] = pane088;
            pnl["090"] = pane090;
            pnl["091"] = pane091;
            pnl["092"] = pane092;
            pnl["093"] = pane093;
            pnl["094"] = pane094;
            pnl["095"] = pane095;
            pnl["096"] = pane096;
            pnl["097"] = pane097;
            pnl["098"] = pane098;
            pnl["099"] = pane099;
            pnl["100"] = pane100;
            pnl["101"] = pane101;
            pnl["102"] = pane102;
            pnl["103"] = pane103;
            pnl["104"] = pane104;
            pnl["105"] = pane105;
            pnl["106"] = pane106;
            pnl["107"] = pane107;
            pnl["108"] = pane108;
            pnl["110"] = pane110;
            pnl["111"] = pane111;
            pnl["112"] = pane112;
            pnl["113"] = pane113;
            pnl["114"] = pane114;
            pnl["115"] = pane115;
            pnl["116"] = pane116;
            pnl["117"] = pane117;
            pnl["118"] = pane118;
            pnl["119"] = pane119;
            pnl["120"] = pane120;
            pnl["121"] = pane121;
            pnl["122"] = pane122;
            pnl["123"] = pane123;
            pnl["124"] = pane124;
            pnl["125"] = pane125;
            pnl["126"] = pane126;
            pnl["127"] = pane127;
            pnl["128"] = pane128;


            m["00"] = label1;
            m["01"] = label2;
            m["02"] = label3;
            m["03"] = label4;
            m["04"] = label5;
            m["05"] = label6;
            m["06"] = label7;
            m["07"] = label8;
            m["08"] = label9;

            m["10"] = label10;
            m["11"] = label11;
            m["12"] = label12;
            m["13"] = label13;
            m["14"] = label14;
            m["15"] = label15;
            m["16"] = label16;
            m["17"] = label17;
            m["18"] = label18;

            m["20"] = label19;
            m["21"] = label20;
            m["22"] = label21;
            m["23"] = label22;
            m["24"] = label23;
            m["25"] = label24;
            m["26"] = label25;
            m["27"] = label26;
            m["28"] = label27;

            m["30"] = label28;
            m["31"] = label29;
            m["32"] = label30;
            m["33"] = label31;
            m["34"] = label32;
            m["35"] = label33;
            m["36"] = label34;
            m["37"] = label35;
            m["38"] = label36;

            m["40"] = label37;
            m["41"] = label38;
            m["42"] = label39;
            m["43"] = label40;
            m["44"] = label41;
            m["45"] = label42;
            m["46"] = label43;
            m["47"] = label44;
            m["48"] = label45;

            m["50"] = label46;
            m["51"] = label47;
            m["52"] = label48;
            m["53"] = label49;
            m["54"] = label50;
            m["55"] = label51;
            m["56"] = label52;
            m["57"] = label53;
            m["58"] = label54;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new TcpClient("127.0.0.1", 50000);
            stream = connection.GetStream();
            writer = new BinaryWriter(stream);
            reader = new BinaryReader(stream);
            outputThread = new Thread(new ThreadStart(Run));
            outputThread.Start();
        }
        private delegate void DisplayDelegate(string message);
        private void DisplayMessage(string message)
        {
            // if modifying displayTextBox is not thread safe
            if (displayTextBox.InvokeRequired)
            {
                // use inherited method Invoke to execute DisplayMessage
                // via a delegate                                       
                Invoke(new DisplayDelegate(DisplayMessage),
                   new object[] { message });
            } // end if
            else // OK to modify displayTextBox in current thread
                displayTextBox.Text += message;
        } // end method DisplayMessage

        private delegate void ChangeIdLabelDelegate(string message);

        // method ChangeIdLabel sets displayTextBox's Text property
        // in a thread-safe manner
        private void ChangeIdLabel(string label)
        {
            // if modifying idLabel is not thread safe
            if (acts.InvokeRequired)
            {
                // use inherited method Invoke to execute ChangeIdLabel
                // via a delegate                                       
                Invoke(new ChangeIdLabelDelegate(ChangeIdLabel),
                   new object[] { label });
            } // end if
            else // OK to modify idLabel in current thread
                acts.Text = label;
        } // end method ChangeIdLabel
        private void Dn(string location)
        {
            int row = Int32.Parse(location.Substring(0, 2));
            int col = Int32.Parse(location.Substring(2, 1));
            lin[row, col] = true;
            if (row % 2 == 0)
            {
                if (row / 2 > 0 && row / 2 < 6)
                {
                    crs[row / 2, col]++;
                    Ch(row / 2, col);
                    crs[row / 2 - 1, col]++;
                    Ch(row / 2 - 1, col);
                }
                else if (row / 2 == 0)
                {
                    crs[row / 2, col]++;
                    Ch(row / 2, col);
                }
                else
                {
                    crs[row / 2 - 1, col]++;
                    Ch(row / 2 - 1, col);
                }
            }
            else
            {
                if (col < 9 && col > 0)
                {
                    crs[row / 2, col]++;
                    Ch(row / 2, col);
                    crs[row / 2, col - 1]++;
                    Ch(row / 2, col - 1);
                }
                else if (col == 0)
                {
                    crs[row / 2, col]++;
                    Ch(row / 2, col);
                }
                else
                {
                    crs[row / 2, col - 1]++;
                    Ch(row / 2, col - 1);
                }
            }
        }
        public void Run()
        {
            // first get players's mark (X or O)                 
            myMark = reader.ReadInt32();
            ChangeIdLabel("You are player " + myMark);
            myTurn = (myMark == 1 ? true : false);

            // process incoming messages
            try
            {
                // receive messages sent to client       
                while (!done)
                    ProcessMessage(reader.ReadString());
            } // end try
            catch (IOException)
            {
                MessageBox.Show("Server is down, game over", "Error",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
            } // end catch
        } // end method Run
        public void ProcessMessage(string message)
        {
            // if the move the player sent to the server is valid
            // update the display, set that square's mark to be
            // the mark of the current player and repaint the board
            if (message == "Valid move.")
            {
                DisplayMessage("Valid move, please wait.\r\n");
                string loc = reader.ReadString();
                if (myMark == 1) pnl[loc].BackColor = Color.RoyalBlue;
                else pnl[loc].BackColor = Color.Brown;
                player = (myMark == 1) ? 1 : 2;
                Dn(loc);
                //currentSquare.Mark = myMark;
                //PaintSquares();
            } // end if
            else if (message == "Invalid move, try again.")
            {
                // if the move is invalid, display that and it is now
                // this player's turn again
                DisplayMessage(message + "\r\n");
                myTurn = true;

            } // end else if
            else if (message == "Opponent moved.")
            {
                // if opponent moved, find location of their move
                string location = reader.ReadString();
                int row = Int32.Parse(location.Substring(0, 2));
                int col = Int32.Parse(location.Substring(2, 1));
                lin[row, col] = true;

                if (myMark == 1) pnl[location].BackColor = Color.Brown;
                else pnl[location].BackColor = Color.RoyalBlue;
                player = (myMark == 1) ? 2 : 1;
                Dn(location);
                DisplayMessage("Opponent moved.  Your turn.\r\n");

                // it is now this player's turn
                myTurn = true;
            } // end else if
            else
                DisplayMessage(message + "\r\n"); // display message
        } // end method ProcessMessage

        private Label h;
        private void Cl1(string message)
        {
            if (h.InvokeRequired)
            {
                Invoke(new DisplayDelegate(Cl1),
                   new object[] { message });
            }
            else
            {
                h.Text = message;
                h.BackColor = Color.DarkBlue;
                h.Visible = true;
            }
        }
        private void Cl2(string message)
        {
            if (h.InvokeRequired)
            {
                Invoke(new DisplayDelegate(Cl2),
                   new object[] { message });
            }
            else
            {
                h.Text = message;
                h.BackColor = Color.DarkRed;
                h.Visible = true;
            }
        }
        Label k;
        private void Changescore(string message)
        {
            if (h.InvokeRequired)
            {
                Invoke(new DisplayDelegate(Changescore),
                   new object[] { message });
            }
            else k.Text = message;

        }

        void Ch(int x, int y)
        {
            string f = "" + x + y;
            if (crs[x, y] == 4)
            {
                if (player == 2)
                {
                    p2++;
                    h = m[f];
                    Cl2("2");
                    k = p2s;
                    Changescore(p2.ToString());
                }
                else
                {
                    p1++;
                    h = m[f];
                    Cl1("1");
                    k = p1s;
                    Changescore(p1.ToString());
                }
            }
            if (p1 + p2 == 54)
            {
                MessageBox.Show(reader.ReadString());
                return;
            }
        }
        private void Superevent_mousedown(object sender, MouseEventArgs e)
        {
            if (myTurn)
            {
                Panel l = (Panel)sender;
                string location = l.Name.Substring(4, 3);
                writer.Write(location);
                myTurn = false;
            }
            else DisplayMessage("It's not your turn ,, please wait \r\n");
        }

        #region form properties


        private void Panel1_MouseEnter(object sender, EventArgs e)
        {
            Panel l = (Panel)sender;
            int row = Int32.Parse(l.Name.Substring(4, 2));
            int col = Int32.Parse(l.Name.Substring(6, 1));
            if (!lin[row, col])
                l.BackColor = Color.Black;
        }

        private void Panel1_MouseLeave(object sender, EventArgs e)
        {
            Panel l = (Panel)sender;
            int row = Int32.Parse(l.Name.Substring(4, 2));
            int col = Int32.Parse(l.Name.Substring(6, 1));
            if (!lin[row, col])
                l.BackColor = Color.Gray;
        }

        private void Panelv_MouseEnter(object sender, EventArgs e)
        {
            Panel l = (Panel)sender;
            int row = Int32.Parse(l.Name.Substring(4, 2));
            int col = Int32.Parse(l.Name.Substring(6, 1));
            if (!lin[row, col])
                l.BackColor = Color.Black;
        }

        private void Panelv_MouseLeave(object sender, EventArgs e)
        {
            Panel l = (Panel)sender;
            int row = Int32.Parse(l.Name.Substring(4, 2));
            int col = Int32.Parse(l.Name.Substring(6, 1));
            if (!lin[row, col])
                l.BackColor = Color.Gray;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            done = true;
            System.Environment.Exit(System.Environment.ExitCode);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void DisplayTextBox_TextChanged(object sender, EventArgs e)
        {
            displayTextBox.SelectionStart = displayTextBox.Text.Length;
            displayTextBox.ScrollToCaret();
        }

        private void Dots_And_Boxes_Client_Activated(object sender, EventArgs e)
        {
            panel1.BackColor = panel2.BackColor = panel3.BackColor = panel4.BackColor = Color.SteelBlue;
        }

        private void Dots_And_Boxes_Client_Deactivate(object sender, EventArgs e)
        {
            panel1.BackColor = panel2.BackColor = panel3.BackColor = panel4.BackColor = Color.Gray;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void PictureBox71_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
