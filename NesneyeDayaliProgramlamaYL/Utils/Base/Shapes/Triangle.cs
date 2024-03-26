using NesneyeDayaliProgramlamaYL.Utils.Base;
using NesneyeDayaliProgramlamaYL.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesneyeDayaliProgramlamaYL.Utils.Base.Shapes
{
    public class Triangle : Shape
    {
        public override ShapeType ShapeType => ShapeType.Triangle;
        public Triangle(Color color, Point startPoint, Point endPoint) : base(color, startPoint, endPoint)
        {
        }

        public override void Draw(Graphics g)
        {
            Point[] points = new Point[3]
            {
            new Point(StartPoint.X, EndPoint.Y),
            new Point((StartPoint.X + EndPoint.X) / 2, StartPoint.Y),
            new Point(EndPoint.X, EndPoint.Y)
            };

            using (Brush brush = new SolidBrush(Color))
            {
                g.FillPolygon(brush, points);
            }

            if (Selected)
            {
                using (Brush backgroundBrush = new SolidBrush(Color.FromArgb(200, 216, 197, 224)))
                {
                    g.FillRectangle(backgroundBrush, StartPoint.X - 1, StartPoint.Y - 1, EndPoint.X - StartPoint.X + 2, EndPoint.Y - StartPoint.Y + 2);
                }
                using (Pen pen = new Pen(Color.FromArgb(128, Color.Blue), 2))
                {
                    pen.DashStyle = DashStyle.Dot;
                    g.DrawRectangle(pen, StartPoint.X - 1, StartPoint.Y - 1, EndPoint.X - StartPoint.X + 2, EndPoint.Y - StartPoint.Y + 2);
                }
            }
        }
        public override bool IsPointInside(Point point)
        {
            Point tpoint1 = new Point(StartPoint.X, EndPoint.Y);
            Point tpoint2 = new Point((StartPoint.X + EndPoint.X) / 2, StartPoint.Y);
            Point tpoint3 = new Point(EndPoint.X, EndPoint.Y);

            double mainTriangleArea = TriangleArea(tpoint1, tpoint2, tpoint3);

            double subTriangleArea1 = TriangleArea(point, tpoint1, tpoint2);
            double subTriangleArea2 = TriangleArea(point, tpoint2, tpoint3);
            double subTriangleArea3 = TriangleArea(point, tpoint3, tpoint1);

            double totalArea = subTriangleArea1 + subTriangleArea2 + subTriangleArea3;

            return Math.Abs(totalArea - mainTriangleArea) < 0.0001;

        }
        private double TriangleArea(Point A, Point B, Point C)
        {
            return Math.Abs((A.X * (B.Y - C.Y) + B.X * (C.Y - A.Y) + C.X * (A.Y - B.Y)) / 2.0);
        }
    }
}
