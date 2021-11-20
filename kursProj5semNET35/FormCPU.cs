using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace kursProj5semNET35
{
    public partial class FormCPU : Form
    {
        public FormCPU()
        {
            InitializeComponent();
            int i = 0;
            label1.Text = CPUInfo.TotalUsage.ToString();
            label1.Visible = false;
            int xtext = 0 + label1.Size.Width;
            int ytext = label1.Location.Y;
            foreach (Core core in CPUInfo.Cores)
            {

                Label label = new Label();
                label.Name = "CPUNamelabel" + i;
                label.Text = core.CPUCoreName;
                label.Location = new Point(xtext, ytext);
                int tempy = ytext + label.Size.Height;
                Label labelUsage = new Label();
                labelUsage.Name = "CPUUsage" + i;
                labelUsage.Text = core.CPUCoreUsage.ToString();
                labelUsage.Location = new Point(xtext, tempy);
                xtext = label.Location.X + label.Size.Width;


                this.Controls.Add(label);
                this.Controls.Add(labelUsage);

            }
        }
    }
}
