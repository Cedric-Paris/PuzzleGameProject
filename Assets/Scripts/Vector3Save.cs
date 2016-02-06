using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Serializable Vector3.
/// </summary>
[Serializable]
public class Vector3Save {
	public float x;
	public float y;
	public float z;

	/// <summary>
	/// Initializes a new instance of the <see cref="Vector3Save"/> class.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	private Vector3Save(float x,float y,float z){
		this.x=x;
		this.y=y;
		this.z=z;
	}

	/// <summary>
	/// Gets the Vector3Save of a vector3.
	/// </summary>
	/// <returns>The vector3 save.</returns>
	/// <param name="vector3">Vector3.</param>
	public static Vector3Save getVector3Save(Vector3 vector3){
		return new Vector3Save(vector3.x, vector3.y, vector3.z);
	}

	/// <summary>
	/// Gets the vector3 with the value of the Vector3Save.
	/// </summary>
	/// <returns>The vector3.</returns>
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
