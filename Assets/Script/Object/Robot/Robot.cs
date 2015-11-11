using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Robot : MonoBehaviour {

	[System.Serializable]
	public struct BodyParameter{
		public string Name;
		public float Health;
		public float TurnAngleSpeed;
		public float MoveSpeed;
		public float Power;
		public float CreateTime;
	}
	[SerializeField] BodyParameter parameter;
	[SerializeField] Animator animator;
	// [SerializeField] Transform destination;

	public NavMeshAgent agent{
		get{return _agent;}
	}
	NavMeshAgent _agent;

	public float health;

	//TODO: Public for test
	public TeamColor teamColor;
	// public TeamColor teamColor{
	// 	get {return _teamColor;}
	// }
	// TeamColor _teamColor;

	public List<Equipment> equipmentList = new List<Equipment>();

	List<Robot> enemyList = new List<Robot>();

	public bool isMeetEnemy = false;
	public bool isAttack = false;
	public bool isDestory = false;

	public RobotHealth robotHealth;

	void Update(){
		health = robotHealth.health;
	}

	void OnEnable()
	{
		InitBehaviors();
		SetDestination();
	}

	bool isInit = false;
	public virtual void Init(RobotCreatInfo info , Vector3 destination)
	{
		BodyParameter _parameter = DataManager.Instance.GetBodyParameter(info.bodyName);

		//agent
		if ( _agent == null )
			_agent = GetComponent<NavMeshAgent>();

		//init state machine behaviors & navigate agent
		InitBehaviors();
		SetDestination();

		_agent.speed = parameter.MoveSpeed;

		//health TODO: change the health
		robotHealth = new RobotHealth();
		robotHealth.Init(this, parameter.Health);

		//parameter
		parameter = _parameter;
		//teamcolor
		teamColor = info.teamColor;

		isInit = true;

		//init equipment
		InitEquitments(info);

	}

	public void InitBehaviors()
	{
		if ( animator == null)
			animator = GetComponent<Animator>();
		RobotBehavior[] behaviors = animator.GetBehaviours<RobotBehavior>();
		foreach(RobotBehavior behavior in behaviors)
		{
			behavior.Rob = this; 
		}
	}

	public void SetDestination()
	{
		_agent.destination = LogicManager.Instance.GetDestination(this.teamColor);
	}


	public void EnterEnemy( Robot enemy_rob )
	{
		Debug.Log(name + "enter enemy" + enemy_rob.name);
		isMeetEnemy = true;
		enemyList.Add(enemy_rob);
	}

	public void ExitEnemy( Robot enemy_rob)
	{
		enemyList.Remove(enemy_rob);
		if ( enemyList.Count <= 0 )
			isMeetEnemy = false;	
	}	

	public virtual void AttackBegin()
	{
		foreach(Equipment e in equipmentList)
		{
			e.AttackBegin(this);
		}

	}

	public virtual void AttackUpdate()
	{
		foreach(Equipment e in equipmentList)
		{
			e.AttackUpdate(this);
		}
	}

	public virtual void AttackEnd()
	{
		foreach(Equipment e in equipmentList)
		{
			e.AttackEnd(this);
		}

	}

	public virtual void CauseDamage(Damage dmg)
	{
		Damage c_dmg = dmg;
		foreach(Equipment e in equipmentList)
		{
			c_dmg = e.RecieveDamage(c_dmg);
		}
		if ( robotHealth.RecieveDamage(c_dmg) )
		{
			Destory();
		}
	}

	public virtual void Destory()
	{
		isDestory = true;
	}

	public virtual bool RotateTo(Vector3 target)
	{
		Vector3 toward = target - transform.position;
		float angle = Vector3.Dot(Vector3.up , Vector3.Cross( toward , GetFaceToward() ) );
		if ( Mathf.Abs( angle ) < 0.1f )
			return true;
		transform.Rotate( Vector3.up , parameter.TurnAngleSpeed * ((angle>0)? -1f : 1f) );
		return false;
	}

	public virtual Vector3 GetFaceToward()
	{
		return transform.forward;
	}

	public Robot GetEnemy()
	{
		return GetNeastEnemy();
	}

	public Robot GetFirstEnemy()
	{
		while ( enemyList.Count > 0 )
		{
			if ( !enemyList[0].isDestory )
				return enemyList[0];
			else
				enemyList.RemoveAt(0);
		}
		return null;
	}
	public Robot GetNeastEnemy()
	{
		// if ( enemyList.Count <= 0 )
		// 	return null;

		// Robot res = enemyList[0];
		// foreach(Robot r in enemyList)
		// {
		// 	if ( Global.GetDistance(r.transform.position, transform.position)
		// 		< Global.GetDistance(res.transform.position, transform.position))
		// 		res = r;
		// }

		while ( enemyList.Count > 0 )
		{
			Robot res = enemyList[0];
			foreach(Robot r in enemyList)
			{
				if ( Global.GetDistance(r.transform.position, transform.position)
					< Global.GetDistance(res.transform.position, transform.position))
					res = r;
			}

			if ( res.isDestory )
				enemyList.Remove(res);
			else
				return res;
			
		}


		return null;
	}

	virtual public void InitEquitments(RobotCreatInfo info )
	{
		Debug.Log("init Equipment");
		//Create Weapon
		// GameObject weapon = Instantiate((GameObject) Resources.Load(Global.ROBOT_PREFAB_PATH
		// 	+"/"+Global.DataTableType.Weapon.ToString()
		// 	+"/"+info.equipments[0])) as GameObject;
		// Weapon robotWeapon =null;
		// if (weapon!=null)
		// {
		// 	weapon.transform.parent = this.transform;
		// 	weapon.transform.localPosition = Global.equipmentInitPosition[(int)Global.DataTableType.Weapon];
		// 	weapon.transform.localRotation = Quaternion.Euler(Global.equipmentInitRotation);
		// 	robotWeapon = weapon.GetComponent<Weapon>();
		// 	robotWeapon.Init(DataManager.Instance.GetWeaponParameter(info.equipments[0]), this);
		// }

		// // Create Pusher
		// GameObject pusher = Instantiate((GameObject) Resources.Load(Global.ROBOT_PREFAB_PATH
		// 	+"/"+Global.DataTableType.Pusher.ToString()
		// 	+"/"+info.equipments[1])) as GameObject;
		// Pusher robotPusher =null;
		// if (pusher!=null)
		// {
		// 	pusher.transform.parent = this.transform;
		// 	pusher.transform.localPosition = Global.equipmentInitPosition[(int)Global.DataTableType.Pusher];
		// 	pusher.transform.localRotation = Quaternion.Euler(Global.equipmentInitRotation);
		// 	robotPusher = pusher.GetComponent<Pusher>();
		// 	robotPusher.Init(DataManager.Instance.GetPusherParameter(info.equipments[1]), this);
		// }

		// // Create Pusher
		// GameObject armor = Instantiate((GameObject) Resources.Load(Global.ROBOT_PREFAB_PATH
		// 	+"/"+Global.DataTableType.Armor.ToString()
		// 	+"/"+info.equipments[2])) as GameObject;
		// Pusher robotPusher =null;
		// if (pusher!=null)
		// {
		// 	pusher.transform.parent = this.transform;
		// 	pusher.transform.localPosition = Global.equipmentInitPosition[(int)Global.DataTableType.Armor];
		// 	pusher.transform.localRotation = Quaternion.Euler(Global.equipmentInitRotation);
		// 	robotPusher = pusher.GetComponent<Armor>();
		// 	robotPusher.Init(DataManager.Instance.GetPusherParameter(info.equipments[2]), this);
		// }

		//add equipment to list
		// if (robotWeapon != null)
		// 	this.equipmentList.Add(robotWeapon);

		EquipmentFactory.EquipmentCreatePosition[] posList =
		{
			EquipmentFactory.EquipmentCreatePosition.Up,
			EquipmentFactory.EquipmentCreatePosition.LeftRight,
			EquipmentFactory.EquipmentCreatePosition.Down,
		};

		for(int i = 0; i < info.equipments.Length && i < posList.Length ; ++i )
		{
			string equipmentName = info.equipments[i];
			EquipmentFactory.EquipmentCreatePosition pos = posList[i];
			equipmentList.Add(EquipmentFactory.CreateEquipment(equipmentName, this , pos ));
		}
	}

	virtual public float GetCreateTime(RobotCreatInfo info)
	{
		float res = DataManager.Instance.GetBodyParameter(info.bodyName).CreateTime
						+ DataManager.Instance.GetWeaponParameter(info.equipments[0]).CreateTime
						+ DataManager.Instance.GetPusherParameter(info.equipments[1]).CreateTime
						+ DataManager.Instance.GetArmorParameter(info.equipments[2]).CreateTime;
		return res;
	}
}
