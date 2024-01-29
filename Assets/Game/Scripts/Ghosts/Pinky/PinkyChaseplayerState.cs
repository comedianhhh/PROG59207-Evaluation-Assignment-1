using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyChaseplayerState : GhostBaseState
{
    public string GoToRunawayState = "Runaway";
    private int gotoRunawayStateHash;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        gotoRunawayStateHash = Animator.StringToHash(GoToRunawayState);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (_ghostController != null&& _ghostController.PacMan != null)
        {
            if(GameDirector.Instance.state == GameDirector.States.enState_PacmanInvincible)
            {
                fsm.ChangeState(gotoRunawayStateHash);
                return;
            }

            Vector3 playerPosition = _ghostController.PacMan.transform.position;
            if (playerPosition.x >= 10 || playerPosition.x <= -10 || playerPosition.y >= 10 || playerPosition.y <= -10)
            {
                _ghostController.SetMoveToLocation(new Vector2(Random.Range(-10, 10), Random.Range(-10, 10)));
            }
            else
            {
                Vector3 pinkyTargetPosition = CalculatePinkyTarget(playerPosition);

                _ghostController.SetMoveToLocation(pinkyTargetPosition);

            }


        }

    }

    private Vector3 CalculatePinkyTarget(Vector3 playerPosition)
    {
        // Determine Pac-Man's direction.
        PacmanController.MoveDirection pacManDirection = GameDirector.Instance.pacmanController.moveDirection;
        Vector3 targetPosition = playerPosition;
        switch (pacManDirection)
        {
            case PacmanController.MoveDirection.Left:
                targetPosition.x-=3;
                break;
            case PacmanController.MoveDirection.Right:
                targetPosition.x += 3;
                break;
            case PacmanController.MoveDirection.Up:
                targetPosition.y += 3;
                break;
            case PacmanController.MoveDirection.Down:
                targetPosition.y -= 3;
                break;
        }

        return targetPosition;
    }
}
