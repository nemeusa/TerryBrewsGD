
using UnityEngine;

public class ExitBarState : State
{
    FSM<TypeFSM> _fsm;
    Client _client;

    public ExitBarState(FSM<TypeFSM> fsm, Client client)
    {
        _fsm = fsm;
        _client = client;
    }

    public void OnEnter()
    {

    }

    public void OnUpdate()
    {
        _client.LeaveChair();
    }

    public void OnExit()
    {
    }
}
