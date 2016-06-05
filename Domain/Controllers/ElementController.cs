namespace Trizbort.Domain.Controllers
{
  public class ElementController
  {
    public void ShowElementProperties(Element element)
    {
      if (element.HasDialog)
        element.ShowDialog();
    }
  }
}