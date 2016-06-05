namespace Trizbort.Domain
{
  public interface IParameterizedCommand<T,  Value>
  {
    T Execute(Value value);
  }

  public interface ICommand<T>
  {
    T Execute();
  }

  public interface IParameterizedCommand<V>
  {
    void Execute(V value);
  }

  public interface ICanvasCommand<T, V>
  {
    T Execute(Canvas canvas, V value);
  }

  public interface ICanvasCommand<V>
  {
    void Execute(Canvas canvas, V value);
    void Execute(Canvas canvas, V value, object other);
  }
}