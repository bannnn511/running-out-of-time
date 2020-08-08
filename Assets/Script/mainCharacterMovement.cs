using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCharacterMovement : MonoBehaviour
{
	public float moveSpeed = 5f;
	Animator animator;
	Rigidbody2D rb;
	Vector2 movement;
	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

		animator.SetFloat("Horizontal", movement.x);
		animator.SetFloat("Vertical", movement.y);
		animator.SetFloat("Speed", movement.sqrMagnitude);
	}

	private void FixedUpdate()
	{
		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
	}
}
