using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ookii.Dialogs.WinForms;

namespace audioplayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            axWindowsMediaPlayer1.settings.volume = 20;
        }

        string[] files, paths;
        string localSelectPath;
        List<string> localSelectPathList = new List<string>();
        Random random = new Random();

        private void button1_Click(object sender, EventArgs e)
        {
            /*if (listBox1.SelectedIndex >= 0)
            {
                if (listBox1.SelectedIndex == 0)
                {
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                }
                else
                {
                    listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
                }
            }*/

            playMode(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Pause")
            {
                button2.Text = "Play";

                axWindowsMediaPlayer1.Ctlcontrols.pause();
            }
            else
            {
                button2.Text = "Pause";

                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*if (listBox1.SelectedIndex < listBox1.Items.Count - 1)
            {
                listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
            }
            else
            {
                listBox1.SelectedIndex = 0;
            }*/

            playMode(false);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (button4.Text == "Play In Order")
            {
                button4.Text = "Play Random";
                button4.Width = 80;
            }
            else if (button4.Text == "Play Random")
            {
                button4.Text = "Repeat Single";
                button4.Width = 83;
            }
            else
            {
                button4.Text = "Play In Order";
                button4.Width = 80;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Choose Audio|*.mp3; *.flac; *.wav;";
            ofd.Multiselect = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                localSelectPathList.Clear();
                listBox1.Items.Clear();

                if (files != null || paths != null)
                {
                    Array.Clear(files, 0, files.Length);
                    Array.Clear(paths, 0, files.Length);
                }

                files = ofd.FileNames;
                paths = ofd.FileNames;

                foreach (var item in files)
                {
                    listBox1.Items.Add(item);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog vfbd = new VistaFolderBrowserDialog();

            if (vfbd.ShowDialog() == DialogResult.OK)
            {
                localSelectPathList.Clear();
                listBox1.Items.Clear();

                if (files != null || paths != null)
                {
                    Array.Clear(files, 0, files.Length);
                    Array.Clear(paths, 0, files.Length);
                }

                localSelectPath = vfbd.SelectedPath;

                foreach (var item in Directory.GetFiles(localSelectPath))
                {
                    localSelectPathList.Add(item);
                    listBox1.Items.Add(item);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (localSelectPathList.Count > 0)
            {
                axWindowsMediaPlayer1.URL = localSelectPathList[listBox1.SelectedIndex];
                axWindowsMediaPlayer1.Ctlcontrols.play();

                label5.Text = Path.GetFileNameWithoutExtension(localSelectPathList[listBox1.SelectedIndex]);
            }
            else
            {
                axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
                axWindowsMediaPlayer1.Ctlcontrols.play();

                label5.Text = Path.GetFileNameWithoutExtension(paths[listBox1.SelectedIndex]);
            }

            button2.Text = "Pause";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume = trackBar1.Value;

            label6.Text = trackBar1.Value + "%";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                progressBar1.Maximum = (int)axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration;
                progressBar1.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;

                label1.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
                label2.Text = axWindowsMediaPlayer1.currentMedia.durationString;

                if (progressBar1.Maximum == progressBar1.Value)
                {
                    axWindowsMediaPlayer1.Ctlcontrols.stop();
                }
                else if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
                {
                    playMode(false);
                }
            }
        }

        private void progressBar1_MouseDown(object sender, MouseEventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.currentMedia.duration * e.X / progressBar1.Width;
        }

        #region Playmode
        public void playMode(bool mode)
        {
            if (button4.Text == "Play In Order")
            {
                if (mode)
                {
                    if (listBox1.SelectedIndex >= 0)
                    {
                        if (listBox1.SelectedIndex == 0)
                        {
                            listBox1.SelectedIndex = listBox1.Items.Count - 1;
                        }
                        else
                        {
                            listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
                        }
                    }
                }
                else
                {
                    if (listBox1.SelectedIndex < listBox1.Items.Count - 1)
                    {
                        listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
                    }
                    else
                    {
                        listBox1.SelectedIndex = 0;
                    }
                }
                
            }
            else if (button4.Text == "Play Random")
            {
                
                int r = random.Next(0, listBox1.Items.Count);

                if (r == listBox1.SelectedIndex)
                {
                    if ((r+1) == listBox1.Items.Count)
                    {
                        r--;
                    }
                    else
                    {
                        r++;
                    }
                }

                if (localSelectPathList.Count > 0)
                {
                    axWindowsMediaPlayer1.URL = localSelectPathList[listBox1.SelectedIndex = r];
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                }
                else
                {
                    axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex = r];
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                }
            }
            else if (button4.Text == "Repeat Single")
            {
                if (localSelectPathList.Count > 0)
                {
                    axWindowsMediaPlayer1.URL = localSelectPathList[listBox1.SelectedIndex];
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                }
                else
                {
                    axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                }
            }
        }

        #endregion

    }
}
