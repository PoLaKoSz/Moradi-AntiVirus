﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using AutoUpdaterDotNET;

namespace Moradi_Anti_Virus
{
    public partial class Anti_Virus : MetroFramework.Forms.MetroForm
    {
        public Anti_Virus()
        {
            InitializeComponent();
        }
        Process[] proc;

        void GetAllProcess()
        {
            proc = Process.GetProcesses();
            listBox2.Items.Clear();
            foreach (Process p in proc)
                listBox2.Items.Add(p.ProcessName);
        }

        private void Anti_Virus_Load(object sender, EventArgs e)
        {
            AutoUpdater.Start("https://raw.githubusercontent.com/Brett-Tech-Networking/Moradi-AntiVirus/master/VersionManager.xml");

            AutoUpdater.ReportErrors = true;

            AutoUpdater.Mandatory = true;

            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;

            CPURAM.Start();

            //Welcome Back
            WelcomeBack.Text = "Current User: " +  System.Environment.UserName.ToString();

            //version 
            Version.Text = "Version: " + ProductVersion.ToString();

           
        }

        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args != null)
            {
                if (args.IsUpdateAvailable)
                {
                    UpdatePrompt updater = new UpdatePrompt();
                    updater.Show();
                }
            }
        }

       

        private void EmptyFolderContents(string tempFolder)
        {
            throw new NotImplementedException();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            float fcpu = pCPU.NextValue();
            float fram = pRAM.NextValue();
            metroProgressBar2.Value = (int)fcpu;
            metroProgressBar3.Value = (int)fram;
            CPUPercent.Text = string.Format("{0:0.00}%", fcpu);
            RAMPercent.Text = string.Format("{0:0.00}%", fram);
            chart1.Series["CPU"].Points.AddY(fcpu);
            chart1.Series["RAM"].Points.AddY(fram);
        }

        private void SysInfo_Tick(object sender, EventArgs e)
        {

            try
            {

                // username
                usrname.Visible = true;
                usrname.Text = "Username: " + Environment.UserName;

                // Operating System
                OS.Visible = true;
                OS.Text = "OS: " + Environment.OSVersion;

                // Machine Name
                mchName.Visible = true;
                mchName.Text = "PC Name: " + Environment.MachineName;

                // WIFI 
                var process = new Process
                {
                    StartInfo =
                          {
                              FileName = "netsh.exe",
                              Arguments = "wlan show interfaces",
                              UseShellExecute = false,
                              RedirectStandardOutput = true,
                              CreateNoWindow = true
                          }
                };
                process.Start();

                var output = process.StandardOutput.ReadToEnd();
                var line = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                                 .FirstOrDefault(l => l.Contains("SSID") && !l.Contains("BSSID"));
                if (line == null)
                {
                    //
                }
                var ssid = line.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1].TrimStart();
                SSID.Text = "Current WIFI SSID: " + (ssid);
            }


            catch
            {
               SSID.Text = ("Unavalible (Are You Hard Wired?)");
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void ProgressBar_Tick(object sender, EventArgs e)
        {
           
        }

        private void TaskmgrTimer_Tick(object sender, EventArgs e)
        {
            GetAllProcess();
            TaskmgrTimer.Stop();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                proc[listBox2.SelectedIndex].Kill();
                GetAllProcess();
            }
            catch (Exception)
            {
                MessageBox.Show("Could Not Stop Task", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NewTask_Click(object sender, EventArgs e)
        {
            using (frmRunTask frm = new frmRunTask())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    GetAllProcess();
            }
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            listBox2.SelectedItems.Clear();
            for (int i = listBox2.Items.Count - 1; i >= 0; i--)
            {
                if (listBox2.Items[i].ToString().ToLower().Contains(metroTextBox1.Text.ToLower()))
                {
                    listBox2.SetSelected(i, true);
                }
            }
            Status.Text = listBox2.SelectedItems.Count.ToString() + " Items Found";
            metroTextBox1.Clear();
        }

        private void OpacityControl_Tick(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Form.ActiveForm.Opacity = ((double)(metroTrackBar1.Value) / 100.0);
            }
            catch
            {
                //
            }
        }

        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            if (metroToggle1.Checked == true)
            {
                this.TopMost = true;
                metroLabel1.Text = "Top Most Enabled";
            }

            if (metroToggle1.Checked == false)
            {
                this.TopMost = false;
                metroLabel1.Text = "Top Most Disabled";
            }
            }

        private void CurrentTime_Tick(object sender, EventArgs e)
        {
            time.Text = DateTime.Now.ToString("hh:mm:ss tt");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdatePrompt update = new UpdatePrompt();
            update.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            UpdatePrompt updater = new UpdatePrompt();
            updater.Show();
        }

        private void version_Click(object sender, EventArgs e)
        {

        }

        private void Overview_Click(object sender, EventArgs e)
        {

        }

        private void metroPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
    }