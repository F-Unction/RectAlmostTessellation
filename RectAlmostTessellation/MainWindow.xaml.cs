using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RectAlmostTessellation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Rect> rects = new List<Rect>();
        private int RectNum = new int();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Start();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Start();
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            for (int i = 0; i < RectNum; i++)
            {
                rects[i].Done = false;
            }
            Tessellation(0, 0, (int)Rgn.ActualWidth, (int)Rgn.ActualWidth);
            ShowRects();
        }

        private void Start()
        {
            if (int.TryParse(NumTextBox.Text, out RectNum))
            {
                GenRandonRects();
                rects.Sort();
                Tessellation(0, 0, (int)Rgn.ActualWidth, (int)Rgn.ActualWidth);
            }
            ShowRects();
        }

        private void GenRandonRects()
        {
            rects.Clear();
            for (var i = 0; i < RectNum; i++)
            {
                Random rd = new Random();
                rects.Add(new Rect(0, 0, rd.Next(1, 7) * 20, rd.Next(1, 5) * 20));
            }
        }

        private void Tessellation(int RgnLeft, int RgnTop, int RgnW, int RgnH)
        {
            if (RectNum != 0)
            {
                int i;
                for (i = 0; i < RectNum; i++)
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

                        Tessellation(RgnLeft + rects[i].Width, RgnTop, RgnW - rects[i].Width, rects[i].Height);
                        Tessellation(RgnLeft, RgnTop + rects[i].Height, RgnW, RgnH - rects[i].Height);

                        break;
                    }
                }
             
            }
        }

        private void ShowRects()
        {
            Rgn.Children.Clear();
            foreach (var i in rects)
            {
                PaintRect(i);
            }
        }

        private void PaintRect(Rect rect)
        {
            if (rect.Done)
            {
                var trect = new Canvas
                {
                    Background = new SolidColorBrush(rect.Color),
                    Width = rect.Width,
                    Height = rect.Height
                };
                Canvas.SetLeft(trect, rect.X);
                Canvas.SetTop(trect, rect.Y);
                Rgn.Children.Add(trect);
            }
        }
    }
}
