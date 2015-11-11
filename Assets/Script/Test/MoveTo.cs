using System.Collections;
using UnityEngine;

public class MoveTo : MonoBehaviour {
	
	public Transform obj;
	public NavMeshAgent agent;

	// Use this for initialization
	void Awake () {
		if ( agent == null )
			agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		agent.destination = obj.position;
		Message msg = new Message();
		EventManager.Instance.PostEvent(EventDefine.TestSend, msg , this);
	}

	void OnEnable()
	{
		EventManager.Instance.RegistersEvent(EventDefine.TestSend , TestSend);
	}

	void OnDisable()
	{
		EventManager.Instance.UnregistersEvent(EventDefine.TestSend , TestSend);
	}

	void TestSend(Message msg)
	{
		Debug.Log(name + "got messages from " + ((MoveTo)msg.sender).name);
	}
}