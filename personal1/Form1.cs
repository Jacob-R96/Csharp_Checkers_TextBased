using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static personal1.Form1.Board;

namespace personal1
{

    public partial class Form1 : Form
    {
        static Board myBoard = new Board(8);
        public string CheckerPiece;
        public Button[,] btnGrid = new Button[8, 8];
        public bool Checkerstored = false;
        
        public Form1()
        {
            InitializeComponent();
            MaketheBoard();
            radioButton2.Checked = true;
        }

        private void MaketheBoard()
        {
            int buttonsize = panel1.Width / 8;
            panel1.Height = panel1.Width;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    
                    btnGrid[i, j] = new Button();
                    btnGrid[i, j].Height = buttonsize;
                    btnGrid[i, j].Width = buttonsize;
                    btnGrid[i, j].Click += Store_Button_Click;
                    panel1.Controls.Add(btnGrid[i, j]);

                    btnGrid[i, j].Location = new Point(i * buttonsize, j * buttonsize);
                    btnGrid[i, j].Tag = new Point(i, j);

                    if (i == 0 || i == 2 || i == 4 || i == 6)
                    {
                        if (j == 1 || j == 3 || j == 5 || j == 7)
                        {
                            btnGrid[i, j].BackColor = Color.DarkGray;
                        }

                    }
                    if (i == 1 || i == 3 || i == 5 || i == 7)
                    {
                        if (j == 0 || j == 2 || j == 4 || j == 6)
                        {
                            btnGrid[i, j].BackColor = Color.DarkGray;
                        }
                    }
                    btnGrid[i, j].Text = i + "|" + j;
                }
            }
          //  radioButton1.Visible=false; radioButton2.Visible=false; checkedbox1.Visible=false;
          //Black Checker Pieces 
            btnGrid[0, 1].Text = "()";  btnGrid[1, 0].Text = "()"; btnGrid[1, 2].Text = "()"; btnGrid[2, 1].Text = "()"; btnGrid[3, 2].Text = "()"; btnGrid[3, 0].Text = "()"; btnGrid[4, 1].Text = "()"; btnGrid[5, 0].Text = "()"; btnGrid[5, 2].Text = "()"; btnGrid[6, 1].Text = "()"; 
            btnGrid[7, 0].Text = "()"; btnGrid[7, 2].Text = "()";
             btnGrid[0, 1].ForeColor = Color.Black; btnGrid[1, 0].ForeColor = Color.Black; btnGrid[1, 2].ForeColor = Color.Black; btnGrid[2, 1].ForeColor = Color.Black; btnGrid[3, 2].ForeColor = Color.Black; btnGrid[3, 0].ForeColor = Color.Black;
            btnGrid[4, 1].ForeColor = Color.Black; btnGrid[5, 0].ForeColor = Color.Black; btnGrid[5, 2].ForeColor = Color.Black; btnGrid[6, 1].ForeColor = Color.Black; btnGrid[7, 0].ForeColor = Color.Black; btnGrid[7, 2].ForeColor = Color.Black;
          
