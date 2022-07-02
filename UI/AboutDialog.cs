/*
    Copyright (c) 2010-2018 by Genstein and Jason Lautzenheiser.

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
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace Trizbort.UI; 

public sealed partial class AboutDialog : Form {
  public AboutDialog() {
    InitializeComponent();
    try {
      m_versionLabel.Text = $"Version {Assembly.GetExecutingAssembly().GetName().Version.ToString().Trim('.', '0')}";
    }
    catch (Exception) {
      // ignored
    }
  }

  private void onLinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
    var label = (LinkLabel) sender;
    var url = label.Text.Substring(label.LinkArea.Start, label.LinkArea.Length);
    if (!url.StartsWith("http")) url = "http://" + url;
    Process.Start(url);
  }
}