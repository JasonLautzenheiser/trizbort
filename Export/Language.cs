using System.Collections.Generic;
using System.IO;
using System.Linq;
using Trizbort.Domain.Elements;
using Trizbort.Export.Domain;
using Trizbort.Export.Inform6;

namespace Trizbort.Export; 

public abstract class Language : ILanguage {
  protected const char SINGLE_QUOTE = '\'';
  protected const char DOUBLE_QUOTE = '"';

  public abstract void WriteRegions(TextWriter writer, IEnumerable<ExportRegion> regionsInExportOrder);
  public abstract void WriteLocations(TextWriter writer, IEnumerable<Location> locationsInExportOrder);
  public abstract string ExportName(string displayName, int? suffix);
  public abstract string ExportName(Room room, int? suffix);
  public abstract void WriteIncludes(TextWriter writer);
  public abstract void WriteAbout(TextWriter writer);
  public abstract void WriteInitialize(TextWriter writer, IEnumerable<Location> locationsInExportOrder);
  public abstract void WriteHeader(TextWriter writer, string title, string author, string description);
  
  
  public static string DeAccent(string myStr) {
    return myStr.Aggregate("", (current, c) => c switch {
      >= 'à' and <= 'å' => current + 'a',
      >= 'À' and <= 'Å' => current + 'A',
      'Ç' => current + 'C',
      'ç' => current + 'c',
      >= 'è' and <= 'ë' => current + 'e',
      >= 'È' and <= 'Ë' => current + 'E',
      >= 'ì' and <= 'ï' => current + 'i',
      >= 'Ì' and <= 'Ï' => current + 'I',
      'ñ' => current + 'n',
      'Ñ' => current + 'N',
      >= 'Ò' and <= 'Ö' => current + 'o',
      >= 'ò' and <= 'ö' => current + 'O',
      >= 'ù' and <= 'ü' => current + 'u',
      >= 'Ù' and <= 'Ü' => current + 'U',
      _ => current + c
    });
  }

  public static string StripUnaccentedCharacters(string text) {
    return stripOddCharacters(text, 'À', 'Á', 'Â', 'Ã', 'Ä', 'Å', 'Æ', 'Ç', 'È', 'É', 'Ê', 'Ë', 'Ì', 'Í', 'Î', 'Ï',
      'Ñ', 'Ò', 'Ó', 'Ô', 'Õ', 'Ö', 'Ù', 'Ú', 'Û', 'Ü', 'ß', 'à', 'á', 'â', 'ã', 'ä', 'å', 'æ', 'ç', 'è', 'é', 'ê',
      'ë', 'ì', 'í', 'î', 'ï', 'ñ', 'ò', 'ó', 'ô', 'õ', 'ö', 'ù', 'ú', 'û', 'ü', ' ', '-');
  }

  private static string stripOddCharacters(string text, params char[] exclude) {
    var exclusions = new List<char>(exclude);
    return string.IsNullOrEmpty(text) 
      ? string.Empty 
      : text.Where(c => c is >= 'A' and <= 'Z' or >= 'a' and <= 'z' or >= '0' and <= '9' or '_' || exclusions.Contains(c))
            .Aggregate(string.Empty, (current, c) => current + c);
  }
}