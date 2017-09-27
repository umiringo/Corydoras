using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateTransition
{
	NullTransition = 0,
	Start = 1,
	Quit = 2,
}

public enum StateID
{
	NullStateID = 0,
	MainMenu = 1,
	GameScene = 2,
}

public abstract class FiniteState
{
	protected Dictionary<StateTransition, StateID> _stateTransitionMap = new Dictionary<StateTransition, StateID>();
	protected StateID _stateID;
	public StateID ID { get { return _stateID; }}

	public void AddTransition(StateTransition trans, StateID id)
	{
		if( trans == StateTransition.NullTransition ) {
			return;
		}
		if( id == StateID.NullStateID ) {
			return;
		}

		if( _stateTransitionMap.ContainsKey(trans) ) {
			return;
		}
		_stateTransitionMap.Add(trans, id);
	}

	public void DeleteTransition(StateTransition trans)
	{
		if( trans == StateTransition.NullTransition )
		{
			return;
		}

		if(_stateTransitionMap.ContainsKey(trans))
		{
			_stateTransitionMap.Remove(trans);
			return;
		}
	}

	public StateID GetTargetState(StateTransition trans)
	{
		if(_stateTransitionMap.ContainsKey(trans))
		{
			return _stateTransitionMap[trans];
		}
		return StateID.NullStateID;
	}

	public virtual void DoBeforeEnter() {}
	public virtual void DoBeforeQuit() {}

	// public abstract void Reason(GameObject go);
	// public abstract void Act(GameObject go);
}