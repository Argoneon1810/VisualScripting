using System;

namespace NodeGraph
{
    public class Result
    {
        public static implicit operator bool(Result self)
        {
            return self != null;
        }
    }

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
}