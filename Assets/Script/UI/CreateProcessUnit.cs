using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateProcessUnit : MonoBehaviour {

	ObjectCreatManager.RobotCreateControllor rcc; 

	[SerializeField] Image body;
	[SerializeField] Image[] equipments;
	[SerializeField] Image percentage;
	[SerializeField] Image back;

	public void Init(ObjectCreatManager.RobotCreateControllor _rcc , int order )
	{
		rcc = _rcc;
		Debug.Log("CreateProcessUnit Init " + Global.UI_ROBOT_PATH + rcc.GetInfo().bodyName.ToString());
		body.sprite = Resources.Load<Sprite>(Global.UI_ROBOT_PATH + rcc.GetInfo().bodyName.ToString());
		for(int i = 0 ; i < 3 ; ++ i )
		{
			equipments[i].sprite = Resources.Load<Sprite>(Global.UI_ROBOT_PATH + rcc.GetInfo().equipments[i].ToString());
		}
		equipments[3].sprite = equipments[1].sprite;

		Vector2 pos = back.rectTransform.anchoredPosition;
		pos.x = 60f + order * 100f;

		back.rectTransform.anchoredPosition = pos;
	}

	void Update()
	{
		float p = rcc.GetPrecentage();
		Vector2 size = percentage.rectTransform.sizeDelta;
		size.y = 100f * p;
		percentage.rectTransform.sizeDelta = size;
	}

	void SetPosition()
	{

	}

}
