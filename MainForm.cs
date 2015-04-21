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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Trizbort.Export;
using Trizbort.Properties;

namespace Trizbort
{
  internal partial class MainForm : Form
  {
    private static readonly TimeSpan IdleProcessingEveryNSeconds = TimeSpan.FromSeconds(0.2);
    private readonly string m_caption;
    public Canvas Canvas;
    private DateTime m_lastUpdateUITime;

    public MainForm()
    {
      InitializeComponent();

      m_caption = Text;

      Application.Idle += OnIdle;
      m_lastUpdateUITime = DateTime.MinValue;

      m_automapBar.StopClick += onMAutomapBarOnStopClick;
      Canvas.ZoomChanged += adjustZoomed;
    }

    private void adjustZoomed(object sender, EventArgs e)
    {
      txtZoom.Value = (int)(Canvas.ZoomFactor*100.0f);
    }

    private void onMAutomapBarOnStopClick(object sender, EventArgs e)
    {
      Canvas.StopAutomapping();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      Canvas.MinimapVisible = Settings.ShowMiniMap;

      var args = Environment.GetCommandLineArgs();
      if (args.Length > 1 && File.Exists(args[1]))
      {
        try
        {
          BeginInvoke((MethodInvoker) delegate { OpenProject(args[1]); });
        }
        catch (Exception)
        {
        }
      }
      NewVersionDialog.CheckForUpdatesAsync(this, false);
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
      Canvas.StopAutomapping();

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

    private void smartSaveToolStripMenuItem_Click(object sender, EventArgs e)
    {
      smartSave();
    }

    private void smartSave()
    {

      if (!Settings.SaveToPDF && !Settings.SaveToImage)
      {
        MessageBox.Show("Your settings are set to not save anything. Please check your App Settings if this is not what you want.");
        return;
      }

      bool mSaved = false;
      if (!Project.Current.HasFileName || Project.Current.IsDirty)
      {
        if (MessageBox.Show("Your project needs to be saved before we can do a SmartSave.  Would you like to save the project now?", "Save Project?", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
        {
          SaveProject();
          mSaved = true;
        }
      }
      else
      {
        mSaved = true;
      }


      if (mSaved)
      {
        if (Project.Current.HasFileName)
        {
          bool bSaveError = false;
          string sPDFFile = string.Empty;
          if (Settings.SaveToPDF)
          {
            sPDFFile = exportPDF();
            if (sPDFFile == string.Empty)
            {
              MessageBox.Show(string.Format("There was an error saving the PDF file during the SmartSave.  Please make sure the PDF is not already opened."), "Smart Save", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              bSaveError = true;
            }
          }
          string sImageFile = string.Empty;
          if (Settings.SaveToImage)
          {
            sImageFile = exportImage();
            if (sImageFile == string.Empty)
            {
              MessageBox.Show(string.Format("There was an error saving the Image file during the SmartSave.  Please make sure the Image is not already opened."), "Smart Save", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              bSaveError = true;
            }

          }

          if (!bSaveError)
          {
            string sText = string.Empty;
            if (Settings.SaveToPDF)
            {
              sText += string.Format("PDF file has been saved to {0}", sPDFFile);
            }

            if (Settings.SaveToImage)
            {
              if (sText != string.Empty)
                sText += Environment.NewLine;
              sText += string.Format("Image file has been saved to {0}", sImageFile);
            }

            MessageBox.Show(sText, "Smart Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
        }
      }
      else
      {
        MessageBox.Show("No files have been saved during the SmartSave.");
      }
    }

    private void appSettingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Settings.ShowAppDialog();
    }

    private string exportImage()
    {
      var folder = PathHelper.SafeGetDirectoryName(Project.Current.FileName);
      var fileName = PathHelper.SafeGetFilenameWithoutExtension(Project.Current.FileName);

      var extension = getExtensionForDefaultImageType();

      var imageFile = Path.Combine(folder, fileName + extension);
      try
      {
        saveImage(imageFile);
      }
      catch (Exception)
      {
        return string.Empty;
      }

      return imageFile;
    }

    private static string getExtensionForDefaultImageType()
    {
      string extension = ".png";
      switch (Settings.DefaultImageType)
      {
        case 0:
          extension = ".png";
          break;
        case 1:
          extension = ".jpg";
          break;
        case 2:
          extension = ".bmp";
          break;
        case 3:
          extension = ".emf";
          break;
      }
      return extension;
    }

    private string exportPDF()
    {
      var folder = PathHelper.SafeGetDirectoryName(Project.Current.FileName);
      var fileName = PathHelper.SafeGetFilenameWithoutExtension(Project.Current.FileName);
      var pdfFile = Path.Combine(folder, fileName + ".pdf");
      try
      {
        savePDF(pdfFile);
      }
      catch (Exception)
      {
        return string.Empty;
      }
      return pdfFile;
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
            savePDF(dialog.FileName);
          }
          catch (Exception ex)
          {
            MessageBox.Show(Program.MainForm, string.Format("There was a problem exporting the map:\n\n{0}", ex.Message), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }
      }
    }

    private void savePDF(string fileName)
    {
      Settings.LastExportImageFileName = fileName;

      var doc = new PdfDocument();
      doc.Info.Title = Project.Current.Title;
      doc.Info.Author = Project.Current.Author;
      doc.Info.Creator = Application.ProductName;
      doc.Info.CreationDate = DateTime.Now;
      doc.Info.Subject = Project.Current.Description;
      var page = doc.AddPage();

      var size = Canvas.ComputeCanvasBounds(true).Size;
      page.Width = new XUnit(size.X);
      page.Height = new XUnit(size.Y);
      using (var graphics = XGraphics.FromPdfPage(page))
      {
        Canvas.Draw(graphics, true, size.X, size.Y);
      }

      doc.Save(fileName);
    }

    private void FileExportImageMenuItem_Click(object sender, EventArgs e)
    {
      using (var dialog = new SaveFileDialog())
      {
        dialog.Filter = "PNG Images|*.png|JPEG Images|*.jpg|BMP Images|*.bmp|Enhanced Metafiles (EMF)|*.emf|All Files|*.*||";
        dialog.Title = "Export Image";
        dialog.DefaultExt = getExtensionForDefaultImageType();
        dialog.InitialDirectory = PathHelper.SafeGetDirectoryName(Settings.LastExportImageFileName);
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          Settings.LastExportImageFileName = Path.GetDirectoryName(dialog.FileName)+@"\";
          saveImage(dialog.FileName);
        }
      }
    }

    private void saveImage(string fileName)
    {
      var format = ImageFormat.Png;
      var ext = Path.GetExtension(fileName);
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

      var size = Canvas.ComputeCanvasBounds(true).Size*(Settings.SaveAt100 ? 1.0f : Canvas.ZoomFactor);
      size.X = Numeric.Clamp(size.X, 16, 8192);
      size.Y = Numeric.Clamp(size.Y, 16, 8192);

      try
      {
        if (format == ImageFormat.Emf)
        {
          // export as a metafile
          using (var nativeGraphics = Graphics.FromHwnd(Canvas.Handle))
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
                      Canvas.Draw(graphics, true, size.X, size.Y);
                    }
                  }
                  var handle = metafile.GetHenhmetafile();
                  var copy = CopyEnhMetaFile(handle, fileName);
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
          using (var bitmap = new Bitmap((int) Math.Ceiling(size.X), (int) Math.Ceiling(size.Y)))
          {
            using (var imageGraphics = Graphics.FromImage(bitmap))
            {
              using (var graphics = XGraphics.FromGraphics(imageGraphics, new XSize(size.X, size.Y)))
              {
                Canvas.Draw(graphics, true, size.X, size.Y);
              }
            }
            bitmap.Save(fileName, format);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show(Program.MainForm, string.Format("There was a problem exporting the map:\n\n{0}", ex.Message),
          Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
      Canvas.AddRoom(false);
    }

    private void EditDeleteMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.DeleteSelection();
    }

    private void EditPropertiesMenuItem_Click(object sender, EventArgs e)
    {
      if (Canvas.HasSingleSelectedElement && Canvas.SelectedElement.HasDialog)
      {
        Canvas.SelectedElement.ShowDialog();
      }
    }

    private void PlainLinesMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.ApplyNewPlainConnectionSettings();
    }

    private void ToggleDottedLines_Click(object sender, EventArgs e)
    {
      switch (Canvas.NewConnectionStyle)
      {
        case ConnectionStyle.Solid:
          Canvas.NewConnectionStyle = ConnectionStyle.Dashed;
          break;
        case ConnectionStyle.Dashed:
          Canvas.NewConnectionStyle = ConnectionStyle.Solid;
          break;
      }
      Canvas.ApplyConnectionStyle(Canvas.NewConnectionStyle);
    }

    private void ToggleDirectionalLines_Click(object sender, EventArgs e)
    {
      switch (Canvas.NewConnectionFlow)
      {
        case ConnectionFlow.TwoWay:
          Canvas.NewConnectionFlow = ConnectionFlow.OneWay;
          break;
        case ConnectionFlow.OneWay:
          Canvas.NewConnectionFlow = ConnectionFlow.TwoWay;
          break;
      }
      Canvas.ApplyConnectionFlow(Canvas.NewConnectionFlow);
    }

    private void UpLinesMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.NewConnectionLabel = ConnectionLabel.Up;
      Canvas.ApplyConnectionLabel(Canvas.NewConnectionLabel);
    }

    private void DownLinesMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.NewConnectionLabel = ConnectionLabel.Down;
      Canvas.ApplyConnectionLabel(Canvas.NewConnectionLabel);
    }

    private void InLinesMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.NewConnectionLabel = ConnectionLabel.In;
      Canvas.ApplyConnectionLabel(Canvas.NewConnectionLabel);
    }

    private void OutLinesMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.NewConnectionLabel = ConnectionLabel.Out;
      Canvas.ApplyConnectionLabel(Canvas.NewConnectionLabel);
    }

