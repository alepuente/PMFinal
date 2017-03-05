using UnityEngine;
using System.Collections;

public class Death : State<EnemyController> {

    public override void CheckForNewState()
    {

    }

    public override void Update()
    {
        
    }

    public override void OnEnable(EnemyController owner, StateMachine<EnemyController> newStateMachine)
    {
        base.OnEnable(owner, newStateMachine);
        ownerObject.destroy();
    }
}
