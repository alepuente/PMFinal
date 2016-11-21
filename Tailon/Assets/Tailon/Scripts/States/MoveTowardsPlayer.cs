using UnityEngine;
using System.Collections;

public class MoveTowardsPlayer : State<EnemyController> 
{
	public override void CheckForNewState ()
	{
		if (Vector3.Distance(ownerObject.gameObject.transform.position,ownerObject._target.position)>= ownerObject._maxDistance&&ownerObject._isTouching){
			ownerStateMachine.CurrentState = new WanderAround();
		}
	}

	public override void Update ()
	{
		ownerObject._rgb.isKinematic = true;
		ownerObject._rgb.isKinematic = false;
		ownerObject.anim.SetBool("IsWalking", true);
		ownerObject.gameObject.transform.Translate(Vector3.forward * ownerObject._speed * Time.deltaTime);
		ownerObject.gameObject.transform.LookAt(ownerObject._target);
	}

	public override void OnEnable (EnemyController owner, StateMachine<EnemyController> newStateMachine)
	{
		base.OnEnable (owner, newStateMachine);

		owner._target = Object.FindObjectOfType<PlayerController>().transform;
	}
}
