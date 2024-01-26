using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostReturnState : GhostBaseState
{
    public string GoToRespawnState = "Respawn";
    private int gotoRespawnState;

    public override void Init(GameObject _owner, FSM _fsm)
    {
        base.Init(_owner, _fsm);
        gotoRespawnState = Animator.StringToHash(GoToRespawnState);
    }
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (_ghostController != null)
        {
            _ghostController.SetMoveToLocation(_ghostController.ReturnLocation);
            _ghostController.pathCompletedEvent.AddListener(() => fsm.ChangeState(gotoRespawnState));
        }
 
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }
}
