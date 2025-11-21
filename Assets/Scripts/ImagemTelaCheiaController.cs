using UnityEngine;

public class ImagemTelaCheiaController : MonoBehaviour
{
    public GameObject telaImagem; // arraste a Image (ou painel) aqui
    private bool desbloqueado = false;

    void Update()
    {
        if (!desbloqueado)
        {
            // Fecha a imagem inicial com ESC
            if (telaImagem.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                FecharImagem();
                desbloqueado = true;
            }
        }
        else
        {
            // Abrir com M
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (!telaImagem.activeSelf)
                    AbrirImagem();
                else
                    FecharImagem();
            }

            // Fechar com ESC
            if (telaImagem.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                FecharImagem();
            }
        }
    }

    // Chamado pelo script do objeto destruído
    public void MostrarImagemInicial()
    {
        AbrirImagem();
    }

    private void AbrirImagem()
    {
        telaImagem.SetActive(true);
        Time.timeScale = 0f;   // PAUSA O JOGO
    }

    private void FecharImagem()
    {
        telaImagem.SetActive(false);
        Time.timeScale = 1f;   // DESPAUSA O JOGO
    }
}
