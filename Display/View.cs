using System;
using Model;
using Control;

namespace View
{
    public class ProgramView
    {
        static void Main(string[] args)     //main function
        {
            bool result = true;
            bool turn;
            //初始化棋子方为红方
            int player = (int)Chess.Player.red;
            //实例化一个控制模块
            ProCon con = new ProCon();
            //实例化一个棋子模块
            ProMod mod = new ProMod();
            ProgramView view = new ProgramView();
            //初始化棋子位置 方法在pieces中 Matrix中包含棋盘上每个坐标的type，path，等属性
            Chess[,] Matrix = mod.SetPosition();

            //结果为真，也就是场上仍存在两名将时
            while (result == true)
            {
                //board 包含棋盘上的每个图案 棋子文字等等（没有坐标轴）
                string[,] Board = mod.Piece(Matrix);//传入棋盘坐标 每个图案
                view.Displaying(Matrix);
                view.Start(player);

                try
                {
                    //OriginalX,Y为选中棋子原来的位置，X，Y为移动后的位置
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("                 X = ");
                    int OriginalX = Convert.ToInt32(Console.ReadLine());
                    Console.Write("                 Y = ");
                    int OriginalY  = Convert.ToInt32(Console.ReadLine());
                    
                    int checkpiece = con.CheckPiece(OriginalX * 2, OriginalY *2, Matrix);

                    if (checkpiece == 2)                //check if there is a piece
                    {
                        
                        view.Displaying(Matrix);
                        view.Check(checkpiece, Board, OriginalX, OriginalY );
                        Console.Write("                 X = ");
                        int CurrentX = Convert.ToInt32(Console.ReadLine());
                        Console.Write("                 Y = ");
                        int CurrentY = Convert.ToInt32(Console.ReadLine());
                        turn = con.SwitchPlayer(CurrentX * 2, CurrentY * 2, OriginalX * 2, OriginalY *2, Matrix);
                        player = view.Move(turn, player, OriginalX, OriginalY , CurrentX, CurrentY);
                        result = con.Result(Matrix);
                    }
                    else if (checkpiece == 0)
                    {
                        view.Check(checkpiece, Board, OriginalX, OriginalY );
                    }
                    else
                    {
                        view.Check(checkpiece, Board, OriginalX, OriginalY );
                    }
                }

                catch (Exception)       //check if there is something wrong
                {
                    view.Wrong();
                }
            }

            view.Displaying(Matrix);
            view.Win(player);
        }


        public void Start(int start)            //print the start statement
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("      Welcome to our XiangQi Game!     ");
            if (start % 2 == 0)
            {
                Console.WriteLine("          It's RED turn now!         ");
                Console.WriteLine("     Select your piece（X&Y) to move!    ");
            }
            else
            {
                Console.WriteLine("          It's BLACK turn now!         ");
                Console.WriteLine("     Select your piece (X&Y) to move!      ");
            }
        }


        public int Move(bool turn, int player, int chozenX, int chozenY, int X, int Y)          //print the statement of movement
        {
            Console.ForegroundColor = ConsoleColor.White;

            if (turn == false)
            {
                Console.Write(" This is the wrong path, please set it again! ");               
            }
            else
            {
                Console.Write(" Your piece move from (" + chozenX + "," + chozenY  + ") to (" + X + "," + Y + ") successfully!");
                player++;
            }
            Console.ReadLine();
            return player;
        }


        public void Check(int checkpiece, string[,] Board, int chozenX, int chozenY)        //tell the user the condition of the movement of the piece
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.Black;

            if (checkpiece == 2)
            {
                Console.WriteLine("   You now select the piece: " + Board[chozenX * 2, chozenY*2] + "(" + chozenX + "," + chozenY + ").");
                Console.WriteLine(" Please set your position of moving(X & Y):");
            }
            //选择了空棋子
            else if (checkpiece == 0)
            {
                Console.WriteLine("   Sorry, there is no piece. Please try again.            ");
            }
            //1选错棋子（选择了对方的棋子）
            else
            {
                Console.WriteLine("   You cannot choose this position.          ");
            }
        }


        public void Wrong()             //print the statement if the user choose the wrong piece
        {
            Console.Write("  You cannot select this  position! Please select again! ");
            Console.ReadLine();
        }


        public void Win(int player)     //print the statement of WIN in the base
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            if ((player+1) % 2 == (int)Chess.Player.red)
            {
                Console.Write("           The  RED SIDE IS WIN!!!                  ");               
            }
            else
            {
                Console.Write("           The  BLACK SIDE IS WIN!!!                ");
            }
            Console.ReadKey();
        }


        //Matrix含有棋盘和棋子的整个图像，此时还未包含坐标轴
        public void Displaying(Chess[,] Matrix)              //print checkerboard coordinates
        {
            ProMod Mod = new ProMod();
            string[,] Board = Mod.Piece(Matrix);//
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" X/Y ");

            for (int j = 0; j <= 17; j++)
            {
                if (j == 0)
                {
                    Console.Write(j + "   ");
                }
                else if (j == 16)
                {
                    Console.Write(j / 2 + "      ");
                }
                else if (j % 2 == 0 && j > 0)
                {
                    Console.Write(j / 2 + "   ");
                }
            }

            Console.WriteLine(" ");
            //the x index
            for (int i = 0; i <= 18; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;

                if (i % 2 == 0)
                {
                    Console.Write("  " + i / 2 + "  ");
                }
                else
                {
                    Console.Write("     ");
                }
                //y index
                for (int j = 0; j < 17; j++)
                {
                    if (Matrix[i, j].side == Chess.Player.red)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (Matrix[i, j].side == Chess.Player.black)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    Console.Write(Board[i, j]);
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                }

                if (i % 2 == 0)
                {
                    Console.Write("     ");
                }
                else
                {
                    Console.Write("     ");
                }
                Console.WriteLine(" ");
            }
        }
    }
}
