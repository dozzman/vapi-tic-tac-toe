using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using System.Threading;

public class PlayerEventHandler : MonoBehaviour {

	private HttpListener listener;
	public GameController gameController;

	// Use this for initialization
	void Awake () {
		Debug.Log ("Starting up Http Server at port 12321");
		listener = new HttpListener ();
		listener.Prefixes.Add ("http://localhost:12321/playermove/");
		listener.Start ();
		Debug.Log ("Http Server started...");

		Thread t = new Thread(

		while (true) {
			HttpListenerContext context = listener.GetContext ();
			HttpListenerRequest request = context.Request;
			StreamReader reader = new StreamReader (request.InputStream);
			string body = reader.ReadToEnd ();

			Debug.Log ("Received input: " + body);

			HttpListenerResponse response = context.Response;
			StreamWriter writer = new StreamWriter (response.OutputStream);
			writer.WriteLine ("ok");
			writer.Close ();

			if (gameController.isGameOver) {
				Debug.Log ("Shutting down Http Server...");
				listener.Close ();
				break;
			}
		}
	}
}
