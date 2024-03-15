using System.Globalization;

namespace Domain.Exceptions.Base
{
    public class BaseException : Exception
    {
        public string Key { get; set; }

        public Dictionary<string, object> Values { get; private set; } = new();

        public BaseException(string key) : base(key)
        {
            Key = key;
        }

        protected void AddOrReplaceValue(string key, IConvertible value)
        {
            Values[key] = value.ToString(CultureInfo.InvariantCulture);
        }
    }
}

