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
using System.Threading.Tasks;
using System.Windows.Forms;
using CommandLine;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Annotations;
using Trizbort.Domain;
using Trizbort.Domain.Application;
using Trizbort.Domain.AppSettings;
using Trizbort.Domain.Controllers;
using Trizbort.Domain.Elements;
using Trizbort.Domain.Enums;
using Trizbort.Domain.Misc;
using Trizbort.Export;
using Trizbort.Properties;
using Trizbort.Util;
using Settings = Trizbort.Setup.Settings;

namespace Trizbort.UI
{
  public partial class MainForm : Form
  {
    private readonly CommandController commandController;

    private static readonly TimeSpan IdleProcessingEveryNSeconds = TimeSpan.FromSeconds(0.2);
    private readonly string mCaption;
    public UI.Controls.Canvas Canvas;
    private DateTime mLastUpdateUITime;
    public bool FromCmdLine = false;

    public MainForm()
    {
      InitializeComponent();

      TrizbortApplication.MainForm = this;

      commandController = new CommandController(Canvas);

      mCaption = Text;

      System.Windows.Forms.Application.Idle += onIdle;
      mLastUpdateUITime = DateTime.MinValue;

      m_automapBar.StopClick += onMAutomapBarOnStopClick;
      Canvas.ZoomChanged += adjustZoomed;
    }

    public sealed override string Text { get { return base.Text; }
      set { base.Text = value; } }

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
      Canvas.MinimapVisible = ApplicationSettingsController.AppSettings.ShowMiniMap;
      bool projectLoaded = false;

      var args = Environment.GetCommandLineArgs();

      var ext = Parser.Default.ParseArguments<CommandLineOptions>(args);

      if (ext.Tag == ParserResultType.Parsed)
      {
        var result = (Parsed<CommandLineOptions>) ext;
        projectLoaded = commandLineActions(result.Value);
      }

      if ((ApplicationSettingsController.AppSettings.LoadLastProjectOnStart) && (!projectLoaded))
      {
        try
        {
          BeginInvoke((MethodInvoker) delegate { OpenProject(ApplicationSettingsController.AppSettings.LastProjectFileName); });
        }
        catch (Exception)
        {
          // ignored
        }
      }
      NewVersionDialog.CheckForUpdatesAsync(this, false);
    }


    private bool commandLineActions(CommandLineOptions options)
    {
      bool projectLoaded = false;

      if (options.LoadLastProject)
      {
        OpenProject(ApplicationSettingsController.AppSettings.LastProjectFileName);
        projectLoaded = true;
      }

      if (options.Transcript != null)
      {
        projectLoaded = clAutoMap(options).Result;
      }

      if (options.QuickSave != null && options.Transcript == null)
      {
        saveAsCmdLineProject(options.QuickSave);
        Project.Current.IsDirty = false;
      }


      if (options.FileName != null)
      {
        if (!projectLoaded)
        {
          OpenProject(options.FileName);
          projectLoaded = true;

          if (options.SmartSave)
          {
            smartSave(true);
          }
        }
      }

      if (!string.IsNullOrWhiteSpace(options.I6))
      {
        exportCodeCl<Inform6Exporter>(options.I6);
      }

      if (!string.IsNullOrWhiteSpace(options.I7))
      {
        exportCodeCl<Inform7Exporter>(options.I7);
      }

      if (!string.IsNullOrWhiteSpace(options.Tads))
      {
        exportCodeCl<TadsExporter>(options.Tads);
      }


      if (!string.IsNullOrWhiteSpace(options.Alan))
      {
        exportCodeCl<AlanExporter>(options.Alan);
      }

      if (!string.IsNullOrWhiteSpace(options.Hugo))
      {
        exportCodeCl<HugoExporter>(options.Hugo);
      }

      if (!string.IsNullOrWhiteSpace(options.Zil))
      {
        exportCodeCl<ZilExporter>(options.Zil);
      }

      if (!string.IsNullOrWhiteSpace(options.Quest))
      {
        exportCodeCl<QuestExporter>(options.Quest);
      }

      if (!string.IsNullOrWhiteSpace(options.QuestRooms))
      {
        exportCodeCl<QuestRoomsExporter>(options.QuestRooms);
      }

      if (options.Exit)
      {
        Close();
      }

      return projectLoaded;
    }

    

