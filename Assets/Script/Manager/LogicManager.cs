using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LogicManager : MonoBehaviour {

	public LogicManager() { s_Instance = this; }
	public static LogicManager Instance { get { return s_Instance; } }
	private static LogicManager s_Instance;

	public int playerNumber;
	// public for now
	public List<Transform> destinationList;

	public Vector3 GetDestination(TeamColor color)
	{
		return destinationList[(int)color].position;
	}

	void Awake()
	{
		while(destinationList.Count < playerNumber )
			destinationList.Add(this.transform);
	}
}
