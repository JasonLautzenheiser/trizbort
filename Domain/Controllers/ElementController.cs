using Trizbort.Domain.Elements;

namespace Trizbort.Domain.Controllers; 

public sealed class ElementController {
  public void ShowElementProperties(Element element) {
    if (element.HasDialog)
      element.ShowDialog();
  }
}