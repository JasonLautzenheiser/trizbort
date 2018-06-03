using System.Drawing;

namespace Trizbort.Domain.StatusBar {
  public interface IStatusWidget {
    StatusItems Id { get; }
    string Name { get; }
    string MenuName { get; }
    string HelpText { get;  }
    Color DisplayColor { get;  }

    string DisplayText();

    void ClickHandler();
  }
}