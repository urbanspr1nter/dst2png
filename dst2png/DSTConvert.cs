using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;

namespace dst2png
{
    public class DSTConvert
    {
        private byte[] dstData;

        private byte[] designNameData;
        private byte[] totalStitchesData;
        private byte[] totalColorsData;
        private byte[] extentPosXData;
        private byte[] extentNegXData;
        private byte[] extentPosYData;
        private byte[] extentNegYData;
        private byte[] startXData;
        private byte[] startYData;
        private byte[] prevXData;
        private byte[] prevYData;
        private byte[] PDData;

        private string designName = "";
        private int totalStitches = 0;
        private int totalColors = 0;
        private int extentPosX = 0;
        private int extentNegX = 0;
        private int extentPosY = 0;
        private int extentNegY = 0;
        private int startX = 0;
        private int startY = 0;
        private int prevX = 0;
        private int prevY = 0;
        private string PD = "";

        public DSTConvert(byte[] data)
        {
            this.dstData = data;

            this.designNameData = new byte[17];
            this.totalStitchesData = new byte[8];
            this.totalColorsData = new byte[4];
            this.extentPosXData = new byte[6];
            this.extentNegXData = new byte[6];
            this.extentPosYData = new byte[6];
            this.extentNegYData = new byte[6];
            this.startXData = new byte[7];
            this.startYData = new byte[7];
            this.prevXData = new byte[7];
            this.prevYData = new byte[7];
            this.PDData = new byte[8];

            this.designName = "";
            this.totalStitches = 0;
            this.totalColors = 0;
            this.extentPosX = 0;
            this.extentNegX = 0;
            this.extentPosY = 0;
            this.extentNegY = 0;
            this.startX = 0;
            this.startY = 0;
            this.prevX = 0;
            this.prevY = 0;
            this.PD = "";

        }

        public void ReadData()
        {
            for (int i = 3, j = 0; i < designNameData.Length + 3; i++, j++)
            {
                designNameData[j] = dstData[i];
            }

            for (int i = 23, j = 0; i < totalStitchesData.Length + 23; i++, j++)
            {
                totalStitchesData[j] = dstData[i];
            }

            for (int i = 34, j = 0; i < totalColorsData.Length + 34; i++, j++)
            {
                totalColorsData[j] = dstData[i];
            }

            for (int i = 41, j = 0; i < extentPosXData.Length + 41; i++, j++)
            {
                extentPosXData[j] = dstData[i];
            }

            for (int i = 50, j = 0; i < extentNegXData.Length + 50; i++, j++)
            {
                extentNegXData[j] = dstData[i];
            }

            for (int i = 59, j = 0; i < extentPosYData.Length + 59; i++, j++)
            {
                extentPosYData[j] = dstData[i];
            }

            for (int i = 68, j = 0; i < extentNegYData.Length + 68; i++, j++)
            {
                extentNegYData[j] = dstData[i];
            }

            for (int i = 77, j = 0; i < startXData.Length + 77; i++, j++)
            {
                startXData[j] = dstData[i];
            }

            for (int i = 87, j = 0; i < startYData.Length + 87; i++, j++)
            {
                startYData[j] = dstData[i];
            }

            for (int i = 97, j = 0; i < prevXData.Length + 97; i++, j++)
            {
                prevXData[j] = dstData[i];
            }

            for (int i = 107, j = 0; i < prevYData.Length + 107; i++, j++)
            {
                prevYData[j] = dstData[i];
            }

            designName = System.Text.ASCIIEncoding.ASCII.GetString(designNameData).Trim();
            totalStitches = Convert.ToInt32(System.Text.ASCIIEncoding.ASCII.GetString(totalStitchesData).Trim());
            totalColors = Convert.ToInt32(System.Text.ASCIIEncoding.ASCII.GetString(totalColorsData).Trim());
            extentPosX = Convert.ToInt32(System.Text.ASCIIEncoding.ASCII.GetString(extentPosXData).Trim());
            extentNegX = Convert.ToInt32(System.Text.ASCIIEncoding.ASCII.GetString(extentNegXData).Trim());
            extentPosY = Convert.ToInt32(System.Text.ASCIIEncoding.ASCII.GetString(extentPosYData).Trim());
            extentNegY = Convert.ToInt32(System.Text.ASCIIEncoding.ASCII.GetString(extentNegYData).Trim());
            startX = Convert.ToInt32(System.Text.ASCIIEncoding.ASCII.GetString(startXData).Replace('+', ' ').Trim());
            startY = Convert.ToInt32(System.Text.ASCIIEncoding.ASCII.GetString(startYData).Replace('+', ' ').Trim());
            prevX = Convert.ToInt32(System.Text.ASCIIEncoding.ASCII.GetString(prevXData).Replace('+', ' ').Trim());
            prevY = Convert.ToInt32(System.Text.ASCIIEncoding.ASCII.GetString(prevYData).Replace('+', ' ').Trim());
            PD = System.Text.ASCIIEncoding.ASCII.GetString(PDData).Trim();
        }

