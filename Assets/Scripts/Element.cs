using UnityEngine;
using System.Collections;

public class Element : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual EffectTransformation Effect(bool isTreated = false)//ici element sans effet
    {
        return new EffectTransformation();
    }
}
