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
	public Transform BulletField;

	public Vector3 GetDestination(TeamColor color)
	{
		return destinationList[(int)color].position;
	}

	public Quaternion GetRotation(TeamColor color)
	{
		if ( color.Equals(TeamColor.Blue))
			return Quaternion.EulerAngles(0, 90f, 0);
		if ( color.Equals(TeamColor.Red))
			return Quaternion.EulerAngles(0, -90f , 0);

		return Quaternion.EulerAngles(0, 0, 0);
	}

	void Awake()
	{
		while (destinationList.Count < playerNumber+1 )
			destinationList.Add(this.transform);
	}

	[SerializeField] ObjCreateWindow objCreateWindow;
	void ObjCreateControllerSwitch()
	{
		if (objCreateWindow.isShow)
			objCreateWindow.Hide();
		else
			objCreateWindow.Show();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
			ObjCreateControllerSwitch();
	}

}
