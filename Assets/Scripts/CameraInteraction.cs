using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraInteraction : MonoBehaviour
{
    [Header("Imagem de Overlay")]
    [Tooltip("Imagem que ficará cobrindo a visão")]
    public Image overlayImage;

    [Header("Mudança de Cena")]
    [Tooltip("Nome da cena ao clicar no BoxCollider")]
    public string sceneBoxCollider;

    [Tooltip("Nome da cena ao clicar no PolygonCollider")]
    public string scenePolygonCollider;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        CentralizarImagem();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            VerificarClique();
        }
    }

    void CentralizarImagem()
    {
        if (overlayImage == null)
            return;

        RectTransform rect = overlayImage.rectTransform;

        // Centraliza a imagem na tela
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = Vector2.zero;

        // Mantém proporção e cobre a tela
        overlayImage.preserveAspect = true;

        float screenRatio = (float)Screen.width / Screen.height;
        float imageRatio = rect.sizeDelta.x / rect.sizeDelta.y;

        if (imageRatio > screenRatio)
        {
            // Imagem mais larga
            rect.sizeDelta = new Vector2(Screen.height * imageRatio, Screen.height);
        }
        else
        {
            // Imagem mais alta
            rect.sizeDelta = new Vector2(Screen.width, Screen.width / imageRatio);
        }
    }

    void VerificarClique()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Verifica BoxCollider
            if (hit.collider is BoxCollider)
            {
                if (!string.IsNullOrEmpty(sceneBoxCollider))
                {
                    SceneManager.LoadScene(sceneBoxCollider);
                }
            }

            // Verifica PolygonCollider
            if (hit.collider is MeshCollider)
            {
                if (!string.IsNullOrEmpty(scenePolygonCollider))
                {
                    SceneManager.LoadScene(scenePolygonCollider);
                }
            }
        }
    }
}