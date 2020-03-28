using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimations : MonoBehaviour
{
	public enum LegState
	{
		Set,
		Raising,
		Lowering,
	}

	[Serializable]
	public class Leg
	{
		public bool firstSet = true;
		public LegState state = LegState.Set;

		public Transform root = null;
		public Transform segment0 = null;
		public Transform segment1 = null;
		public Transform segment2 = null;
		public Transform toe = null;

		public Vector3 target = Vector3.zero;
	}

	public Leg[] legs = new Leg[8];
	public float minLegStretch = 0.05f;
	public float maxLegStretch = 0.4f;
	public float maxLegRaise = 0.4f;
	public float relocationDistance = 0.4f;

	public Vector3 speed = Vector3.zero;
	private Vector3 moveDir = Vector3.zero;
	private Vector3 prevPosition = Vector3.zero;

	private CharacterController ctrl = null;
	

    // Start is called before the first frame update
    void Start()
    {
        foreach (Leg leg in legs)
		{
			if (leg == null || leg.root == null) continue;

			leg.segment0 = leg.root.GetChild(0);
			leg.segment1 = leg.segment0?.GetChild(0);
			leg.segment2 = leg.segment1?.GetChild(0);
			leg.toe = leg.segment2?.GetChild(0);

			leg.target = leg.toe.position;
		}

		if (ctrl == null) ctrl = GetComponent<CharacterController>();
    }

	private void OnDrawGizmosSelected()
	{
		if (legs != null)
		{
			foreach (Leg leg in legs)
			{
				if (leg.root == null || leg.toe == null) continue;

				Gizmos.color = Color.red;
				Gizmos.DrawSphere(leg.target, 0.05f);
				Gizmos.DrawLine(leg.toe.position, leg.target);

				Gizmos.color = Color.yellow;
				if (leg.segment0 != null)
				{
					Gizmos.DrawLine(leg.root.position, leg.segment0.position);
					if (leg.segment1 != null)
					{
						Gizmos.DrawLine(leg.segment0.position, leg.segment1.position);
						if (leg.segment2 != null)
						{
							Gizmos.DrawLine(leg.segment1.position, leg.segment2.position);
							Gizmos.DrawLine(leg.segment2.position, leg.toe.position);
						}
					}
				}
			}
		}
	}

	// Update is called once per frame
	void Update()
    {
		Vector3 ctrlSpeed = ctrl != null ? ctrl.velocity : transform.position - prevPosition;
		prevPosition = transform.position;
		if (ctrlSpeed.sqrMagnitude > float.Epsilon)
		{
			speed = ctrlSpeed;
			moveDir = speed.normalized;
		}

		foreach (Leg leg in legs)
		{
			if (leg != null && leg.root != null && leg.toe != null)
			{
				UpdateLeg(leg);
			}
		}
    }

	private void UpdateLeg(Leg leg)
	{
		Vector3 target = leg.target;
		if (leg.state == LegState.Raising)
		{
			target.y += maxLegRaise;
		}

		float distSq = Vector3.SqrMagnitude(target - leg.toe.position);
		if (distSq > maxLegStretch * maxLegStretch && leg.state == LegState.Set)
		{
			RecalculateLegTarget(leg);
			leg.state = LegState.Raising;
		}
		else if (distSq < minLegStretch * minLegStretch)
		{
			switch (leg.state)
			{
				case LegState.Raising:
					leg.state = LegState.Lowering;
					break;
				case LegState.Lowering:
					leg.state = LegState.Set;
					break;
				default:
					break;
			}
		}

		
		// TODO
	}

	private void RecalculateLegTarget(Leg leg)
	{
		Vector3 offset = moveDir * relocationDistance;
		leg.target += offset;
	}
}
