using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectCreatManager : Manager {

	public ObjectCreatManager() { s_Instance = this; }
	public static ObjectCreatManager Instance { get { return s_Instance; } }
	private static ObjectCreatManager s_Instance;

	public List<BodyNames> selectableBody;
	public List<WeaponNames> selectableWeapon;
	public List<PusherNames> selectablePusher;
	public List<ArmorNames> selectableArmor;
	public Transform BlueTeamInitPos;

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
			if ( GetPrecentage() > 1f )
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

			GameObject body = Instantiate((GameObject) Resources.Load(Global.ROBOT_PREFAB_PATH
					+"/"+Global.DataTableType.Body.ToString()
					+"/"+info.bodyName)) as GameObject;
			body.transform.parent = info.initPos;
			body.transform.localPosition = Vector3.zero;
			body.transform.localRotation = Quaternion.EulerAngles(0, 0, 0);
			Robot robotBody = body.GetComponent<Robot>();
			robotBody.Init(info, destination);

			return robotBody;
		}

		public float GetPrecentage()
		{
			return timer / CreateTime;
		}

		public RobotCreatInfo GetInfo()
		{
			return info;
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

	public void AddNewRobotCreator( RobotCreatInfo info )
	{
		info.teamColor = TeamColor.Blue;
		info.initPos = BlueTeamInitPos;
		RobotCreateControllor newRCC = new RobotCreateControllor(info);
		RCCList.Add(newRCC);

		Message msg = new Message();
		msg.AddMessage("Controller" , newRCC);
		EventManager.Instance.PostEvent(EventDefine.NewCreateController, msg);
	}
}
