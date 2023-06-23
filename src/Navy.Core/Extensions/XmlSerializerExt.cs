#region

using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

#endregion

namespace Navy.Core.Extensions
{
    public static class XmlSerializerExt
    {
        public static T XmlDeserialize<T>(this string serializedObject)
        {
            var s = new XmlSerializer(typeof(T));
            using (var stream = new StringReader(serializedObject))
            using (var reader = XmlReader.Create(stream))
            {
                return (T)s.Deserialize(reader);
            }
        }

        public static T XmlDeserialize<T>(this byte[] serializedObject)
        {
            var s = new XmlSerializer(typeof(T));
            using (var stream = new MemoryStream(serializedObject))
            using (var reader = XmlReader.Create(stream))
            {
                return (T)s.Deserialize(reader);
            }
        }

        public static T XmlDeserialize<T>(this XElement serializedObject)
        {
            Stream s = new MemoryStream();
            var serializer = new XmlSerializer(typeof(T));
            serializedObject.Save(s);
            s.Seek(0, SeekOrigin.Begin);
            using (var newReader = XmlReader.Create(s))
            {
                return (T)serializer.Deserialize(newReader);
            }
        }

        public static string XmlSerialize(this object objectToSerialize, Type[] extraTypes = null)
        {
            var s = new XmlSerializer(objectToSerialize.GetType(), extraTypes);
            var settings = new XmlWriterSettings
            {
                NewLineHandling = NewLineHandling.Entitize
            };
            using (var stream = new StringWriter())
            using (var writer = XmlWriter.Create(stream, settings))
            {
                s.Serialize(writer, objectToSerialize);
                return stream.ToString();
            }
        }

        public static T? GetAttributeValue<T>(this XElement obj, string name) where T : struct
        {
            var attr = obj.Attribute(name);
            if (attr == null)
                return null;
            try
            {
                if (typeof(T) == typeof(Guid))
                    return (T)(object)Guid.Parse(attr.Value);

                if (typeof(T) == typeof(uint) && attr.Value.StartsWith("0x"))
                    return (T)(object)Convert.ToUInt32(attr.Value.Substring(2), 16);

                return (T)Convert.ChangeType(attr.Value, typeof(T));
            }
            catch (FormatException)
            {
                throw new FormatException(string.Format(
                    "Error converting attribute '{0}'='{3}' in attribute '{1}' to format '{2}'", name, obj.Name,
                    typeof(T), attr.Value));
            }
        }

        public static string GetAttributeValue(this XElement obj, string name)
        {
            var attr = obj.Attribute(name);
            return attr?.Value;
        }
    }
}