using System;
using System.Collections;
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
    public TMP_Text textNames;
    public TMP_Text textProfesion;

    public string currentRequest;
    public string currentDialogue;
    [SerializeField] string[] names;
    [SerializeField] string profesion;


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

    public GameObject globoTexto;

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
        globoTexto = this.chair.globo;
        textOrder = this.chair.textoPedido;
        textCharla = this.chair.textoCharla;
        textNames = this.chair.textoNames;
        textProfesion = this.chair.textoProfesion;

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

        namesAndOffices();
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

    public void TextColor()
    {
        if (player.help)
        {
            if (!imposter) textCharla.color = UnityEngine.Color.green;
            else textCharla.color = UnityEngine.Color.red;
        }

        else textCharla.color = UnityEngine.Color.black;
    }


    public void namesAndOffices()
    {
        names = new string[]
        {
            "Mateo", "Santiago", "Benjam�n", "Thiago", "Joaqu�n",
            "Lucas", "Mart�n", "Nicol�s", "Tom�s", "Dylan",
            "Leonardo", "Gabriel", "Mat�as", "Juli�n", "Gael",
            "Lautaro", "Bruno", "Emiliano", "Franco", "Andr�s",
            "Sim�n", "Alan", "David", "Iv�n", "Federico",
            "Juan", "Facundo", "Axel", "Luciano", "El�as",
            "Agust�n", "Jerem�as", "Samuel", "Aar�n", "Lorenzo",
            "Nahuel", "Valentino", "Enzo", "Ezequiel", "Maximiliano",
            "Esteban", "Rodrigo", "Dami�n", "Leandro", "Sebasti�n",
            "Pablo", "Ignacio", "Ramiro", "Rafael", "Adri�n",
            "Hugo", "Mauro", "Ariel", "Marcelo", "Messi", "Ramiro", "Tom�",
            "Ulises", "Diego", "Amadeo", "Agustin", "Mariano", "Gonzalo",
            "Alonso", "Camilo", "Cristian", "Lisandro", "Guido",
            "Ismael", "Rub�n", "Ivano", "V�ctor", "Hern�n",
            "Fabi�n", "Lauterio", "C�sar", "Rocco", "Alanis",
            "Teo", "Blas", "Dante", "Emil", "Aar�n",
            "Ezra", "Josu�", "Dorian", "Axelino", "Iker",
            "Basti�n", "Odin", "Baltazar", "Elian", "Joaquim",
            "Salom�n", "Lionel", "Jairo", "�ngel", "Kevin",
            "Jon�s", "Gaetano", "Eitan", "L�zaro", "Matheo", "Federico", "Agustin"

        };
        
        textNames.text = names[UnityEngine.Random.Range(0, names.Length)];
        textProfesion.text = profesion;
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
