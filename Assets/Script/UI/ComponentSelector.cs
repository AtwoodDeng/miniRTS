using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class ComponentSelector : MonoBehaviour {

	public EquipmentType type;
	[SerializeField] Text text;
	[SerializeField] Image back;
	[SerializeField] Button nextBotton;
	[SerializeField] Button lastBotton;
	public ObjCreateWindow objCreateWindow;
	int index;
	string name;
	const string DEFAULT_NAME = "No Found";

	public void Show(float time)
	{
		text.DOFade(1f, time);
		back.DOFade(1f, time);
		nextBotton.image.DOFade(1f, time);
		lastBotton.image.DOFade(1f, time);
	}

	public void Hide(float time)
	{
		text.DOFade(0f, time);
		back.DOFade(0f, time);
		nextBotton.image.DOFade(0f, time);
		lastBotton.image.DOFade(0f, time);

	}

	public void NextComponent()
	{
		index ++;
		UpdateContent();
	}

	public void LastComponent()
	{
		index --;
		if ( index < 0 )
			index = 0 ;
		UpdateContent();
	}

	void UpdateContent()
	{
		UpdateName();

		if(type == EquipmentType.Body && objCreateWindow != null )
			if ( name != "" )
				objCreateWindow.UpdateBody(name);
	}

	public void Reset()
	{
		index = 0;
		name = "";
	}

	public void UpdateName()
	{
		string showName = DEFAULT_NAME;
		switch(type)
		{
			case EquipmentType.Body:
				name = ObjectCreatManager.Instance.selectableBody[ index % ObjectCreatManager.Instance.selectableBody.Count].ToString();
				showName = name;
			break;
			case EquipmentType.Weapon:
				name = ObjectCreatManager.Instance.selectableWeapon[index % ObjectCreatManager.Instance.selectableWeapon.Count].ToString();
				showName = name;
			break;
			case EquipmentType.Pusher:
				name = ObjectCreatManager.Instance.selectablePusher[index % ObjectCreatManager.Instance.selectablePusher.Count].ToString();
				showName = name;
			break;
			case EquipmentType.Armor:
				name = ObjectCreatManager.Instance.selectableArmor[index % ObjectCreatManager.Instance.selectableArmor.Count].ToString();
				showName = name;
			break;
		}
		text.text = showName;
	}


	public string GetName()
	{
		return name;
	}
}
