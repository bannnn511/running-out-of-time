using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Attach this script to every object you want them to be attracted by the specified GameObject (Center of a planet for example)

[RequireComponent(typeof(Rigidbody2D))]
public class GravityTowardPoint : MonoBehaviour{

	

	public  GameObject CenterOfGravity;
	public float GravityForce = 1F;

	[Tooltip("If true, the object will always have his feet toward the Gravity Object. If not, the object can rotate in any direction")]
	public bool AlwaysRotateObjectFromCenter;

	private Rigidbody2D RB2D;


	private void Start() {
		RB2D = this.GetComponent<Rigidbody2D>();
		if (CenterOfGravity == null){ //If the gravity object is not set. Find the first object of the scene with the Tag "GravityObject"//
			if (CenterOfGravity = GameObject.FindGameObjectWithTag("GravityObject")){}
			else { Debug.LogWarning("Can't find Gravity Object for this GameObject. He will not be attracted by anything");}
		}
	
	}


	private void Update() {
		if (CenterOfGravity != null){
			RB2D.AddForce((CenterOfGravity.transform.position - transform.position) * GravityForce);

			if(AlwaysRotateObjectFromCenter == true){
				Vector3 dif = CenterOfGravity.transform.position - transform.position;
				float RotationZ = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler(0.0F, 0.0F, RotationZ + 90);
			}
		}
	}
}