        public int GetDeltaY(byte b0, byte b1, byte b2)
        {
            int deltaY = 0;

            deltaY = deltaY + (((b0 >> 7) & 1) * 1);
            deltaY = deltaY + (((b0 >> 6) & 1) * (-1));
            deltaY = deltaY + (((b0 >> 5) & 1) * 9);
            deltaY = deltaY + (((b0 >> 4) & 1) * (-9));
            deltaY = deltaY + (((b1 >> 7) & 1) * 3);
            deltaY = deltaY + (((b1 >> 6) & 1) * (-3));
            deltaY = deltaY + (((b1 >> 5) & 1) * 27);
            deltaY = deltaY + (((b1 >> 4) & 1) * (-27));
            deltaY = deltaY + (((b2 >> 5) & 1) * 81);
            deltaY = deltaY + (((b2 >> 4) & 1) * (-81));

            return deltaY;
        }

        public int GetDeltaX(byte b0, byte b1, byte b2)
        {
            int deltaX = 0;

            deltaX = deltaX + (((b0 >> 3) & 1) * (-9));
            deltaX = deltaX + (((b0 >> 2) & 1) * 9);
            deltaX = deltaX + (((b0 >> 1) & 1) * (-1));
            deltaX = deltaX + (((b0 >> 0) & 1) * 1);
            deltaX = deltaX + (((b1 >> 3) & 1) * (-27));
            deltaX = deltaX + (((b1 >> 2) & 1) * 27);
            deltaX = deltaX + (((b1 >> 1) & 1) * (-3));
            deltaX = deltaX + (((b1 >> 0) & 1) * 3);
            deltaX = deltaX + (((b2 >> 3) & 1) * (-81));
            deltaX = deltaX + (((b2 >> 2) & 1) * 81);

            return deltaX;
        }

        public bool IsJump(byte b2)
        {
            return (b2 >> 7) == 1;
        }

        public bool IsColorChange(byte b2)
        {
            return (b2 >> 6) == 3; ;
        }

        public Color GetRandomColor()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());

            byte red = (byte)rand.Next(0, 255);
            byte green = (byte)rand.Next(0, 255);
            byte blue = (byte)rand.Next(0, 255);

            Color color = Color.FromArgb(red, green, blue);

