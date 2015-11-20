using UnityEngine;
using System.Collections;

public class LookForward : MonoBehaviour {

    /*
    Tout les objets dans une scene ont un Transform qui permet de manipuler la position,
    la rotation ou l'échelle (scale) -> Panneau supperieur droit
    */
    public Transform startPlace;
    public Transform endPlace;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit2D r;
        r = Physics2D.Linecast(startPlace.position, endPlace.position);
        //Trace une ligne imaginaire entre deux point. Si la ligne rencontreau moins un collider la fonction retourne un RaycastHit2D
        //Layer Mask est utilisé pour detecter des objects de maniere selective
        if (r.collider != null)
        {
            Debug.Log(r.collider.name);
            Destroy(r.collider);
        }
        Debug.DrawLine(startPlace.position, endPlace.position, Color.green);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        Debug.Log(target.gameObject.GetType());
    }
}
