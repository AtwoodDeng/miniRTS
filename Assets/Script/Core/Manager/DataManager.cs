using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class DataManager : Manager {

	public DataManager() { s_Instance = this; }
	public static DataManager Instance { get { return s_Instance; } }
	private static DataManager s_Instance;

	List<DataTable> robotData = new List<DataTable>();
	public List<List<string>> equipmentList = new List<List<string>>();

	void Awake()
	{
		s_Instance = this;


		var helper = new XMLHelper( Global.ROBOT_DATA_PATH);
		for(int i = 0 ; i < (int)Global.DataTableType.None ; ++ i )
		{
			DataTable table = helper.ReadSheet( ((Global.DataTableType)i).ToString() );
			robotData.Add(table);

			List<string> equipmentNames = new List<string>();
			for(int j = 0 ; j < table.rows.Count ; ++ j)
				equipmentNames.Add(table.rows[j].row.Select("Name").ToString());
			equipmentList.Add(equipmentNames);
			// Debug.Log( ((Global.DataTableType)i).ToString() + " " + equipmentNames[0]);
		}
		// ReadXLS(Application.dataPath+"/RobotBody.xls");
	}

	public DataRow getDataRowByType(Global.DataTableType type , string Name )
	{
		DataRow row = robotData[(int)type].Select(Name);
		return row;
	}

	public DataRow getDataRowByName( string Name )
	{
		Global.DataTableType type = Global.NameToType(Name);
		return getDataRowByType(type,Name);
	}

	public Robot.BodyParameter GetBodyParameter(string bodyName, DataRow row = null )
	{
		if ( row == null )
			row = getDataRowByType(Global.DataTableType.Body,bodyName);
		return GetBodyParameter(row);
	}

	public Robot.BodyParameter GetBodyParameter(DataRow row )
	{
		Robot.BodyParameter res = new Robot.BodyParameter();
		if (row == null) return res;
		res.Name = row.Select("Name").ToString();
		res.Health = float.Parse(row.Select("Health").ToString());
		res.Power = float.Parse(row.Select("Power").ToString());
		res.MoveSpeed = float.Parse(row.Select("MoveSpeed").ToString());
		res.TurnAngleSpeed = float.Parse(row.Select("TurnAngleSpeed").ToString());
		res.Armor = float.Parse(row.Select("Armor").ToString());
		res.CreateTime = float.Parse(row.Select("CreateTime").ToString());

		res.EquipmentI = res.EquipmentII = res.EquipmentIII = EquipmentType.None; 
		if (row.Select("EquipmentI").ToString() != "")
			res.EquipmentI = (EquipmentType)Enum.Parse(typeof(EquipmentType),row.Select("EquipmentI").ToString());	
		if (row.Select("EquipmentII").ToString() != "")
			res.EquipmentII = (EquipmentType)Enum.Parse(typeof(EquipmentType),row.Select("EquipmentII").ToString());
		if (row.Select("EquipmentIII").ToString() != "")
			res.EquipmentIII = (EquipmentType)Enum.Parse(typeof(EquipmentType),row.Select("EquipmentIII").ToString());
		
		return res;
	}

	public Weapon.WeaponParameter GetWeaponParameter(string weaponName, DataRow row = null )
	{
		if ( row == null )
			row = getDataRowByType(Global.DataTableType.Weapon,weaponName);
		return GetWeaponParameter(row);
	}

	public Weapon.WeaponParameter GetWeaponParameter(DataRow row )
	{
		Weapon.WeaponParameter res = new Weapon.WeaponParameter();
		if (row == null) return res;
		res.Name = row.Select("Name").ToString();
		res.Damage = float.Parse(row.Select("Damage").ToString());
		res.ShootSpeed = float.Parse(row.Select("ShootSpeed").ToString());
		res.ShootCoolDown = float.Parse(row.Select("ShootCoolDown").ToString());
		res.DamageType = (DamageType)Enum.Parse(typeof(DamageType),row.Select("DamageType").ToString());
		res.BulletName = row.Select("BulletName").ToString();
		res.CreateTime = float.Parse(row.Select("CreateTime").ToString());
		res.BulletPrefab = Resources.Load(Global.BULLET_PREFAB_PATH + res.BulletName) as GameObject;
		res.Range = float.Parse(row.Select("Range").ToString());
		res.PowerCost = float.Parse(row.Select("PowerCost").ToString());

		//parameters
		if (row.Select("PNumber").ToString() != "")
		res.PNumber = int.Parse(row.Select("PNumber").ToString());
		if (row.Select("PPercentage").ToString() != "")
		res.PPercentage = float.Parse(row.Select("PPercentage").ToString());
		if (row.Select("PTime").ToString() != "")
		res.PTime = float.Parse(row.Select("PTime").ToString());
		if (row.Select("PRange").ToString() != "")
		res.PRange = float.Parse(row.Select("PRange").ToString());
		if (row.Select("PDamage").ToString() != "")
		res.PDamage = float.Parse(row.Select("PDamage").ToString());
		if (row.Select("Parameter1").ToString() != "")
		res.Parameter1 = row.Select("Parameter1").ToString();
		if (row.Select("Parameter2").ToString() != "")
		res.Parameter2 = row.Select("Parameter1").ToString();

		//info
		res.Info = row.Select("Info").ToString();

		return res;
	}

	public Pusher.PusherParameter GetPusherParameter(string pusherName, DataRow row = null )
	{
		if ( row == null )
			row = getDataRowByType(Global.DataTableType.Pusher,pusherName);
		return GetPusherParameter(row);
	}

	public Pusher.PusherParameter GetPusherParameter(DataRow row)
	{
		Pusher.PusherParameter res = new Pusher.PusherParameter();
		if (row == null) return res;
		res.Name = row.Select("Name").ToString();
		res.CreateTime = float.Parse(row.Select("CreateTime").ToString());
		res.MoveSpeed = float.Parse(row.Select("MoveSpeed").ToString());
		res.TurnAngleSpeed = float.Parse(row.Select("TurnAngleSpeed").ToString());

		//parameters
		if (row.Select("PNumber").ToString() != "")
		res.PNumber = int.Parse(row.Select("PNumber").ToString());
		if (row.Select("PPercentage").ToString() != "")
		res.PPercentage = float.Parse(row.Select("PPercentage").ToString());
		if (row.Select("PTime").ToString() != "")
		res.PTime = float.Parse(row.Select("PTime").ToString());
		if (row.Select("PRange").ToString() != "")
		res.PRange = float.Parse(row.Select("PRange").ToString());
		if (row.Select("PDamage").ToString() != "")
		res.PDamage = float.Parse(row.Select("PDamage").ToString());
		if (row.Select("Parameter1").ToString() != "")
		res.Parameter1 = row.Select("Parameter1").ToString();
		if (row.Select("Parameter2").ToString() != "")
		res.Parameter2 = row.Select("Parameter1").ToString();

		//info
		res.Info = row.Select("Info").ToString();
		return res;
	}
	public Armor.ArmorParameter GetArmorParameter(string armorName, DataRow row = null )
	{
		if ( row == null )
			row = getDataRowByType(Global.DataTableType.Armor,armorName);
		return GetArmorParameter(row);
	}

	public Armor.ArmorParameter GetArmorParameter(DataRow row = null)
	{
		Armor.ArmorParameter res = new Armor.ArmorParameter();
		if (row == null) return res;
		res.Name = row.Select("Name").ToString();
		res.CreateTime = float.Parse(row.Select("CreateTime").ToString());
		res.Armor = float.Parse(row.Select("Armor").ToString());

				//parameters
		if (row.Select("PNumber").ToString() != "")
		res.PNumber = int.Parse(row.Select("PNumber").ToString());
		if (row.Select("PPercentage").ToString() != "")
		res.PPercentage = float.Parse(row.Select("PPercentage").ToString());
		if (row.Select("PTime").ToString() != "")
		res.PTime = float.Parse(row.Select("PTime").ToString());
		if (row.Select("PRange").ToString() != "")
		res.PRange = float.Parse(row.Select("PRange").ToString());
		if (row.Select("PDamage").ToString() != "")
		res.PDamage = float.Parse(row.Select("PDamage").ToString());
		if (row.Select("Parameter1").ToString() != "")
		res.Parameter1 = row.Select("Parameter1").ToString();
		if (row.Select("Parameter2").ToString() != "")
		res.Parameter2 = row.Select("Parameter1").ToString();

		//info
		res.Info = row.Select("Info").ToString();
		return res;
	}
}
