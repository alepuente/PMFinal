using UnityEngine;
using System.Collections;

public abstract class State<T> 
{
    public T ownerObject;
    public StateMachine<T> ownerStateMachine;

	public abstract void CheckForNewState();
	public abstract void Update();

	public virtual void OnEnable(T owner, StateMachine<T> newStateMachine)
	{
		ownerObject = owner;
		ownerStateMachine = newStateMachine;
	}

	public virtual void OnDisable()
	{
	}
}
