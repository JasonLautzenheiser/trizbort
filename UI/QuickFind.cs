using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Trizbort.Domain.Cache;
using Trizbort.Domain.Controllers;

namespace Trizbort.UI
{
  public partial class QuickFind : Form
  {
    private List<FindCacheItem> cache;

    public QuickFind()
    {
      InitializeComponent();

      cache = buildFindCache();
      cache = cache.OrderBy(p => p.Name).ToList();

      var source = new AutoCompleteStringCollection();
      source.AddRange(cache.Select(p=>p.ToString()).ToArray());
      cboFind.AutoCompleteCustomSource = source;
    }

    private List<FindCacheItem> buildFindCache()
    {
      var indexer = new Indexer();
      return indexer.Index();
    }

    private void QuickFind_Deactivate(object sender, EventArgs e)
    {
        Close();
    }

    private void QuickFind_Activated(object sender, EventArgs e)
    {
      cboFind.Focus();
    }

    private void btnFind_Click(object sender, EventArgs e)
    {
      doFind();
    }

    private void doFind()
    {
      string s = cboFind.Text;
      if (string.IsNullOrWhiteSpace(s)) Close();

      var found = getResults(s);

      var controller = new CanvasController();
      controller.SelectElements(found);

      Project.Current.ActiveSelectedElement = found.FirstOrDefault();

      controller.EnsureVisible(Project.Current.ActiveSelectedElement);

      Close();
    }

    private List<Element> getResults(string s)
    {
      var list = cache.Where(xx => (xx.Name?.IndexOf(s,StringComparison.CurrentCultureIgnoreCase) > -1) || (xx.Description?.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) > -1) || (xx.Objects?.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) > -1)).Select(p => p.Element).ToList();
      return list;
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void cboFind_KeyPress(object sender, KeyPressEventArgs e)
    {
      if (e.KeyChar == (int) Keys.Enter)
      {
        doFind();
      }
    }
  }
}
