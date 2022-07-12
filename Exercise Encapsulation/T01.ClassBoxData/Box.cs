using System;
using System.Collections.Generic;
using System.Text;

namespace T01.ClassBoxData
{
    public class Box
    {
        private const int minNumber = 0;
        private double length;
        private double width;
        private double height;

        public Box(double length, double width, double height)
        {
            this.Length = length;
            this.Width = width;
            this.Height = height;
        }

        public double Length
        { 
            get
            {
                return this.length;
            }
            set
            {
                if (value <= minNumber)
                {
                    throw new ArgumentException($"{nameof(this.Length)} cannot be zero or negative.");
              
                }
                this.length = value;
            }
        }
        public double Width
        {
            get 
            {
                return this.width;
            }
            set 
            {
                if (value <= minNumber)
                {
                    throw new ArgumentException($"{nameof(this.Width)} cannot be zero or negative.");
                }
                this.width = value;
            }
        }
        public double Height
        {
            get 
            {
                return this.height;
            }
            set
            {
                if (value <= minNumber)
                {
                    throw new ArgumentException($"{nameof(this.Height)} cannot be zero or negative.");
                }
                this.height = value; 
            }

        }

        public double SurfaceArea => 2 * (this.Length * this.Width) + 2 * (this.Length * this.Height) + 2 * (this.Width * this.Height);
        public double LateralSurfaceArea => 2 * (this.Length * this.Height) + 2 * (this.Width * this.Height);
        public double Volume => this.Length * this.Width * this.Height;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Surface Area - {this.SurfaceArea:f2}")
                .AppendLine($"Lateral Surface Area - {this.LateralSurfaceArea:f2}")
                .AppendLine($"Volume - {this.Volume:f2}");
                
            return sb.ToString();
        }



    }
}
