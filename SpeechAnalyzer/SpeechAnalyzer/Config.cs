using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace SpeechAnalyzer
{
	class Config
	{
		public String SonicAnnotator { get; set; }
		public String DataDirectory { get; set; }
		public String TempDirectory { get; set; }

		public static Config ReadConfigFile()
		{
			Config cfg = null;
			try
			{
				StreamReader sr = new StreamReader("config.js");
				String json = sr.ReadToEnd();
				cfg = JsonConvert.DeserializeObject<Config>(json);
				sr.Close();
			}
			catch (FileNotFoundException /*fnfe*/ )
			{
				cfg = new Config()
				{
					SonicAnnotator = @"sonic-annotator.exe",
					TempDirectory = "temp",
					DataDirectory = "data"
				};

				StreamWriter sw = new StreamWriter("config.js");
				sw.Write(JsonConvert.SerializeObject(cfg));
				sw.Close();
			}

			return cfg;
		}
	}
}
