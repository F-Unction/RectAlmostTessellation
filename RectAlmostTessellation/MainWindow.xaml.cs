using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static RectAlmostTessellation.Program;

namespace RectAlmostTessellation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Rect> rects = new List<Rect>();
        private Rect[] rectsArray = new Rect[0];
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
            rectsArray = rects.ToArray();
            Tessellation(0, 0, (int)Rgn.ActualWidth, (int)Rgn.ActualWidth,RectNum,ref rectsArray);
            ShowRects();
        }

        private void Start()
        {
            if (int.TryParse(NumTextBox.Text, out RectNum))
            {
                GenRandonRects();
                rects.Sort();
                rectsArray = rects.ToArray();
                Tessellation(0, 0, (int)Rgn.ActualWidth, (int)Rgn.ActualWidth, RectNum, ref rectsArray);
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
