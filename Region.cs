using System.Drawing;

namespace Trizbort
{
    public class Region
    {
        public string RegionName { get; set; }
        public Color RColor { get; set; }
        public Color TextColor { get; set; }
        public static string DefaultRegion {get { return "NoRegion"; }}

        public Region()
        {
            RColor = Color.White;
            TextColor = Color.Blue;
            RegionName = DefaultRegion;
        }

        public string ClearRegionNameObfuscation()
        {
            return RegionName.Replace("____", " ");
        }

        public string FixupRegionNameForSave()
        {
            return RegionName.Replace(" ", "____");
        }
    }
}