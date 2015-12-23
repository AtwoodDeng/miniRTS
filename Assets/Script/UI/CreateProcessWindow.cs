using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateProcessWindow : MonoBehaviour {

	[SerializeField] List<CreateProcessUnit> units = new List<CreateProcessUnit>();

	void OnEnable()
	{
		EventManager.Instance.RegistersEvent(EventDefine.NewCreateController, OnCreateController );
	}

	void OnDisable()
	{
		EventManager.Instance.UnregistersEvent(EventDefine.NewCreateController, OnCreateController );

	}

	public void OnCreateController(Message msg)
	{
		AddProcessUnit(msg.GetMessage("Controller") as ObjectCreatManager.RobotCreateControllor);
	}

	public void AddProcessUnit(ObjectCreatManager.RobotCreateControllor rcc )
	{
		GameObject unitObj = Instantiate(Resources.Load("UI/Prefab/CreateProcessUnit") as GameObject) as GameObject;
		CreateProcessUnit unitCom = unitObj.GetComponent<CreateProcessUnit>();
		unitObj.transform.parent = this.transform;

		units.Add(unitCom);

		unitCom.Init(rcc,units.Count );
	}
}
