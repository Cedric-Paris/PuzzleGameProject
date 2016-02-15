using UnityEngine;
using System.Collections;

/// <summary>
/// Teleport the player to the associated Teleporter
/// </summary>
public class Teleporter : Element {

	/// <summary>
	/// The associated teleporter.
	/// </summary>
	public Teleporter AssociatedTeleporter;

	/// <summary>
	/// The time before the teleporter can teleport the player again (without it the player is stuck in a teleporter loop.
	/// </summary>
	public int DeactivatedTime;

	/// <summary>
	/// A free teleporter waiting to be associated.
	/// </summary>
	private static Teleporter FreeTeleporter;



	/// <summary>
	/// Start this instance. If the Teleporter isn't paired, associate it to the free Teleporter (or make it the free teleporter
	/// </summary>
	void Start () {
		AssociateToFree();
	}
	
	void Update(){
		if(DeactivatedTime>0)
			DeactivatedTime --;
	}

	/// <summary>
	/// Associates the Teleporter to a free Teleporter, or make it the free teleporter if there isn't one
	/// </summary>
	public void AssociateToFree(){
		if (FreeTeleporter!=null && FreeTeleporter.Associate(this)){
				AssociatedTeleporter=FreeTeleporter;
				FreeTeleporter=null;
		}
		else {
			FreeTeleporter=this;			
		}
	}

	/// <summary>
	/// Associate the Teleporter to the one passed in argument
	/// </summary>
	/// <param name="t"> the teleporter that must be associated to this one.</param>
	public bool Associate(Teleporter t){
		if (AssociatedTeleporter == null){
			AssociatedTeleporter=t;
			return true;
		}
		return false;
	}




	/// <summary>
	/// Deactivate the teleporter during the specified time.
	/// </summary>
	/// <param name="time">Time.</param>
	public void Deactivate(int time){
		DeactivatedTime= Mathf.Abs(time);
	}

	/// <summary>
	/// Element Effect. An EffectTransformation object type is returned with the modification applied by the effect.
	/// The EffectTransformation contain the position of the associated teleporter.
	/// </summary>
	/// <param name="isTreated">true = the element is treated definitively. / false = the element effect may be requested again.</param>
	public override EffectTransformation Effect(bool isTreated = false)
	{
		if (AssociatedTeleporter!=null && DeactivatedTime<=0) {
			EffectTransformation eTransf = new EffectTransformation ();
			eTransf.newPosition = AssociatedTeleporter.transform.position;
			AssociatedTeleporter.Deactivate(10);
			return eTransf;
		}
		return new EffectTransformation(false);
	}


}
