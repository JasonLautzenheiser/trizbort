/*
    Copyright (c) 2010-2015 by Genstein and Jason Lautzenheiser.

    This file is (or was originally) part of Trizbort, the Interactive Fiction Mapper.

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using System;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Globalization;
using System.Reflection;
using System.IO;
using System.Threading;

namespace Trizbort
{
    public partial class NewVersionDialog : Form
    {
        public NewVersionDialog()
        {
            InitializeComponent();
        }

        public static void Show(Version currentVersion, Version latestVersion, bool alwaysAsk)
        {
            if (alwaysAsk || Settings.DontCareAboutVersion < latestVersion)
            {
                using (var dialog = new NewVersionDialog())
                {
                    dialog.m_yourVersionLabel.Text = currentVersion.ToString().Trim('0', '.');
                    dialog.m_latestVersionLabel.Text = latestVersion.ToString().Trim('0', '.');
                    dialog.m_dontCareCheckBox.Visible = !alwaysAsk;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            Process.Start("http://trizbort.genstein.net/?getlatest");
                        }
                        catch (Exception)
                        {
                            CannotLaunchWebSite();
                        }
                    }
                    else if (!alwaysAsk && dialog.m_dontCareCheckBox.Checked)
                    {
                        Settings.DontCareAboutVersion = latestVersion;
                    }
                }
            }
        }

        public static void CannotLaunchWebSite()
        {
            MessageBox.Show("Unable to launch the Trizbort website.\n\nVisit http://trizbort.genstein.net for help and support.", Application.ProductName, MessageBoxButtons.OK);
        }

        public static void CheckForUpdatesAsync(Form form, bool verbose)
        {
            try
            {
                ThreadPool.QueueUserWorkItem(delegate(object _) { CheckForUpdates(form, verbose); });
            }
            catch (Exception)
            {
            }
        }

        public static void CheckForUpdates(Form form, bool verbose)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(Uri.EscapeUriString(string.Format("http://trizbort.genstein.net/webservice.php?op=updatecheck&current={0}", Application.ProductVersion)));
                var response = (HttpWebResponse)request.GetResponse();

                string responseText;
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    responseText = reader.ReadToEnd();
                }

                var latestVersion = new Version(responseText.Split('\n')[0].Replace("\r", string.Empty).Trim());
                if (latestVersion.Major < 0)
                {
                    latestVersion = new Version(0, 0, 0, 0);
                }
                if (latestVersion.Minor < 0)
                {
                    latestVersion = new Version(latestVersion.Major, 0, 0, 0);
                }
                if (latestVersion.Build < 0)
                {
                    latestVersion = new Version(latestVersion.Major, latestVersion.Minor, 0, 0);
                }
                if (latestVersion.Revision < 0)
                {
                    latestVersion = new Version(latestVersion.Major, latestVersion.Minor, latestVersion.Build, 0);
                }
                var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
                if (currentVersion < latestVersion)
                {
                    form.BeginInvoke((MethodInvoker)delegate()
                    {
                        Show(currentVersion, latestVersion, verbose);
                    });
                }
                else if (verbose)
                {
                    form.BeginInvoke((MethodInvoker)delegate() { MessageBox.Show(form, string.Format("Your version of Trizbort is {0}", currentVersion == latestVersion ? "up to date." : "more recent than the latest version!"), Application.ProductName, MessageBoxButtons.OK); });
                }
            }
            catch (Exception ex)
            {
                if (verbose)
                {
                    MessageBox.Show(form, string.Format("An error occurred whilst checking for the latest version of Trizbort.\n\n{0}", ex.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
