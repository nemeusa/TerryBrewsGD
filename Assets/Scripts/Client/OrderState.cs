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
        _client._textMesh.gameObject.SetActive(true);
        _client._textMesh.text = _client.currentRequest;
    }

    public void OnUpdate()
    {
        Debug.Log("Order");
        //_client.chair.Ocuppy();
        _client.transform.forward = new Vector3 (0, 0, 0);
        if (_client.goodOrder)
        {
            _fsm.ChangeState(TypeFSM.ExitBar);
        }
    }

    public void OnExit()
    {
        _client._textMesh.gameObject.SetActive(false);
    }
}
