using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnterBarState : State
{
    FSM<TypeFSM> _fsm;
    Client _client;
    public EnterBarState(FSM<TypeFSM> fsm, Client client)
    {
        _fsm = fsm;
        _client = client;
    }

    public void OnEnter()
    {

    }

    public void OnUpdate()
    {

    }

    public void OnExit()
    {

    }

}
