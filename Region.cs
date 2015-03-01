using System;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace Trizbort
{
  public class Region
  {
    public Region()
    {
      RColor = Color.White;
      TextColor = Color.Blue;
      RegionName = DefaultRegion;
      RegionID =new Guid();
    }

    public Guid RegionID { get; set; }
    public string RegionName { get; set; }
    public Color RColor { get; set; }
    public Color TextColor { get; set; }

    public static string DefaultRegion
    {
      get { return "NoRegion"; }
    }

    public static bool ValidRegionName(string name)
    {
      if (string.IsNullOrWhiteSpace(name))
        return false;

      return true;
    }

    public string ClearRegionNameObfuscation()
    {
      return RegionName.Replace("____", " ");
    }

    public string FixupRegionNameForSave()
    {
      var s = RegionName.Replace(" ", "____");
      var validXmlChars =XmlConvert.EncodeName(s);
      return validXmlChars; 
    }
  }
}