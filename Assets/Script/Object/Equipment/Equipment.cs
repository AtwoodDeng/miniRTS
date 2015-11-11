using UnityEngine;
using System.Collections;
using System.Data;

public class Equipment : MonoBehaviour {

	// [System.Serializable]
	// public struct EquipmentParameter
	// {
	// 	float cost;
	// }

	// public EquipmentParameter equipmentParameter;

	public enum Type{
		None,
		Weapon,
		Armor,
		Pusher,
	}

	public Type type;

	public virtual void AttackBegin(Robot _robot){}

	public virtual void AttackUpdate(Robot _robot){}

	public virtual void AttackEnd(Robot _robot){}

	public virtual Damage RecieveDamage(Damage dmg){return dmg;}

	public virtual void Init(DataRow data , Robot _robot)
	{	

	}


}

public class EquipmentFactory
{
	public enum EquipmentCreatePosition
	{
		Up,
		Down,
		Left,
		Right,
		LeftRight,
	}
	public static Vector3[] equipmentInitPositions
	= {
		new Vector3(0,0,1f),
		new Vector3(0,0,-1f),
		new Vector3(-1f,0,0),
		new Vector3(1f,0,0),
		new Vector3(0,0,0),
	};

	public static string NameToPath(string equipmentName)
	{
		for (int i = 0 ; i < 4 ; ++ i )
		{
			Global.DataTableType type = (Global.DataTableType) i ;
			if ( DataManager.Instance.equipmentList[i].Contains(equipmentName))
				return Global.ROBOT_PREFAB_PATH + "/" + type.ToString() + "/" + equipmentName;
		}

		return "";
	}



	public static Equipment  CreateEquipment(string equipmentName, Robot robot,  EquipmentCreatePosition pos){
		
		Debug.Log("create equipment");

		string path = NameToPath(equipmentName);
		if (path == "" )
		{
			Debug.LogError("Cannnot find prefab path");
			return null;
		}

		GameObject equipment = GameObject.Instantiate( Resources.Load(path) as GameObject) as GameObject;
		if (equipment == null )
		{
			Debug.LogError("Cannot found prefab" + equipmentName);
			return null;
		}

		Equipment equipmentComp = equipment.GetComponent<Equipment>();
		if (equipmentComp == null )
		{
			Debug.LogError("Cannot found equiment component" + equipmentName);
			return null;
		}

		equipment.transform.parent = robot.transform;
		equipment.transform.localPosition = equipmentInitPositions[(int)pos];
		equipment.transform.localRotation = Quaternion.Euler(Global.equipmentInitRotation);

		equipmentComp.Init(DataManager.Instance.getDataRowByName(equipmentName), robot);

		return equipmentComp;

	}	
}
