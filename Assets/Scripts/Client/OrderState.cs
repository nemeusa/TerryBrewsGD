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
        _client.colorDrink();
        _client.textOrder.gameObject.SetActive(true);
        _client.textOrder.text = _client.currentRequest;
    }

    public void OnUpdate()
    {
        //Debug.Log("Order");
        _client.chair.Ocuppy();
        _client.transform.forward = new Vector3 (0, 0, 0);
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
}
