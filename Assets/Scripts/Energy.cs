using UnityEngine;
using System.Collections;

public class Energy : MonoBehaviour {

	public float ressource = 60;
	public float maxRessource = 80;
	public float ressourceBurnRate = 10;
	public float ressourceIncrement = 10;
	
	private Player player;
	
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!player)
			return;
		if (ressource > 9) {
			//décrémenter les ressources en fonction d'une variable (temps/distance/changement de case) a choisir plus tard.
		} else {
			//GameOver: Appelle d'une methode destruction qui effectuera la destruction en rapport au thème choisis (pattern stratégie?)
			player.Explode();
		}
	}

	public void AddEnergy() {
		ressource += ressourceIncrement;
	}
}
