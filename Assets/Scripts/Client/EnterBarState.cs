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
        //_client.Charla();
        _client.textOrder.gameObject.SetActive(false);
        _client.textCharla.gameObject.SetActive(false);
    }

    public void OnUpdate()
    {
        //Debug.Log("entrando");
        var dir = _client.chair.transform.position - _client.transform.position;
        _client.transform.forward = dir;
        _client.transform.position += (dir * _client.speed * Time.deltaTime);

        if (MathF.Abs(dir.x) < 0.1)
        {
            _fsm.ChangeState(TypeFSM.Order);


            //Cliente vip
            //if(UnityEngine.Random.Range(0, 101) == 1)
            //{
            //    _fsm.ChangeState(TypeFSM.VIP);
            //}
        }

    }

    public void OnExit()
    {
    }


}
