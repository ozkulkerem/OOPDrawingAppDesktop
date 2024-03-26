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
    public class Hexagon : Shape
    {
        public override ShapeType ShapeType => ShapeType.Hexagon;
        public Hexagon(Color color, Point startPoint, Point endPoint) : base(color, startPoint, endPoint)
        {
        }

        public override void Draw(Graphics g)
        {
            int width = Math.Abs(EndPoint.X - StartPoint.X);
            int height = Math.Abs(EndPoint.Y - StartPoint.Y);
            int x = Math.Min(StartPoint.X, EndPoint.X);
            int y = Math.Min(StartPoint.Y, EndPoint.Y);

            PointF[] points = new PointF[6];
            for (int i = 0; i < 6; i++)
            {
                double angle = 2 * Math.PI / 6 * (i + 0.5);
                points[i] = new PointF(x + (float)(width / 2 * (1 + Math.Cos(angle))), y + (float)(height / 2 * (1 + Math.Sin(angle))));
            }

            using (Brush brush = new SolidBrush(Color))
            {
                g.FillPolygon(brush, points);
            }

            if (Selected)
            {
                using (Brush backgroundBrush = new SolidBrush(Color.FromArgb(200, 216, 197, 224)))
                {
                    g.FillRectangle(backgroundBrush, x - 1, y - 1, width + 2, height + 2);
                }
                using (Pen pen = new Pen(Color.FromArgb(128, Color.Blue), 2))
                {
                    pen.DashStyle = DashStyle.Dot;
                    g.DrawRectangle(pen, x - 1, y - 1, width + 2, height + 2);
                }
            }
        }

        public override bool IsPointInside(Point point)
        {
            PointF[] points = GetHexagonPoints();
            int j = points.Length - 1;
            bool inside = false;

            for (int i = 0; i < points.Length; i++)
            {
                if (points[i].Y > point.Y != points[j].Y > point.Y &&
                    point.X < (points[j].X - points[i].X) * (point.Y - points[i].Y) / (points[j].Y - points[i].Y) + points[i].X)
                {
                    inside = !inside;
                }
                j = i;
            }

            return inside;
        }
        private PointF[] GetHexagonPoints()
        {
            int width = Math.Abs(EndPoint.X - StartPoint.X);
            int height = Math.Abs(EndPoint.Y - StartPoint.Y);
            int x = Math.Min(StartPoint.X, EndPoint.X);
            int y = Math.Min(StartPoint.Y, EndPoint.Y);

            PointF[] points = new PointF[6];
            for (int i = 0; i < 6; i++)
            {
                double angle = 2 * Math.PI / 6 * (i + 0.5);
                points[i] = new PointF(x + (float)(width / 2 * (1 + Math.Cos(angle))), y + (float)(height / 2 * (1 + Math.Sin(angle))));
            }

            return points;
        }
    }
}
