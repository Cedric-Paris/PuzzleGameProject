using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Vector3Save {
	public float x;
	public float y;
	public float z;

	private Vector3Save(float x,float y,float z){
		this.x=x;
		this.y=y;
		this.z=z;
	}
	public static Vector3Save getVector3Save(Vector3 vecteur){
		return new Vector3Save(vecteur.x, vecteur.y, vecteur.z);
	}

	public Vector3 getVector3(){
		return new Vector3(x,y,z);
	}
	public override bool Equals (object obj)
	{
		if (obj == null)
			return false;
		if (ReferenceEquals (this, obj))
			return true;
		if (obj.GetType () != typeof(Vector3Save))
			return false;
		Vector3Save other = (Vector3Save)obj;
		return x == other.x && y == other.y && z == other.z;
	}
	

	public override int GetHashCode ()
	{
		unchecked {
			return x.GetHashCode () ^ y.GetHashCode () ^ z.GetHashCode ();
		}
	}
	
}
