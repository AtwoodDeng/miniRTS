using UnityEngine;
using System.Collections;

public class MtkButton : MBasicButton {

	[SerializeField] string name;
	[SerializeField] tk2dSprite sprite;
	[SerializeField] Color normalColor;
	[SerializeField] Color ClickColor;
	[SerializeField] Color HoverColor;

	void Awake()
	{
		if (sprite == null )
			sprite = GetComponent<tk2dSprite>();
		sprite.color = normalColor;
	}
	override public void OnClick()
	{
		sprite.color = ClickColor;
	}

	override public void OnPress()
	{

	}

	override public void OnHover()
	{
		sprite.color = HoverColor;
	}

	override public void OnExit()
	{
		sprite.color = normalColor;
	}

}
