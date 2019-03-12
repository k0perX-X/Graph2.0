using System;
using System.Drawing;
using System.Windows.Forms;

namespace Graph2._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public const int otstup = 30;
        public const int size = 10;
        
        //функция 
        public double f(double x)
        {
            return Math.Sin(x*x)*x;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBoxMax.Text != "") && (textBoxMin.Text != "") && (d.xMax > d.xMin))
            {
                double max = int.MinValue;
                double min = int.MaxValue;
                double sgPixX = (d.xMax - d.xMin) / (pictureBox.Width - 2 * otstup); //x на pixel
                //создаём массив точек
                PointF[] xy = new PointF[(pictureBox.Width - 2 * otstup + 1)];
                double[] y = new double[(pictureBox.Width - otstup + 1)];
                double j = d.xMin;
                for (int i = otstup; i <= pictureBox.Width - otstup; i++)
                {
                    j += sgPixX;
                    y[i] = f(j);
                    if (min > y[i]) { min = y[i]; }
                    if (max < y[i]) { max = y[i]; }
                }
                double sgPixY = (max - min) / (pictureBox.Height - 2 * otstup); //y на pixel
                //создаем поле 
                Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
                // Чем будем рисовать.
                Graphics g = Graphics.FromImage(bitmap);
                // Устанавливаем сглаживание.
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                // "Карандаш" для отрисовки.
                Pen p = new Pen(Color.Black, 1);
                //ось y
                PointF[] s = new PointF[3];
                if ((d.xMin <= 0) && (d.xMax >= 0))
                {
                    g.DrawLine(p, (float)((-d.xMin) / sgPixX) + otstup, 0, (float)((-d.xMin) / sgPixX) + otstup, pictureBox.Height);
                    s[0].X = (float)((-d.xMin) / sgPixX) + otstup; s[0].Y = 0; s[1].X = (float)((-d.xMin) / sgPixX) + 5 + otstup; s[1].Y = 15; s[2].X = (float)((-d.xMin) / sgPixX) - 5 + otstup; s[2].Y = 15;
                    g.FillPolygon(Brushes.Black, s);
                }
                else if (d.xMax < 0)
                {
                    g.DrawLine(p, pictureBox.Width - otstup, 0, pictureBox.Width - otstup, pictureBox.Height);
                    s[0].X = pictureBox.Width - otstup; s[0].Y = 0; s[1].X = pictureBox.Width - otstup + 5; s[1].Y = 15; s[2].X = pictureBox.Width - otstup - 5; s[2].Y = 15;
                    g.FillPolygon(Brushes.Black, s);
                }
                else
                {
                    g.DrawLine(p, otstup, 0, otstup, pictureBox.Height);
                    s[0].X = otstup; s[0].Y = 0; s[1].X = otstup + 5; s[1].Y = 15; s[2].X = otstup - 5; s[2].Y = 15;
                    g.FillPolygon(Brushes.Black, s);
                }
                //ось x
                if ((min <= 0) && (max >= 0))
                {
                    g.DrawLine(p, 0, (float)(max / sgPixY) + otstup, pictureBox.Width, (float)(max / sgPixY) + otstup);
                    s[0].X = pictureBox.Width; s[0].Y = (float)(max / sgPixY) + otstup; s[1].X = pictureBox.Width - 15; s[1].Y = (float)(max / sgPixY) - 5 + otstup; s[2].X = pictureBox.Width - 15; s[2].Y = (float)(max / sgPixY) + 5 + otstup;
                    g.FillPolygon(Brushes.Black, s);
                }
                else if (max < 0)
                {
                    g.DrawLine(p, 0, otstup, pictureBox.Width, otstup);
                    s[0].X = pictureBox.Width; s[0].Y = otstup; s[1].X = pictureBox.Width - 15; s[1].Y = otstup - 5; s[2].X = pictureBox.Width - 15; s[2].Y = otstup + 5;
                    g.FillPolygon(Brushes.Black, s);
                }
                else
                {
                    g.DrawLine(p, 0, pictureBox.Height - otstup, pictureBox.Width, pictureBox.Height - otstup);
                    s[0].X = pictureBox.Width; s[0].Y = pictureBox.Height - otstup; s[1].X = pictureBox.Width - 15; s[1].Y = pictureBox.Height - otstup - 5; s[2].X = pictureBox.Width - 15; s[2].Y = pictureBox.Height - otstup + 5;
                    g.FillPolygon(Brushes.Black, s);
                }
                //шаг x 
                double shagX = Math.Pow(10, -5);
                while ((d.xMax - d.xMin) / shagX > 20)
                {
                    shagX *= 2;
                    if (!((d.xMax - d.xMin) / shagX > 20)) { break; }
                    shagX *= 5;
                }
                //шаг Y
                double shagY = Math.Pow(10, -5);
                while ((max - min) / shagY > 20)
                {
                    shagY *= 2;
                    if (!((max - min) / shagY > 20)) { break; }
                    shagY *= 5;
                }
                //шаги x
                p = new Pen(Color.Gray, 1);
                double xmod;
                if (d.xMin < 0) { xmod = d.xMin - ((int)(d.xMin / shagX) - 1) * shagX; }
                else { xmod = d.xMin - ((int)(d.xMin / shagX)) * shagX; }
                double xRast = (shagX - xmod)/ sgPixX + otstup;
                double xNach = (shagX - xmod) + d.xMin;
                if ((d.xMin <= 0) && (d.xMax >= 0))
                    if ((min <= 0) && (max >= 0))
                        for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                        {
                            if (!(((float)(xRast + (shagX * i) / sgPixX) >= (float)((-d.xMin) / sgPixX) + otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= (float)((-d.xMin) / sgPixX) + otstup + 5)))
                                g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                            g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, (float)(max / sgPixY) + otstup + 3);
                        }
                    else if (max < 0)
                        for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                        {
                            if (!(((float)(xRast + (shagX * i) / sgPixX) >= (float)((-d.xMin) / sgPixX) + otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= (float)((-d.xMin) / sgPixX) + otstup + 5)))
                                g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                            g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, otstup + 3);
                        }
                    else
                        for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                        {
                            if (!(((float)(xRast + (shagX * i) / sgPixX) >= (float)((-d.xMin) / sgPixX) + otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= (float)((-d.xMin) / sgPixX) + otstup + 5)))
                                g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                            g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, pictureBox.Height - otstup + 3);
                        }
                else if (d.xMax < 0)
                    if ((min <= 0) && (max >= 0))
                        for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                        {
                            if (!(((float)(xRast + (shagX * i) / sgPixX) >= pictureBox.Width - otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= pictureBox.Width - otstup + 5)))
                                g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                            g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, (float)(max / sgPixY) + otstup + 3);
                        }
                    else if (max < 0)
                        for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                        {
                            if (!(((float)(xRast + (shagX * i) / sgPixX) >= pictureBox.Width - otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= pictureBox.Width - otstup + otstup + 5)))
                                g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                            g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, otstup + 3);
                        }
                    else
                        for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                        {
                            if (!(((float)(xRast + (shagX * i) / sgPixX) >= pictureBox.Width - otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= pictureBox.Width - otstup + 5)))
                                g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                            g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, pictureBox.Height - otstup + 3);
                        }
                else
                {
                    if ((min <= 0) && (max >= 0))
                        for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                        {
                            if (!(((float)(xRast + (shagX * i) / sgPixX) >= otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= otstup + 5)))
                                g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                            g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, (float)(max / sgPixY) + otstup + 3);
                        }
                    else if (max < 0)
                        for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                        {
                            if (!(((float)(xRast + (shagX * i) / sgPixX) >= otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= otstup + 5)))
                                g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                            g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, otstup + 3);
                        }
                    else
                        for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                        {
                            if (!(((float)(xRast + (shagX * i) / sgPixX) >= otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= otstup + 5)))
                                g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                            g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, pictureBox.Height - otstup + 3);
                        }
                }
                //шаги y
                double ymod;
                if (max < 0) { ymod = max - ((int)(max / shagY) - 1) * shagY; }
                else { ymod = max - ((int)(max / shagY)) * shagY; }
                double yRast = ymod / sgPixY + otstup;
                double yNach = -ymod + max;
                if ((min <= 0) && (max >= 0))
                    if ((d.xMin <= 0) && (d.xMax >= 0))
                        for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                        {
                            if (!(((float)(yRast + (shagY * i) / sgPixY) >= (float)(max / sgPixY) + otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= (float)(max / sgPixY) + otstup + 5)))
                                g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                            g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)((-d.xMin) / sgPixX) + otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                        }
                    else if (d.xMax < 0)
                        for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                        {
                            if (!(((float)(yRast + (shagY * i) / sgPixY) >= (float)(max / sgPixY) + otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= (float)(max / sgPixY) + otstup + 5)))
                                g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                            g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), pictureBox.Width - otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                        }
                    else
                        for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                        {
                            if (!(((float)(yRast + (shagY * i) / sgPixY) >= (float)(max / sgPixY) + otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= (float)(max / sgPixY) + otstup + 5)))
                                g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                            g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                        }
                else if (max < 0)
                    if ((d.xMin <= 0) && (d.xMax >= 0))
                        for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                        {
                            if (!(((float)(yRast + (shagY * i) / sgPixY) >= otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= otstup + 5)))
                                g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                            g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)((-d.xMin) / sgPixX) + otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                        }
                    else if (d.xMax < 0)
                        for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                        {
                            if (!(((float)(yRast + (shagY * i) / sgPixY) >= otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= otstup + 5)))
                                g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                            g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), pictureBox.Width - otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                        }
                    else
                        for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                        {
                            if (!(((float)(yRast + (shagY * i) / sgPixY) >= otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= otstup + 5)))
                                g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                            g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                        }
                else
                {
                    if ((d.xMin <= 0) && (d.xMax >= 0))
                        for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                        {
                            if (!(((float)(yRast + (shagY * i) / sgPixY) >= pictureBox.Height - otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= pictureBox.Height - otstup + 5)))
                                g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                            g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)((-d.xMin) / sgPixX) + otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                        }
                    else if (d.xMax < 0)
                        for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                        {
                            if (!(((float)(yRast + (shagY * i) / sgPixY) >= pictureBox.Height - otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= pictureBox.Height - otstup + 5)))
                                g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                            g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), pictureBox.Width - otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                        }
                    else
                        for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                        {
                            if (!(((float)(yRast + (shagY * i) / sgPixY) >= pictureBox.Height - otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= pictureBox.Height - otstup + 5)))
                                g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                            g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                        }
                }
                //график
                j = d.xMin;
                p = new Pen(Color.Red, 1);
                for (int i = otstup; i <= pictureBox.Width - otstup; i++)
                {
                    j += sgPixX;
                    xy[i - otstup].X = i;
                    xy[i - otstup].Y = (float)((max - y[i]) / sgPixY) + otstup;
                }
                g.DrawLines(p, xy);
                //выводим 
                pictureBox.Image = bitmap;
            }
            else
            {
                Ошибка f = new Ошибка();
                f.Show();
            }
        }
        //ввод
        double n;
        private void textBoxMin_TextChanged(object sender, EventArgs e)
        {
            if (textBoxMin.Text != "")
            {
                if (textBoxMin.Text == ",")
                {
                    textBoxMin.Text = "";
                }
                else if ((!(textBoxMin.Text[textBoxMin.Text.Length - 1] == ',')) && (textBoxMin.Text != "-"))
                {
                    if ((!(double.TryParse(textBoxMin.Text, out n))))
                    {
                        textBoxMin.Text = "";
                    }
                    else
                    {
                        d.xMin = double.Parse(textBoxMin.Text);
                    }
                }
            }
        }
        private void textBoxMax_TextChanged(object sender, EventArgs e)
        {
            if (textBoxMax.Text != "")
            {
                if (textBoxMax.Text == ",")
                {
                    textBoxMax.Text = "";
                }
                else if (!(textBoxMax.Text[textBoxMax.Text.Length - 1] == ',') && (textBoxMax.Text != "-"))
                {
                    if ((!(double.TryParse(textBoxMax.Text, out n))))
                    {
                        textBoxMax.Text = "";
                    }
                    else
                    {
                        d.xMax = double.Parse(textBoxMax.Text);
                    }
                }
            }
        }
        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ((textBoxMax.Text != "") && (textBoxMin.Text != "") && (d.xMax > d.xMin))
                {
                    double max = int.MinValue;
                    double min = int.MaxValue;
                    double sgPixX = (d.xMax - d.xMin) / (pictureBox.Width - 2 * otstup); //x на pixel
                    //создаём массив точек
                    PointF[] xy = new PointF[(pictureBox.Width - 2 * otstup + 1)];
                    double[] y = new double[(pictureBox.Width - otstup + 1)];
                    double j = d.xMin;
                    for (int i = otstup; i <= pictureBox.Width - otstup; i++)
                    {
                        j += sgPixX;
                        y[i] = f(j);
                        if (min > y[i]) { min = y[i]; }
                        if (max < y[i]) { max = y[i]; }
                    }
                    double sgPixY = (max - min) / (pictureBox.Height - 2 * otstup); //y на pixel
                    //создаем поле 
                    Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
                    // Чем будем рисовать.
                    Graphics g = Graphics.FromImage(bitmap);
                    // Устанавливаем сглаживание.
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    // "Карандаш" для отрисовки.
                    Pen p = new Pen(Color.Black, 1);
                    //ось y
                    PointF[] s = new PointF[3];
                    if ((d.xMin <= 0) && (d.xMax >= 0))
                    {
                        g.DrawLine(p, (float)((-d.xMin) / sgPixX) + otstup, 0, (float)((-d.xMin) / sgPixX) + otstup, pictureBox.Height);
                        s[0].X = (float)((-d.xMin) / sgPixX) + otstup; s[0].Y = 0; s[1].X = (float)((-d.xMin) / sgPixX) + 5 + otstup; s[1].Y = 15; s[2].X = (float)((-d.xMin) / sgPixX) - 5 + otstup; s[2].Y = 15;
                        g.FillPolygon(Brushes.Black, s);
                    }
                    else if (d.xMax < 0)
                    {
                        g.DrawLine(p, pictureBox.Width - otstup, 0, pictureBox.Width - otstup, pictureBox.Height);
                        s[0].X = pictureBox.Width - otstup; s[0].Y = 0; s[1].X = pictureBox.Width - otstup + 5; s[1].Y = 15; s[2].X = pictureBox.Width - otstup - 5; s[2].Y = 15;
                        g.FillPolygon(Brushes.Black, s);
                    }
                    else
                    {
                        g.DrawLine(p, otstup, 0, otstup, pictureBox.Height);
                        s[0].X = otstup; s[0].Y = 0; s[1].X = otstup + 5; s[1].Y = 15; s[2].X = otstup - 5; s[2].Y = 15;
                        g.FillPolygon(Brushes.Black, s);
                    }
                    //ось x
                    if ((min <= 0) && (max >= 0))
                    {
                        g.DrawLine(p, 0, (float)(max / sgPixY) + otstup, pictureBox.Width, (float)(max / sgPixY) + otstup);
                        s[0].X = pictureBox.Width; s[0].Y = (float)(max / sgPixY) + otstup; s[1].X = pictureBox.Width - 15; s[1].Y = (float)(max / sgPixY) - 5 + otstup; s[2].X = pictureBox.Width - 15; s[2].Y = (float)(max / sgPixY) + 5 + otstup;
                        g.FillPolygon(Brushes.Black, s);
                    }
                    else if (max < 0)
                    {
                        g.DrawLine(p, 0, otstup, pictureBox.Width, otstup);
                        s[0].X = pictureBox.Width; s[0].Y = otstup; s[1].X = pictureBox.Width - 15; s[1].Y = otstup - 5; s[2].X = pictureBox.Width - 15; s[2].Y = otstup + 5;
                        g.FillPolygon(Brushes.Black, s);
                    }
                    else
                    {
                        g.DrawLine(p, 0, pictureBox.Height - otstup, pictureBox.Width, pictureBox.Height - otstup);
                        s[0].X = pictureBox.Width; s[0].Y = pictureBox.Height - otstup; s[1].X = pictureBox.Width - 15; s[1].Y = pictureBox.Height - otstup - 5; s[2].X = pictureBox.Width - 15; s[2].Y = pictureBox.Height - otstup + 5;
                        g.FillPolygon(Brushes.Black, s);
                    }
                    //шаг x 
                    double shagX = Math.Pow(10, -5);
                    while ((d.xMax - d.xMin) / shagX > 20)
                    {
                        shagX *= 2;
                        if (!((d.xMax - d.xMin) / shagX > 20)) { break; }
                        shagX *= 5;
                    }
                    //шаг Y
                    double shagY = Math.Pow(10, -5);
                    while ((max - min) / shagY > 20)
                    {
                        shagY *= 2;
                        if (!((max - min) / shagY > 20)) { break; }
                        shagY *= 5;
                    }
                    //шаги x
                    p = new Pen(Color.Gray, 1);
                    double xmod;
                    if (d.xMin < 0) { xmod = d.xMin - ((int)(d.xMin / shagX) - 1) * shagX; }
                    else { xmod = d.xMin - ((int)(d.xMin / shagX)) * shagX; }
                    double xRast = (shagX - xmod) / sgPixX + otstup;
                    double xNach = (shagX - xmod) + d.xMin;
                    if ((d.xMin <= 0) && (d.xMax >= 0))
                        if ((min <= 0) && (max >= 0))
                            for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                            {
                                if (!(((float)(xRast + (shagX * i) / sgPixX) >= (float)((-d.xMin) / sgPixX) + otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= (float)((-d.xMin) / sgPixX) + otstup + 5)))
                                    g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                                g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, (float)(max / sgPixY) + otstup + 3);
                            }
                        else if (max < 0)
                            for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                            {
                                if (!(((float)(xRast + (shagX * i) / sgPixX) >= (float)((-d.xMin) / sgPixX) + otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= (float)((-d.xMin) / sgPixX) + otstup + 5)))
                                    g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                                g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, otstup + 3);
                            }
                        else
                            for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                            {
                                if (!(((float)(xRast + (shagX * i) / sgPixX) >= (float)((-d.xMin) / sgPixX) + otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= (float)((-d.xMin) / sgPixX) + otstup + 5)))
                                    g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                                g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, pictureBox.Height - otstup + 3);
                            }
                    else if (d.xMax < 0)
                        if ((min <= 0) && (max >= 0))
                            for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                            {
                                if (!(((float)(xRast + (shagX * i) / sgPixX) >= pictureBox.Width - otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= pictureBox.Width - otstup + 5)))
                                    g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                                g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, (float)(max / sgPixY) + otstup + 3);
                            }
                        else if (max < 0)
                            for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                            {
                                if (!(((float)(xRast + (shagX * i) / sgPixX) >= pictureBox.Width - otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= pictureBox.Width - otstup + otstup + 5)))
                                    g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                                g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, otstup + 3);
                            }
                        else
                            for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                            {
                                if (!(((float)(xRast + (shagX * i) / sgPixX) >= pictureBox.Width - otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= pictureBox.Width - otstup + 5)))
                                    g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                                g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, pictureBox.Height - otstup + 3);
                            }
                    else
                    {
                        if ((min <= 0) && (max >= 0))
                            for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                            {
                                if (!(((float)(xRast + (shagX * i) / sgPixX) >= otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= otstup + 5)))
                                    g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                                g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, (float)(max / sgPixY) + otstup + 3);
                            }
                        else if (max < 0)
                            for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                            {
                                if (!(((float)(xRast + (shagX * i) / sgPixX) >= otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= otstup + 5)))
                                    g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                                g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, otstup + 3);
                            }
                        else
                            for (int i = 0; i <= (int)((d.xMax - d.xMin) / shagX) + 1; i++)
                            {
                                if (!(((float)(xRast + (shagX * i) / sgPixX) >= otstup - 5) && ((float)(xRast + (shagX * i) / sgPixX) <= otstup + 5)))
                                    g.DrawLine(p, (float)(xRast + (shagX * i) / sgPixX), 0, (float)(xRast + (shagX * i) / sgPixX), pictureBox.Height);
                                g.DrawString((xNach + shagX * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)(xRast + (shagX * i) / sgPixX) + 3, pictureBox.Height - otstup + 3);
                            }
                    }
                    //шаги y
                    double ymod;
                    if (max < 0) { ymod = max - ((int)(max / shagY) - 1) * shagY; }
                    else { ymod = max - ((int)(max / shagY)) * shagY; }
                    double yRast = ymod / sgPixY + otstup;
                    double yNach = -ymod + max;
                    if ((min <= 0) && (max >= 0))
                        if ((d.xMin <= 0) && (d.xMax >= 0))
                            for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                            {
                                if (!(((float)(yRast + (shagY * i) / sgPixY) >= (float)(max / sgPixY) + otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= (float)(max / sgPixY) + otstup + 5)))
                                    g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                                g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)((-d.xMin) / sgPixX) + otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                            }
                        else if (d.xMax < 0)
                            for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                            {
                                if (!(((float)(yRast + (shagY * i) / sgPixY) >= (float)(max / sgPixY) + otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= (float)(max / sgPixY) + otstup + 5)))
                                    g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                                g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), pictureBox.Width - otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                            }
                        else
                            for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                            {
                                if (!(((float)(yRast + (shagY * i) / sgPixY) >= (float)(max / sgPixY) + otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= (float)(max / sgPixY) + otstup + 5)))
                                    g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                                g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                            }
                    else if (max < 0)
                        if ((d.xMin <= 0) && (d.xMax >= 0))
                            for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                            {
                                if (!(((float)(yRast + (shagY * i) / sgPixY) >= otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= otstup + 5)))
                                    g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                                g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)((-d.xMin) / sgPixX) + otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                            }
                        else if (d.xMax < 0)
                            for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                            {
                                if (!(((float)(yRast + (shagY * i) / sgPixY) >= otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= otstup + 5)))
                                    g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                                g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), pictureBox.Width - otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                            }
                        else
                            for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                            {
                                if (!(((float)(yRast + (shagY * i) / sgPixY) >= otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= otstup + 5)))
                                    g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                                g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                            }
                    else
                    {
                        if ((d.xMin <= 0) && (d.xMax >= 0))
                            for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                            {
                                if (!(((float)(yRast + (shagY * i) / sgPixY) >= pictureBox.Height - otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= pictureBox.Height - otstup + 5)))
                                    g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                                g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), (float)((-d.xMin) / sgPixX) + otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                            }
                        else if (d.xMax < 0)
                            for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                            {
                                if (!(((float)(yRast + (shagY * i) / sgPixY) >= pictureBox.Height - otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= pictureBox.Height - otstup + 5)))
                                    g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                                g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), pictureBox.Width - otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                            }
                        else
                            for (int i = 0; i <= (int)((max - min) / shagY) + 1; i++)
                            {
                                if (!(((float)(yRast + (shagY * i) / sgPixY) >= pictureBox.Height - otstup - 5) && ((float)(yRast + (shagY * i) / sgPixY) <= pictureBox.Height - otstup + 5)))
                                    g.DrawLine(p, 0, (float)(yRast + (shagY * i) / sgPixY), pictureBox.Width, (float)(yRast + (shagY * i) / sgPixY));
                                g.DrawString((yNach - shagY * i).ToString(), new Font("Arial", 7), new SolidBrush(Color.Black), otstup + 3, (float)(yRast + (shagY * i) / sgPixY) + 3);
                            }
                    }
                    //график
                    j = d.xMin;
                    p = new Pen(Color.Red, 1);
                    for (int i = otstup; i <= pictureBox.Width - otstup; i++)
                    {
                        j += sgPixX;
                        xy[i - otstup].X = i;
                        xy[i - otstup].Y = (float)((max - y[i]) / sgPixY) + otstup;
                    }
                    g.DrawLines(p, xy);
                    //выводим 
                    pictureBox.Image = bitmap;
                }
                else
                {
                    Ошибка f = new Ошибка();
                    f.Show();
                }
            }
        }
    }
}