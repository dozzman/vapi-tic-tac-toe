using System;
using UnityEngine;

namespace Ncco {
	public interface INcco {}

	public class NccoUtils {
		public static string ToJson(INcco ncco) {
			return "[" + JsonUtility.ToJson(ncco) + "]";
		}

		public static string ToJson(INcco[] nccos) {
			string json = "[";
			foreach (INcco ncco in nccos) {
				json += JsonUtility.ToJson(ncco);
				json += ",";
			}

			json = json.TrimEnd(new char[]{','});
			json += "]";
			return json;
		}
	}

	[Serializable]
	public class WelcomeNcco : INcco {
		public string action = "talk";
		public string text = "Welcome to Tic Tac Toe.";
	}

	[Serializable]
	public class WaitNcco : INcco {
		public string action = "input";
		//public int timeout = 1;
		//public bool submitOnHash = true;
		public string[] eventUrl = new string[]{"http://cf8fa786.ngrok.io/event/"};
	}
}

