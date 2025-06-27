using UnityEngine.UI;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] hojas;
    [SerializeField] private Button flechaIzquierda;
    [SerializeField] private Button flechaDerecha;

    private int paginaActual = 0;
    private bool tutorialActivo = false;

    void Start()
    {
        OcultarTodasLasHojas();

        // Asigna listeners a los botones
        flechaIzquierda.onClick.AddListener(PaginaAnterior);
        flechaDerecha.onClick.AddListener(PaginaSiguiente);
    }

    public void ToggleTutorial()
    {
        tutorialActivo = !tutorialActivo;

        if (tutorialActivo)
        {
            paginaActual = 0;
            MostrarPagina(paginaActual);
        }
        else
        {
            OcultarTodasLasHojas();
        }
    }

    private void MostrarPagina(int indice)
    {
        for (int i = 0; i < hojas.Length; i++)
        {
            hojas[i].SetActive(i == indice);
        }

        flechaIzquierda.gameObject.SetActive(indice > 0);
        flechaDerecha.gameObject.SetActive(indice < hojas.Length - 1);
    }

    private void OcultarTodasLasHojas()
    {
        foreach (var hoja in hojas)
        {
            hoja.SetActive(false);
        }

        flechaIzquierda.gameObject.SetActive(false);
        flechaDerecha.gameObject.SetActive(false);
    }

    private void PaginaSiguiente()
    {
        if (!tutorialActivo) return;

        if (paginaActual < hojas.Length - 1)
        {
            paginaActual++;
            MostrarPagina(paginaActual);
        }
    }

    private void PaginaAnterior()
    {
        if (!tutorialActivo) return;

        if (paginaActual > 0)
        {
            paginaActual--;
            MostrarPagina(paginaActual);
        }
    }
}