    private async Task<bool> clAutoMap(CommandLineOptions options)
    {
      bool projectLoaded = false;
      try
      {
        var cmdLineAutomap = ApplicationSettingsController.AppSettings.Automap;
        cmdLineAutomap.FileName = options.Transcript;

        await Canvas.StartAutomapping(cmdLineAutomap,true);
        Canvas.StopAutomapping();

        if (options.QuickSave != null)
        {
          saveAsCmdLineProject(options.QuickSave);
          Project.Current.IsDirty = false;
        }

        Project.Current.IsDirty = false;
        projectLoaded = true;
      }
      catch (Exception)
      {
        // ignored
      }
      Project.Current.IsDirty = false;
      return projectLoaded;
    }

    private void FileNewMenuItem_Click(object sender, EventArgs e)
    {
      if (!checkLoseProject())
        return;

      Project.Current = new Project();
      Settings.Reset();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      if (!checkLoseProject())
      {
        e.Cancel = true;
        return;
      }

      ApplicationSettingsController.AppSettings.CanvasHeight = Height - 27;
      ApplicationSettingsController.AppSettings.CanvasWidth = Width - 8;
      ApplicationSettingsController.SaveSettings();
      Canvas.StopAutomapping();

      Project.FileWatcher.Dispose();

      base.OnClosing(e);
    }

