using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ActionsPanel : MonoBehaviour {

	private Dictionary<string, int> actions = new Dictionary<string, int>();
	
	void Start () {
		actions.Add("UpArrow", 0);
		actions.Add("RightArrow", 0);
		actions.Add("DownArrow", 0);
		actions.Add("LeftArrow", 0);
		actions.Add("Jump", 0);
	}

	public void IncreaseAction(Text textAssociated)
	{
		string key = textAssociated.gameObject.name;
		actions[key] += 1;
		textAssociated.text = actions[key].ToString();
	}

	public void DecreaseAction(Text textAssociated)
	{
		string key = textAssociated.gameObject.name;
		actions[key] -= 1;
		if(actions[key] < 0)
			actions[key] = 0;
		textAssociated.text = actions[key].ToString();
	}
}
