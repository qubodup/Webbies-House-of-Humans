using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
	public float moveSpeed = 3.0f;
	public float turnSpeed = 50.0f;

	private CharacterController ctrl = null;
	private Animator anim = null;

    void Start()
    {
		ctrl = GetComponent<CharacterController>();
		anim = GetComponentInChildren<Animator>();
	}

    void Update()
    {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		float turnRate = turnSpeed * Time.deltaTime * x * Mathf.Sign(y);
		float moveRate = moveSpeed * y;
		transform.Rotate(0, turnRate, 0);

		Vector3 moveDir = transform.forward;
		Vector3 movement = moveDir * moveRate;
		ctrl.SimpleMove(movement);

		anim.SetFloat("Forward", y);
	}
}
