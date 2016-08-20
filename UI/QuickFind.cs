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
      cache = cache.OrderBy(p => p.Text).ToList();

      var source = new AutoCompleteStringCollection();
      source.AddRange(cache.Select(p=>p.Text).ToArray());
      cboFind.AutoCompleteCustomSource = source;

      cboFind.Items.AddRange(cache.ToArray());
      cboFind.DisplayMember = "Text";
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
      string s = cboFind.Text;
      if (string.IsNullOrWhiteSpace(s)) Close();

      var found = Project.Current.GetElementByName(s);

      var controller = new CanvasController();
      controller.SelectElements(found);
      controller.EnsureVisible(found.FirstOrDefault());

      Close();

    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
