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
        if(_ghostController!=null&&_ghostController.PacMan!=null)
        {
            Vector3 playerPosition = _ghostController.PacMan.transform.position;
            if(playerPosition.x>=10||playerPosition.x<=-10||playerPosition.y>=10||playerPosition.y<=-10)
            {
                _ghostController.SetMoveToLocation(new Vector2(Random.Range(-10,10),Random.Range(-10,10)));
            }
            else
            {
                _ghostController.SetMoveToLocation(_ghostController.PacMan.transform.position);

            }

        }
        if(GameDirector.Instance.state==GameDirector.States.enState_PacmanInvincible)
        {
            fsm.ChangeState(gotoRunawayStateHash);
        }

    }
}
