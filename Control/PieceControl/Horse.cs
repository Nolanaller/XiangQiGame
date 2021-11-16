using System;
using Model;

namespace Control
{
    class Horse: ProCon
    {
        public bool Ma(int CurrentX, int CurrentY, int OriginalX, int OriginalY, Chess[,] Matrix)
        {
            //横着走
            Boolean YMoving = Math.Abs(OriginalX - CurrentX) == 2 && Math.Abs(OriginalY - CurrentY) == 4;
            //竖着走
            Boolean XMoving = Math.Abs(OriginalX - CurrentX) == 4 && Math.Abs(OriginalY - CurrentY) == 2;

            switch (XMoving||YMoving) {
                case true:
                    break;
                case false:
                    return false;
                    break;
            }

            //不能吃掉自己方的棋子
            if (Matrix[OriginalX, OriginalY].side == Matrix[CurrentX, CurrentY].side)             
            {
                return false;
            }

            SetMove(CurrentX, CurrentY, OriginalX, OriginalY, Matrix);

            return true;
        }
    }
}