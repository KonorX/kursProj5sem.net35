using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace kursProj5semNET35
{
    public partial class FormMem : Form
    {
        public FormMem()
        {
            InitializeComponent();
            ManagementObjectSearcher searcherCUsage = new ManagementObjectSearcher("Select * From Win32_LogicalDisk Where DeviceID = 'C:'");
            foreach (ManagementObject obj in searcherCUsage.Get())
            {
                label1.Text = $"Всего на диске С : {(UInt64)obj["Size"] / 1048576}МБ ({(UInt64)obj["Size"] / 1073741824}ГБ) ({obj["Size"]}Б)";
                label2.Text = $"Осталось места на диске С :{(UInt64)obj["FreeSpace"] / 1048576}МБ ({(UInt64)obj["FreeSpace"] / 1073741824}ГБ) ({obj["FreeSpace"]}Б)";
            }
        }

        private void FormMem_Load(object sender, EventArgs e)
        {

        }
    }
}
