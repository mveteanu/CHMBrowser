using System;
using System.Configuration;

namespace CHM.HttpHandler
{
	public class Settings
	{
		private Settings(){}

		public static bool CheckBoolSetting(string KeyName, bool defaultValue)
		{
			string setting = ConfigurationSettings.AppSettings[KeyName];
			if ((setting == null) || (setting.Length == 0))
				return defaultValue;

			setting = setting.Trim().ToLower();

			return setting == "yes";
		}

	}
}
