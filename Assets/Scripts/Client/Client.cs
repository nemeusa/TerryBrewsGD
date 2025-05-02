using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Client : MonoBehaviour
{
    FSM<TypeFSM> _fsm;

    [SerializeField] float _movSpeed;
    [HideInInspector]
    public Chair _chair;

    void Awake()
    {
        _fsm = new FSM<TypeFSM>();
        _fsm.AddState(TypeFSM.EnterBar, new EnterBarState(_fsm, this));
        _fsm.AddState(TypeFSM.Order, new OrderState(_fsm, this));
        _fsm.AddState(TypeFSM.ExitBar, new ExitBarState(_fsm, this));

        _fsm.ChangeState(TypeFSM.EnterBar);
    }

    void Update()
    {
        _fsm.Execute();
    }

    public void AssignChair(Chair chair)
    {
        _chair = chair;
        _chair.Ocuppy();
        StartCoroutine(GoSeat());
    }

    IEnumerator GoSeat()
    {
        var dir = transform.position - _chair.transform.position;
        while (dir.x < 0.1)
        {
            //transform.forward = dir;
            transform.position -= Vector3.right * _movSpeed * Time.deltaTime;
            yield return null;
        }
    }

    public void LeaveChair()
    {
        _chair.Free();
        Destroy(gameObject);
    }
}

public enum TypeFSM
{
    EnterBar,
    Order,
    ExitBar,
    Imposter
}
