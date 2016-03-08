using System;
using System.Runtime.Serialization;

namespace AppSettingsByConvention
{
    [Serializable]
    public class UnsupportedPropertyTypeException : Exception
    {
        public UnsupportedPropertyTypeException(Type propertyType)
            : base($"Cannot handle properties of type {propertyType}! You can support it if you add to the Dictionary SettingsByConvention.ParserMappings.") { }

        public UnsupportedPropertyTypeException() { }
        public UnsupportedPropertyTypeException(string message)
            : base(message) { }
        public UnsupportedPropertyTypeException(string message, Exception innerException)
            : base (message, innerException) { }
        protected UnsupportedPropertyTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}