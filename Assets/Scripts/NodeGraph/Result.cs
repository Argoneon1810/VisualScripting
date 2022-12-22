public class Result { }

public class Result<T> : Result
{
    protected T Value;

    public virtual T GetValue()
    {
        return Value;
    }

    public virtual void SetValue(T t)
    {
        Value = t;
    }
}
