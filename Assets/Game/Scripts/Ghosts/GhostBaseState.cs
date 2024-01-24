using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBaseState : FSMBaseState
{
    protected GhostController _ghostController;

    public override void Init(GameObject _owner, FSM _fsm)
    {
        base.Init(_owner, _fsm);
        _ghostController = _owner.GetComponent<GhostController>();
        Debug.Assert(_ghostController != null, $"{_owner.name} GhostController is null");
    }
}
