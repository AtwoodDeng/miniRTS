using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectCreatManager : Manager {

	public ObjectCreatManager() { s_Instance = this; }
	public static ObjectCreatManager Instance { get { return s_Instance; } }
	private static ObjectCreatManager s_Instance;


	// [System.SerializableAttribute]
	public class RobotCreateControllor
	{
		Robot robot;
		public RobotCreateControllor(RobotCreatInfo _info)
		{
			timer = 0;
			info = _info;
		}
		float timer=0;
		RobotCreatInfo info;

		float CreateTime{
			get{
			if ( _createTime < 0 )
				_createTime = GetCreateTime();
			return _createTime;
			}
		}
		float _createTime = -1;

		public void Update(float dtime)
		{
			timer += dtime;
			if ( robot == null )
			{
				robot = CreateRobot();
				robot.gameObject.SetActive(false);
			}
			if ( timer > CreateTime)
			{
				timer = 0;
				robot.gameObject.SetActive(true);
				robot = null;
			}
		}

		public float GetCreateTime()
		{
			//if info.bodyName == ''
			// if is basic
			return robot.GetCreateTime(info);
		}

		Robot CreateRobot()
		{
			Vector3 destination = Vector3.zero;

			Debug.Log("Begin to Create ...");
			// Create Body
			GameObject body = Instantiate((GameObject) Resources.Load(Global.ROBOT_PREFAB_PATH
					+"/"+Global.DataTableType.Body.ToString()
					+"/"+info.bodyName)) as GameObject;
			Debug.Log("body path "+ Global.ROBOT_PREFAB_PATH
					+"/"+Global.DataTableType.Body.ToString()
					+"/"+info.bodyName);
			body.transform.parent = info.initPos;
			body.transform.localPosition = Vector3.zero;
			Robot robotBody = body.GetComponent<Robot>();
			robotBody.Init(info, destination);

			return robotBody;

			// // Create Weapon
			// GameObject weapon = Instantiate((GameObject) Resources.Load(Global.ROBOT_PREFAB_PATH
			// 	+"/"+Global.DataTableType.Weapon.ToString()
			// 	+"/"+info.weaponName)) as GameObject;
			// Weapon robotWeapon =null;
			// if (weapon!=null)
			// {
			// 	weapon.transform.parent = body.transform;
			// 	weapon.transform.localPosition = Global.equipmentInitPosition[(int)Global.DataTableType.Weapon];
			// 	weapon.transform.localRotation = Quaternion.Euler(Global.equipmentInitRotation);
			// 	robotWeapon = weapon.GetComponent<Weapon>();
			// 	robotWeapon.Init(DataManager.Instance.GetWeaponParameter(info.weaponName), robotBody);
			// }


			// //add equipment to list
			// if (robotWeapon != null)
			// 	robotBody.equipmentList.Add(robotWeapon);
		}
	}

	//For test
	[SerializeField] RobotCreatInfo[] robotInfos;

	List<RobotCreateControllor> RCCList = new List<RobotCreateControllor>();

	void Awake()
	{
		Init();
	}

	public void Init()
	{
		foreach(RobotCreatInfo info in robotInfos)
		{
			RCCList.Add(new RobotCreateControllor(info));

		}
	}

	void Update()
	{
		foreach(RobotCreateControllor rcc in RCCList)
		{
			rcc.Update(Time.deltaTime);
		}
	}
}
