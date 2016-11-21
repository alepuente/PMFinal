using UnityEngine;
using System.Collections;

public class WanderAround : State<EnemyController>  
{
	public float directionChangeInterval = 1;
	public float maxHeadingChange = 100;

	private float _heading;
	private Vector3 _targetRotation;

	public override void CheckForNewState ()
	{
		if (Vector3.Distance(ownerObject.gameObject.transform.position,ownerObject._target.position)<= ownerObject._maxDistance&&ownerObject._isTouching){
			ownerStateMachine.CurrentState = new MoveTowardsPlayer();
		}
	}
	
	public override void Update ()
	{
		ownerObject.transform.eulerAngles = Vector3.Slerp(ownerObject.transform.eulerAngles, _targetRotation, Time.deltaTime * directionChangeInterval);
		ownerObject.transform.Translate (ownerObject.transform.forward *-ownerObject._speed*Time.deltaTime);
	}
	
	public override void OnEnable (EnemyController owner, StateMachine<EnemyController> newStateMachine)
	{
		base.OnEnable (owner, newStateMachine);

		_heading = Random.Range(0, 360);
		ownerObject.transform.eulerAngles = new Vector3(0, _heading, 0);
		
		ownerObject.StartCoroutine(NewHeading());
	}

	IEnumerator NewHeading ()
	{
		while (true) 
		{
			var floor = Mathf.Clamp(_heading - maxHeadingChange, 0, 360);
			var ceil  = Mathf.Clamp(_heading + maxHeadingChange, 0, 360);
			_heading = Random.Range(floor, ceil);
			_targetRotation = new Vector3(0, _heading, 0);

			yield return new WaitForSeconds(directionChangeInterval);
		}
	}
}
