using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SaveDataGrid.Lib
{
	public static class ObjectExtensions
	{
		public static T GetValue<T>(this object obj, string propName)
		{
			var res = obj.GetType().GetProperty(propName)?.GetValue(obj);

			if (res != null)
				return (T)res;

			//If the result is null and T type is nullable (e.g. double?), it is OK to return null value
			//e.g. res == null -> return value is null
			if (Nullable.GetUnderlyingType(typeof(T)) == null)
				return (T)res;

			//not nullable value, and T type isn't nullable (e.g. double), return value is (T)default (e.g. (double)default == 0)
			return default;
		}

		public static bool SetValue<T>(this object obj, string propName, T value)
		{
			if (obj != null)
			{
				PropertyInfo prop = obj.GetType().GetProperty(propName);
				if (prop != null)
					prop.SetValue(obj, value);
				return true;
			}
			return false;
		}




		public static void CopyProperties(this object dstObject, object srcObject, IEnumerable<string> propertyNames)
		{
			var srcProperties = srcObject.GetType().GetProperties().Select(p => p.Name);
			foreach (var name in propertyNames)
			{
				if (srcProperties.Contains(name))
					dstObject.SetValue(name, srcObject.GetValue<object>(name));
			}
		}
		public static void CopyPropertiesExclude(this object dstObject, object srcObject, IList<string> excludedProperties)
		{
			//done: Here take only properties with same name in source and destination object
			var dstPropertyNames = dstObject.GetType()
				.GetProperties()
				.Where(p => p.CanWrite)
				.Select(p => p.Name)
				.Where(p => !excludedProperties.Contains(p));
			var srcPropertyNames = srcObject.GetType()
				.GetProperties()
				.Where(p => p.CanRead)
				.Select(p => p.Name)
				.Where(p => !excludedProperties.Contains(p));

			List<string> propertyNames = new List<string>();
			foreach (var item in srcPropertyNames)
				if (dstPropertyNames.Contains(item))
					propertyNames.Add(item);
			if (propertyNames != null)
				dstObject.CopyProperties(srcObject, propertyNames);
		}
		public static void CopyPropertiesExclude(this object dstObject, object srcObject, string propName)
			=> dstObject.CopyPropertiesExclude(srcObject, new List<string>() { propName });

	}
}
