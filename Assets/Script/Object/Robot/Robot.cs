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
		public float Armor;
		public EquipmentType EquipmentI;
		public EquipmentType EquipmentII;
		public EquipmentType EquipmentIII;
	}
	public BodyParameter parameter;
	[SerializeField] Animator animator;
	// Transform destination;

	public NavMeshAgent agent{
		get{return _agent;}
	}
	NavMeshAgent _agent;

	//TODO: Make it visible by visual
	[SerializeField] float health;
	[SerializeField] float power;
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
	public bool canAttack = false;

	public RobotHealth robotHealth = new RobotHealth();
	public RobotEffect robotEffect = new RobotEffect();

	void Update(){
		//TODO: remove the showing mechanism
		health = robotHealth.health;
		power = robotHealth.power;
		// if (enemyList.Count <= 0 )
		// 	isMeetEnemy = false;

		robotEffect.UpdateEffect();
	}

	void OnEnable()
	{
		InitBehaviors();
		SetDestination();
	}

	public virtual void InitParameterI(ref BodyParameter _parameter)
	{
		foreach(Equipment e in equipmentList)
			e.InitParameterI(ref _parameter);

	}

	public virtual void InitParameterII(ref BodyParameter _parameter)
	{
		foreach(Equipment e in equipmentList)
			e.InitParameterII(ref _parameter);
	}

	bool isInit = false;
	public virtual void Init(RobotCreatInfo info , Vector3 destination)
	{
		//init equipment
		InitEquitments(info);

		//init the parameter
		BodyParameter _parameter = DataManager.Instance.GetBodyParameter(info.bodyName.ToString());
		InitParameterI(ref _parameter);
		InitParameterII(ref _parameter);
		parameter = _parameter;

		//agent
		if ( _agent == null )
			_agent = GetComponent<NavMeshAgent>();

		//init state machine behaviors & navigate agent
		InitBehaviors();
		SetDestination();

		_agent.speed = parameter.MoveSpeed;

		//health TODO: change the health
		robotHealth.Init(this, parameter.Health , parameter.Power);

		//effect
		robotEffect.Init(this);

		//teamcolor
		teamColor = info.teamColor;

		isInit = true;


	}

	public string GetName()
	{
		string res = parameter.Name;
		foreach(Equipment e in equipmentList)
		{
			res += e.name;
		}

		return res;
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
		if ( _agent != null )
			_agent.destination = LogicManager.Instance.GetDestination(this.teamColor);
	}


	public void EnterEnemy( Robot enemy_rob )
	{
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
		if ( enemyList.Count <= 0 )
			isMeetEnemy = false;

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

	public virtual void RecieveDamage(Damage dmg)
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

		//get effect
		if (dmg.effectList != null && dmg.effectList.Count > 0 )
		{
			foreach(Effect e in dmg.effectList)
			{
				robotEffect.RecieveEffect(e);
			}
		}
	}

	public virtual void Destory()
	{
		isDestory = true;

		Destroy(this.gameObject);
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
		ClearEnemyList();
		return GetNeastEnemy();
	}

	public void ClearEnemyList()
	{
		for(int i = enemyList.Count - 1  ; i >= 0 ; i--)
			if (enemyList[i] == null)
				enemyList.RemoveAt(i);
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
		// Debug.Log("init Equipment");

		//TODO: make the position selection not in an unchangeable function
		//the positions are now defined in the function 
		EquipmentFactory.EquipmentCreatePosition[] posList =
		{
			EquipmentFactory.EquipmentCreatePosition.Up,
			EquipmentFactory.EquipmentCreatePosition.LeftRight,
			EquipmentFactory.EquipmentCreatePosition.Down,
		};

		for(int i = 0; i < info.equipments.Length && i < posList.Length ; ++i )
		{
			string equipmentName = info.equipments[i].ToString();
			EquipmentFactory.EquipmentCreatePosition pos = posList[i];
			equipmentList.Add(EquipmentFactory.CreateEquipment(equipmentName, this , pos ));
		}
	}

	virtual public float GetCreateTime(RobotCreatInfo info)
	{
		float res = DataManager.Instance.GetBodyParameter(info.bodyName.ToString()).CreateTime
						+ DataManager.Instance.GetWeaponParameter(info.equipments[0].ToString()).CreateTime
						+ DataManager.Instance.GetPusherParameter(info.equipments[1].ToString()).CreateTime
						+ DataManager.Instance.GetArmorParameter(info.equipments[2].ToString()).CreateTime;
		return res;
	}

	public float GetSpeed()
	{
		return robotEffect.GetSpeed( parameter.MoveSpeed );
	}

	public void RecieveEffect(Effect e)
	{
		robotEffect.RecieveEffect(e);
	}

	public bool UsePower(float power)
	{
		return robotHealth.UsePower(power);
	}

	virtual public float GetWeaponPowerCost(float basePower)
	{
		return basePower;
	}

}
