using System;
using Model;

namespace Control
{
    class Advisor: ProCon
    {
       
        public bool Shi(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        {
  
            //一次只能移动一格
            if (Math.Abs(CurrentX - OriginalX) != 2 || Math.Abs(OriginalY - CurrentY) != 2)
            {
                return false;
            }
            //不能吃自己的棋子
            if (Matrix[OriginalX, OriginalY].side == Matrix[CurrentX, CurrentY].side)
            {
                return false;
            }

            switch (Matrix[OriginalX, OriginalY].side)
            {

                case Chess.Player.red:

                    if (CurrentY < 6 || CurrentY > 10 || CurrentX < 14)
                    {
                        return false;
                    }

                    break;

                case Chess.Player.black:

                    if (CurrentY < 6 || CurrentY > 10 || CurrentX > 4)
                    {
                        return false;
                    }

                    break;
                default:
                    break;
            }
            
            SetMove(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);

            return true;
        }

    }
}