    private void ReverseLineMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.ReverseLineDirection();
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
      Text = string.Format("{0}{1} - {2} - {3}", Project.Current.Name, Project.Current.IsDirty ? "*" : string.Empty, m_caption, Application.ProductVersion);

      // line drawing options
      m_toggleDottedLinesButton.Checked = Canvas.NewConnectionStyle == ConnectionStyle.Dashed;
      m_toggleDottedLinesMenuItem.Checked = m_toggleDottedLinesButton.Checked;
      m_toggleDirectionalLinesButton.Checked = Canvas.NewConnectionFlow == ConnectionFlow.OneWay;
      m_toggleDirectionalLinesMenuItem.Checked = m_toggleDirectionalLinesButton.Checked;
      m_plainLinesMenuItem.Checked = !m_toggleDirectionalLinesMenuItem.Checked && !m_toggleDottedLinesMenuItem.Checked && Canvas.NewConnectionLabel == ConnectionLabel.None;
      m_upLinesMenuItem.Checked = Canvas.NewConnectionLabel == ConnectionLabel.Up;
      m_downLinesMenuItem.Checked = Canvas.NewConnectionLabel == ConnectionLabel.Down;
      m_inLinesMenuItem.Checked = Canvas.NewConnectionLabel == ConnectionLabel.In;
      m_outLinesMenuItem.Checked = Canvas.NewConnectionLabel == ConnectionLabel.Out;

