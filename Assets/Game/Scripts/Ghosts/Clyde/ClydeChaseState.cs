using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeChaseState : GhostBaseState
{
    public string GoToRunawayState = "Runaway";
    private int gotoRunawayStateHash;

    public float clydeDistance = 5.0f;

    public bool isOutside = false;
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
        if(_ghostController!=null&&_ghostController.PacMan!=null)
        {
            Vector3 playerPosition = _ghostController.PacMan.transform.position;
            Debug.Log(playerPosition);
            if (playerPosition.x >= 10 || playerPosition.x <= -10 || playerPosition.y >= 10 || playerPosition.y <= -10)
            {
                isOutside = true;
            }
            else
            {
                isOutside = false;
            }

            if (isOutside)
            {
                _ghostController.SetMoveToLocation(new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)));
            }
            else
            {
                _ghostController.SetMoveToLocation(_ghostController.PacMan.transform.position);

            }
        }
    }
}
