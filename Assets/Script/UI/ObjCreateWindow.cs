using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class ObjCreateWindow : MonoBehaviour {

	public ComponentSelector bodySelector;
	public ComponentSelector[] selectors;
	public Image back;
	public Button createButton;
	public float fadeTime = 1f;
	public bool isShow = true;

	void Awake()
	{
		bodySelector.objCreateWindow = this;
		bodySelector.type = EquipmentType.Body;
		foreach(ComponentSelector s in selectors)
		{
			s.objCreateWindow = this;
		}
		Hide();
		gameObject.SetActive(false);
	}

	public void Show()
	{
		this.gameObject.SetActive(true);
		bodySelector.Show(fadeTime);
		foreach(ComponentSelector s in selectors)
			s.Show(fadeTime);
		back.DOFade(1f, fadeTime );
		createButton.image.DOFade(1f , fadeTime );
		isShow = true;
	}

	public void Hide()
	{
		bodySelector.Hide(fadeTime);
		foreach(ComponentSelector s in selectors)
			s.Hide(fadeTime);
		StartCoroutine(setActiveFalse(fadeTime));
		back.DOFade(0, fadeTime);
		createButton.image.DOFade(0, fadeTime);
		isShow = false;
	}

	IEnumerator setActiveFalse(float delay)
	{
		yield return new WaitForSeconds(delay);
		this.gameObject.SetActive(false);
	}

	public void UpdateBody(string bodyName)
	{
		Robot.BodyParameter bp = DataManager.Instance.GetBodyParameter(bodyName);
		
		if (selectors.Length > 2)
		{
			selectors[0].type = bp.EquipmentI;
			selectors[1].type = bp.EquipmentII;
			selectors[2].type = bp.EquipmentIII;
		}
	}

	public void Create()
	{
		RobotCreatInfo info = new RobotCreatInfo();

		info.bodyName = (BodyNames) Enum.Parse( typeof(BodyNames), bodySelector.GetName());

		info.equipments = new EquipmentNames[3];
		for(int i = 0 ; i < 3 ; ++ i )
		if (selectors[i].GetName() != "")
			info.equipments[i] = (EquipmentNames) Enum.Parse( typeof(EquipmentNames), selectors[i].GetName());
		else
			info.equipments[i] = EquipmentNames.None;

		ObjectCreatManager.Instance.AddNewRobotCreator(info);
	}
}
