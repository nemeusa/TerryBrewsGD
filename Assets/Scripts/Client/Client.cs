using System.Collections;
using System.Drawing;
using TMPro;
using UnityEngine;

public class Client : MonoBehaviour
{
    FSM<TypeFSM> _fsm;

    public float speed, exitSpeed;

    [HideInInspector]
    public Chair chair;

    [HideInInspector]
    public Player player;

    public TMP_Text textOrder;
    public TMP_Text textCharla;

    public string currentRequest;

    //public List<string> charlaGood = new List<string>();

    //public List<string> charlaBad = new List<string>();

    public string[] charlaGood, charlaBad;

    public UnityEngine.Color agua;
    public UnityEngine.Color jugo;
    public UnityEngine.Color cerveza;
    public UnityEngine.Color gaseosa;

    public bool ocuppy;

    public bool goodOrder;

    public bool imposter;
    public bool isDeath;

    public ParticleSystem _goodClientParticles;

    public GameObject visor;

    [SerializeField] bool _onlyGood;

    void Awake()
    {
        _fsm = new FSM<TypeFSM>();
        _fsm.AddState(TypeFSM.EnterBar, new EnterBarState(_fsm, this));
        _fsm.AddState(TypeFSM.Order, new OrderState(_fsm, this));
        _fsm.AddState(TypeFSM.ExitBar, new ExitBarState(_fsm, this));
        _fsm.AddState(TypeFSM.Attack, new AttackState(_fsm, this));
        _fsm.AddState(TypeFSM.Death, new DeathState(_fsm, this));
        _fsm.AddState(TypeFSM.VIP, new VIPState(_fsm, this));

        _fsm.ChangeState(TypeFSM.EnterBar);

        if(!_onlyGood)
        RandomImposter();
    }

    void Update()
    {
        _fsm.Execute();
        //Destroy(gameObject, 2);
    }

    public void AssignChair(Chair chair)
    {
        this.chair = chair;
        this.chair.Ocuppy();
        StartCoroutine(GoSeat());
    }

    IEnumerator GoSeat()
    {
        var dir = transform.position - chair.transform.position;
        while (dir.x < 0.1)
        {
            //transform.forward = dir;
            transform.position -= Vector3.right * speed * Time.deltaTime;
            yield return null;
        }
    }

    public void LeaveChair()
    {
        chair.Free();
    }

    public IEnumerator IsDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void RandomImposter()
    {
        if (Random.Range(0, 101) > 50) imposter = true;
        else imposter = false;

        if (!imposter)
        {
            string charla;

            charla = charlaGood[Random.Range(0, charlaGood.Length)];
            textCharla.text = charla;
            //Debug.Log(charla);

        }

        else
        {
            string charla;

            charla = charlaBad[Random.Range(0, charlaBad.Length)];
            textCharla.text = charla;
        }

    }

    public void colorDrink()
    {
        if (currentRequest == "Agua")
        {
            Debug.Log("funciona xd");
            textOrder.color = agua;
        }

        if (currentRequest == "Jugo")
        {
            Debug.Log("funciona xd");
            textOrder.color = jugo;
        }

        if (currentRequest == "Cerveza")
        {
            Debug.Log("funciona xd");
            textOrder.color = cerveza;
        }

        if (currentRequest == "Gaseosa")
        {
            Debug.Log("funciona xd");
            textOrder.color = gaseosa;
        }
    }

}

public enum TypeFSM
{
    EnterBar,
    Order,
    ExitBar,
    Imposter,
    Attack,
    Death,
    VIP
}