            return color;
        }

        public void ToPNG(string filename)
        {
            ReadData();

            // start at byte 512

            int startIndex = 512;
            int stitchCount = 0;

            double pixelWidth = ((Math.Abs(extentPosX) + Math.Abs(extentNegX)));
            double pixelHeight = ((Math.Abs(extentPosY) + Math.Abs(extentNegY)));

            Bitmap b = new Bitmap(Convert.ToInt32(pixelWidth) + 100, Convert.ToInt32(pixelHeight) + 120);
            Graphics g = Graphics.FromImage(b);

            g.FillRectangle(new SolidBrush(Color.WhiteSmoke), new Rectangle(new Point(0, 0), b.Size));

            Pen p = new Pen(Color.Black);

            int canvasStartX = extentNegX + 50;
            int canvasStartY = extentNegY + 50;

            System.Drawing.Drawing2D.GraphicsPath gPath = new System.Drawing.Drawing2D.GraphicsPath();

            List<Point> points = new List<Point>();

            int totalJumps = 0;

            for (int i = startIndex; i < dstData.Length; i += 3)
            {
                if (stitchCount == totalStitches)
                {
                    break;
                }

                byte b0 = dstData[i];
                byte b1 = dstData[i + 1];
                byte b2 = dstData[i + 2];

                stitchCount++;

                bool jump = IsJump(b2);

                int deltaY = GetDeltaY(b0, b1, b2);
                int deltaX = GetDeltaX(b0, b1, b2);

                canvasStartX += deltaX;
                canvasStartY += deltaY;

                // draw shit
                int x0 = canvasStartX;
                int x1 = x0 + deltaX;

                int y0 = canvasStartY;
                int y1 = y0 + deltaY;

                Point p1 = new Point(x0, y0);
                Point p2 = new Point(x1, y1);

                if (IsColorChange(b2))
                {
                    p = new Pen(GetRandomColor(), 1.0f);
                }

                points.Add(p1);
                points.Add(p2);

                //gPath.AddLine(p1, p2);
                    
                //g.DrawLine(p, p1, p2);

                if (jump)
                {
                    if (points.Count > 0)
                    {
                        gPath.AddCurve(points.ToArray());
                        g.DrawPath(p, gPath);
                    }
                    totalJumps++;
                }
            }

            b.RotateFlip(RotateFlipType.Rotate180FlipX);
            b.Save(filename);

            g.Dispose();
        }

        public string ToPNGBase64()
        {
            ReadData();

            // start at byte 512

            int startIndex = 512;
            int stitchCount = 0;

            double pixelWidth = ((Math.Abs(extentPosX) + Math.Abs(extentNegX)));
            double pixelHeight = ((Math.Abs(extentPosY) + Math.Abs(extentNegY)));

            Bitmap b = new Bitmap(Convert.ToInt32(pixelWidth) + 100, Convert.ToInt32(pixelHeight) + 120);
            Graphics g = Graphics.FromImage(b);

            g.FillRectangle(new SolidBrush(Color.WhiteSmoke), new Rectangle(new Point(0, 0), b.Size));

            Pen p = new Pen(Color.Black);

            int canvasStartX = extentNegX + 50;
            int canvasStartY = extentNegY + 50;

            for (int i = startIndex; i < dstData.Length; i += 3)
            {
                if (stitchCount == totalStitches)
                {
                    break;
                }

                byte b0 = dstData[i];
                byte b1 = dstData[i + 1];
                byte b2 = dstData[i + 2];

                stitchCount++;

                bool jump = IsJump(b2);

                int deltaY = GetDeltaY(b0, b1, b2);
                int deltaX = GetDeltaX(b0, b1, b2);

                canvasStartX += deltaX;
                canvasStartY += deltaY;

                // draw shit
                int x0 = canvasStartX;
                int x1 = x0 + deltaX;

                int y0 = canvasStartY;
                int y1 = y0 + deltaY;

                Point p1 = new Point(x0, y0);
                Point p2 = new Point(x1, y1);

                if (IsColorChange(b2))
                {
                    p = new Pen(GetRandomColor());
                }

                g.DrawLine(p, p1, p2);
            }

            b.RotateFlip(RotateFlipType.Rotate180FlipX);

            System.IO.MemoryStream ms = new System.IO.MemoryStream();

            b.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            g.Dispose();

            return Convert.ToBase64String(ms.ToArray());

        }
    }
}
