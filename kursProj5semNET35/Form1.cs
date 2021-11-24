using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Threading;

namespace kursProj5semNET35
{
    public partial class Form1 : Form
    {
        UInt64 mbs = 1048576;
        UInt64 gbs = 1073741824;
        public Form1()
        {
            InitializeComponent();
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            ObjectQuery queryBattery = new ObjectQuery("Select * FROM Win32_Battery");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(queryBattery);
            ManagementObjectCollection collection = searcher.Get();
            if (collection.Count != 0)
            {
                foreach (ManagementObject item in collection)
                {
                    if (collection.Count != 0)
                    {
                        label2.Text = $"{ item["EstimatedChargeRemaining"]} %";
                    }
                    else
                    {
                        label2.Text = "Вы зашли с компьютера, у вас нет батареи";
                    }


                }
            }
            else
            {
                label2.Text = "Вы сидите с компьютера,у вас нет батареи";
            }

            ManagementObjectSearcher searcherRAM = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject item in searcherRAM.Get())
            {
                label3.Text = $"Всего оперативной памяти: {item["TotalVisibleMemorySize"]} KB";

                label4.Text = $"Свободно оперативной памяти: {item["FreePhysicalMemory"]} KB";
            }
            ManagementObjectSearcher searcherMEM = new ManagementObjectSearcher("select FreeSpace,Size,Name,SystemName from Win32_LogicalDisk");
            foreach (ManagementObject item in searcherMEM.Get())
            {
                label1.Text = $"Название компьютера: {item["SystemName"]}";
                UInt64 allSpace = (UInt64)item["Size"];
                UInt64 freeSpace = (UInt64)item["FreeSpace"];
                UInt64 spaceInMBs = allSpace / mbs;
                UInt64 spaceInGBs = allSpace / gbs;
                UInt64 freeInMBs = freeSpace / mbs;
                UInt64 freeInGBs = freeSpace / gbs;
                label5.Text = $"Изначальное количество места на дисках {spaceInMBs} МБ ({spaceInGBs} ГБ)";
                label6.Text = $"Свободно дискового пространства {freeInMBs} МБ ({freeInGBs} ГБ)";
            }
            int u = 0;
            string gpuText = "";
            ManagementObjectSearcher searcherGPU = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
            foreach (ManagementObject item in searcherGPU.Get())
            {
                while (u < 2)
                {
                    gpuText += $"Название {item["Caption"]}\n";



                    foreach (PropertyData data in item.Properties)
                    {

                        switch (data.Name)
                        {
                            case "AdapterRAM":
                                gpuText += $"количество памяти {data.Value} Б \n";
                                break;
                            default:
                                break;
                        }

                    }
                    u++;
                }
            }

            label9.Text = gpuText;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateInfo();
            foreach (var item in this.Controls)
            {
                if (item is Label)
                {
                    Label label = (Label)item;
                    label.Update();
                }

            }
            CPUInfo.Cores.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormRAM formRAM = new FormRAM();
            formRAM.ShowDialog();
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            CPUInfo.Cores.Clear();
            ManagementObjectSearcher searcherProcUsage = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");
                    foreach (ManagementObject obj in searcherProcUsage.Get())
                    {
                        if (obj["Name"] == "_Total")
                        {
                            CPUInfo.TotalUsage = Convert.ToInt32(obj["PercentProcessorTime"]);
                        }
                        else
                        {
                            Core core = new Core();
                            core.CPUCoreName = obj["Name"].ToString();
                            core.CPUCoreUsage = Convert.ToInt32(obj["PercentProcessorTime"]);
                            CPUInfo.Cores.Add(core);
                        }

                        




                    }
                    FormCPU formCPU = new FormCPU();
            formCPU.ShowDialog();
                    
        }

        private void дискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMem mem=new FormMem();
            mem.ShowDialog();
        }
    }
}

