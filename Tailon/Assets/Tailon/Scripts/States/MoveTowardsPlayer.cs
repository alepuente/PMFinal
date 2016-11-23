using UnityEngine;
using System.Collections;

public class MoveTowardsPlayer : State<EnemyController> 
{
	public override void CheckForNewState ()
	{
        if (ownerObject._health < 0)
        {
            ownerStateMachine.CurrentState = new Death();
        }
        if (Vector3.Distance(ownerObject.gameObject.transform.position, ownerObject._target.position) >= ownerObject._maxDistance && ownerObject._isTouching)
        {
            ownerStateMachine.CurrentState = new WanderAround();
        }
        if (Vector3.Distance(ownerObject.gameObject.transform.position, ownerObject._target.position) >= ownerObject._maxDistance && !ownerObject._isTouching)
        {
            ownerStateMachine.CurrentState = new Iddle();
        }
	}

	public override void Update ()
	{	
		ownerObject.gameObject.transform.Translate(Vector3.forward * ownerObject._speed * Time.deltaTime);
		ownerObject.gameObject.transform.LookAt(ownerObject._target);
	}

	public override void OnEnable (EnemyController owner, StateMachine<EnemyController> newStateMachine)
	{
		base.OnEnable (owner, newStateMachine);
        ownerObject._rgb.isKinematic = true;
        ownerObject._rgb.isKinematic = false;
        ownerObject.anim.SetBool("IsWalking", true);
        ownerObject._canAttack = true;
	}
}
