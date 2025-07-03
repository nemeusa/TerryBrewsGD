using UnityEngine;
using UnityEngine.Video;

public class VideoToggle : MonoBehaviour
{
    [Header("Video Settings")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject otherObjectToCloseWith; // Objeto que apaga el video si se clickea

    private bool isPlaying = false;

    void Update()
    {
        // Tecla Q para detener el video
        if (isPlaying && Input.GetKeyDown(KeyCode.Q))
        {
            StopVideo();
        }

        // Click para detectar selección
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject clicked = hit.transform.gameObject;

                if (clicked == gameObject)
                {
                    if (!isPlaying)
                        PlayVideo();
                    else
                        StopVideo();
                }
                else if (clicked == otherObjectToCloseWith && isPlaying)
                {
                    StopVideo();
                }
            }
        }
    }

    void PlayVideo()
    {
        videoPlayer.Play();
        isPlaying = true;

        // Activa el renderer si lo tenés desactivado
        if (videoPlayer.targetCameraAlpha == 0)
            videoPlayer.targetCameraAlpha = 1;

        Debug.Log("Video encendido");
    }

    void StopVideo()
    {
        videoPlayer.Stop();
        isPlaying = false;

        // Oculta el video si estás usando Camera Alpha
        if (videoPlayer.targetCameraAlpha > 0)
            videoPlayer.targetCameraAlpha = 0;

        Debug.Log("Video apagado");
    }
}
