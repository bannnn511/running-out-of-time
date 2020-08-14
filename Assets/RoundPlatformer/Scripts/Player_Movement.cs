using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player_Movement : MonoBehaviour{


	[Tooltip("This is the gameobject were the player is attracted to. If nothing, the player will fly, you can change that gameobject on Runtime")]
	public GameObject CenterOfGravity;
	public float GravityForce;
	
	public float PlayerSpeed;
	public float MaxSpeed;
	public float JumpSpeed;

	[Tooltip("For Double Jump or more. Set to 1 for a single jump")]
	public int NumberOfJump;
	private int JumpCount;
	private bool IsGrounded;
	private float distToGround;
	private Collider2D collider;
	public LayerMask GroundedMask;
	
	private Rigidbody2D RB2B;
	

	private SpriteRenderer PlayerSpriteRenderer;
	private Animator anim;
	private float AngularSpeedLimitation;



	void Start() {
		RB2B = GetComponent<Rigidbody2D>();
		PlayerSpriteRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();

		collider = GetComponent<Collider2D>();
		distToGround = collider.bounds.extents.y;
		JumpCount = NumberOfJump;
	}



	void Update() {
		On_PlayerMovement();
		On_PlayerJump();
		MirrorAnimationPlayer();
		ResetNumberOfJump();
		CheckIfPlayerGrounded();
		GravityDrag();

		Debug.DrawRay(this.transform.position, -transform.up, Color.green);
	}




	//This function calculate the speed limitation of the player depending of how far he is from the center of Gravity.
	//This will prevent the player from flying if he goes too fast, too close from the center of gravity 
	private float CalculateAngularSpeedLimitation(){

		if(CenterOfGravity != null){
			float speedLimitation;
			float distance;

			distance = Vector3.Distance(transform.position, CenterOfGravity.transform.position);
			distance = distance/10;

			speedLimitation = Mathf.Lerp(0.5F, 2F, distance);
			speedLimitation = speedLimitation/5;

			return speedLimitation;
			
		}
		else{ return 1; } //If the player don't have gravity center, no speed limitation is set.
	}


	private void GravityDrag(){
		if(CenterOfGravity != null){
			RB2B.AddForce((CenterOfGravity.transform.position - transform.position) * GravityForce);
			Vector3 dif = CenterOfGravity.transform.position - transform.position;
			float RotationZ = Mathf.Atan2(dif.y , dif.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0.0F, 0.0F, RotationZ + 90);
		}
	}





	private void On_PlayerMovement(){
		if(Input.GetAxis("Horizontal") != 0){
			Vector2 localvelocity;
			localvelocity = transform.InverseTransformDirection(RB2B.velocity);
			localvelocity.x = Input.GetAxis("Horizontal") * Time.deltaTime * PlayerSpeed * 100 * CalculateAngularSpeedLimitation();
			RB2B.velocity = transform.TransformDirection(localvelocity);

			anim.SetBool("PlayerMoving", true);
		}
		else { //Slow down the player when no pressure on the Horizontal Axis (For more responcive controls).
			
			Vector2 localvelocity;
			localvelocity = transform.InverseTransformDirection(RB2B.velocity);
			localvelocity.x = localvelocity.x * 0.5F;
			RB2B.velocity = transform.TransformDirection(localvelocity);

			anim.SetBool("PlayerMoving", false);
		}
	}

	private void On_PlayerJump(){
		if(Input.GetButtonDown("Jump")){
			if(JumpCount != 0){
				Vector2 localvelocity;
				localvelocity = transform.InverseTransformDirection(RB2B.velocity);
				localvelocity.y = 0;
				RB2B.velocity = transform.TransformDirection(localvelocity);
				JumpCount --;
				RB2B.AddRelativeForce(new Vector2(0,1) * JumpSpeed * 10, ForceMode2D.Impulse);
			}
		}
	}


	private void CheckIfPlayerGrounded(){
		
		if (isGrounded()) {
			IsGrounded = true;
			anim.SetBool("PlayerJumping", false);
		}
		else {
			IsGrounded = false;
			anim.SetBool("PlayerJumping", true);
		}
	}


	private bool isGrounded(){
		if(Physics2D.Raycast(transform.position, -transform.up, 1F, GroundedMask, -Mathf.Infinity, Mathf.Infinity)){
			return true;
		}
		return false;
	}



	//Below this point, The script is doing normal stuff (like animation)//


	private void MirrorAnimationPlayer(){
		Vector2 localVelocity = transform.InverseTransformDirection(RB2B.velocity);

		if(localVelocity.x > 0.5F){
			if(PlayerSpriteRenderer.flipX == false){
				PlayerSpriteRenderer.flipX = true;
			}
		}
		
		else if (localVelocity.x < -0.5){
			if(PlayerSpriteRenderer.flipX == true){
				PlayerSpriteRenderer.flipX = false;
			}
		}
		
	}
	


	private void ResetNumberOfJump(){
		if(JumpCount < NumberOfJump){
			if(IsGrounded){
				JumpCount = NumberOfJump;
			}
		}
	}
}
