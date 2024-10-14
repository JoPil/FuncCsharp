namespace FunctionalSandBox.Common;
public abstract class Maybe<T>
{
    public virtual T Value { get; protected set; }

    public static implicit operator Maybe<T>(T @this) => @this.ToMaybe();
    public static implicit operator T(Maybe<T> @this) => @this.Value;
}

public class Error<T> : Maybe<T>
{
    public Error(Exception e)
    {
        ErrorMessage = e;
    }
    public Exception ErrorMessage { get; set; }
    public override T Value => default!;

}

public class Something<T> : Maybe<T>
{
    public Something(T value)
    {
        Value = value;
    }

    public static implicit operator Something<T>(T @this) => new Something<T>(@this);
    public static implicit operator T(Something<T> @this) => @this.Value;
}

public class Nothing<T> : Maybe<T>
{
    public override T Value => default!;
}

public abstract class Maybe
{

}

public class Something : Maybe
{

}

public class Nothing : Maybe
{

}

public class Error : Maybe
{
    public Error(Exception e)
    {
        CapturedError = e;
    }

    public Exception CapturedError { get; set; }
}