﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using NAudio.Wave;

namespace FishingPlanetHelper
{
    public partial class Form1 : Form
    {
        string GamePath = @"C:\Program Files (x86)\Steam\steamapps\common\Fishing Planet\FishingPlanet_Data\Managed";
        string DllPath = @"C:\Program Files (x86)\Steam\steamapps\common\Fishing Planet\FishingPlanet_Data\Managed\Assembly-CSharp.dll";

        public Form1()
        {
            InitializeComponent();

             ThreadPool.QueueUserWorkItem(
             delegate (object param)
             {
                 MemoryStream mp3file = new MemoryStream(Properties.Resources.mmm);
                 Mp3FileReader mp3reader = new Mp3FileReader(mp3file);

                 using (var waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                 {
                     waveOut.Init(mp3reader);
                     waveOut.Play();
                     while (waveOut.PlaybackState == PlaybackState.Playing)
                     {
                         Thread.Sleep(100);
                     }
                 }
             });

            button2.Enabled = false;
            textBox1.Text = GamePath;

            string[] file = Directory.GetFiles(GamePath, "Assembly-CSharp.dll");

            if (file.Length != 0)
            {
                var versInfo = FileVersionInfo.GetVersionInfo(DllPath);

                String fileVersion = versInfo.FileVersion;
                label2.Text = "Assembly-Csharp.dll version = " + fileVersion;
                if(fileVersion == "0.0.6740.24271")
                {
                    button2.Enabled = true;
                } 
            }
            else
            {
                label2.Text = "Assembly-Csharp.dll not found or version is not correct!";
                button2.Enabled = false;
            }
        }

        bool CheckDll()
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;

                string[] file = Directory.GetFiles(folderBrowserDialog1.SelectedPath, "Assembly-CSharp.dll");

                if (file.Length != 0)
                {
                    return true;
                }   
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckDll() == true)
            {
                label2.Text = "Assembly-Csharp.dll found!";
                button2.Enabled = true;
            }
            else
            {
                label2.Text = "Assembly-Csharp.dll not found or version is not correct!";
                button2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add("Starting");

            var versInfo = FileVersionInfo.GetVersionInfo(textBox1.Text + @"\Assembly-CSharp.dll");
            String fileVersion = versInfo.FileVersion;
            listBox1.Items.Add("Assembly-CSharp.dll version = " + fileVersion);

            System.Threading.Thread.Sleep(200);

            listBox1.Items.Add("Backuping original file");
            if (File.Exists(textBox1.Text + @"\Assembly-CSharp.dll" + ".bak"))
            {
                File.Delete(textBox1.Text + @"\Assembly-CSharp.dll" + ".bak");
            }
            System.IO.File.Move(textBox1.Text + @"\Assembly-CSharp.dll", textBox1.Text + @"\Assembly-CSharp.dll" + ".bak");

            System.Threading.Thread.Sleep(200);

            listBox1.Items.Add("Copying files..");

            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("FishingPlanetHelper.Assembly-CSharp.dll");
            var fileStream = File.Create(textBox1.Text + @"\Assembly-CSharp.dll");
            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(fileStream);
            fileStream.Close();

            System.Threading.Thread.Sleep(200);
            listBox1.Items.Add("Done!");
        }
    }
}
