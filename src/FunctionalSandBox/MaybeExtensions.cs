using FunctionalSandBox.Common;

namespace FunctionalSandBox;

public static class MaybeExtensions
{
    public static Maybe<T> ToMaybe<T>(this T @this) => new Something<T>(@this);

    public static Maybe<TOut> Bind<TIn, TOut>(this Maybe<TIn> @this, Func<TIn, TOut> func)
    {
        switch (@this)
        {
            case Something<TIn> sth when !EqualityComparer<TIn>.Default.Equals(sth.Value, default):
                try
                {
                    return func(sth).ToMaybe();
                }
                catch (Exception e)
                {
                    return new Error<TOut>(e);
                }
            case Error<TIn> err:
                return new Error<TOut>(err.ErrorMessage);
            default:
                return new Nothing<TOut>();
        }

    }
    public static async Task<Maybe<TToType>> BindAsync<TFromType, TToType>(this Maybe<TFromType> @this, Func<TFromType, Task<TToType>> f)
    {
        switch (@this)
        {
            case Something<TFromType> sth when !EqualityComparer<TFromType>.Default.Equals(sth.Value, default):
                try
                {
                    var result = await f(sth.Value);
                    return result.ToMaybe();
                }
                catch (Exception e)
                {
                    return new Error<TToType>(e);
                }
            case Error<TFromType> err:
                return new Error<TToType>(err.ErrorMessage);
            default:
                return new Nothing<TToType>();
        }
    }
    public static async Task<Maybe<TToType>> BindAsync<TFromType, TToType>(this Maybe<TFromType> @this, Func<TFromType, ValueTask<TToType>> f)
    {
        switch (@this)
        {
            case Something<TFromType> sth when !EqualityComparer<TFromType>.Default.Equals(sth.Value, default):
                try
                {
                    var result = await f(sth.Value);
                    return result.ToMaybe();
                }
                catch (Exception e)
                {
                    return new Error<TToType>(e);
                }
            case Error<TFromType> err:
                return new Error<TToType>(err.ErrorMessage);
            default:
                return new Nothing<TToType>();
        }
    }


    public static Maybe<TToType> Map<TFromType, TToType>(this Maybe<TFromType> @this, Func<TFromType, Maybe<TToType>> f)
    {
        switch (@this)
        {
            case Something<TFromType> sth when !EqualityComparer<TFromType>.Default.Equals(sth.Value, default):
                try
                {
                    return f(sth);
                }
                catch (Exception e)
                {
                    return new Error<TToType>(e);
                }
            case Error<TFromType> err:
                return new Error<TToType>(err.ErrorMessage);
            default:
                return new Nothing<TToType>();
        }
    }
    public static async Task<Maybe<TToType>> MapAsync<TFromType, TToType>(this Maybe<TFromType> @this, Func<TFromType, Task<Maybe<TToType>>> f)
    {
        switch (@this)
        {
            case Something<TFromType> sth when !EqualityComparer<TFromType>.Default.Equals(sth.Value, default):
                try
                {
                    var result = await f(sth.Value);
                    return result.ToMaybe();
                }
                catch (Exception e)
                {
                    return new Error<TToType>(e);
                }
            case Error<TFromType> err:
                return new Error<TToType>(err.ErrorMessage);
            default:
                return new Nothing<TToType>();
        }
    }
    public static Maybe Map<T>(this Maybe<T> @this, Action<T> act)
    {
        switch (@this)
        {
            case Something<T> sth when !EqualityComparer<T>.Default.Equals(sth.Value, default):
                try
                {
                    act(sth);
                    return new Something();
                }
                catch (Exception e)
                {
                    return new Error(e);
                }
            case Error<T> err:
                return new Error(err.ErrorMessage);
            default:
                return new Something();
        }
    }
    public static Maybe Map<TFromType>(this Maybe<TFromType> @this, Func<TFromType, Maybe> f)
    {
        switch (@this)
        {
            case Something<TFromType> sth when !EqualityComparer<TFromType>.Default.Equals(sth.Value, default):
                try
                {
                    return f(sth);
                }
                catch (Exception e)
                {
                    return new Error(e);
                }
            case Error<TFromType> err:
                return new Error(err.ErrorMessage);
            default:
                return new Nothing();
        }
    }


    public static Maybe<T> OnNothing<T>(this Maybe<T> @this, Action act)
    {
        try
        {
            if (@this is Nothing<T> nth)
                act();
            return @this;
        }
        catch (Exception e)
        {
            return new Error<T>(e);
        }
    }
    public static Maybe<T> OnError<T>(this Maybe<T> @this, Action<Exception> act)
    {
        try
        {
            if (@this is Error<T> err)
                act(err.ErrorMessage);
            return @this;
        }
        catch (Exception e)
        {
            return new Error<T>(e);
        }

    }
    public static Maybe<T> OnSomething<T>(this Maybe<T> @this, Action<T> act)
    {
        try
        {
            if (@this is Something<T> err)
                act(err.Value);
            return @this;
        }
        catch (Exception e)
        {
            return new Error<T>(e);
        }
    }


    public static Maybe OnError(this Maybe @this, Action<Exception> act)
    {
        try
        {
            if (@this is Error err)
                act(err.CapturedError);
            return @this;
        }
        catch (Exception e)
        {
            return new Error(e);
        }

    }
    public static Maybe OnSomething(this Maybe @this, Action act)
    {
        try
        {
            if (@this is Something sth)
                act();
            return @this;
        }
        catch (Exception e)
        {
            return new Error(e);
        }

    }
    public static Maybe OnNothing(this Maybe @this, Action act)
    {
        try
        {
            if (@this is Nothing nth)
                act();
            return @this;
        }
        catch (Exception e)
        {
            return new Error(e);
        }

    }

}
