using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FishingPlanetHelper
{
    public partial class Form1 : Form
    {
        string GamePath = @"C:\Program Files (x86)\Steam\steamapps\common\Fishing Planet\FishingPlanet_Data\Managed";
        string DllPath = @"C:\Program Files (x86)\Steam\steamapps\common\Fishing Planet\FishingPlanet_Data\Managed\Assembly-CSharp.dll";

        public Form1()
        {
            InitializeComponent();

            textBox1.Text = GamePath;

            var versInfo = FileVersionInfo.GetVersionInfo(DllPath);

            String fileVersion = versInfo.FileVersion;
            label2.Text = "Assembly-Csharp.dll version = " + fileVersion;
        }

        bool CheckDll()
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                 string[] file = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "Assembly-CSharp.dll");

                if (file.Length != 0)
                {
                    label2.Text = "Assembly-Csharp.dll found!";
                    return true;
                }
                label2.Text = "Assembly-Csharp.dll not found!";
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckDll() == true)
            {
                textBox1.Text = "ITS OK";
            }
            else
            {
                textBox1.Text = "ITS BAAD";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Starting");

            var versInfo = FileVersionInfo.GetVersionInfo("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Fishing Planet\\FishingPlanet_Data\\Managed\\Assembly-CSharp.dll");
            String fileVersion = versInfo.FileVersion;
            listBox1.Items.Add("CSharp.dll version = " + fileVersion);
            label2.Text = "Csharp.dll FOUND - current version = " + fileVersion;

            System.Threading.Thread.Sleep(200);
            listBox1.Items.Add("Backup original file");
            if (File.Exists("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Fishing Planet\\FishingPlanet_Data\\Managed\\Assembly-CSharp.dll.bak"))
            {
                File.Delete("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Fishing Planet\\FishingPlanet_Data\\Managed\\Assembly-CSharp.dll.bak");
            }
            System.IO.File.Move("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Fishing Planet\\FishingPlanet_Data\\Managed\\Assembly-CSharp.dll", "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Fishing Planet\\FishingPlanet_Data\\Managed\\Assembly-CSharp.dll.bak");

            System.Threading.Thread.Sleep(200);
            listBox1.Items.Add("Copying files..");
            

            //string[] files = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames();
            //foreach(string element in files)
            //{
            //    listBox1.Items.Add(element);
            //}

            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("FishingPlanetHelper.Assembly-CSharp.dll");
            var fileStream = File.Create("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Fishing Planet\\FishingPlanet_Data\\Managed\\Assembly-CSharp.dll");
            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(fileStream);
            fileStream.Close();

            System.Threading.Thread.Sleep(200);
            listBox1.Items.Add("Done!");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
