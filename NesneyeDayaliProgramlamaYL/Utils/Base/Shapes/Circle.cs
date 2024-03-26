using NesneyeDayaliProgramlamaYL.Utils.Helper;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesneyeDayaliProgramlamaYL.Utils.Base.Shapes
{
    public class Circle : Shape
    {
        public override ShapeType ShapeType => ShapeType.Circle;
        public Circle(Color color, Point startPoint, Point endPoint) : base(color, startPoint, endPoint)
        {
        }

        public override void Draw(Graphics g)
        {
            int radius = (int)Math.Sqrt(Math.Pow(EndPoint.X - StartPoint.X, 2) + Math.Pow(EndPoint.Y - StartPoint.Y, 2));

            using (Brush brush = new SolidBrush(Color))
            {
                g.FillEllipse(brush, StartPoint.X - radius, StartPoint.Y - radius, radius * 2, radius * 2);
            }

            if (Selected)
            {
                using (Brush backgroundBrush = new SolidBrush(Color.FromArgb(200, 216, 197, 224)))
                {
                    g.FillRectangle(backgroundBrush, StartPoint.X - radius - 1, StartPoint.Y - radius - 1, radius * 2 + 2, radius * 2 + 2);
                }
                using (Pen pen = new Pen(Color.FromArgb(128, Color.Blue), 2))
                {
                    pen.DashStyle = DashStyle.Dot;
                    g.DrawRectangle(pen, StartPoint.X - radius - 1, StartPoint.Y - radius - 1, radius * 2 + 2, radius * 2 + 2);
                }
            }
        }
        public override bool IsPointInside(Point point)
        {
            int radius = (int)Math.Sqrt(Math.Pow(EndPoint.X - StartPoint.X, 2) + Math.Pow(EndPoint.Y - StartPoint.Y, 2));
            int dx = point.X - StartPoint.X;
            int dy = point.Y - StartPoint.Y;

            return dx * dx + dy * dy <= radius * radius;
        }
    }
}
