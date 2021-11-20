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
    
    public partial class FormRAM : Form
    {
        List<string> lines = new List<string>();

        public FormRAM()
        {
            InitializeComponent();
            
            int i = 0;
            ManagementObjectSearcher searcherRAMInfo = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            foreach (ManagementObject item in searcherRAMInfo.Get())
            {

                string line = "";
                line += $"Слот: {item["BankLabel"]} \n";
                line += $"Находится в слоте : {item["DeviceLocator"]} \n";
                line += $"Производитель : {item["Manufacturer"]} \n";
                line += $"Объём : {item["Capacity"]} \n";
                line += $"Максимальное напряжение : {item["MaxVoltage"]}\n";
                line += $"Минимальное напряжение : {item["MinVoltage"]}  \n";
                line += $"Установленное напряжение : {item["ConfiguredVoltage"]}  \n";
                line += $"Установленная частота : {item["ConfiguredClockSpeed"]}  \n";
                lines.Add(line);
                i++;
            }
            richTextBox1.Lines = lines.ToArray();
        }
    }
}
