
using System.Collections;
using UnityEngine;

public class ExitBarState : State
{
    FSM<TypeFSM> _fsm;
    Client _client;
    Vector3 _dir;

    public ExitBarState(FSM<TypeFSM> fsm, Client client)
    {
        _fsm = fsm;
        _client = client;
    }

    public void OnEnter()
    {
        _dir = (Random.Range(0, 2) == 0) ? Vector3.left : Vector3.right;
        if(_client.goodOrder)
        {
            GameStats.ordersCompletadas++;
            _client.soundEfects.PlaySoundFromGroup(3);
            _client._goodClientParticles.Play();
            _client.GetComponent<MeshRenderer>().material.color = Color.green;
            _client.player.MoreMoney(100);
            _client.player.cordura += 5;
        }
        else if (_client.badOrder)
        {
            _client.soundEfects.PlaySoundFromGroup(4);
            _client._badClientParticles.Play();
            _client.GetComponent<MeshRenderer>().material.color = Color.red;
            _client.player.LessMoney(50);
        }
    }

    public void OnUpdate()
    {
       // Debug.Log("Exit");
        //var dir = _client.chair.transform.position + _client.transform.position;
        _client.transform.forward = _dir;
        _client.transform.position += _dir * _client.exitSpeed * Time.deltaTime;
        _client.StartCoroutine(_client.IsDestroy());
    }

    public void OnExit()
    {
    }
}
