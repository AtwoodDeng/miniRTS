using UnityEngine;
using System.Collections;

public struct Global {

	public static string BULLET_TAG = "BULLET";
	public static float GetDistance(Vector3 pos1 , Vector3 pos2)
	{
		return ( pos1 - pos2 ).magnitude;
	}


	public static string ROBOT_DATA_PATH =  "Data/Robot/RobotData";

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
	public static string UI_ROBOT_PATH = "UI/Image/Robot/";



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

	public static Color GetColorByAlpha(Color col , float alpha)
	{
		return new Color(col.r,col.g,col.b,alpha);
	}

	public static Vector3 GetRandomDirection()
	{
		float angle = Random.Range(0, Mathf.PI * 2f );
		return new Vector3( Mathf.Cos(angle),
							0,
							Mathf.Sin(angle));
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
	Light,
	Magic,
	Biochemical,
	Nuclear,
}

public enum EquipmentType
{
	None,
	Body,
	Weapon,
	Pusher,
	Armor,
}

[System.SerializableAttribute]
public struct RobotCreatInfo
{
	public BodyNames bodyName;
	public EquipmentNames[] equipments;
	public TeamColor teamColor;
	public Transform initPos;
}
