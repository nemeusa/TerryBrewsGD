using System;
using System.Collections;
using System.Drawing;
using TMPro;
using UnityEditor;
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

    public GameObject visor;

    [SerializeField] bool _onlyGood, _onlyImposter;

    public string Theme;

    public ParticleSystem bloodPartycles;

    public bool talkThemes;

    public Dialogue dialogue;

    public bool randomBlock;

    void Awake()
    {

        //dialogue = GetComponent<ClientDialogue>();

        _fsm = new FSM<TypeFSM>();
        _fsm.AddState(TypeFSM.EnterBar, new EnterBarState(_fsm, this));
        _fsm.AddState(TypeFSM.Order, new OrderState(_fsm, this));
        _fsm.AddState(TypeFSM.ExitBar, new ExitBarState(_fsm, this));
        _fsm.AddState(TypeFSM.Attack, new AttackState(_fsm, this));
        _fsm.AddState(TypeFSM.Death, new DeathState(_fsm, this));
        _fsm.AddState(TypeFSM.VIP, new VIPState(_fsm, this));

        StartCoroutine(StartVars());

        //else Charla();
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
    public void Charla()
    {
        if (!imposter)
        {


            string charla;

            charla = charlaGood[UnityEngine.Random.Range(0, charlaGood.Length)];
            Debug.Log("charla normal good");
            textCharla.text = charla;
            currentDialogue = charla;

        }

        else
        {
            string charla;

            charla = charlaBad[UnityEngine.Random.Range(0, charlaBad.Length)];
            Debug.Log("charla normal bad");
            textCharla.text = charla;
            currentDialogue = charla;
        }

    }

    //public void Charla()
    //{
    //    if (!imposter)
    //    {

    //        if (UnityEngine.Random.Range(0, 101) <= 50)
    //        {
    //            string charla;

    //            charla = charlaGood[UnityEngine.Random.Range(0, charlaGood.Length)];
    //            Debug.Log("charla normal good");
    //            textCharla.text = charla;
    //            currentDialogue = charla;
    //        }
    //        else
    //        {
    //            talkThemes = true;
    //            StartCoroutine(CharlaThemeGoodCoroutine());
    //            //CharlaThemeGood();
    //            Debug.Log("charla tema good");
    //            currentDialogue = Theme;

    //        }
    //        //Debug.Log(charla);
    //    }

    //    else
    //    {

    //        if (UnityEngine.Random.Range(0, 101) <= 50)
    //        {
    //            string charla;

    //            charla = charlaBad[UnityEngine.Random.Range(0, charlaBad.Length)];
    //            Debug.Log("charla normal bad");
    //            textCharla.text = charla;
    //            currentDialogue = charla;
    //        }
    //        else
    //        {
    //            talkThemes = true;
    //            StartCoroutine(CharlaThemeBadCoroutine());
    //            //CharlaThemeBad();
    //            Debug.Log("charla tema bad");
    //            currentDialogue = Theme;
    //        }
    //    }

    //}

    //public void CharlaThemeBad()
    //{
    //    string[] blabla = { "Frio", "Calor", "Trafico", "Despejado" };

    //    //Theme = blabla[UnityEngine.Random.Range(0, blabla.Length)];

    //    //if (Theme != player._talkTheme.currentTheme[player._talkTheme._indexTheme])
    //    //    textCharla.text = Theme;

    //    do
    //    {
    //        Theme = blabla[UnityEngine.Random.Range(0, blabla.Length)];
    //    }
    //    while (Theme == player._talkTheme.currentTheme[player._talkTheme._indexTheme]);
    //    textCharla.text = Theme;
    //}

    //public void CharlaThemeGood()
    //{
    //    string[] blabla = { "Frio", "Calor", "Trafico", "Despejado" };

    //    //Theme = blabla[UnityEngine.Random.Range(0, blabla.Length)];

    //    //if (Theme == player._talkTheme.currentTheme[player._talkTheme._indexTheme])
    //    //    textCharla.text = Theme;

    //    //else CharlaThemeGood();

    //    do
    //    {
    //        Theme = blabla[UnityEngine.Random.Range(0, blabla.Length)];
    //    }
    //    while (Theme != player._talkTheme.currentTheme[player._talkTheme._indexTheme]);

    //    textCharla.text = Theme;
    //}


    public IEnumerator CharlaThemeGoodCoroutine()
    {
        string[] blabla = { "Frio", "Calor", "Trafico", "Carretera_Libre" };

        do
        {
            Theme = blabla[UnityEngine.Random.Range(0, blabla.Length)];

            textCharla.text = "Probando: " + Theme;

            yield return new WaitForSeconds(0.01f);

        } 
        while (Theme != player._talkTheme.currentTheme[player._talkTheme._indexTheme]);

        textCharla.text = Theme;
    }

    public IEnumerator CharlaThemeBadCoroutine()
    {
        string[] blabla = { "Frio", "Calor", "Trafico", "Carretera_Libre" };

        do
        {
            Theme = blabla[UnityEngine.Random.Range(0, blabla.Length)];

            textCharla.text = "Probando: " + Theme;

            yield return new WaitForSeconds(0.01f);

        } 
        while (Theme == player._talkTheme.currentTheme[player._talkTheme._indexTheme]);

        textCharla.text = Theme;
    }

    public void TextColor()
    {
        if (player.help)
        {
            if (!imposter) textCharla.color = UnityEngine.Color.green;
            else textCharla.color = UnityEngine.Color.red;
        }

        else textCharla.color = UnityEngine.Color.white;
    }

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
