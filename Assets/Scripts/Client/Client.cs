using System.Collections;
using UnityEngine;

public class Client : MonoBehaviour
{
    FSM<TypeFSM> _fsm;

    public float _movSpeed;

    public bool isSit;
    [HideInInspector]
    public Chair chair;

    public string currentRequest;

    public bool ocuppy;

    public bool goodOrder;

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
        this.chair = chair;
        this.chair.Ocuppy();
        isSit = true;
        StartCoroutine(GoSeat());
    }

    IEnumerator GoSeat()
    {
        var dir = transform.position - chair.transform.position;
        while (dir.x < 0.1)
        {
            //transform.forward = dir;
            transform.position -= Vector3.right * _movSpeed * Time.deltaTime;
            yield return null;
        }
    }

    public void LeaveChair()
    {
        chair.Free();
        isSit = false;
    }

    public IEnumerator IsDestroy()
    {
        yield return new WaitForSeconds(2);
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
