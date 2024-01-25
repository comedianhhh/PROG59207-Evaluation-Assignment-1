using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyChasePlayerState : GhostBaseState
{
    public string GoToRunawayState = "Runaway";
    private int gotoRunawayStateHash;


    public override void Init(GameObject _owner, FSM _fsm)
    {
        base.Init(_owner, _fsm);
        gotoRunawayStateHash = Animator.StringToHash(GoToRunawayState);
    }
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        _ghostController.SetMoveToLocation(_ghostController.PacMan.transform.position);
        if(GameDirector.Instance.state==GameDirector.States.enState_PacmanInvincible)
        {
            fsm.ChangeState(gotoRunawayStateHash);
        }

    }
}
