using UnityEngine;

public class TalksTeme : MonoBehaviour
{
    Talks _talks;

    private void Update()
    {
        TalkTheme();
    }

    void TalkTheme()
    {
        if(_talks == Talks.frio)
        {
            Debug.Log("hace frio");
        }

        if (_talks == Talks.calor) 
        {
            Debug.Log("hace calor");

        }

        if (_talks == Talks.transito)
        {
            Debug.Log("no hay transito");
        }

        if (_talks == Talks.despejado)
        {
            Debug.Log("esta despejado");
        }
    }
}

public enum Talks
{
    frio,
    calor,
    transito,
    despejado
}
