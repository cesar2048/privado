using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace SpeechAnalyzer.Model
{
	class JsonEncoder
	{
		public static T Load<T>(String file)
		{
			T obj = default(T);
			try
			{
				StreamReader sr = new StreamReader(file);
				String serialization = sr.ReadToEnd();
				sr.Close();

				obj = JsonConvert.DeserializeObject<T>(serialization);
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine("Error {0} al cargar archivo: {1}", e.Message, file);
			}

			return obj;
		}

		public static void Save(String file, Object obj)
		{
			String serialization = JsonConvert.SerializeObject(obj, Formatting.Indented);
			
			StreamWriter sw = new StreamWriter(file);
			sw.Write(serialization);
			sw.Close();
		}
	}
}
