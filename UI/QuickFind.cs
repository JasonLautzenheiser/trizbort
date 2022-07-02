using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Trizbort.Domain.Application;
using Trizbort.Domain.Cache;
using Trizbort.Domain.Controllers;
using Trizbort.Domain.Elements;

namespace Trizbort.UI; 

public sealed partial class QuickFind : Form {
  private readonly List<findAutofindCacheItem> cache;

  public QuickFind() {
    InitializeComponent();

    cache = buildFindCache();
    cache = cache.OrderBy(p => p.Text).ToList();

    var source = new AutoCompleteStringCollection();
    source.AddRange(cache.Select(p => p.ToString()).ToArray());
    txtFind.AutoCompleteCustomSource = source;
  }

  private void btnCancel_Click(object sender, EventArgs e) {
    Close();
  }

  private void btnFind_Click(object sender, EventArgs e) {
    doFind();
  }

  private List<findAutofindCacheItem> buildFindCache() {
    var indexer = new Indexer();
    var findCacheItems = indexer.Index();

    var list = new List<findAutofindCacheItem>();

    foreach (var item in findCacheItems) {
      var x1 = new findAutofindCacheItem {Room = item.Element, Text = item.Name?.Trim()};
      var x2 = new findAutofindCacheItem {Room = item.Element, Text = item.Description?.Trim()};
      var x3 = new findAutofindCacheItem {Room = item.Element, Text = item.Objects?.Trim()};
      var x4 = new findAutofindCacheItem {Room = item.Element, Text = item.Subtitle?.Trim()};

      list.Add(x1);
      if (!string.IsNullOrEmpty(x2.Text)) list.Add(x2);
      if (!string.IsNullOrEmpty(x3.Text)) list.Add(x3);
      if (!string.IsNullOrEmpty(x4.Text)) list.Add(x4);
    }


    return list;
  }

  private void doFind() {
    var s = txtFind.Text;
    if (string.IsNullOrWhiteSpace(s)) Close();

    var found = getResults(s);

    var controller = new CanvasController();
    controller.SelectElements(found);

    Project.Current.ActiveSelectedElement = found.FirstOrDefault();

    controller.EnsureVisible(Project.Current.ActiveSelectedElement);

    Close();
  }

  private List<Element> getResults(string s) {
    var list = cache.Where(xx => xx.Text?.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) > -1).Select(p => p.Room).ToList();
    return list;
  }

  private void QuickFind_Activated(object sender, EventArgs e) {
    txtFind.Focus();
  }

  private void QuickFind_Deactivate(object sender, EventArgs e) {
    Close();
  }

  private void QuickFind_KeyDown(object sender, KeyEventArgs e) {
    if (e.KeyCode == Keys.Escape)
      Close();
  }

  private class findAutofindCacheItem {
    public Element Room { get; set; }
    public string Text { get; set; }

    public override string ToString() {
      return $"{Text}";
    }
  }

  private void txtFind_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar == (int) Keys.Enter) doFind();
  }
}