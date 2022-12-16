using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SaveDataGrid.Lib
{
	public class BaseSerializer<TData> where TData : new()
	{
		protected string FileName;
		public static JsonSerializerOptions DefaultOptions = new JsonSerializerOptions()
		{
			WriteIndented = true
		};
		public JsonSerializerOptions Options = DefaultOptions;

		public BaseSerializer(string fileName, bool autoCreateDirectoryIfNotExists = true)
		{
			FileName = fileName;

			if (autoCreateDirectoryIfNotExists && !string.IsNullOrEmpty(fileName))
			{
				string dir = Path.GetDirectoryName(FileName);
				if (!Directory.Exists(dir))
					Directory.CreateDirectory(dir);
			}
		}

		public string GetJsonString() => File.ReadAllText(FileName);
		public TData? Deserialize(string jsonString) => JsonSerializer.Deserialize<TData>(jsonString, Options);
		public TData? Deserialize()
		{
			TData? res = default;
			try
			{
				if (File.Exists(FileName))
					res = Deserialize(GetJsonString());
			}
			catch (Exception ex)
			{
				Debugger.Log(1, null, ex.Message);
			}

			return res ?? new TData();
		}

		public void Serialize(TData objectToSave)
		{
			try
			{
				string jsonString = JsonSerializer.Serialize<TData>(objectToSave, Options);
				File.WriteAllText(FileName, jsonString, Encoding.UTF8);
			}
			catch(Exception ex)
			{
				Debugger.Log(1, null, ex.Message);
			}
		}
	}
}
