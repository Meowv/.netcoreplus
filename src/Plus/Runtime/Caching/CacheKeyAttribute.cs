using System;

namespace Plus.Runtime.Caching
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheKeyAttribute : Attribute
    {
        public string Name { get; private set; }

        public string Format { get; private set; }

        public string Description { get; private set; }

        public CacheKeyAttribute(string name, string description)
            : this(name, "", description)
        {
        }

        public CacheKeyAttribute(string name, string format, string description)
        {
            Name = name;
            Format = format;
            Description = description;
        }

        public string GetLocalizedKey(params object[] args)
        {
            string text = Name;
            if (!string.IsNullOrEmpty(Format))
            {
                text = text + "[" + Format;
                int num = 0;
                foreach (object obj in args)
                {
                    text = text.Replace("{" + num + "}", (obj != null) ? obj.ToString() : "");
                    num++;
                }
                text += "]";
            }
            return text;
        }
    }
}