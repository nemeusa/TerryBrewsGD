using System;
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
        NuevaPeticion();
    }

    public void OnUpdate()
    {
        Debug.Log("entrando");
        var dir = _client.chair.transform.position - _client.transform.position;
        _client.transform.forward = dir;
        _client.transform.position += (dir * _client.speed * Time.deltaTime);

        if (MathF.Abs(dir.x) < 0.1)
        {
            _fsm.ChangeState(TypeFSM.Order);
        }
    }

    public void OnExit()
    {

    }

    public void NuevaPeticion()
    {
        string[] opciones = { "Agua", "Jugo", "Cerveza", "Gaseosa" };
        _client.currentRequest = opciones[UnityEngine.Random.Range(0, opciones.Length)];
    }

}
