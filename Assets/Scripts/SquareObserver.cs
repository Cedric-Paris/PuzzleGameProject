using UnityEngine;
using System.Collections;

public class SquareObserver : MonoBehaviour {

    public Element ElementDetected
    {
        get { return mElementDetected; }
        set
        {
			Debug.Log ("Square dect");
            mElementDetected = value;
            isTreated = false;
        }
    }
    private Element mElementDetected;

    public bool isTreated;//true si l'effet associé a cet element a été traité

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Component com = other.gameObject.GetComponent<Element>();//Recuperation de l'element associé a la case touchée (null si vide)
        Element element = com as Element;
        if (element != null)
            ElementDetected = element;
    }
}
