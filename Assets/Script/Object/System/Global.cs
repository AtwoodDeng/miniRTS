using UnityEngine;
using System.Collections;

public struct Global {

	public static string BULLET_TAG = "BULLET";
	public static float GetDistance(Vector3 pos1 , Vector3 pos2)
	{
		return ( pos1 - pos2 ).magnitude;
	}


	public static string ROBOT_DATA_PATH =  "/Data/Robot/RobotData.xls";

	public enum DataTableType
	{
		Body,
		Weapon,
		Pusher,
		Armor,
		None,
	}

	public static string ROBOT_PREFAB_PATH = "Robot";
	public static string BULLET_PREFAB_PATH = "Robot/Bullet/";


	public static Vector3[] equipmentInitPosition
	= {
		new Vector3(0,0,0),
		new Vector3(0,0,1f),
		new Vector3(0,0,0),
		new Vector3(0,0,-1f),
	};

	public static Vector3 equipmentInitRotation = new Vector3(90f,0,0);

	public static Global.DataTableType NameToType(string equipmentName)
	{
		for (int i = 0 ; i < 4 ; ++ i )
		{
			Global.DataTableType type = (Global.DataTableType) i ;
			if ( DataManager.Instance.equipmentList[i].Contains(equipmentName))
				return type;
		}
		return Global.DataTableType.Weapon;
	}
}


public enum TeamColor{
	None,
	Red,
	Blue
}

public enum DamageType
{
	None,
	Fire,
	Water,
}

[System.SerializableAttribute]
public struct RobotCreatInfo
{
	public string bodyName;
	public string[] equipments;
	public TeamColor teamColor;
	public Transform initPos;
}
