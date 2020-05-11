﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace ManyEventsDiscreth
{
    public partial class Form1 : Form
    {
        
        Random rnd;
        List<float> values = new List<float>();
        int[] Count = new int[5] {0,0,0,0,0 };
        public Form1()
        {
            InitializeComponent();
            chart1.ChartAreas[0].AxisX.Maximum = 1;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.IntervalOffset = 0;
            chart1.ChartAreas[0].AxisX.Interval = 0;
            rnd = new Random();
            
        }

        private void CalculateBtn_Click(object sender, EventArgs e)
        {
            
            init();
            if (check())
            {
                StatPanel.Visible = true;
                double A = 0;
                int k = 0;
                float M = 0, D = 0, Derror = 0, Merror = 0, Chi_square = 0;
                for (int i = 0; i < numericUpDown1.Value; i++)
                {
                    A = rnd.NextDouble();
                    k = 0;
                    foreach (var value in values)
                    {
                        A -= value;
                        if (A<=0)
                        {
                            Count[k]++;
                            break;
                        }
                        k++;
                    }
                }
                

                for (int i = 0; i < 5; i++)
                {
                    M += (Count[i] / (float)numericUpDown1.Value) * values[i];
                    Merror = Count[i] * values[i];

                    D += (Count[i] / (float)numericUpDown1.Value) * values[i] * values[i];
                    Derror = Count[i] * values[i] * values[i];

                    Chi_square += ((Count[i] * Count[i]) / ((float)numericUpDown1.Value * values[i]));
                }
                Chi_square -= (float)numericUpDown1.Value;
                D -= M * M;
                Derror -= Merror * Merror;
                VarianceLbl.Text = D.ToString();
                AverageLbl.Text = M.ToString();
                ErrAvgLbl.Text = (Math.Abs((M - Merror)/Math.Abs(Merror))).ToString();
                ErrVarLbl.Text = (Math.Abs((D - Derror)/Math.Abs(Derror))).ToString();
                Chi1Lbl.Text = Chi_square.ToString();
                Chi2Lbl.Text = "11.070";
                for (int i = 0; i < 5; i++)
                {
                    Count[i] = 0;
                }
            }
            

            
        }

        bool check()
        {
            var SumToCheck = 0f;
            foreach (var value in values)
            {
                SumToCheck += value;
            }
            if (SumToCheck == 1f)
            {
                var x = 0.1;
                foreach (var value in values)
                {

                    chart1.Series[0].Points.AddXY(x, value);
                    x += 0.2;
                }
                return true;
            }
            else
            {
                chart1.Visible = false;
                ErrorLbl.Visible = true;
                return false;
            }
        }

        void init()
        {
            ErrorLbl.Visible = false;
            chart1.Visible = true;
            chart1.Series[0].Points.Clear();
            values.Clear();
            values.Add(float.Parse(textBox1.Text, CultureInfo.InvariantCulture.NumberFormat));
            values.Add(float.Parse(textBox2.Text, CultureInfo.InvariantCulture.NumberFormat));
            values.Add(float.Parse(textBox3.Text, CultureInfo.InvariantCulture.NumberFormat));
            values.Add(float.Parse(textBox4.Text, CultureInfo.InvariantCulture.NumberFormat));
            values.Add(float.Parse(textBox5.Text, CultureInfo.InvariantCulture.NumberFormat));

            
        }
    }
}
