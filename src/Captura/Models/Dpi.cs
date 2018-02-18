﻿using System.Drawing;
using System.Windows.Interop;

namespace Captura
{
    /// <summary>
    /// Provides DPI scaling factor.
    /// Can be used by multiplying <see cref="Instance"/> with <see cref="Rectangle"/>, <see cref="Point"/> or <see cref="Size"/> objects.
    /// The reverse effect can be attained by multiplying with <see cref="Inverse"/>.
    /// Only needs to be used when dealing with WPF since their sizes are specified in Device Independent Pixels.
    /// </summary>
    public class Dpi
    {
        Dpi()
        {
            using (var src = new HwndSource(new HwndSourceParameters()))
            {
                if (src.CompositionTarget != null)
                {
                    var matrix = src.CompositionTarget.TransformToDevice;

                    X = matrix.M11;
                    Y = matrix.M22;
                }
            }
        }

        public static Dpi Instance { get; } = new Dpi();

        public static Dpi Inverse { get; } = new Dpi
        {
            X = 1 / Instance.X,
            Y = 1 / Instance.Y
        };

        public static Point operator *(Point P, Dpi D)
        {
            return new Point((int)(P.X * D.X), (int)(P.Y * D.Y));
        }

        public static Size operator *(Size S, Dpi D)
        {
            return new Size((int)(S.Width * D.X), (int)(S.Height * D.Y));
        }
        
        public static Rectangle operator *(Rectangle R, Dpi D)
        {
            return new Rectangle(R.Location * D, R.Size * D);
        }

        public double X { get; private set; }

        public double Y { get; private set; }
    }
}
