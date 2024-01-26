using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRespawn : GhostBaseState
{
    public string GoToChaseState = "Chase";
    private int gotoChaseStateHash;

    public float respawnDelay = 3.0f;
    private float respawnTimer = 0.0f;
    public override void Init(GameObject _owner, FSM _fsm)
    {
        base.Init(_owner, _fsm);
        gotoChaseStateHash = Animator.StringToHash(GoToChaseState);
 
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log($"{owner.name} Respawn");
        respawnTimer = respawnDelay;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        respawnTimer -= Time.deltaTime;

        if (respawnTimer <= 0.0f)
        {
            fsm.ChangeState(gotoChaseStateHash);
            _ghostController.gameObject.GetComponent<CircleCollider2D>().enabled = true;
            _ghostController._animator.SetBool("IsDead", false);
        }

    }

}
