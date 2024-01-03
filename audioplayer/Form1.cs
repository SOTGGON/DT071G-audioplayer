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
            }
            else
            {
                axWindowsMediaPlayer1.URL = paths[listBox1.SelectedIndex];
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }

            button2.Text = "Pause";
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

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
