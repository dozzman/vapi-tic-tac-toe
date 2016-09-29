using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Net;
using System.Threading;
using System.IO;
using System;
using Ncco;

public class EventHandler : MonoBehaviour {

	private HttpListener connectListener;
	private HttpListener eventListener;
	public GameController gameController;

	private Player player1;
	private Player player2;

	// Use this for initialization
	void Awake () {
		connectListener = new HttpListener();
		connectListener.Prefixes.Add("http://cf8fa786.ngrok.io:12321/connect/");

		eventListener = new HttpListener();
		eventListener.Prefixes.Add("http://cf8fa786.ngrok.io:12321/event/");
		Thread connectThread = new Thread(this.StartConnectServer);
		Thread eventThread = new Thread(this.StartEventServer);
		connectThread.Start();
		eventThread.Start();
	}

	void StartConnectServer() {
		Debug.Log("Starting connect server");
		connectListener.Start();
		Debug.Log("Connect server started");

		while (true) {
			ConnectPlayer();
		}
	}

	void ConnectPlayer() {
		HttpListenerContext ctx = connectListener.GetContext();
		HttpListenerResponse response = ctx.Response;
		HttpListenerRequest request = ctx.Request;
		StreamWriter writer = new StreamWriter(response.OutputStream);
		StreamReader reader = new StreamReader(request.InputStream);
		Debug.Log("Connect body = " + reader.ReadToEnd());
		response.ContentType = "application/json";
		writer.WriteLine(NccoUtils.ToJson(new INcco[]{new WelcomeNcco(), new WaitNcco()}));
		Debug.Log("Ncco = " + NccoUtils.ToJson(new INcco[]{new WelcomeNcco(), new WaitNcco()}));
		writer.Close();
		response.Close();
	}

	void StartEventServer() {
		Debug.Log("Starting Event server");
		eventListener.Start();
		Debug.Log("Event server started");

		while (true) {
			HandleEvent();
		}
	}

	void HandleEvent() {
		HttpListenerContext ctx = eventListener.GetContext();
		HttpListenerResponse response = ctx.Response;
		HttpListenerRequest request = ctx.Request;
		StreamReader reader = new StreamReader(request.InputStream);
		string body = reader.ReadToEnd();
		PlayerMove move = JsonUtility.FromJson<PlayerMove>(body);
		if (move.dtmf != null) {
			int gridNum = Convert.ToInt32(move.dtmf);
			GameController.ExecuteOnMainThread.Enqueue(new Action(() => {
			gameController.buttonList[gridNum].GetComponentInParent<Button>().onClick.Invoke();
			}));
		}
		Debug.Log("Event body = " + reader.ReadToEnd());
		StreamWriter writer = new StreamWriter(response.OutputStream);
		writer.WriteLine(NccoUtils.ToJson(new WaitNcco()));
		writer.Close();
		response.Close();
	}

	[Serializable]
	public class PlayerMove {
		public string dtmf;
	}
}