      // selection-specific commands
      var hasSelectedElement = Canvas.SelectedElement != null;
      m_editDeleteMenuItem.Enabled = hasSelectedElement;
      m_editPropertiesMenuItem.Enabled = Canvas.HasSingleSelectedElement;
      m_editIsDarkMenuItem.Enabled = hasSelectedElement;
      m_editSelectNoneMenuItem.Enabled = hasSelectedElement;
      m_editSelectAllMenuItem.Enabled = Canvas.SelectedElementCount < Project.Current.Elements.Count;
      m_editCopyMenuItem.Enabled = Canvas.SelectedElement != null;
      m_editCopyColorToolMenuItem.Enabled = Canvas.HasSingleSelectedElement && (Canvas.SelectedElement is Room);
      m_editPasteMenuItem.Enabled = (!String.IsNullOrEmpty(Clipboard.GetText())) && ((Clipboard.GetText().Replace("\r\n", "|").Split('|')[0] == "Elements") || (Clipboard.GetText().Replace("\r\n", "|").Split('|')[0] == "Colors"));
      m_editRenameMenuItem.Enabled = Canvas.HasSingleSelectedElement && (Canvas.SelectedElement is Room);
      m_editIsDarkMenuItem.Enabled = Canvas.HasSingleSelectedElement && (Canvas.SelectedElement is Room);
      m_editIsDarkMenuItem.Checked = Canvas.HasSingleSelectedElement && (Canvas.SelectedElement is Room) && ((Room) Canvas.SelectedElement).IsDark;
      m_reverseLineMenuItem.Enabled = Canvas.HasSelectedElement<Connection>();

      // automapping
      m_automapStartMenuItem.Enabled = !Canvas.IsAutomapping;
      m_automapStopMenuItem.Enabled = Canvas.IsAutomapping;
      m_automapBar.Visible = Canvas.IsAutomapping;
      m_automapBar.Status = Canvas.AutomappingStatus;

      // minimap
      m_viewMinimapMenuItem.Checked = Canvas.MinimapVisible;

      UpdateToolStripImages();
      Canvas.UpdateScrollBars();

      Debug.WriteLine(Canvas.Focused ? "Focused!" : "NOT FOCUSED");
    }