    private bool checkLoseProject()
    {
      if (Project.Current.IsDirty)
      {
        // see if the user would like to save
        var result = MessageBox.Show(this, $"Do you want to save changes to {Project.Current.Name}?", Text, MessageBoxButtons.YesNoCancel);
        switch (result)
        {
          case DialogResult.Yes:
            // user would like to save
            if (!saveProject())
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

    public void OpenProject()
    {
      if (!checkLoseProject())
        return;

      using (var dialog = new OpenFileDialog())
      {
        dialog.InitialDirectory = PathHelper.SafeGetDirectoryName(ApplicationSettingsController.AppSettings.LastProjectFileName);
        dialog.Filter = $"{Project.FilterString}|All Files|*.*||";
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          ApplicationSettingsController.AppSettings.LastProjectFileName = dialog.FileName;
          OpenProject(dialog.FileName);
        }
      }
    }

    public bool OpenProject(string fileName)
    {
      var project = new Project() {FileName = fileName};
      if (project.Load())
      {
        Project.Current = project;
        ApplicationSettingsController.AppSettings.LastProjectFileName = fileName;
        ApplicationSettingsController.AppSettings.RecentProjects.Add(fileName);
        return true;
      }

      return false;
    }

    private void aboutMap()
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
          builder.AppendLine($"by {project.Author}");
        }
        if (!string.IsNullOrEmpty(project.Description))
        {
          if (builder.Length > 0)
          {
            builder.AppendLine();
          }
          builder.AppendLine(project.Description);
        }
        MessageBox.Show(builder.ToString(), System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK);
      }
    }

    private bool saveProject()
    {
      if (!Project.Current.HasFileName)
      {
        return saveAsProject();
      }

      if (Project.Current.Save())
      {
        ApplicationSettingsController.AppSettings.RecentProjects.Add(Project.Current.FileName);
        return true;
      }
      return false;
    }

    private void saveAsCmdLineProject(string outfile)
    {
      ApplicationSettingsController.AppSettings.LastProjectFileName = outfile;
      Project.Current.FileName = outfile;
      if (Project.Current.Save())
      {
        ApplicationSettingsController.AppSettings.RecentProjects.Add(Project.Current.FileName);
        Project.Current.IsDirty = false;
      }
    }

    private bool saveAsProject()
    {
      using (var dialog = new SaveFileDialog())
      {
        if (!string.IsNullOrEmpty(Project.Current.FileName))
        {
          dialog.FileName = Project.Current.FileName;
        }
        else
        {
          dialog.InitialDirectory = PathHelper.SafeGetDirectoryName(ApplicationSettingsController.AppSettings.LastProjectFileName);
        }
        dialog.Filter = $"{Project.FilterString}|All Files|*.*||";
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          ApplicationSettingsController.AppSettings.LastProjectFileName = dialog.FileName;
          Project.Current.FileName = dialog.FileName;
          if (Project.Current.Save())
          {
            ApplicationSettingsController.AppSettings.RecentProjects.Add(Project.Current.FileName);
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
      saveProject();
    }

    private void FileSaveAsMenuItem_Click(object sender, EventArgs e)
    {
      saveAsProject();
    }

    private void smartSaveToolStripMenuItem_Click(object sender, EventArgs e)
    {
      smartSave();
    }

    private void smartSave(bool silent = false)
    {

      if (!ApplicationSettingsController.AppSettings.SaveToPDF && !ApplicationSettingsController.AppSettings.SaveToImage)
      {
        if (!silent)
          MessageBox.Show("Your settings are set to not save anything. Please check your App Settings if this is not what you want.");
        return;
      }

      bool mSaved = false;
      if (!Project.Current.HasFileName || Project.Current.IsDirty)
      {
        if (MessageBox.Show("Your project needs to be saved before we can do a SmartSave.  Would you like to save the project now?", "Save Project?", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
        {
          mSaved = saveProject();
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
          if (ApplicationSettingsController.AppSettings.SaveToPDF)
          {
            sPDFFile = exportPDF();
            if (sPDFFile == string.Empty)
            {
              MessageBox.Show("There was an error saving the PDF file during the SmartSave.  Please make sure the PDF is not already opened.", "Smart Save", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              bSaveError = true;
            }
          }
          string sImageFile = string.Empty;
          if (ApplicationSettingsController.AppSettings.SaveToImage)
          {
            sImageFile = exportImage();
            if (sImageFile == string.Empty)
            {
              MessageBox.Show("There was an error saving the Image file during the SmartSave.  Please make sure the Image is not already opened.", "Smart Save", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              bSaveError = true;
            }

          }

          if (!bSaveError)
          {
            string sText = string.Empty;
            if (ApplicationSettingsController.AppSettings.SaveToPDF)
            {
              sText += $"PDF file has been saved to {sPDFFile}";
            }

            if (ApplicationSettingsController.AppSettings.SaveToImage)
            {
              if (sText != string.Empty)
                sText += Environment.NewLine;
              sText += $"Image file has been saved to {sImageFile}";
            }

            if (!silent) MessageBox.Show(sText, "Smart Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
      ApplicationSettingsController.ShowAppDialog();
    }

    private string exportImage()
    {
      var folder = PathHelper.SafeGetDirectoryName(Project.Current.FileName);
      var fileName = PathHelper.SafeGetFilenameWithoutExtension(Project.Current.FileName);

      var extension = getExtensionForDefaultImageType();

      var imageFile = Path.Combine(folder, fileName + extension);
      try
      {
        if (!saveImage(imageFile))
          return string.Empty;
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
      switch (ApplicationSettingsController.AppSettings.DefaultImageType)
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
        dialog.InitialDirectory = PathHelper.SafeGetDirectoryName(ApplicationSettingsController.AppSettings.LastExportImageFileName);
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          try
          {
            savePDF(dialog.FileName);
          }
          catch (Exception ex)
          {
            MessageBox.Show(Program.MainForm, $"There was a problem exporting the map:\n\n{ex.Message}", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }
      }
    }

    private void savePDF(string fileName)
    {
      ApplicationSettingsController.AppSettings.LastExportImageFileName = fileName;

      var doc = new PdfDocument();
      doc.Info.Title = Project.Current.Title;
      doc.Info.Author = Project.Current.Author;
      doc.Info.Creator = System.Windows.Forms.Application.ProductName;
      doc.Info.CreationDate = DateTime.Now;
      doc.Info.Subject = Project.Current.Description;
      var page = doc.AddPage();

      var pdfBoundRectangle = Canvas.ComputeCanvasBounds(true);
      var size = pdfBoundRectangle.Size;

      page.Width = new XUnit(size.X);
      page.Height = new XUnit(size.Y);
      using (var graphics = XGraphics.FromPdfPage(page))
      {
        Canvas.Draw(graphics, true, size.X, size.Y);
      }

      var descripRooms = Project.Current.Elements.OfType<Room>().ToList().Where(p => p.HasDescription);

      foreach (var myroom in descripRooms)
      {
        XRect rect = new XRect();
        PdfTextAnnotation textAnnot = new PdfTextAnnotation
        {
          Contents = myroom.PrimaryDescription,
          Color = Color.Orange,
          Icon = PdfTextAnnotationIcon.Note
        };

        rect.Width = myroom.Width / 4; // first, decide square dimensions
        rect.Height = myroom.Height / 4;
        if (rect.Width > rect.Height)
          rect.Width = rect.Height;
        else
          rect.Height = rect.Width;

        if (myroom.Shape == RoomShape.SquareCorners) //Now, place it in the upper left or upper center based on room shape
          rect.X = myroom.X - pdfBoundRectangle.Left;
        else
          rect.X = myroom.X - pdfBoundRectangle.Left + myroom.Width/2 - rect.Width/2;
        rect.Y = pdfBoundRectangle.Height - (myroom.Y - pdfBoundRectangle.Top + rect.Height);

        textAnnot.Rectangle = new PdfRectangle(rect);
        page.Annotations.Add(textAnnot);
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
        dialog.InitialDirectory = PathHelper.SafeGetDirectoryName(ApplicationSettingsController.AppSettings.LastExportImageFileName);
        if (dialog.ShowDialog() == DialogResult.OK)
        {
          ApplicationSettingsController.AppSettings.LastExportImageFileName = Path.GetDirectoryName(dialog.FileName)+@"\";
          if (!saveImage(dialog.FileName))
          {
            MessageBox.Show("There was an error saving the image file.  Please make sure the image is not already opened.", "Export Image", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
          }
        }
      }
    }

    private bool saveImage(string fileName)
    {
      bool sReturn = true;

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

      var size = Canvas.ComputeCanvasBounds(true).Size*(ApplicationSettingsController.AppSettings.SaveAt100 ? 1.0f : Canvas.ZoomFactor);
      size.X = Numeric.Clamp(size.X, 16, 8192);
      size.Y = Numeric.Clamp(size.Y, 16, 8192);

      try
      {
        if (Equals(format, ImageFormat.Emf))
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
                  if (copy == IntPtr.Zero)
                    sReturn = false;

                  DeleteEnhMetaFile(copy);
                }
              }
              catch
              {
                sReturn = false;
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
      catch 
      {
        sReturn = false;
      }

      return sReturn;
    }

    private void FileExportInform7MenuItem_Click(object sender, EventArgs e)
    {
      var fileName = ApplicationSettingsController.AppSettings.LastExportInform7FileName;
      if (exportCode<Inform7Exporter>(ref fileName))
      {
        ApplicationSettingsController.AppSettings.LastExportInform7FileName = fileName;
      }
    }

    private void inform7ToTextToolStripMenuItem_Click(object sender, EventArgs e)
    {
      exportCode<Inform7Exporter>();
    }


    private void FileExportInform6MenuItem_Click(object sender, EventArgs e)
    {
      var fileName = ApplicationSettingsController.AppSettings.LastExportInform6FileName;
      if (exportCode<Inform6Exporter>(ref fileName))
      {
        ApplicationSettingsController.AppSettings.LastExportInform6FileName = fileName;
      }
    }

    private void inform6ToTextToolStripMenuItem_Click(object sender, EventArgs e)
    {
      exportCode<Inform6Exporter>();
    }

    private void zILToolStripMenuItem_Click(object sender, EventArgs e)
    {
      var fileName = ApplicationSettingsController.AppSettings.LastExportZilFileName;
      if (exportCode<ZilExporter>(ref fileName))
      {
        ApplicationSettingsController.AppSettings.LastExportZilFileName = fileName;
      }
    }

    private void zILToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
    {
      exportCode<ZilExporter>();
    }

    private void FileExportHugoMenuItem_Click(object sender, EventArgs e)
    {
        var fileName = ApplicationSettingsController.AppSettings.LastExportHugoFileName;
        if (exportCode<HugoExporter>(ref fileName))
        {
          ApplicationSettingsController.AppSettings.LastExportHugoFileName = fileName;
        }
    }

    private void hugoToTextToolStripMenuItem_Click(object sender, EventArgs e)
    {
        exportCode<HugoExporter>();
    }

    private void FileExportTadsMenuItem_Click(object sender, EventArgs e)
    {
      var fileName = ApplicationSettingsController.AppSettings.LastExportTadsFileName;
      if (exportCode<TadsExporter>(ref fileName))
      {
        ApplicationSettingsController.AppSettings.LastExportTadsFileName = fileName;
      }
    }

    private void FileExportAlanMenuItem_Click(object sender, EventArgs e)
    {
        var fileName = ApplicationSettingsController.AppSettings.LastExportAlanFileName;
        if (exportCode<AlanExporter>(ref fileName))
        {
          ApplicationSettingsController.AppSettings.LastExportAlanFileName = fileName;
        }
    }

    private void alanToTextToolStripMenuItem_Click(object sender, EventArgs e)
    {
        exportCode<AlanExporter>();
    }

    private void FileExportTADSMenuItem_Click(object sender, EventArgs e)
    {
      var fileName = ApplicationSettingsController.AppSettings.LastExportTadsFileName;
      if (exportCode<TadsExporter>(ref fileName))
      {
        ApplicationSettingsController.AppSettings.LastExportTadsFileName = fileName;
      }
    }

    private void tADSToTextToolStripMenuItem_Click(object sender, EventArgs e)
    {
      exportCode<TadsExporter>();
    }

    private void FileExportQuestMenuItem_Click(object sender, EventArgs e)
    {
        var fileName = ApplicationSettingsController.AppSettings.LastExportQuestFileName;
        if (exportCode<QuestExporter>(ref fileName))
        {
            ApplicationSettingsController.AppSettings.LastExportQuestFileName = fileName;
        }
    }

    private void questToTextToolStripMenuItem_Click(object sender, EventArgs e)
    {
        exportCode<QuestExporter>();
    }

    private void FileExportQuestRoomsMenuItem_Click(object sender, EventArgs e)
    {
        var fileName = ApplicationSettingsController.AppSettings.LastExportQuestRoomsFileName;
        if (exportCode<QuestRoomsExporter>(ref fileName))
        {
            ApplicationSettingsController.AppSettings.LastExportQuestRoomsFileName = fileName;
        }
    }

    private void questRoomsToTextToolStripMenuItem_Click(object sender, EventArgs e)
    {
        exportCode<QuestRoomsExporter>();
    }

    private void exportCodeCl<T>(string exportFile) where T : CodeExporter, new()
    {
      using (var exporter = new T())
      {
        exporter.Export(exportFile);
      }
    }

    private void exportCode<T>() where T: CodeExporter, new()
    {
      using (var exporter = new T())
      {
        string s = exporter.Export();
        Clipboard.SetText(s,TextDataFormat.Text);
      }
    }

    private bool exportCode<T>(ref string lastExportFileName) where T : CodeExporter, new()
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
            filterString += $"{filter.Key}|*{filter.Value}";
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
              MessageBox.Show(Program.MainForm, $"There was a problem exporting the map:\n\n{ex.Message}", System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        commandController.ShowElementProperties(Canvas.SelectedElement);

      }
    }

    private void PlainLinesMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.ApplyNewPlainConnectionSettings();
    }

    private void ToggleDottedLines_Click(object sender, EventArgs e)
    {
      commandController.ToggleConnectionStyle(Canvas.NewConnectionStyle);
    }

    private void ToggleDirectionalLines_Click(object sender, EventArgs e)
    {
      commandController.ToggleConnectionFlow(Canvas.NewConnectionFlow);
    }

    private void UpLinesMenuItem_Click(object sender, EventArgs e)
    {
      commandController.SetConnectionLabel(ConnectionLabel.Up);
    }

    private void DownLinesMenuItem_Click(object sender, EventArgs e)
    {
      commandController.SetConnectionLabel(ConnectionLabel.Down);
    }

    private void InLinesMenuItem_Click(object sender, EventArgs e)
    {
      commandController.SetConnectionLabel(ConnectionLabel.In);
    }

    private void OutLinesMenuItem_Click(object sender, EventArgs e)
    {
      commandController.SetConnectionLabel(ConnectionLabel.Out);
    }

    private void ReverseLineMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.ReverseLineDirection();
    }

    private void onIdle(object sender, EventArgs e)
    {
      var now = DateTime.Now;
      if (now - mLastUpdateUITime > IdleProcessingEveryNSeconds)
      {
        mLastUpdateUITime = now;
        updateCommandUI();
      }
    }

    private void updateCommandUI()
    {
      // caption
      Text = $"{(ApplicationSettingsController.AppSettings.ShowFullPathInTitleBar && !string.IsNullOrEmpty(Project.Current.FileName) ? Project.Current.FileName : Project.Current.Name)}{(Project.Current.IsDirty ? "*" : string.Empty)} - {mCaption} - {System.Windows.Forms.Application.ProductVersion}";

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
      m_editSelectNoneMenuItem.Enabled = hasSelectedElement;
      m_editSelectAllMenuItem.Enabled = Canvas.SelectedElementCount < Project.Current.Elements.Count;
      m_editCopyMenuItem.Enabled = Canvas.SelectedElement != null;
      m_editCopyColorToolMenuItem.Enabled = Canvas.HasSingleSelectedElement && (Canvas.SelectedElement is Room);
      m_editPasteMenuItem.Enabled = (!string.IsNullOrEmpty(Clipboard.GetText()) && ((Clipboard.GetText().Replace("\r\n", "|").Split('|')[0] == "Elements") || (Clipboard.GetText().Replace("\r\n", "|").Split('|')[0] == "Colors")));
      if (Canvas.HasSingleSelectedElement) //Allow flipping light in all rooms if 1+ are selected. Issue #138 flicker
        m_editIsDarkMenuItem.Enabled = Canvas.HasSingleSelectedElement && (Canvas.SelectedElement is Room);
      else
        m_editIsDarkMenuItem.Enabled = hasSelectedElement;
      m_editIsDarkMenuItem.Checked = Canvas.HasSingleSelectedElement && (Canvas.SelectedElement is Room) && ((Room)Canvas.SelectedElement).IsDark;
      m_editRenameMenuItem.Enabled = Canvas.HasSingleSelectedElement && (Canvas.SelectedElement is Room);
      joinRoomsToolStripMenuItem.Enabled = Canvas.SelectedRooms.Count == 2 && !Project.Current.AreRoomsConnected(Canvas.SelectedRooms);
      swapObjectsToolStripMenuItem.Enabled = Canvas.SelectedRooms.Count == 2;
      swapNamesToolStripMenuItem.Enabled = Canvas.SelectedRooms.Count == 2;
      swapFormatsFillsToolStripMenuItem.Enabled = Canvas.SelectedRooms.Count == 2;
      swapRegionsToolStripMenuItem.Enabled = Canvas.SelectedRooms.Count == 2;

      startRoomToolStripMenuItem.Enabled = Canvas.HasSingleSelectedElement && Canvas.SelectedElement is Room;
      startRoomToolStripMenuItem.Checked = Canvas.HasSingleSelectedElement && Canvas.SelectedElement is Room && ((Room)Canvas.SelectedElement).IsStartRoom;
      endRoomToolStripMenuItem.Enabled = Canvas.SelectedElement is Room;
      endRoomToolStripMenuItem.Checked = Canvas.SelectedElement is Room && ((Room)Canvas.SelectedElement).IsEndRoom;


      roomsMustHaveUniqueNamesToolStripMenuItem.Checked = Project.Current.MustHaveUniqueNames;
      roomsMustHaveADescriptionToolStripMenuItem.Checked = Project.Current.MustHaveDescription;
      roomsMustHaveASubtitleToolStripMenuItem.Checked = Project.Current.MustHaveSubtitle;
      roomsMustNotHaveADanglingConnectionToolStripMenuItem.Checked = Project.Current.MustHaveNoDanglingConnectors;
      
      m_editChangeRegionMenuItem.Enabled = (Canvas.SelectedRooms.Any() && Settings.Regions.Count > 1 );
      handDrawnToolStripMenuItem.Enabled = (Canvas.SelectedRooms.Any());
      ellipseToolStripMenuItem.Enabled = (Canvas.SelectedRooms.Any());
      roundedEdgesToolStripMenuItem.Enabled = (Canvas.SelectedRooms.Any());
      octagonalEdgesToolStripMenuItem.Enabled = (Canvas.SelectedRooms.Any());
      m_reverseLineMenuItem.Enabled = Canvas.HasSelectedElement<Connection>();

      // automapping
      m_automapStartMenuItem.Enabled = !Canvas.IsAutomapping;
      m_automapStopMenuItem.Enabled = Canvas.IsAutomapping;
      m_automapBar.Visible = Canvas.IsAutomapping;
      m_automapBar.Status = Canvas.AutomappingStatus;

      // minimap
      m_viewMinimapMenuItem.Checked = Canvas.MinimapVisible;

      updateToolStripImages();
      Canvas.UpdateScrollBars();
    }

    private void FileRecentProject_Click(object sender, EventArgs e)
    {
      if (!checkLoseProject())
      {
        return;
      }

      var fileName = (string) ((ToolStripMenuItem) sender).Tag;
      OpenProject(fileName);
    }

    private void updateToolStripImages()
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

    private void ViewZoomMiniIn_Click(object sender, EventArgs e)
    {
      Canvas.ZoomInMicro();
    }

    private void ViewZoomMiniOut_Click(object sender, EventArgs e)
    {
      Canvas.ZoomOutMicro();
    }

    private void EditRenameMenuItem_Click(object sender, EventArgs e)
    {
      if (Canvas.HasSingleSelectedElement && Canvas.SelectedElement.HasDialog)
      {
        commandController.ShowElementProperties(Canvas.SelectedElement);
      }
    }

    private void EditIsDarkMenuItem_Click(object sender, EventArgs e)
    {
      commandController.SetRoomLighting(LightingActionType.Toggle);
    }

    private void ProjectSettingsMenuItem_Click(object sender, EventArgs e)
    {
      Settings.ShowMapDialog();
      Canvas.Refresh();
    }

    private void ProjectResetToDefaultSettingsMenuItem_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Restore default settings?\n\nThis will revert any changes to settings in this project.", System.Windows.Forms.Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
      {
        Settings.Reset();
      }
    }

    private void HelpAndSupportMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        Process.Start("http://www.trizbort.com/Docs/index.shtml");
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
      ApplicationSettingsController.AppSettings.ShowMiniMap = Canvas.MinimapVisible;
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
        m_fileExportAlanMenuItem.Enabled = true;
        m_fileExportHugoMenuItem.Enabled = true;
        m_fileExportInform7MenuItem.Enabled = true;
        m_fileExportInform6MenuItem.Enabled = true;
        m_fileExportTADSMenuItem.Enabled = true;
        zILToolStripMenuItem.Enabled = true;
      }
      else
      {
        m_fileExportAlanMenuItem.Enabled = false;
        m_fileExportHugoMenuItem.Enabled = false;
        m_fileExportInform7MenuItem.Enabled = false;
        m_fileExportInform6MenuItem.Enabled = false;
        m_fileExportTADSMenuItem.Enabled = false;
        zILToolStripMenuItem.Enabled = false;
      }
    }

    private void setupMRUMenu()
    {
      var existingItems = m_fileRecentMapsMenuItem.DropDownItems.Cast<ToolStripItem>().ToList();
      foreach (var existingItem in existingItems)
      {
        existingItem.Click -= FileRecentProject_Click;
        existingItem.Dispose();
      }
      if (ApplicationSettingsController.AppSettings.RecentProjects.Count == 0)
      {
        m_fileRecentMapsMenuItem.Enabled = false;
      }
      else
      {
        m_fileRecentMapsMenuItem.Enabled = true;
        var index = 1;
        var removedFiles = new List<string>();
        foreach (var recentProject in ApplicationSettingsController.AppSettings.RecentProjects)
        {
          if (File.Exists(recentProject))
          {
            var menuItem = new ToolStripMenuItem($"&{index++} {recentProject}") {Tag = recentProject};
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
          removedFiles.ForEach(p => ApplicationSettingsController.AppSettings.RecentProjects.Remove(p));
        }
      }
    }

    private void EditSelectAllMenuItem_Click(object sender, EventArgs e)
    {
      commandController.Select(SelectTypes.All);
    }

    private void EditSelectNoneMenuItem_Click(object sender, EventArgs e)
    {
      commandController.Select(SelectTypes.None);
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

    private void mapStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      var frm = new MapStatisticsView();
      frm.ShowDialog();
    }

    private void mapStatisticsExportToolStripMenuItem_Click(object sender, EventArgs e)
    {
      var frm = new MapStatisticsView();
      frm.MapStatisticsView_Export(sender, e);
    }

    private void selectAllRoomsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      commandController.Select(SelectTypes.Rooms);
    }

    private void selectedUnconnectedRoomsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      commandController.Select(SelectTypes.UnconnectedRooms);
    }

    private void selectAllConnectionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      commandController.Select(SelectTypes.Connections);
    }

    private void selectDanglingConnectionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      commandController.Select(SelectTypes.DanglingConnections);
    }

    private void selectSelfLoopingConnectionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      commandController.Select(SelectTypes.SelfLoopingConnections);
    }

    private void selectRoomsWObjectsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      commandController.Select(SelectTypes.RoomsWithObjects);
    }

    private void selectRoomsWoObjectsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      commandController.Select(SelectTypes.RoomsWithOutObjects);
    }

    private void toggleTextToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.ToggleText();
    }

    private void handDrawnToolStripMenuItem_Click(object sender, EventArgs e)
    {
      foreach (var room in Canvas.SelectedRooms)
      {
        room.Shape = RoomShape.SquareCorners;
      }
      Invalidate();
    }

    private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
    {
      foreach (var room in Canvas.SelectedRooms)
      {
        room.Shape = RoomShape.Ellipse;
      }
      Invalidate();
    }

    private void roundedEdgesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      foreach (var room in Canvas.SelectedRooms)
      {
        room.Shape = RoomShape.RoundedCorners;
      }
      Invalidate();
    }

    private void octagonalEdgesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      foreach (var room in Canvas.SelectedRooms)
      {
        room.Shape = RoomShape.Octagonal;
      }
      Invalidate();
    }

    private void joinRoomsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.JoinSelectedRooms(Canvas.SelectedRooms.First(), Canvas.SelectedRooms.Last());
    }

    private void swapObjectsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.SwapRooms();
    }

    private void swapNamesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.SwapRoomNames();
    }

    private void swapFormatsFillsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.SwapRoomFill();
    }

    private void swapRegionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Canvas.SwapRoomRegions();
    }

    private void makeRoomDarkToolStripMenuItem_Click(object sender, EventArgs e)
    {
      commandController.SetRoomLighting(LightingActionType.ForceDark);
    }

    private void makeRoomLightToolStripMenuItem_Click(object sender, EventArgs e)
    {
      commandController.SetRoomLighting(LightingActionType.ForceLight);
    }

    private void roomsMustHaveUniqueNamesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      commandController.SetValidation(ValidationType.RoomUniqueName);
      Project.Current.Canvas.Invalidate();
    }

    private void roomsMustHaveADescriptionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      commandController.SetValidation(ValidationType.RoomDescription);
      Project.Current.Canvas.Invalidate();
    }

    private void roomsMustHaveASubtitleToolStripMenuItem_Click(object sender, EventArgs e)
    {
      commandController.SetValidation(ValidationType.RoomSubTitle);
      Project.Current.Canvas.Invalidate();
    }

    private void roomsMustNotHaveADanglingConnectionToolStripMenuItem_Click(object sender, EventArgs e)
    {
      commandController.SetValidation(ValidationType.RoomDanglingConnection);
      Project.Current.Canvas.Invalidate();
    }

    private void startRoomToolStripMenuItem_Click(object sender, EventArgs e)
    {
      commandController.SetStartRoom();
    }

    private void endRoomToolStripMenuItem_Click(object sender, EventArgs e)
    {
      commandController.SetEndRoom();
    }
  }
}