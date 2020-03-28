using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
	public float walkSpeed = 2.0f;
	public float runningSpeed = 4.0f;
	public float turnSpeed = 50.0f;
	public float interactDuration = 1.0f;
	private float interactStartTime = -1.0f;

	private CharacterController ctrl = null;
	private Animator anim = null;

	public bool IsInteracting => Time.time < interactStartTime + interactDuration;

    void Start()
    {
		ctrl = GetComponent<CharacterController>();
		anim = GetComponentInChildren<Animator>();
	}

    void Update()
    {
		float forward = 0.0f;
		if (!IsInteracting)
		{
			float x = Input.GetAxis("Horizontal");
			float y = Input.GetAxis("Vertical");
			forward = y;

			float moveSpeed = walkSpeed;
			if (Input.GetKey(KeyCode.LeftShift))
			{
				forward *= 2;
				moveSpeed = runningSpeed;
			}

			float turnRate = turnSpeed * Time.deltaTime * x * Mathf.Sign(y);
			float moveRate = moveSpeed * y;
			transform.Rotate(0, turnRate, 0);

			Vector3 moveDir = transform.forward;
			Vector3 movement = moveDir * moveRate;
			ctrl.SimpleMove(movement);

			if (Input.GetKeyDown(KeyCode.E))
			{
				anim.SetTrigger("Interact");
				interactStartTime = Time.time;
			}
		}

		anim.SetFloat("Forward", forward);
	}
}
