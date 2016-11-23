using UnityEngine;
using System.Collections;

public class Iddle : State<EnemyController>
{
    public override void CheckForNewState()
    {
        if (ownerObject._health < 0)
        {
            ownerStateMachine.CurrentState = new Death();
        }
        else if (Vector3.Distance(ownerObject.gameObject.transform.position, ownerObject._target.position) >= ownerObject._maxDistance && ownerObject._isTouching)
        {
            ownerStateMachine.CurrentState = new WanderAround();
        }
        else if (Vector3.Distance(ownerObject.gameObject.transform.position, ownerObject._target.position) <= ownerObject._maxDistance && ownerObject._isTouching)
        {
            ownerStateMachine.CurrentState = new MoveTowardsPlayer();
        }
    }

    public override void Update(){
    
    }

    public override void OnEnable(EnemyController owner, StateMachine<EnemyController> newStateMachine)
    {
        base.OnEnable(owner, newStateMachine);
        ownerObject.anim.SetBool("IsWalking", false);
        ownerObject._canAttack = false;
    }
}
