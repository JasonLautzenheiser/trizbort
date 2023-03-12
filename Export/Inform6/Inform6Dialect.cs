using NetEscapades.EnumGenerators;

namespace Trizbort.Export.Inform6;

[EnumExtensions]
public enum Inform6Dialect {
  I6,
  PunyInform
}

public interface IDialect  {
}