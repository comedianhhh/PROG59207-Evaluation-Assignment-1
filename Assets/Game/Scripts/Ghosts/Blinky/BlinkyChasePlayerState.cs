using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyChasePlayerState : GhostBaseState
{
    public string GoToRunawayState = "Runaway";
    private int gotoRunawayStateHash;

    public string GoToReturnState = "Return";
    private int gotoReturnStateHash;

    public string GoToDieState = "Die";
    private int gotoDieStateHash;

    public override void Init(GameObject _owner, FSM _fsm)
    {
        base.Init(_owner, _fsm);
        gotoRunawayStateHash = Animator.StringToHash(GoToRunawayState);
        gotoReturnStateHash = Animator.StringToHash(GoToReturnState);
        gotoDieStateHash = Animator.StringToHash(GoToDieState);
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (_ghostController != null)
        {
            _ghostController.SetMoveToLocation(_ghostController.PacMan.transform.position);
        }
    }
}
