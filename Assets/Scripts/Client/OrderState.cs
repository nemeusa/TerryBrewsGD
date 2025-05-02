using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderState : State
{
    FSM<TypeFSM> _fsm;
    Client _client;

    public OrderState(FSM<TypeFSM> fsm, Client client)
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
