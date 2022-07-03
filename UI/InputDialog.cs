/*
 * Author: Tony Brix, http://tonybrix.info
 * License: MIT
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Trizbort.UI; 

public enum InputBoxButtons
{
  OK,
  OKCancel,
  YesNo,
  YesNoCancel,
  Save,
  SaveCancel
}

public enum InputBoxResult
{
  Cancel,
  OK,
  Yes,
  No,
  Save
}

public struct InputDialogItem
{
  public string Label;
  public string Text;
  public bool IsPassword;

  public InputDialogItem(string label)
  {
    Label = label;
    Text = "";
    IsPassword = false;
  }
  public InputDialogItem(string label, string text)
  {
    Label = label;
    Text = text;
    IsPassword = false;
  }
  public InputDialogItem(string label, bool isPassword)
  {
    Label = label;
    Text = "";
    IsPassword = isPassword;
  }
  public InputDialogItem(string label, string text, bool isPassword)
  {
    Label = label;
    Text = text;
    IsPassword = isPassword;
  }
}


public sealed class InputDialog
{
  private InputDialog(dialogForm dialog)
  {
    Result = dialog.InputResult;
    Items = new Dictionary<string, string>();
    for (int i = 0; i < dialog.label.Length; i++)
    {
      Items.Add(dialog.label[i].Text, dialog.textBox[i].Text);
    }
  }

  public static InputDialog Show(string title, string label)
  {
    dialogForm dialog = new dialogForm(title, new InputDialogItem[] { new InputDialogItem(label) }, InputBoxButtons.OK);
    dialog.ShowDialog();
    return new InputDialog(dialog);
  }

  public static InputDialog Show(string title, string label, InputBoxButtons buttons)
  {
    dialogForm dialog = new dialogForm(title, new InputDialogItem[] { new InputDialogItem(label) }, buttons);
    dialog.ShowDialog();
    return new InputDialog(dialog);
  }

  public static InputDialog Show(string title, string label, string text)
  {
    dialogForm dialog = new dialogForm(title, new InputDialogItem[] { new InputDialogItem(label, text) }, InputBoxButtons.OK);
    dialog.ShowDialog();
    return new InputDialog(dialog);
  }

  public static InputDialog Show(string title, string label, string text, InputBoxButtons buttons)
  {
    dialogForm dialog = new dialogForm(title, new InputDialogItem[] { new InputDialogItem(label, text) }, buttons);
    dialog.ShowDialog();
    return new InputDialog(dialog);
  }

  public static InputDialog Show(string title, string[] labels)
  {
    InputDialogItem[] items = new InputDialogItem[labels.Length];
    for (int i = 0; i < labels.Length; i++)
    {
      items[i] = new InputDialogItem(labels[i]);
    }

    dialogForm dialog = new dialogForm(title, items, InputBoxButtons.OK);
    dialog.ShowDialog();
    return new InputDialog(dialog);
  }

  public static InputDialog Show(string title, string[] labels, InputBoxButtons buttons)
  {
    InputDialogItem[] items = new InputDialogItem[labels.Length];
    for (int i = 0; i < labels.Length; i++)
    {
      items[i] = new InputDialogItem(labels[i]);
    }

    dialogForm dialog = new dialogForm(title, items, buttons);
    dialog.ShowDialog();
    return new InputDialog(dialog);
  }

  public static InputDialog Show(string title, InputDialogItem item)
  {
    dialogForm dialog = new dialogForm(title, new InputDialogItem[] { item }, InputBoxButtons.OK);
    dialog.ShowDialog();
    return new InputDialog(dialog);
  }

  public static InputDialog Show(string title, InputDialogItem item, InputBoxButtons buttons)
  {
    dialogForm dialog = new dialogForm(title, new InputDialogItem[] { item }, buttons);
    dialog.ShowDialog();
    return new InputDialog(dialog);
  }

  public static InputDialog Show(string title, InputDialogItem[] items)
  {
    dialogForm dialog = new dialogForm(title, items, InputBoxButtons.OK);
    dialog.ShowDialog();
    return new InputDialog(dialog);
  }

  public static InputDialog Show(string title, InputDialogItem[] items, InputBoxButtons buttons)
  {
    dialogForm dialog = new dialogForm(title, items, buttons);
    dialog.StartPosition = FormStartPosition.CenterScreen;
    dialog.ShowDialog();
    return new InputDialog(dialog);
  }

  public static InputDialog Show(IWin32Window window, string title, string label)
  {
    dialogForm dialog = new dialogForm(title, new InputDialogItem[] { new InputDialogItem(label) }, InputBoxButtons.OK);
    dialog.ShowDialog(window);
    return new InputDialog(dialog);
  }

  public static InputDialog Show(IWin32Window window, string title, string label, InputBoxButtons buttons)
  {
    dialogForm dialog = new dialogForm(title, new InputDialogItem[] { new InputDialogItem(label) }, buttons);
    dialog.ShowDialog(window);
    return new InputDialog(dialog);
  }

  public static InputDialog Show(IWin32Window window, string title, string label, string text)
  {
    dialogForm dialog = new dialogForm(title, new InputDialogItem[] { new InputDialogItem(label, text) }, InputBoxButtons.OK);
    dialog.ShowDialog(window);
    return new InputDialog(dialog);
  }

  public static InputDialog Show(IWin32Window window, string title, string label, string text, InputBoxButtons buttons)
  {
    dialogForm dialog = new dialogForm(title, new InputDialogItem[] { new InputDialogItem(label, text) }, buttons);
    dialog.ShowDialog(window);
    return new InputDialog(dialog);
  }

  public static InputDialog Show(IWin32Window window, string title, string[] labels)
  {
    InputDialogItem[] items = new InputDialogItem[labels.Length];
    for (int i = 0; i < labels.Length; i++)
    {
      items[i] = new InputDialogItem(labels[i]);
    }

    dialogForm dialog = new dialogForm(title, items, InputBoxButtons.OK);
    dialog.ShowDialog(window);
    return new InputDialog(dialog);
  }

  public static InputDialog Show(IWin32Window window, string title, string[] labels, InputBoxButtons buttons)
  {
    InputDialogItem[] items = new InputDialogItem[labels.Length];
    for (int i = 0; i < labels.Length; i++)
    {
      items[i] = new InputDialogItem(labels[i]);
    }

    dialogForm dialog = new dialogForm(title, items, buttons);
    dialog.ShowDialog(window);
    return new InputDialog(dialog);
  }

  public static InputDialog Show(IWin32Window window, string title, InputDialogItem item)
  {
    dialogForm dialog = new dialogForm(title, new InputDialogItem[] { item }, InputBoxButtons.OK);
    dialog.ShowDialog(window);
    return new InputDialog(dialog);
  }

  public static InputDialog Show(IWin32Window window, string title, InputDialogItem item, InputBoxButtons buttons)
  {
    dialogForm dialog = new dialogForm(title, new InputDialogItem[] { item }, buttons);
    dialog.ShowDialog(window);
    return new InputDialog(dialog);
  }

  public static InputDialog Show(IWin32Window window, string title, InputDialogItem[] items)
  {
    dialogForm dialog = new dialogForm(title, items, InputBoxButtons.OK);
    dialog.ShowDialog(window);
    return new InputDialog(dialog);
  }

  public static InputDialog Show(IWin32Window window, string title, InputDialogItem[] items, InputBoxButtons buttons)
  {
    dialogForm dialog = new dialogForm(title, items, buttons);
    dialog.ShowDialog(window);
    return new InputDialog(dialog);
  }

  public Dictionary<string, string> Items { get; }

  public InputBoxResult Result { get; }

  private class dialogForm : Form
  {
    private InputBoxResult inputResult = InputBoxResult.Cancel;
    public TextBox[] textBox;
    public Label[] label;
    private Button button1;
    private Button button2;
    private Button button3;

    public InputBoxResult InputResult
    {
      get { return inputResult; }
    }

    public dialogForm(string title, InputDialogItem[] items, InputBoxButtons buttons)
    {
      int minWidth = 312;
      label = new Label[items.Length];
      for (int i = 0; i < label.Length; i++)
      {
        label[i] = new Label();
      }
      textBox = new TextBox[items.Length];
      for (int i = 0; i < textBox.Length; i++)
      {
        textBox[i] = new TextBox();
      }
      button2 = new Button();
      button3 = new Button();
      button1 = new Button();
      SuspendLayout();
      // 
      // label
      // 
      for (int i = 0; i < items.Length; i++)
      {
        label[i].AutoSize = true;
        label[i].Location = new Point(12, 9 + (i * 39));
        label[i].Name = "label[" + i + "]";
        label[i].Text = items[i].Label;
        if (label[i].Width > minWidth)
        {
          minWidth = label[i].Width;
        }
      }
      // 
      // textBox
      // 
      for (int i = 0; i < items.Length; i++)
      {
        textBox[i].Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        textBox[i].Location = new Point(12, 25 + (i * 39));
        textBox[i].Name = "textBox[" + i + "]";
        textBox[i].Size = new Size(288, 20);
        textBox[i].TabIndex = i;
        textBox[i].Text = items[i].Text;
        if (items[i].IsPassword)
        {
          textBox[i].UseSystemPasswordChar = true;
        }
      }
      // 
      // button1
      // 
      button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      button1.Location = new Point(208, 15 + (39 * label.Length));
      button1.Name = "button1";
      button1.Size = new Size(92, 23);
      button1.TabIndex = items.Length + 2;
      button1.Text = "button1";
      button1.UseVisualStyleBackColor = true;
      // 
      // button2
      // 
      button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      button2.Location = new Point(110, 15 + (39 * label.Length));
      button2.Name = "button2";
      button2.Size = new Size(92, 23);
      button2.TabIndex = items.Length + 1;
      button2.Text = "button2";
      button2.UseVisualStyleBackColor = true;
      // 
      // button3
      // 
      button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      button3.Location = new Point(12, 15 + (39 * label.Length));
      button3.Name = "button3";
      button3.Size = new Size(92, 23);
      button3.TabIndex = items.Length;
      button3.Text = "button3";
      button3.UseVisualStyleBackColor = true;
      //
      // Evaluate MessageBoxButtons
      //
      switch (buttons)
      {
        case InputBoxButtons.OK:
          button1.Text = "OK";
          button1.Click += OK_Click;
          button2.Visible = false;
          button3.Visible = false;
          AcceptButton = button1;
          break;
        case InputBoxButtons.OKCancel:
          button1.Text = "Cancel";
          button1.Click += Cancel_Click;
          button2.Text = "OK";
          button2.Click += OK_Click;
          button3.Visible = false;
          AcceptButton = button2;
          break;
        case InputBoxButtons.YesNo:
          button1.Text = "No";
          button1.Click += No_Click;
          button2.Text = "Yes";
          button2.Click += Yes_Click;
          button3.Visible = false;
          AcceptButton = button2;
          break;
        case InputBoxButtons.YesNoCancel:
          button1.Text = "Cancel";
          button1.Click += Cancel_Click;
          button2.Text = "No";
          button2.Click += No_Click;
          button3.Text = "Yes";
          button3.Click += Yes_Click;
          AcceptButton = button3;
          break;
        case InputBoxButtons.Save:
          button1.Text = "Save";
          button1.Click += Save_Click;
          button2.Visible = false;
          button3.Visible = false;
          AcceptButton = button1;
          break;
        case InputBoxButtons.SaveCancel:
          button1.Text = "Cancel";
          button1.Click += Cancel_Click;
          button2.Text = "Save";
          button2.Click += Save_Click;
          button3.Visible = false;
          AcceptButton = button2;
          break;
        default:
          throw new Exception("Invalid InputBoxButton Value");
      }
      // 
      // dialogForm
      // 
      AutoScaleDimensions = new SizeF(6F, 13F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(312, 47 + (39 * items.Length));
      for (int i = 0; i < label.Length; i++)
      {
        Controls.Add(label[i]);
      }
      for (int i = 0; i < textBox.Length; i++)
      {
        Controls.Add(textBox[i]);
      }
      Controls.Add(button1);
      Controls.Add(button2);
      Controls.Add(button3);
      MaximizeBox = false;
      MinimizeBox = false;
      MaximumSize = new Size(99999, 85 + (39 * items.Length));
      Name = "dialogForm";
      ShowIcon = false;
      ShowInTaskbar = false;
      Text = title;
      ResumeLayout(false);
      PerformLayout();
      foreach (Label l in label)
      {
        if (l.Width > minWidth)
        {
          minWidth = l.Width;
        }
      }
      ClientSize = new Size(minWidth + 24, 47 + (39 * items.Length));
      MinimumSize = new Size(minWidth + 40, 85 + (39 * items.Length));
    }

    private void OK_Click(object sender, EventArgs e)
    {
      inputResult = InputBoxResult.OK;
      Close();
    }

    private void Cancel_Click(object sender, EventArgs e)
    {
      inputResult = InputBoxResult.Cancel;
      Close();
    }

    private void Yes_Click(object sender, EventArgs e)
    {
      inputResult = InputBoxResult.Yes;
      Close();
    }

    private void No_Click(object sender, EventArgs e)
    {
      inputResult = InputBoxResult.No;
      Close();
    }

    private void Save_Click(object sender, EventArgs e)
    {
      inputResult = InputBoxResult.Save;
      Close();
    }
  }
}