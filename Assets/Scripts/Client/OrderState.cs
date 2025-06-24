using System.Collections;
using UnityEngine;

public class OrderState : MonoBehaviour, State
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
        _client.dialogue.Charla();
        //if(_client.talkThemes)
        //{
        //    if (!_client.imposter)
        //    {
        //        if (_client.Theme != _client.player._talkTheme.currentTheme[_client.player._talkTheme._indexTheme])
        //        _client.StartCoroutine(_client.CharlaThemeGoodCoroutine());

        //    }
        //    else
        //    {
        //        if (_client.Theme == _client.player._talkTheme.currentTheme[_client.player._talkTheme._indexTheme])
        //        _client.StartCoroutine(_client.CharlaThemeBadCoroutine());
        //    }
        //}
        NuevaPeticion();
        _client.textOrder.gameObject.SetActive(true);
        _client.textCharla.gameObject.SetActive(true);
        _client.textOrder.gameObject.SetActive(true);
        _client.textOrder.text = _client.currentRequest;
        //_client.Charla();
    }

    public void OnUpdate()
    {
        var dir = _client.player.transform.position - _client.transform.position;
        _client.transform.forward = dir;

        _client.colorDrink();
        //Debug.Log("Order");
        _client.chair.Ocuppy();
        _client.TextColor();
        //_client.transform.forward = new Vector3 (0, 0, 0.01f);
        if (_client.goodOrder)
        {
            if (_client.imposter) _fsm.ChangeState(TypeFSM.Attack);

            else
            {
                _client._goodClientParticles.Play();
                _fsm.ChangeState(TypeFSM.ExitBar);
            }
            //StartCoroutine(niceOrder());
        }

        if (_client.badOrder)
        {
            _client.player._score -= 50;
            _client.badOrder = false;
        }

        if (_client.isDeath)
        {
            _fsm.ChangeState(TypeFSM.Death);
        }
    }

    public void OnExit()
    {
        _client.LeaveChair();
        _client.textOrder.gameObject.SetActive(false);
        _client.textCharla.gameObject.SetActive(false);
    }

    IEnumerator niceOrder()
    {
        _client.GetComponent<MeshRenderer>().material.color = Color.green;
        yield return new WaitForSeconds(0.1f);
    }


    public void NuevaPeticion()
    {
        //string[] opciones = { "<color=blue>Agua</color>", "<color=orange>Jugo</color>", "<color=yellow>Cerveza</color>", "<color=red>Gaseosa</color>" };
        string[] opciones = { "Agua", "Jugo", "Cerveza", "Gaseosa" };
        //string a = ("< color = blue > " + opciones + " </ color >");


        _client.currentRequest = opciones[UnityEngine.Random.Range(0, opciones.Length)];
    }
}
