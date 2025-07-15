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
        _client.soundEfects.PlaySoundFromGroup(2);
        NuevaPeticion();
        _client.dialogue.CharlaThemes();
        //_client.textOrder.gameObject.SetActive(true);
        //_client.textCharla.gameObject.SetActive(true);
        //_client.textOrder.gameObject.SetActive(true);
        _client.globoTexto.SetActive(true);
        _client.textOrder.text = $"<color=black>Quiero </color> {_client.currentRequest}" ;
        _client.textCharla.text = _client.dialogue.currentDialogue;
        //_client.Charla();
    }

    public void OnUpdate()
    {
        _client.dialogue.Verification();
        var dir = _client.player.transform.position - _client.transform.position;
        //var globoDir = _client.player.transform.position - _client.transform.position;
        //_client.globoTexto.transform.forward = globoDir;
        _client.transform.forward = dir;

        _client.colorDrink();
        //Debug.Log("Order");
        _client.chair.Ocuppy();
        _client.TextColor();
        //_client.transform.forward = new Vector3 (0, 0, 0.01f);
        

        if(_client.goodOrder || _client.badOrder)
        {
            if (_client.imposter) _fsm.ChangeState(TypeFSM.Attack);

            else
            {
                if (_client.goodOrder) _fsm.ChangeState(TypeFSM.ExitBar);
                if (_client.badOrder) _fsm.ChangeState(TypeFSM.ExitBar);
            }
        }


    }

    public void OnExit()
    {
        _client.globoTexto.SetActive(false);
        _client.LeaveChair();
        //_client.textOrder.gameObject.SetActive(false);
        //_client.textCharla.gameObject.SetActive(false);
    }

    IEnumerator niceOrder()
    {
        _client.GetComponent<MeshRenderer>().material.color = Color.green;
        yield return new WaitForSeconds(0.1f);
    }


    public void NuevaPeticion()
    {

        // string[] opciones = { "Agua", "Jugo", "Cerveza", "Gaseosa" };

        if (_client._barManeger.tutorial)
        {
            _client.currentRequest = _client.opciones[_client._barManeger.indexBebida];


            // Incrementa el índice y reinicia si se pasa del final
            _client._barManeger.indexBebida++;
            if (_client._barManeger.indexBebida >= _client.opciones.Length)
            {
                _client._barManeger.indexBebida = 0;
            }
        }

        else
        _client.currentRequest = _client.opciones[UnityEngine.Random.Range(0, _client.opciones.Length)];
    }
}
