using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
	public Button ShopButton;
	public Renderer RedGate;
	public Renderer BlueGate;
	void Start()
	{
		if(ShopButton)
		{
			Button btn = ShopButton.GetComponent<Button>();
			btn.onClick.AddListener(TaskOnClick);
		}
		if(BlueGate!=null && RedGate != null)
		{
			BlueGate.enabled = true;
			RedGate.enabled = true;
		}
		else
		{
			Debug.Log("Blue or Red Gates error - not found or = null");
		}
		
		
	}

	void TaskOnClick()
	{
		Debug.Log("You have clicked the button!");
		if (DragonGemBehavior.RgemCount >= 200)
		{
			BlueGate.enabled = false;
		}
		if (DragonGemBehavior.BgemCount >= 200)
		{
			RedGate.enabled = false;
		}
	}
}
