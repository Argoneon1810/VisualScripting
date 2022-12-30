namespace NodeGraph
{
    public class Result
    {
        public static implicit operator bool(Result self)
        {
            return self != null;
        }
        public virtual string GetResultInString() => "";
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

    public class StringResult : Result<string>
    {
        public StringResult()
        {
            Value = "";
        }

        public StringResult(string value)
        {
            Value = value;
        }

        public override string GetResultInString() => GetValue();
    }

    public class FloatResult : Result<float>
    {
        public FloatResult()
        {
            Value = 0;
        }

        public FloatResult(float value)
        {
            Value = value;
        }

        public override string GetResultInString() => GetValue().ToString();
    }
}