    private void FileRecentProject_Click(object sender, EventArgs e)
    {
      if (!CheckLoseProject())
      {
        return;
      }

      var fileName = (string) ((ToolStripMenuItem) sender).Tag;
      OpenProject(fileName);
    }

    private void UpdateToolStripImages()
    {
      foreach (ToolStripItem item in m_toolStrip.Items)
      {
        if (!(item is ToolStripButton))
          continue;

        var button = (ToolStripButton) item;
        button.BackgroundImage = button.Checked ? Resources.ToolStripBackground2 : Resources.ToolStripBackground;
      }
    }

    private void ViewResetMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.ResetZoomOrigin();
    }

    private void ViewZoomInMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.ZoomIn();
    }

    private void ViewZoomOutMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.ZoomOut();
    }

    private void ViewZoomFiftyPercentMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.ZoomFactor = 0.5f;
    }

    private void ViewZoomOneHundredPercentMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.ZoomFactor = 1.0f;
    }

    private void ViewZoomTwoHundredPercentMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.ZoomFactor = 2.0f;
    }

    private void EditRenameMenuItem_Click(object sender, EventArgs e)
    {
      if (Canvas.HasSingleSelectedElement && Canvas.SelectedElement.HasDialog)
      {
        Canvas.SelectedElement.ShowDialog();
      }
    }

    private void EditIsDarkMenuItem_Click(object sender, EventArgs e)
    {
      foreach (var element in Canvas.SelectedElements)
      {
        if (element is Room)
        {
          var room = (Room) element;
          room.IsDark = !room.IsDark;
        }
      }
    }

    private void ProjectSettingsMenuItem_Click(object sender, EventArgs e)
    {
      Settings.ShowMapDialog();
      Canvas.Refresh();
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
          Canvas.StartAutomapping(dialog.Data);
        }
      }
    }

    private void AutomapStopMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.StopAutomapping();
    }

    private void ViewMinimapMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.MinimapVisible = !Canvas.MinimapVisible;
      Settings.ShowMiniMap = Canvas.MinimapVisible;
    }

    private void FileMenu_DropDownOpening(object sender, EventArgs e)
    {
      setupMRUMenu();

      setupExportMenu();
    }

    private void setupExportMenu()
    {
      if (Project.Current.Elements.OfType<Room>().Any())
      {
        m_fileExportInform7MenuItem.Enabled = true;
        m_fileExportInform6MenuItem.Enabled = true;
        m_fileExportTADSMenuItem.Enabled = true;
      }
      else
      {
        m_fileExportInform7MenuItem.Enabled = false;
        m_fileExportInform6MenuItem.Enabled = false;
        m_fileExportTADSMenuItem.Enabled = false;
      }
    }

    private void setupMRUMenu()
    {
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
        var removedFiles = new List<string>();
        foreach (var recentProject in Settings.RecentProjects)
        {
          if (File.Exists(recentProject))
          {
            var menuItem = new ToolStripMenuItem(string.Format("&{0} {1}", index++, recentProject));
            menuItem.Tag = recentProject;
            menuItem.Click += FileRecentProject_Click;
            m_fileRecentMapsMenuItem.DropDownItems.Add(menuItem);
          }
          else
          {
            removedFiles.Add(recentProject);
          }
        }
        if (removedFiles.Any())
        {
          removedFiles.ForEach(p => Settings.RecentProjects.Remove(p));
        }
      }
    }

    private void EditSelectAllMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.SelectAll();
    }

    private void EditSelectNoneMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.SelectedElement = null;
    }

    private void ViewEntireMapMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.ZoomToFit();
    }

    [DllImport("gdi32.dll")]
    private static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, string lpszFile);

    [DllImport("gdi32.dll")]
    private static extern int DeleteEnhMetaFile(IntPtr hemf);

    private void m_editCopyMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.CopySelectedElements();
    }

    private void m_editCopyColorToolMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.CopySelectedColor();
    }

    private void m_editPasteMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.Paste(false);
    }

    private void m_editChangeRegionMenuItem_Click(object sender, EventArgs e)
    {
      if (Canvas.HasSingleSelectedElement && Canvas.SelectedElement.HasDialog)
      {
        var element = Canvas.SelectedElement as Room;
        if (element != null)
        {
          var room = element;
          room.ShowDialog(PropertiesStartType.Region);
          
        }
      }
    }

    private void txtZoom_ValueChanged(object sender, EventArgs e)
    {
      if (txtZoom.Value <= 0) txtZoom.Value = 10;
      Canvas.ChangeZoom((float)Convert.ToDouble(txtZoom.Value) / 100.0f);
    }
  }
}