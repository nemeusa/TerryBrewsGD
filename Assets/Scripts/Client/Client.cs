using System;
using System.Collections;
using System.Drawing;
using TMPro;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

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

    public string currentDialogue;

    //public List<string> charlaGood = new List<string>();

    //public List<string> charlaBad = new List<string>();

    public string[] charlaGood, charlaBad;

    public UnityEngine.Color agua;
    public UnityEngine.Color jugo;
    public UnityEngine.Color cerveza;
    public UnityEngine.Color gaseosa;

    public bool ocuppy;

    public bool goodOrder, badOrder;

    public bool imposter;
    public bool isDeath;

    public ParticleSystem _goodClientParticles;
    public ParticleSystem _badClientParticles;

    public GameObject visor;

    [SerializeField] bool _onlyGood, _onlyImposter;

    public string Theme;

    public ParticleSystem bloodPartycles;

    public bool talkThemes;

    public Dialogue dialogue;

    public bool randomBlock;

    public BarManager _barManeger;

    bool tutorial;

    public SoundEfects soundEfects;

    public string[] opciones = { "Agua", "Jugo", "Cerveza", "Gaseosa" };

    void Awake()
    {
        soundEfects = GetComponent<SoundEfects>();
        //dialogue = GetComponent<ClientDialogue>();

        _fsm = new FSM<TypeFSM>();
        _fsm.AddState(TypeFSM.EnterBar, new EnterBarState(_fsm, this));
        _fsm.AddState(TypeFSM.Order, new OrderState(_fsm, this));
        _fsm.AddState(TypeFSM.ExitBar, new ExitBarState(_fsm, this));
        _fsm.AddState(TypeFSM.Attack, new AttackState(_fsm, this));
        _fsm.AddState(TypeFSM.Death, new DeathState(_fsm, this));
        _fsm.AddState(TypeFSM.VIP, new VIPState(_fsm, this));

        StartCoroutine(StartVars());

    }

    void Update()
    {
        _fsm.Execute();

        if (isDeath)
        {
            _fsm.ChangeState(TypeFSM.Death);
        }
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

    public void InstantDestroy()
    { 
        Destroy(gameObject);
    }

    IEnumerator StartVars()
    {
        yield return new WaitForSeconds(0.1f);

        if (randomBlock) RandomImposter();
        _fsm.ChangeState(TypeFSM.EnterBar);

    }

    void RandomImposter()
    {
        if (UnityEngine.Random.Range(0, 101) > 50) imposter = true;
        else imposter = false;
       // Charla();
    }

    #region Charla

    public void colorDrink()
    {
        if (currentRequest == "Agua")
        {
           // Debug.Log("funciona xd");
            textOrder.color = agua;
        }

        if (currentRequest == "Jugo")
        {
           // Debug.Log("funciona xd");
            textOrder.color = jugo;
        }

        if (currentRequest == "Cerveza")
        {
           // Debug.Log("funciona xd");
            textOrder.color = cerveza;
        }

        if (currentRequest == "Gaseosa")
        {
           // Debug.Log("funciona xd");
            textOrder.color = gaseosa;
        }
    }

    #endregion

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
