using System;
using Model;


namespace Control
{
    public class ProCon
    {
        //第一周我们写的是0 现在我们换成了red
        //这里的0，1，2是调用了enum里的player，即red=0,black=1,blank=2
        int turn = (int)Chess.Player.red;        //回合数从0开始 ROUND 0

        //这里的turn是交换红黑方，红先黑后，可以认为是调用了递归(recursion),直到游戏结束
        // 确保不能用对方的棋子 也是棋子类的继承方法
        public bool SwitchPlayer(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)       
        {
            
            switch (turn) {
                case (int)Chess.Player.red:
                    if (Matrix[OriginalX, OriginalY].side != Chess.Player.red)
                    {
                        return false;
                    }
                    else
                    {
                        //每种棋子的移动方法
                        bool check = MovePiece(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);
                        if (check == true)
                        {
                            // 红方走完把回合权交给黑方
                            turn = (int)Chess.Player.black;
                            return true;
                        }
                        else
                            return false;
                    }
                case (int)Chess.Player.black:
                    if (Matrix[OriginalX, OriginalY].side != Chess.Player.black)
                    {
                        return false;
                    }
                    else
                    {
                        bool check = MovePiece(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);
                        if (check == true)
                        {
                            turn = (int)Chess.Player.red;
                            return true;
                        }
                        else
                            return false;
                    }
                case (int)Chess.Player.blank:
                    return false;
                default:
                    break;
            }
                return false;
        }


        public bool Result(Chess[,] Matrix)       
        {
            int count = 0;
            bool result = true;
            //遍历上方田字格
            for (int i = 0; i <= 4; i++)
            {
                for (int j = 6; j <= 10; j++)
                {
                    if (Matrix[i, j].type == Chess.Piecetype.jiang)
                        count++;
                }
            }
            //遍历下方田字格
            for (int i = 14; i <= 18; i++)
            {
                for (int j = 6; j <= 10; j++)
                {
                    if (Matrix[i, j].type == Chess.Piecetype.jiang)
                        count++;
                }
            }
            if (count == 2)
                return result;
            else
                return false;
        }


        public bool MovePiece(int X, int Y, int chozenX, int chozenY, Chess[,] Matrix)//定义每种棋子的移动方式,这里是调用pieceControl中的棋子
        {
            //实例化棋子
            Advisor advisor = new Advisor();
            Cannon cannon = new Cannon();
            Elephant elephant = new Elephant();
            General general = new General();
            Horse horse = new Horse();
            Rook rook = new Rook();       
            Soldier soldier = new Soldier();

            bool Move;

            switch (Matrix[chozenX, chozenY].type)
            {
                case Chess.Piecetype.che:
                    Move = rook.Che(X, Y, chozenX, chozenY, Matrix);
                    return Move;
                case Chess.Piecetype.ma:
                    Move = horse.Ma(X, Y, chozenX, chozenY, Matrix);
                    return Move;
                case Chess.Piecetype.xiang:
                    Move = elephant.Xiang(X, Y, chozenX, chozenY, Matrix);
                    return Move;
                case Chess.Piecetype.shi:
                    Move = advisor.Shi(X, Y, chozenX, chozenY, Matrix);
                    return Move;
                case Chess.Piecetype.jiang:
                    Move = general.Jiang(X, Y, chozenX, chozenY, Matrix);
                    return Move;
                case Chess.Piecetype.pao:
                    Move = cannon.Pao(X, Y, chozenX, chozenY, Matrix);
                    return Move;
                case Chess.Piecetype.bing:
                    Move = soldier.Bing(X, Y, chozenX, chozenY, Matrix);
                    return Move;
            }

            return false;
        }

        //判断选中的是否是棋子
        //这里的0，1，2是调用了enum里的player，即red=0,black=1,blank=2
        public int CheckPiece(int chozenX, int chozenY, Chess[,] Matrix)                
        {
            if (Matrix[chozenX, chozenY].type == Chess.Piecetype.blank)
            {
                return 0;//no pieces
            }
            else if (turn == (int)Chess.Player.red)
            {
                if (Matrix[chozenX, chozenY].side != Chess.Player.red)
                {
                    return 1;//选择了对方的棋子
                }
                else
                {
                    return 2;//选择了己方的棋子 选择正确
                }
            }
            else if (turn == (int)Chess.Player.black)
            {
                if (Matrix[chozenX, chozenY].side != Chess.Player.black)
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            
            return 0;
        }


        public void SetMove(int X, int Y, int chozenX, int chozenY, Chess[,] Matrix)       //基本移动方式
        {
            Matrix[X, Y].side = Matrix[chozenX, chozenY].side;
            Matrix[X, Y].type = Matrix[chozenX, chozenY].type;
            Matrix[chozenX, chozenY].side = Chess.Player.blank;
            Matrix[chozenX, chozenY].type = Chess.Piecetype.blank;
        }

        static void Main(string[] args) {        
        }
    }

}