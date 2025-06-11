using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.Timeline;

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

    public void NuevaPeticion()
    {
        //string[] opciones = { "<color=blue>Agua</color>", "<color=orange>Jugo</color>", "<color=yellow>Cerveza</color>", "<color=red>Gaseosa</color>" };
        string[] opciones = { "Agua", "Jugo", "Cerveza", "Gaseosa" };
        //string a = ("< color = blue > " + opciones + " </ color >");


        _client.currentRequest = opciones[UnityEngine.Random.Range(0, opciones.Length)];
    }

}
