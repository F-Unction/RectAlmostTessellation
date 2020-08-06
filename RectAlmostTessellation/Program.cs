using System;
using System.Collections.Generic;
using System.Text;

namespace RectAlmostTessellation
{
    static class Program
    {
        static public void Tessellation(int RgnLeft, int RgnTop, int RgnW, int RgnH,int RectNum,ref Rect[] rects)
        {
            if (RectNum != 0)
            {
                for (int i = 0; i < RectNum; i++)
                {
                    if (!rects[i].Done && rects[i].Width <= RgnW && rects[i].Height <= RgnH)
                    {
                        if (i >= RectNum || i < 0)
                        {
                            return;
                        }
                        if (i == RectNum && rects[i].Done)
                        {
                            return;
                        }
                        rects[i].X = RgnLeft;
                        rects[i].Y = RgnTop;
                        rects[i].Done = true;

                        Tessellation(RgnLeft + rects[i].Width, RgnTop, RgnW - rects[i].Width, rects[i].Height,RectNum,ref rects);
                        Tessellation(RgnLeft, RgnTop + rects[i].Height, RgnW, RgnH - rects[i].Height, RectNum, ref rects);

                        break;
                    }
                }
            }
        }
    }
}