          //Red Checker Pieces
            btnGrid[0, 7].Text = "()"; btnGrid[0, 5].Text = "()"; btnGrid[1, 6].Text = "()"; btnGrid[2, 5].Text = "()"; btnGrid[2, 7].Text = "()"; btnGrid[3, 6].Text = "()"; btnGrid[4, 5].Text = "()"; btnGrid[4, 7].Text = "()"; btnGrid[5, 6].Text = "()"; btnGrid[6, 5].Text = "()";
            btnGrid[6, 7].Text = "()"; btnGrid[7, 6].Text = "()";
             btnGrid[0, 7].ForeColor = Color.Red; btnGrid[0, 5].ForeColor = Color.Red; btnGrid[1, 6].ForeColor = Color.Red; btnGrid[2, 5].ForeColor = Color.Red; btnGrid[2, 7].ForeColor = Color.Red;
            btnGrid[3, 6].ForeColor = Color.Red; btnGrid[4, 5].ForeColor = Color.Red; btnGrid[4, 7].ForeColor = Color.Red; btnGrid[5, 6].ForeColor = Color.Red; btnGrid[6, 5].ForeColor = Color.Red; btnGrid[6, 7].ForeColor = Color.Red; btnGrid[7, 6].ForeColor = Color.Red;

        }

        private void Store_Button_Click(object sender, EventArgs e)
        {
            bool Switch = false;
            bool RedLeft = false;
            bool RedRight = false;
            bool BlackRight = false;
            bool BlackLeft = false;
           
           

            Color Colorr = Color.Blue;
                if (radioButton1.Checked == true) { Colorr = Color.Black; }
                if (radioButton2.Checked == true) { Colorr = Color.Red; }
            Button clickedButton = (Button)sender;
            Point location = (Point)clickedButton.Tag;
            int x = location.X;
            int y = location.Y;

            Cell currentcell = myBoard.TheGrid[x, y];

            if (btnGrid[x, y].Text == "()" ) { boolKingCheck.Checked = false; }
            if (btnGrid[x, y].Text == "(K)") { boolKingCheck.Checked = true; }
           
            if (boolKingCheck.Checked == false)
            {
                //BLACK
                if (radioButton1.Checked == true && btnGrid[x, y].Text != "" && btnGrid[x, y].ForeColor == Color.Black )
                {
                    // This decides if a jump can legally be made for the Black Checker pieces
                    try { if ((btnGrid[x + 1, y + 1].Text == "()" || btnGrid[x + 1, y + 1].Text == "(K)") && btnGrid[x + 1, y + 1].ForeColor == Color.Red && btnGrid[x + 2, y + 2].Text == "") { BlackRight = true; } } catch { }
                    try { if ((btnGrid[x - 1, y + 1].Text == "()" || btnGrid[x - 1, y + 1].Text == "(K)") && btnGrid[x - 1, y + 1].ForeColor == Color.Red && btnGrid[x - 2, y + 2].Text == "") { BlackLeft = true; } } catch { }

                    BlackMovement(ref currentcell, ref Switch, ref x, ref y, ref RedLeft, ref RedRight, ref BlackLeft, ref BlackRight);
                }

                //RED
                if (radioButton2.Checked == true && btnGrid[x, y].Text != "" && btnGrid[x, y].ForeColor == Color.Red && btnGrid[x, y].Text != "K" && (btnGrid[x, y].Text != "Legal" || btnGrid[x, y].Text != "Clicked"))
                {
                    // This decides if a jump can legally be made for The Red Checker pieces
                    try { if ((btnGrid[x + 1, y - 1].Text == "()" || btnGrid[x + 1, y - 1].Text == "(K)") && btnGrid[x + 1, y - 1].ForeColor == Color.Black && btnGrid[x + 2, y - 2].Text == "") { RedLeft = true; } } catch { }
                    try { if ((btnGrid[x - 1, y - 1].Text == "()" || btnGrid[x - 1, y - 1].Text == "(K)") && btnGrid[x - 1, y - 1].ForeColor == Color.Black && btnGrid[x - 2, y - 2].Text == "") { RedRight = true; } } catch { }

                    RedMovement(ref currentcell, ref Switch, ref x, ref y, ref RedLeft, ref RedRight, ref BlackLeft, ref BlackRight);

                }
            }
            if (boolKingCheck.Checked == true)
            {
                //KING MOVEMENT(BLACK/RED)
                if (btnGrid[x, y].Text == "(K)" || btnGrid[x, y].Text == "Legal" || btnGrid[x, y].Text == "Clicked" || btnGrid[x, y].Text == "1" || btnGrid[x, y].Text == "2" || btnGrid[x, y].Text == "3" || btnGrid[x, y].Text == "4")
                {
                    boolKingCheck.Checked = true;
                    // This decides if a jump can legally be made
                    try { if ((btnGrid[x + 1, y + 1].Text == "()" || btnGrid[x + 1, y + 1].Text == "(K)") && btnGrid[x + 1, y + 1].ForeColor != Colorr && btnGrid[x + 2, y + 2].Text == "") { RedRight = true; } } catch { }
                    try { if ((btnGrid[x - 1, y + 1].Text == "()" || btnGrid[x - 1, y + 1].Text == "(K)") && btnGrid[x - 1, y + 1].ForeColor != Colorr && btnGrid[x - 2, y + 2].Text == "") { RedLeft = true; } } catch { }
                    try { if ((btnGrid[x + 1, y - 1].Text == "()" || btnGrid[x + 1, y - 1].Text == "(K)") && btnGrid[x + 1, y - 1].ForeColor != Colorr && btnGrid[x + 2, y - 2].Text == "") { BlackLeft = true; } } catch { }
                    try { if ((btnGrid[x - 1, y - 1].Text == "()" || btnGrid[x - 1, y - 1].Text == "(K)") && btnGrid[x - 1, y - 1].ForeColor != Colorr && btnGrid[x - 2, y - 2].Text == "") { BlackRight = true; } } catch { }
                    KingMovement(ref currentcell, ref Switch, ref x, ref y, ref RedLeft, ref RedRight, ref BlackLeft, ref BlackRight);
                   
                   
                }
                 
            }
          
        }

        private void KingMovement(ref Cell currentcell, ref bool Switch, ref int x, ref int y, ref bool RedLeft, ref bool RedRight, ref bool BlackLeft,  ref bool BlackRight)
        {
            myBoard.CheckerNextLegalMove(currentcell, "KingMe", RedLeft, RedRight, BlackLeft, BlackRight);
            {
                Color Colorr = Color.Blue;
              
                bool doublejumpswitch = false;

                //These two lines will color the checker text and checker piece movement with the valid color
                if (radioButton1.Checked == true) { Colorr = Color.Black; }
                if (radioButton2.Checked == true) { Colorr = Color.Red; }

                if (booldoublejump.Checked == false)
                { 
                    // This display in square where its legally to jump a boards checker piece
                    try { if (RedRight == true && btnGrid[x + 1, y + 1].ForeColor != Colorr) { btnGrid[x + 2, y + 2].Text = "1"; btnGrid[x + 2, y + 2].ForeColor = Colorr; } } catch { }
                    try { if (RedLeft == true && btnGrid[x - 1, y + 1].ForeColor != Colorr) { btnGrid[x - 2, y + 2].Text = "2"; btnGrid[x - 2, y + 2].ForeColor = Colorr; } } catch { }
                    try { if (BlackLeft == true && btnGrid[x + 1, y - 1].ForeColor != Colorr) { btnGrid[x + 2, y - 2].Text = "3"; btnGrid[x + 2, y - 2].ForeColor = Colorr; } } catch { }
                    try { if (BlackRight == true && btnGrid[x - 1, y - 1].ForeColor != Colorr) { btnGrid[x - 2, y - 2].Text = "4"; btnGrid[x - 2, y - 2].ForeColor = Colorr; } } catch { }

                    // A nested loop to read the 2D board.
                    for (int i = 0; i < myBoard.Size1; i++)
                    {
                        // A nested loop to read the 2D board.
                        for (int j = 0; j < myBoard.Size1; j++)
                        {
                            if (btnGrid[i, j].Text == "clicked") { btnGrid[i, j].Text = "()"; }
                            //Clear the board and transition the text Clicked to a King piece
                            if (btnGrid[i, j].Text == "Clicked") { btnGrid[i, j].Text = "(K)"; }
                            //Input a Clicked text on button that represent a King piece
                            if (btnGrid[x, y].Text != "Legal" && Switch == false)
                            {
                                if (btnGrid[x, y].Text != "1" && btnGrid[x, y].Text != "2" && btnGrid[x, y].Text != "3" && btnGrid[x, y].Text != "4" && btnGrid[x, y].ForeColor == Colorr)
                                {
                                    btnGrid[x, y].Text = "Clicked";
                                    btnGrid[x, y].ForeColor = Colorr;
                                }
                            }
                            //Deploy legal movemnent of the King switch from myboard method
                            if (myBoard.TheGrid[i, j].LegalNextMove == true && btnGrid[i, j].Text != "()" && btnGrid[i, j].Text != "(K)" && btnGrid[x, y].ForeColor == Colorr)
                            {
                                btnGrid[i, j].Text = "Legal";
                                if (btnGrid[i, j].Text == "Legal") { btnGrid[i, j].ForeColor = Colorr; }


                            }
                            //If a button is with Legal text exchanges with a (K) text
                            if (btnGrid[x, y].Text == "Legal")
                            {
                                Switch = true;
                                btnGrid[x, y].Text = "(K)";
                                btnGrid[x, y].ForeColor = Colorr;
                            }
                            //If a button is with 1 text exchanges with a (K) text and the piece that is jumped is cleared
                            if (btnGrid[x, y].Text == "1" )
                            {
                                Switch = true;
                                btnGrid[x, y].Text = "(K)";
                                btnGrid[x - 1, y + 1].Text = "";
                                btnGrid[x, y].ForeColor = Colorr;
                                if ((RedLeft == true || RedRight == true || BlackLeft == true || BlackRight == true) && Switch == true) { doublejumpswitch = true; }
                            }
                            //If a button is with 2 text exchanges with a (K) text and the piece that is jumped is cleared

                            if (btnGrid[x, y].Text == "2" )
                            {
                                Switch = true;
                                btnGrid[x, y].Text = "(K)";
                                btnGrid[x + 1, y - 1].Text = "";
                                btnGrid[x, y].ForeColor = Colorr;
                                if ((RedLeft == true || RedRight == true || BlackLeft == true || BlackRight == true) && Switch == true) { doublejumpswitch = true; }

                            }
                            //If a button is with 3 text exchanges with a (K) text and the piece that is jumped is cleared
                            if (btnGrid[x, y].Text == "3" )
                            {
                                Switch = true;
                                btnGrid[x, y].Text = "(K)";
                                btnGrid[x - 1, y + 1].Text = "";
                                btnGrid[x, y].ForeColor = Colorr;
                                if ((RedLeft == true || RedRight == true || BlackLeft == true || BlackRight == true) && Switch == true) { doublejumpswitch = true; }
                            }
                            //If a button is with 4 text exchanges with a (K) text and the piece that is jumped is cleared
                            if (btnGrid[x, y].Text == "4" )
                            {
                                Switch = true;
                                btnGrid[x, y].Text = "(K)";
                                btnGrid[x + 1, y + 1].Text = "";
                                btnGrid[x, y].ForeColor = Colorr;
                                if ((RedLeft == true || RedRight == true || BlackLeft == true || BlackRight == true) && Switch == true) { doublejumpswitch = true; }
                            }
                            if (btnGrid[x, y].Text == "()")
                            {
                                if (btnGrid[i, j].Text == "Clicked") { btnGrid[i, j].Text = "(K)"; }
                            }
                            // deploy legal jumping values if the doublejump bool is true and then enable the Doublejump method when the check box is true
                            if (doublejumpswitch == true)
                            {
                                Clearlegals();
                                if (RedRight == true) { btnGrid[x + 2, y + 2].Text = "1"; btnGrid[x + 2, y + 2].ForeColor = Colorr; } 
                                if (RedLeft == true) { btnGrid[x - 2, y + 2].Text = "2"; btnGrid[x - 2, y + 2].ForeColor = Colorr; } 
                                if (BlackLeft == true) { btnGrid[x + 2, y - 2].Text = "3"; btnGrid[x + 2, y - 2].ForeColor = Colorr; } 
                                if (BlackRight == true) { btnGrid[x - 2, y - 2].Text = "4"; btnGrid[x - 2, y - 2].ForeColor = Colorr; }
                                
                                btnGrid[x, y].Text = "Clicked";
                                booldoublejump.Checked = true;
                            }

                            // If double jump is false after a jump then it the opposite checker piece turn 
                            if (Switch == true && doublejumpswitch==false )
                            {
                                Clearlegals();
                                if (Colorr == Color.Black) { radioButton2.Checked = true; boolKingCheck.Checked = false; Clearlegals(); }
                                else  { radioButton1.Checked = true; boolKingCheck.Checked = false; Clearlegals(); }
                            }
                        }
                    }
                }
                else {DoubleShakeAndBake(ref currentcell, ref Switch, ref x, ref y, ref RedLeft, ref RedRight, ref BlackLeft, ref BlackRight); }
            }
        }
        
          private void RedMovement(ref Cell currentcell, ref bool Switch, ref int x, ref int y, ref bool RedLeft, ref bool RedRight, ref bool BlackLeft, ref bool BlackRight)
        {
           
            myBoard.CheckerNextLegalMove(currentcell, "Redcheckerpiece", RedLeft, RedRight, BlackLeft, BlackRight);
            {
                bool anotherturn=false;
                bool doublejumpswitch = false;
                bool switchh=false;

                

                if (booldoublejump.Checked == false)
                {
                    // A nested loop to read the 2D board.
                    for (int i = 0; i < myBoard.Size1; i++)
                    {
                        // A nested loop to read the 2D board.
                        for (int j = 0; j < myBoard.Size1; j++)
                        {
                           
                            // This decides if a jump can legally be made
                            if (RedLeft == true) { btnGrid[x + 2, y - 2].Text = "1"; btnGrid[x + 2, y - 2].ForeColor = Color.Red; }
                            if (RedRight == true) { btnGrid[x - 2, y - 2].Text = "2"; btnGrid[x - 2, y - 2].ForeColor = Color.Red; }

                            //Clear the board and transition the text Clicked to a King piece/clicked to a regular piece
                            
                            if (btnGrid[i, j].Text == "clicked") { btnGrid[i, j].Text = "()"; }
                            if (btnGrid[i, j].Text == "Clicked") { btnGrid[i, j].Text = "(K)"; }
                            //Clear the board of any illegal text
                            if (btnGrid[i, j].Text != "()" && btnGrid[i, j].Text != "(K)") { btnGrid[i, j].Text = ""; }

                            //Input a clicked text on button that represent a regular piece
                            if (btnGrid[x, y].Text != "legal" && switchh == false && btnGrid[x, y].Text != "1" && btnGrid[x, y].Text != "2" && btnGrid[x, y].Text != "(K)" && btnGrid[x, y].Text != "Clicked" && btnGrid[x, y].Text != "Legal")
                            {
                                btnGrid[x, y].Text = "clicked";
                            }

                            //Deploy legal movemnent of the Red piece in the switch from myboard method
                            if (myBoard.TheGrid[i, j].LegalNextMove == true && btnGrid[i, j].Text != "()" && btnGrid[i, j].Text != "(K)")
                            {

                                btnGrid[i, j].Text = "legal";
                                btnGrid[i, j].ForeColor = Color.Red;
                            }

                            //If a button is with 1 text exchanges with a () text and the piece that is jumped is cleared
                            if (btnGrid[x, y].Text == "1")
                            {
                                btnGrid[x, y].Text = "()"; btnGrid[x, y].ForeColor = Color.Red; 
                                switchh = true;
                                btnGrid[x, y].ForeColor = Color.Red;
                                btnGrid[x - 1, y + 1].Text = "";
                                
                                if (RedLeft == true || RedRight == true) { doublejumpswitch = true; }
                            }

                            //If a button is with 2 text exchanges with a () text and the piece that is jumped is cleared
                            if (btnGrid[x, y].Text == "2")
                            {
                                btnGrid[x, y].Text = "()"; btnGrid[x, y].ForeColor = Color.Red;
                                switchh = true;
                                btnGrid[x, y].ForeColor = Color.Red;
                                btnGrid[x + 1, y + 1].Text = "";
                                if (RedLeft == true || RedRight == true) { doublejumpswitch = true; }
                            }

                            //If a button is with legal text exchanges with a () text and the piece that is jumped is cleared
                            if (btnGrid[x, y].Text == "legal")
                            {
                                btnGrid[x, y].Text = "()"; btnGrid[x, y].ForeColor = Color.Red; 
                                switchh = true;
                                btnGrid[x, y].ForeColor = Color.Red;
                            }

                            // When double jump is deploy the Legal text comes in which is a illegal move during a double jump. This clears that Legal text
                            if (switchh == true)   
                            {
                                if (y == 0)    {    btnGrid[x, y].Text = "(K)";    btnGrid[x, y].ForeColor = Color.Red;    }
                                Clearlegals();   
                            }

                            // deploy legal jumping values if the doublejump bool is true and then enable the Doublejump method when the check box is true
                            if (doublejumpswitch == true && (RedLeft == true || RedRight == true))
                            {
                                anotherturn = true;
                                if (RedLeft == true) { btnGrid[x + 2, y - 2].Text = "1"; btnGrid[x + 2, y - 2].ForeColor = Color.Red; }
                                if (RedRight == true) { btnGrid[x - 2, y - 2].Text = "2"; btnGrid[x - 2, y - 2].ForeColor = Color.Red; }
                                btnGrid[x, y].Text = "clicked";
                                booldoublejump.Checked = true;
                            }

                            // If double jump is false after a jump then it the opposite checker piece turn 
                            if (switchh == true && anotherturn == false)
                            {
                                Clearlegals();
                                radioButton1.Checked = true;
                            }
                        }
                    }
                }
                else { DoubleShakeAndBake(ref currentcell, ref Switch, ref x, ref y, ref RedLeft, ref RedRight, ref BlackLeft, ref BlackRight); }
                
            }
        }
        private void BlackMovement(ref Cell currentcell, ref bool Switch, ref int x, ref int y, ref bool RedLeft, ref bool RedRight, ref bool BlackLeft, ref bool BlackRight)
        {
            
            myBoard.CheckerNextLegalMove(currentcell, "Blackcheckerpiece", RedLeft, RedRight, BlackLeft, BlackRight);
            {
                bool switchh=false;
                bool Anotherturnn = false;
                bool doublejumpswitch = false;

                if (booldoublejump.Checked == false)
                {// A nested loop to read the 2D board.
                    for (int i = 0; i < myBoard.Size1; i++)
                    {// A nested loop to read the 2D board.
                        for (int j = 0; j < myBoard.Size1; j++)
                        {
                            
                            // This decides if a jump can legally be made
                            try {      if (BlackRight == true) {        btnGrid[x + 2, y + 2].Text = "4";               btnGrid[x + 2, y + 2].ForeColor = Color.Black;   }      }   catch { }
                            try {      if (BlackLeft == true)  {        btnGrid[x - 2, y + 2].Text = "3";               btnGrid[x - 2, y + 2].ForeColor = Color.Black;   }      }    catch { }
                           
                            //Clear the board and transition the text Clicked to a King piece/clicked to a regular piece
                            if (btnGrid[i, j].Text == "Clicked")     {      btnGrid[i, j].Text = "(K)";      }
                            if (btnGrid[i, j].Text == "clicked")     {       btnGrid[i, j].Text = "()";      }

                            //Clear the board of any illegal text
                            if (btnGrid[i, j].Text != "()" && btnGrid[i, j].Text != "(K)")      {   btnGrid[i, j].Text = "";    }

                            //Input a clicked text on button that represent a regular piece
                            if (btnGrid[x, y].Text != "legal" && switchh == false && btnGrid[x, y].Text != "3" && btnGrid[x, y].Text != "4" && btnGrid[x, y].Text != "(K)" && btnGrid[x, y].Text != "Clicked" && btnGrid[x, y].Text != "Legal")
                            {
                                    btnGrid[x, y].Text = "clicked";
                            }

                            //Deploy legal movemnent of the Red piece in the switch from myboard method
                            if (myBoard.TheGrid[i, j].LegalNextMove == true && btnGrid[i, j].Text != "()" && btnGrid[i, j].Text != "(K)")
                            {
                                btnGrid[i, j].Text = "legal";
                                btnGrid[i, j].ForeColor = Color.Black;
                            }

                            //If a button is with 3 text exchanges with a () text and the piece that is jumped is cleared // Also reads to see if a double jump is legal
                            if (btnGrid[x, y].Text == "3")
                            {
                                btnGrid[x, y].Text = "()";               btnGrid[x, y].ForeColor = Color.Black; 
                                switchh = true;

                                btnGrid[x, y].ForeColor = Color.Black;
                                btnGrid[x + 1, y - 1].Text = "";

                                if (BlackLeft == true || BlackRight == true) { doublejumpswitch = true; }
                            }

                            //If a button is with 4 text exchanges with a () text and the piece that is jumped is cleared// Also reads to see if a double jump is legal
                            if (btnGrid[x, y].Text == "4")
                            {
                                btnGrid[x, y].Text = "()";               btnGrid[x, y].ForeColor = Color.Black; 
                                switchh = true;
                                btnGrid[x, y].ForeColor = Color.Black;
                                btnGrid[x - 1, y - 1].Text = "";
                                if (BlackLeft == true || BlackRight == true) { doublejumpswitch = true; }
                            }

                            //If a button is with legal text exchanges with a () text and the piece that is jumped is cleared
                            if (btnGrid[x, y].Text == "legal" )
                            {
                                switchh = true;
                                btnGrid[x, y].Text = "()";
                                btnGrid[x, y].ForeColor = Color.Black;
                            }
                            // When double jump is deploy the legal text comes in which is a illegal move during a double jump. This clears that Legal text
                            if (switchh == true)    
                            { 
                                if (y == 7)      {       btnGrid[x, y].Text = "(K)";         btnGrid[x, y].ForeColor = Color.Black;       }
                                Clearlegals(); 
                            }
                           
                            // deploy legal jumping values if the doublejump bool is true and then enable the Doublejump method when the check box is true
                            if (doublejumpswitch == true && (BlackLeft == true || BlackRight == true))
                            {
                                Anotherturnn = true;
                                if (BlackRight == true) { btnGrid[x + 2, y + 2].Text = "4"; btnGrid[x + 2, y + 2].ForeColor = Color.Black; }
                                if (BlackLeft == true) { btnGrid[x - 2, y + 2].Text = "3"; btnGrid[x - 2, y + 2].ForeColor = Color.Black; }
                                btnGrid[x, y].Text = "clicked";
                                booldoublejump.Checked = true;
                            }

                            // If double jump is false after a jump then it the opposite checker piece turn 
                            if (switchh == true && Anotherturnn == false) 
                            {
                                Clearlegals();
                                radioButton2.Checked = true; 
                            }
                        }
                    }
                }
                
                else DoubleShakeAndBake(ref currentcell, ref Switch, ref x, ref y, ref RedLeft, ref RedRight, ref BlackLeft, ref BlackRight);
            }
        }
        private void DoubleShakeAndBake(ref Cell currentcell, ref bool Switch, ref int x, ref int y, ref bool RedLeft, ref bool RedRight, ref bool BlackLeft, ref bool BlackRight)
        {
            Color Colorr = Color.Blue;

            bool doublejumpswitch = false;

            if (radioButton1.Checked == true) { Colorr = Color.Black; }
            if (radioButton2.Checked == true) { Colorr = Color.Red; }
          
               

            if (boolKingCheck.Checked == false && (btnGrid[x, y].Text == "clicked"|| btnGrid[x, y].Text =="1"|| btnGrid[x, y].Text =="2"|| btnGrid[x, y].Text =="3"|| btnGrid[x, y].Text =="4"))
            {
                //RED-MOVEMENT
                try { if (RedRight == true && btnGrid[x + 2, y + 2].Text != "()") { btnGrid[x + 2, y + 2].Text = "1"; btnGrid[x + 2, y + 2].ForeColor = Colorr; } } catch { }
                try { if (RedLeft == true && btnGrid[x - 2, y + 2].Text != "()") { btnGrid[x - 2, y + 2].Text = "2"; btnGrid[x - 2, y + 2].ForeColor = Colorr; } } catch { }
                //BLACK-MOVEMENT
                try { if (BlackLeft == true && btnGrid[x + 2, y - 2].Text != "()") { btnGrid[x - 2, y + 2].Text = "3"; btnGrid[x - 2, y + 2].ForeColor = Color.Black; } } catch { }
                try { if (BlackRight == true && btnGrid[x - 2, y - 2].Text != "()") { btnGrid[x + 2, y + 2].Text = "4"; btnGrid[x + 2, y + 2].ForeColor = Color.Black; } } catch { }

                //If a button is with 1 text exchanges with a () text and the piece that is jumped is cleared// Also reads to see if a double jump is legal

                if (btnGrid[x, y].Text == "clicked")
                {
                    btnGrid[x, y].Text = "clicked";
                }
                if (btnGrid[x, y].Text == "1")
                {
                    Switch = true;
                    btnGrid[x, y].Text = "()";
                    btnGrid[x - 1, y + 1].Text = "";
                    btnGrid[x, y].ForeColor = Colorr;
                    if (RedLeft == true || RedRight == true || BlackLeft == true || BlackRight == true) { doublejumpswitch = true; }

                    if (y == 0) { btnGrid[x, y].Text = "(K)"; btnGrid[x, y].ForeColor = Color.Red; }

                }
                //If a button is with 2 text exchanges with a () text and the piece that is jumped is cleared// Also reads to see if a double jump is legal
                if (btnGrid[x, y].Text == "2")
                {
                    Switch = true;
                    btnGrid[x, y].Text = "()";
                    btnGrid[x + 1, y + 1].Text = "";
                    btnGrid[x, y].ForeColor = Colorr;
                    if (RedLeft == true || RedRight == true || BlackLeft == true || BlackRight == true) { doublejumpswitch = true; }

                    if (y == 0) { btnGrid[x, y].Text = "(K)"; btnGrid[x, y].ForeColor = Color.Red; }


                }
                //If a button is with 3 text exchanges with a () text and the piece that is jumped is cleared// Also reads to see if a double jump is legal
                if (btnGrid[x, y].Text == "3")
                {
                    Switch = true;
                    btnGrid[x, y].Text = "()";
                    btnGrid[x + 1, y - 1].Text = "";
                    btnGrid[x, y].ForeColor = Colorr;
                    if (RedLeft == true || RedRight == true || BlackLeft == true || BlackRight == true) { doublejumpswitch = true; }

                    if (y == 7 && btnGrid[x, y].ForeColor == Color.Black) { btnGrid[x, y].Text = "(K)"; btnGrid[x, y].ForeColor = Color.Black; }
                }
                //If a button is with 4 text exchanges with a () text and the piece that is jumped is cleared// Also reads to see if a double jump is legal
                if (btnGrid[x, y].Text == "4")
                {
                    Switch = true;
                    btnGrid[x, y].Text = "()";
                    btnGrid[x - 1, y - 1].Text = "";
                    btnGrid[x, y].ForeColor = Colorr;
                    if (RedLeft == true || RedRight == true || BlackLeft == true || BlackRight == true) { doublejumpswitch = true; }

                    if (y == 7 && btnGrid[x, y].ForeColor == Color.Black) { btnGrid[x, y].Text = "(K)"; btnGrid[x, y].ForeColor = Color.Black; }
                }
                if (Switch == true) { Clearlegals(); }
                //KING DOESNT DELETE TEXT 'clicked' KING RANDOMLY DIES/ KING JUMP ITS OWN PAWNS
                // If double jump is false after a jump then it the opposite checker piece turn 
                if (doublejumpswitch == false && boolKingCheck.Checked == false&& Switch==true)
                {
                    if (Colorr == Color.Red && Switch == true)
                    {
                        radioButton1.Checked = true;
                        boolKingCheck.Checked = false;
                        Clearlegals();
                        booldoublejump.Checked = false;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                        boolKingCheck.Checked = false;
                        Clearlegals();
                        booldoublejump.Checked = false;
                    }
                }
                // deploy legal jumping values if the doublejump bool is true 
                if (doublejumpswitch == true)
                {
                    Clearlegals();
                    //RED
                    try { if (RedLeft == true) { btnGrid[x + 2, y - 2].Text = "1"; btnGrid[x + 2, y - 2].ForeColor = Colorr; } } catch { }
                    try { if (RedRight == true) { btnGrid[x - 2, y - 2].Text = "2"; btnGrid[x - 2, y - 2].ForeColor = Colorr; } } catch { }
                   //BLACK
                    try { if (BlackLeft == true) { btnGrid[x - 2, y + 2].Text = "3"; btnGrid[x - 2, y + 2].ForeColor = Colorr; } } catch { }
                    try { if (BlackRight == true) { btnGrid[x - 2, y - 2].Text = "4"; btnGrid[x - 2, y - 2].ForeColor = Colorr; } } catch { }
                    btnGrid[x, y].Text = "clicked";

                }
            }


            //King movement in a doublejump or triple jump situation
            if (boolKingCheck.Checked == true && (btnGrid[x, y].Text == "Clicked" || btnGrid[x, y].Text == "1" || btnGrid[x, y].Text == "2" || btnGrid[x, y].Text == "3" || btnGrid[x, y].Text == "4"))
            {
                //If a button is with 1 text exchanges with a (K) text and the piece that is jumped is cleared// Also reads to see if a double jump is legal
                if (btnGrid[x, y].Text == "1")
                {
                    Switch = true;
                    btnGrid[x, y].Text = "(K)";
                    btnGrid[x - 1, y - 1].Text = ""; 
                    btnGrid[x, y].ForeColor = Colorr;
                    if (RedLeft == true || RedRight == true || BlackLeft == true || BlackRight == true) { doublejumpswitch = true; }

                }
                //If a button is with 2 text exchanges with a (K) text and the piece that is jumped is cleared// Also reads to see if a double jump is legal
                if (btnGrid[x, y].Text == "2")
                {
                    Switch = true;
                    btnGrid[x, y].Text = "(K)";
                    btnGrid[x + 1, y - 1].Text = "";
                    btnGrid[x, y].ForeColor = Colorr;
                    if (RedLeft == true || RedRight == true || BlackLeft == true || BlackRight == true)  { doublejumpswitch = true; }
                }
                //If a button is with 3 text exchanges with a (K) text and the piece that is jumped is cleared// Also reads to see if a double jump is legal
                if (btnGrid[x, y].Text == "3")
                {
                    Switch = true;
                    btnGrid[x, y].Text = "(K)";
                    btnGrid[x - 1, y + 1].Text = "";
                    btnGrid[x, y].ForeColor = Colorr;
                    if (RedLeft == true || RedRight == true || BlackLeft == true || BlackRight == true) { doublejumpswitch = true; }
                }
                //If a button is with 4 text exchanges with a (K) text and the piece that is jumped is cleared// Also reads to see if a double jump is legal
                if (btnGrid[x, y].Text == "4")
                {
                    Switch = true;
                    btnGrid[x, y].Text = "(K)";
                    btnGrid[x + 1, y + 1].Text = "";
                    btnGrid[x, y].ForeColor = Colorr;
                    if (RedLeft == true || RedRight == true || BlackLeft == true || BlackRight == true) { doublejumpswitch = true; }
                }
                // If double jump is false after a jump then it the opposite checker piece turn 
                if (doublejumpswitch == false)
                {
                    if (Colorr == Color.Red && Switch == true) 
                    { 
                        radioButton1.Checked = true;
                        boolKingCheck.Checked = false;
                        Clearlegals();
                        booldoublejump.Checked = false;
                    }
                    else
                    {
                        radioButton2.Checked = true; 
                        boolKingCheck.Checked = false;
                        Clearlegals();
                        booldoublejump.Checked = false;
                    }
                }// deploy legal jumping values if the doublejump bool is true 
                else if (doublejumpswitch == true) 
                {
                    Clearlegals();
                    try { if (RedRight == true)         { btnGrid[x + 2, y + 2].Text = "1"; btnGrid[x + 2, y + 2].ForeColor = Colorr; } } catch { }
                    try { if (RedLeft == true )          { btnGrid[x - 2, y + 2].Text = "2"; btnGrid[x - 2, y + 2].ForeColor = Colorr; } } catch { }
                    try { if (BlackLeft == true)        { btnGrid[x + 2, y - 2].Text = "3"; btnGrid[x + 2, y - 2].ForeColor = Colorr; } } catch { }
                    try { if (BlackRight == true)       { btnGrid[x - 2, y - 2].Text = "4"; btnGrid[x - 2, y - 2].ForeColor = Colorr; } } catch { }
                    btnGrid[x, y].Text = "Clicked";
                     
                }
            }
        }
     
        internal class Board
        {

            //Size is 8 Because it represent the size of a checkerBoard
            public int Size1 { get; set; }

            //Variable to setup the Cell process in each grid
            public Cell[,] TheGrid { get; set; }
            public bool Attack { get; set; }


            //constructor
            public Board(int s)
            {
                //intial size of the board   
                Size1 = s;

                //create a new 2d array of type cell
                TheGrid = new Cell[Size1, Size1];

                //fill the Grid with new Cells, each with a x and y
                for (int i = 0; i < Size1; i++)
                {
                    for (int j = 0; j < Size1; j++)
                    {
                        TheGrid[i, j] = new Cell(i, j);
                    }
                }

            }
            internal class Cell
            {
                public int RowNumber { get; set; }
                public int ColumnNumber { get; set; }
                public bool LegalNextMove { get; set; }


                public Cell(int x, int y)
                {
                    RowNumber = x;
                    ColumnNumber = y;
                }

            }

           

            public void CheckerNextLegalMove(Cell currentcell, string checkerpiece, bool AttackL, bool AttackR, bool BlackLeft, bool BlackRight)
            {
                
                for (int i = 0; i < Size1; i++)
                {
                    for (int j = 0; j < Size1; j++)
                    {
                        TheGrid[i, j].LegalNextMove = false;
                    }
                }
                
                    switch (checkerpiece)
                    {
                         case "Redcheckerpiece":
                        try
                        {
                            //Upward-RedMovemnets
                            TheGrid[currentcell.RowNumber + 1, currentcell.ColumnNumber - 1].LegalNextMove = true;
                        }
                        catch { }
                        try 
                        {
                            //Upward-RedMovemnets
                            TheGrid[currentcell.RowNumber - 1, currentcell.ColumnNumber - 1].LegalNextMove = true;
                        }
                        catch { }
                
                        break;
                        
                        case "Blackcheckerpiece":
                        try         
                        {
                            //Downward-BlackMovments
                            TheGrid[currentcell.RowNumber + 1, currentcell.ColumnNumber + 1].LegalNextMove = true; 
                        }
                        catch { }

                        try        
                        {
                            //Downward-BlackMovments
                            TheGrid[currentcell.RowNumber - 1, currentcell.ColumnNumber + 1].LegalNextMove = true;
                        }
                        catch { }


                        break;
                       
                        case "KingMe":
                        try
                        {
                            //Upward-RedMovemnets
                            TheGrid[currentcell.RowNumber + 1, currentcell.ColumnNumber - 1].LegalNextMove = true;
                        }
                        catch { }
                        try
                        {//Upward-RedMovemnets
                            TheGrid[currentcell.RowNumber - 1, currentcell.ColumnNumber - 1].LegalNextMove = true;
                        }
                        
                        catch { }
                        try
                        {
                            //Downward-BlackMovments
                            TheGrid[currentcell.RowNumber + 1, currentcell.ColumnNumber + 1].LegalNextMove = true;
                        }
                        catch { }
                        try
                        {
                            //Downward-BlackMovments
                            TheGrid[currentcell.RowNumber - 1, currentcell.ColumnNumber + 1].LegalNextMove = true;
                        }
                        catch { }


                        break;
                    }
                
            }


        }

        private void CheckerMovement(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void RedTeam_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void Clearlegals()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    if (btnGrid[i, j].Text == "legal"|btnGrid[i, j].Text == "Legal") { btnGrid[i, j].Text = ""; }
                    if (btnGrid[i, j].Text != "()" && btnGrid[i, j].Text != "(K)") { btnGrid[i, j].Text = ""; }
                    if (btnGrid[i, j].Text == "Clicked") { btnGrid[i, j].Text = "(K)"; }
                    if (btnGrid[i, j].Text == "clicked") { btnGrid[i, j].Text = "()"; }

                    
                   
                }
            }
        }

      
        private void StoredButtons( )
        {
            
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }
    }
}
