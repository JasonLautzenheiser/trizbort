/*
    Copyright (c) 2010 by Genstein

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Trizbort.Export;

namespace Trizbort
{
    internal partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            m_caption = Text;

            Application.Idle += OnIdle;
            m_lastUpdateUITime = DateTime.MinValue;

            m_automapBar.StopClick += delegate(object sender, EventArgs e) { m_canvas.StopAutomapping(); };
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            m_canvas.MinimapVisible = Settings.ShowMiniMap;

            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1 && File.Exists(args[1]))
            {
                try
                {
                    BeginInvoke((MethodInvoker)delegate
                    {
                        OpenProject(args[1]); 
                    });
                }
                catch (Exception)
                {
                }
            }
            NewVersionDialog.CheckForUpdatesAsync(this, false);
        }

        public Canvas Canvas
        {
            get { return m_canvas; }
        }

        private void FileNewMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckLoseProject())
                return;

            Project.Current = new Project();
            Settings.Reset();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!CheckLoseProject())
            {
                e.Cancel = true;
                return;
            }

            Settings.SaveApplicationSettings();
            m_canvas.StopAutomapping();

            base.OnClosing(e);
        }


        private bool CheckLoseProject()
        {
            if (Project.Current.IsDirty)
            {
                // see if the user would like to save
                var result = MessageBox.Show(this, string.Format("Do you want to save changes to {0}?", Project.Current.Name), Text, MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Yes:
                        // user would like to save
                        if (!SaveProject())
                        {
                            // didn't actually save; treat as cancel
                            return false;
                        }

                        // user saved; carry on
                        return true;

                    case DialogResult.No:
                        // user wouldn't like to save; carry on
                        return true;

                    default:
                        // user cancelled; cancel
                        return false;
                }
            }

            // project doesn't need saving; carry on
            return true;
        }

        private bool OpenProject()
        {
            if (!CheckLoseProject())
                return false;

            using (var dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = PathHelper.SafeGetDirectoryName(Settings.LastProjectFileName);
                dialog.Filter = string.Format("{0}|All Files|*.*||", Project.FilterString);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Settings.LastProjectFileName = dialog.FileName;
                    return OpenProject(dialog.FileName);
                }
            }

            return false;
        }

        private bool OpenProject(string fileName)
        {
            var project = new Project();
            project.FileName = fileName;
            if (project.Load())
            {
                Project.Current = project;
                //AboutMap();
                Settings.RecentProjects.Add(fileName);
                return true;
            }

            return false;
        }

        private void AboutMap()
        {
            var project = Project.Current;
            if (!string.IsNullOrEmpty(project.Title) || !string.IsNullOrEmpty(project.Author) || !string.IsNullOrEmpty(project.Description))
            {
                var builder = new StringBuilder();
                if (!string.IsNullOrEmpty(project.Title))
                {
                    builder.AppendLine(project.Title);
                }
                if (!string.IsNullOrEmpty(project.Author))
                {
                    if (builder.Length > 0)
                    {
                        builder.AppendLine();
                    }
                    builder.AppendLine(string.Format("by {0}", project.Author));
                }
                if (!string.IsNullOrEmpty(project.Description))
                {
                    if (builder.Length > 0)
                    {
                        builder.AppendLine();
                    }
                    builder.AppendLine(project.Description);
                }
                MessageBox.Show(builder.ToString(), Application.ProductName, MessageBoxButtons.OK);
            }
        }

        private bool SaveProject()
        {
            if (!Project.Current.HasFileName)
            {
                return SaveAsProject();
            }

            if (Project.Current.Save())
            {
                Settings.RecentProjects.Add(Project.Current.FileName);
                return true;
            }
            return false;
        }

        private bool SaveAsProject()
        {
            using (var dialog = new SaveFileDialog())
            {
                if (!string.IsNullOrEmpty(Project.Current.FileName))
                {
                    dialog.FileName = Project.Current.FileName;
                }
                else
                {
                    dialog.InitialDirectory = PathHelper.SafeGetDirectoryName(Settings.LastProjectFileName);
                }
                dialog.Filter = string.Format("{0}|All Files|*.*||", Project.FilterString);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Settings.LastProjectFileName = dialog.FileName;
                    Project.Current.FileName = dialog.FileName;
                    if (Project.Current.Save())
                    {
                        Settings.RecentProjects.Add(Project.Current.FileName);
                        return true;
                    }
                }
            }
            
            return false;                    
        }

        private void FileOpenMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        private void FileSaveMenuItem_Click(object sender, EventArgs e)
        {
            SaveProject();
        }

        private void FileSaveAsMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsProject();
        }

        private void FileExportPDFMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "PDF Files|*.pdf|All Files|*.*||";
                dialog.Title = "Export PDF";
                dialog.InitialDirectory = PathHelper.SafeGetDirectoryName(Settings.LastExportImageFileName);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Settings.LastExportImageFileName = dialog.FileName;

                        var doc = new PdfDocument();
                        doc.Info.Title = Project.Current.Title;
                        doc.Info.Author = Project.Current.Author;
                        doc.Info.Creator = Application.ProductName;
                        doc.Info.CreationDate = DateTime.Now;
                        doc.Info.Subject = Project.Current.Description;
                        var page = doc.AddPage();

                        var size = m_canvas.ComputeCanvasBounds(true).Size;
                        page.Width = new XUnit(size.X);
                        page.Height = new XUnit(size.Y);
                        using (var graphics = XGraphics.FromPdfPage(page))
                        {
                            m_canvas.Draw(graphics, true, size.X, size.Y);
                        }
                        doc.Save(dialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Program.MainForm, string.Format("There was a problem exporting the map:\n\n{0}", ex.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }            
        }

        private void FileExportImageMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "PNG Images|*.png|JPEG Images|*.jpg|BMP Images|*.bmp|Enhanced Metafiles (EMF)|*.emf|All Files|*.*||";
                dialog.Title = "Export Image";
                dialog.InitialDirectory = PathHelper.SafeGetDirectoryName(Settings.LastExportImageFileName);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Settings.LastExportImageFileName = dialog.InitialDirectory;

                    var format = ImageFormat.Png;
                    var ext = Path.GetExtension(dialog.FileName);
                    if (StringComparer.InvariantCultureIgnoreCase.Compare(ext, ".jpg") == 0
                        || StringComparer.InvariantCultureIgnoreCase.Compare(ext, ".jpeg") == 0)
                    {
                        format = ImageFormat.Jpeg;
                    }
                    else if (StringComparer.InvariantCultureIgnoreCase.Compare(ext, ".bmp") == 0)
                    {
                        format = ImageFormat.Bmp;
                    }
                    else if (StringComparer.InvariantCultureIgnoreCase.Compare(ext, ".emf") == 0)
                    {
                        format = ImageFormat.Emf;
                    }

                    var size = m_canvas.ComputeCanvasBounds(true).Size * m_canvas.ZoomFactor;
                    size.X = Numeric.Clamp(size.X, 16, 8192);
                    size.Y = Numeric.Clamp(size.Y, 16, 8192);
                    
                    try
                    {
                        if (format == ImageFormat.Emf)
                        {
                            // export as a metafile
                            using (var nativeGraphics = Graphics.FromHwnd(m_canvas.Handle))
                            {
                                using (var stream = new MemoryStream())
                                {
                                    try
                                    {
                                        var dc = nativeGraphics.GetHdc();
                                        using (var metafile = new Metafile(stream, dc))
                                        {
                                            using (var imageGraphics = Graphics.FromImage(metafile))
                                            {
                                                using (var graphics = XGraphics.FromGraphics(imageGraphics, new XSize(size.X, size.Y)))
                                                {
                                                    m_canvas.Draw(graphics, true, size.X, size.Y);
                                                }
                                            }
                                            var handle = metafile.GetHenhmetafile();
                                            var copy = CopyEnhMetaFile(handle, dialog.FileName);
                                            DeleteEnhMetaFile(copy);
                                        }
                                    }
                                    finally
                                    {
                                        nativeGraphics.ReleaseHdc();
                                    }
                                }
                            }
                        }
                        else
                        {
                            // export as an image
                            using (var bitmap = new Bitmap((int)Math.Ceiling(size.X), (int)Math.Ceiling(size.Y)))
                            {
                                using (var imageGraphics = Graphics.FromImage(bitmap))
                                {
                                    using (var graphics = XGraphics.FromGraphics(imageGraphics, new XSize(size.X, size.Y)))
                                    {
                                        m_canvas.Draw(graphics, true, size.X, size.Y);
                                    }
                                }
                                bitmap.Save(dialog.FileName, format);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(Program.MainForm, string.Format("There was a problem exporting the map:\n\n{0}", ex.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void FileExportInform7MenuItem_Click(object sender, EventArgs e)
        {
            var fileName = Settings.LastExportInform7FileName;
            if (ExportCode<Inform7Exporter>(ref fileName))
            {
                Settings.LastExportInform7FileName = fileName;
            }
        }

        private void FileExportInform6MenuItem_Click(object sender, EventArgs e)
        {
            var fileName = Settings.LastExportInform6FileName;
            if (ExportCode<Inform6Exporter>(ref fileName))
            {
                Settings.LastExportInform6FileName = fileName;
            }
        }

        private void FileExportTadsMenuItem_Click(object sender, EventArgs e)
        {
            var fileName = Settings.LastExportTadsFileName;
            if (ExportCode<TadsExporter>(ref fileName))
            {
                Settings.LastExportTadsFileName = fileName;
            }
        }

        private bool ExportCode<T>(ref string lastExportFileName) where T : CodeExporter, new()
        {
            using (var exporter = new T())
            {
                using (var dialog = new SaveFileDialog())
                {
                    // compose filter string for file dialog
                    var filterString = string.Empty;
                    var filters = exporter.FileDialogFilters;
                    foreach (var filter in filters)
                    {
                        if (!string.IsNullOrEmpty(filterString))
                        {
                            filterString += "|";
                        }
                        filterString += string.Format("{0}|*{1}", filter.Key, filter.Value);
                    }

                    if (!string.IsNullOrEmpty(filterString))
                    {
                        filterString += "|";
                    }
                    filterString += "All Files|*.*||";
                    dialog.Filter = filterString;

                    // set default filter by extension
                    var extension = PathHelper.SafeGetExtension(lastExportFileName);
                    for (var filterIndex = 0; filterIndex < filters.Count; ++filterIndex)
                    {
                        if (StringComparer.InvariantCultureIgnoreCase.Compare(extension, filters[filterIndex].Value) == 0)
                        {
                            dialog.FilterIndex = filterIndex + 1; // 1 based index
                            break;
                        }
                    }

                    // show dialog
                    dialog.Title = exporter.FileDialogTitle;
                    dialog.InitialDirectory = PathHelper.SafeGetDirectoryName(lastExportFileName);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // export source code
                            exporter.Export(dialog.FileName);
                            lastExportFileName = dialog.FileName;
                            return true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Program.MainForm, string.Format("There was a problem exporting the map:\n\n{0}", ex.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            
            return false;
        }

        private void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditAddRoomMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.AddRoom(false);
        }

        private void EditDeleteMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.DeleteSelection();
        }

        private void EditPropertiesMenuItem_Click(object sender, EventArgs e)
        {
            if (m_canvas.HasSingleSelectedElement && m_canvas.SelectedElement.HasDialog)
            {
                m_canvas.SelectedElement.ShowDialog();
            }
        }

        private void PlainLinesMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.ApplyNewPlainConnectionSettings();
        }

        private void ToggleDottedLines_Click(object sender, EventArgs e)
        {
            switch (m_canvas.NewConnectionStyle)
            {
                case ConnectionStyle.Solid:
                    m_canvas.NewConnectionStyle = ConnectionStyle.Dashed;
                    break;
                case ConnectionStyle.Dashed:
                    m_canvas.NewConnectionStyle = ConnectionStyle.Solid;
                    break;
            }
            m_canvas.ApplyConnectionStyle(m_canvas.NewConnectionStyle);
        }

        private void ToggleDirectionalLines_Click(object sender, EventArgs e)
        {
            switch (m_canvas.NewConnectionFlow)
            {
                case ConnectionFlow.TwoWay:
                    m_canvas.NewConnectionFlow = ConnectionFlow.OneWay;
                    break;
                case ConnectionFlow.OneWay:
                    m_canvas.NewConnectionFlow = ConnectionFlow.TwoWay;
                    break;
            }
            m_canvas.ApplyConnectionFlow(m_canvas.NewConnectionFlow);
        }

        private void UpLinesMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.NewConnectionLabel = ConnectionLabel.Up;
            m_canvas.ApplyConnectionLabel(m_canvas.NewConnectionLabel);
        }

        private void DownLinesMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.NewConnectionLabel = ConnectionLabel.Down;
            m_canvas.ApplyConnectionLabel(m_canvas.NewConnectionLabel);
        }

        private void InLinesMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.NewConnectionLabel = ConnectionLabel.In;
            m_canvas.ApplyConnectionLabel(m_canvas.NewConnectionLabel);
        }

        private void OutLinesMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.NewConnectionLabel = ConnectionLabel.Out;
            m_canvas.ApplyConnectionLabel(m_canvas.NewConnectionLabel);
        }

        private void ReverseLineMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.ReverseLineDirection();
        }

        private void OnIdle(object sender, EventArgs e)
        {
            var now = DateTime.Now;
            if (now - m_lastUpdateUITime > IdleProcessingEveryNSeconds)
            {
                m_lastUpdateUITime = now;
                UpdateCommandUI();
            }
        }

        private void UpdateCommandUI()
        {
            // caption
            Text = string.Format("{0}{1} - {2}", Project.Current.Name, Project.Current.IsDirty ? "*" : string.Empty, m_caption);

            // line drawing options
            m_toggleDottedLinesButton.Checked = m_canvas.NewConnectionStyle == ConnectionStyle.Dashed;
            m_toggleDottedLinesMenuItem.Checked = m_toggleDottedLinesButton.Checked;
            m_toggleDirectionalLinesButton.Checked = m_canvas.NewConnectionFlow == ConnectionFlow.OneWay;
            m_toggleDirectionalLinesMenuItem.Checked = m_toggleDirectionalLinesButton.Checked;
            m_plainLinesMenuItem.Checked = !m_toggleDirectionalLinesMenuItem.Checked && !m_toggleDottedLinesMenuItem.Checked && m_canvas.NewConnectionLabel == ConnectionLabel.None;
            m_upLinesMenuItem.Checked = m_canvas.NewConnectionLabel == ConnectionLabel.Up;
            m_downLinesMenuItem.Checked = m_canvas.NewConnectionLabel == ConnectionLabel.Down;
            m_inLinesMenuItem.Checked = m_canvas.NewConnectionLabel == ConnectionLabel.In;
            m_outLinesMenuItem.Checked = m_canvas.NewConnectionLabel == ConnectionLabel.Out;

            // selection-specific commands
            bool hasSelectedElement = m_canvas.SelectedElement != null;
            m_editDeleteMenuItem.Enabled = hasSelectedElement;
            m_editPropertiesMenuItem.Enabled = m_canvas.HasSingleSelectedElement;
            m_editIsDarkMenuItem.Enabled = hasSelectedElement;
            m_editSelectNoneMenuItem.Enabled = hasSelectedElement;
            m_editSelectAllMenuItem.Enabled = m_canvas.SelectedElementCount < Project.Current.Elements.Count;
            m_editCopyMenuItem.Enabled = m_canvas.SelectedElement != null;
            m_editCopyColorToolMenuItem.Enabled = m_canvas.HasSingleSelectedElement && (m_canvas.SelectedElement is Room);
            m_editPasteMenuItem.Enabled = (!String.IsNullOrEmpty(Clipboard.GetText())) && ((Clipboard.GetText().Replace("\r\n", "|").Split('|')[0] == "Elements") || (Clipboard.GetText().Replace("\r\n", "|").Split('|')[0] == "Colors"));
            m_editRenameMenuItem.Enabled = m_canvas.HasSingleSelectedElement && (m_canvas.SelectedElement is Room);
            m_editIsDarkMenuItem.Checked = m_canvas.HasSingleSelectedElement && (m_canvas.SelectedElement is Room) && ((Room)m_canvas.SelectedElement).IsDark;
            m_reverseLineMenuItem.Enabled = m_canvas.HasSelectedElement<Connection>();

            // automapping
            m_automapStartMenuItem.Enabled = !m_canvas.IsAutomapping;
            m_automapStopMenuItem.Enabled = m_canvas.IsAutomapping;
            m_automapBar.Visible = m_canvas.IsAutomapping;
            m_automapBar.Status = m_canvas.AutomappingStatus;

            // minimap
            m_viewMinimapMenuItem.Checked = m_canvas.MinimapVisible;

            UpdateToolStripImages();
            m_canvas.UpdateScrollBars();

            Debug.WriteLine(m_canvas.Focused ? "Focused!" : "NOT FOCUSED");
        }

        private void FileRecentProject_Click(object sender, EventArgs e)
        {
            if (!CheckLoseProject())
            {
                return;
            }

            var fileName = (string)((ToolStripMenuItem)sender).Tag;
            OpenProject(fileName);
        }

        private void UpdateToolStripImages()
        {
            foreach (ToolStripItem item in m_toolStrip.Items)
            {
                if (!(item is ToolStripButton))
                    continue;

                var button = (ToolStripButton)item;
                button.BackgroundImage = button.Checked ? Properties.Resources.ToolStripBackground2 : Properties.Resources.ToolStripBackground;
            }
        }

        private void ViewResetMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.ResetZoomOrigin();
        }

        private void ViewZoomInMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.ZoomIn();
        }

        private void ViewZoomOutMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.ZoomOut();
        }

        private void ViewZoomFiftyPercentMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.ZoomFactor = 0.5f;
        }

        private void ViewZoomOneHundredPercentMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.ZoomFactor = 1.0f;
        }

        private void ViewZoomTwoHundredPercentMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.ZoomFactor = 2.0f;
        }

        private void EditRenameMenuItem_Click(object sender, EventArgs e)
        {
            if (m_canvas.HasSingleSelectedElement && m_canvas.SelectedElement.HasDialog)
            {
                m_canvas.SelectedElement.ShowDialog();
            }
        }

        private void EditIsDarkMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var element in m_canvas.SelectedElements)
            {
                if (element is Room)
                {
                    var room = (Room)element;
                    room.IsDark = !room.IsDark;
                }
            }
        }

        private void ProjectSettingsMenuItem_Click(object sender, EventArgs e)
        {
            Settings.ShowDialog();
        }

        private void ProjectResetToDefaultSettingsMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Restore default settings?\n\nThis will revert any changes to settings in this project.", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Settings.Reset();
            }
        }

        private void HelpAndSupportMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://trizbort.genstein.net/?help");
            }
            catch (Exception)
            {
                NewVersionDialog.CannotLaunchWebSite();
            }
        }

        private void CheckForUpdatesMenuItem_Click(object sender, EventArgs e)
        {
            NewVersionDialog.CheckForUpdatesAsync(this, true);
        }

        private void HelpAboutMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new AboutDialog())
            {
                dialog.ShowDialog();
            }
        }

        private void AutomapStartMenuItem_Click(object sender, EventArgs e)
        {
            using (var dialog = new AutomapDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    m_canvas.StartAutomapping(dialog.Data);
                }
            }
        }

        private void AutomapStopMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.StopAutomapping();
        }

        private void ViewMinimapMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.MinimapVisible = !m_canvas.MinimapVisible;
            Settings.ShowMiniMap = m_canvas.MinimapVisible;
        }

        private void FileMenu_DropDownOpening(object sender, EventArgs e)
        {
            // MRU list
            var existingItems = new List<ToolStripItem>();
            foreach (ToolStripItem existingItem in m_fileRecentMapsMenuItem.DropDownItems)
            {
                existingItems.Add(existingItem);
            }
            foreach (var existingItem in existingItems)
            {
                existingItem.Click -= FileRecentProject_Click;
                existingItem.Dispose();
            }
            if (Settings.RecentProjects.Count == 0)
            {
                m_fileRecentMapsMenuItem.Enabled = false;
            }
            else
            {
                m_fileRecentMapsMenuItem.Enabled = true;
                var index = 1;
                foreach (var recentProject in Settings.RecentProjects)
                {
                    var menuItem = new ToolStripMenuItem(string.Format("&{0} {1}", index++, recentProject));
                    menuItem.Tag = recentProject;
                    menuItem.Click += FileRecentProject_Click;
                    m_fileRecentMapsMenuItem.DropDownItems.Add(menuItem);
                }
            }
        }

        private void EditSelectAllMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.SelectAll();
        }

        private void EditSelectNoneMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.SelectedElement = null;
        }

        private void ViewEntireMapMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.ZoomToFit();
        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, string lpszFile);

        [DllImport("gdi32.dll")]
        private static extern int DeleteEnhMetaFile(IntPtr hemf);

        private string m_caption;
        private DateTime m_lastUpdateUITime;
        private static readonly TimeSpan IdleProcessingEveryNSeconds = TimeSpan.FromSeconds(0.2);

        private void m_editCopyMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.CopySelectedElements();
        }

        private void m_editCopyColorToolMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.CopySelectedColor();
        }

        private void m_editPasteMenuItem_Click(object sender, EventArgs e)
        {
            m_canvas.Paste(false);
        }
    }
}
