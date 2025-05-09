
using System.Collections;
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
        Debug.Log("Exit");
        _client.chair.Free();
        var dir = _client.chair.transform.position + _client.transform.position;
        _client.transform.forward = dir;
        _client.transform.position += (dir * _client._movSpeed * Time.deltaTime);
        _client.IsDestroy();
    }

    public void OnExit()
    {
    }
}
