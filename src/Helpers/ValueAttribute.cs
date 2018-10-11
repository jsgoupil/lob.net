using System;

namespace Lob.Net.Helpers
{
    internal class ValueAttribute : Attribute
    {
        public ValueAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
