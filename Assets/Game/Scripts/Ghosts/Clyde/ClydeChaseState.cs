using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeChaseState : GhostBaseState
{
    public string GoToRunawayState = "Runaway";
    private int gotoRunawayStateHash;

    public float clydeDistance = 5.0f;

    public override void Init(GameObject _owner, FSM _fsm)
    {
        base.Init(_owner, _fsm);
        gotoRunawayStateHash = Animator.StringToHash(GoToRunawayState);

    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        if (_ghostController != null)
        {
            if((_ghostController.PacMan.position-_ghostController.transform.position).magnitude<clydeDistance)
            {
                fsm.ChangeState(gotoRunawayStateHash);
                return;
            }


        }

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (_ghostController != null && GameDirector.Instance.state == GameDirector.States.enState_PacmanInvincible)
        {
            _ghostController.pathCompletedEvent.AddListener(() => fsm.ChangeState(gotoRunawayStateHash));
        }
        _ghostController.SetMoveToLocation(_ghostController.PacMan.position);


    }
}
