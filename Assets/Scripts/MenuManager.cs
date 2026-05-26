using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private string NomeDoLevelJogo;
    [SerializeField] private GameObject MenuCreditos;
    [SerializeField] private GameObject MenuPrincipal;



    public void IniciarJogo()
    {
        AudioManager.Instance.Play("Menu");
        SceneManager.LoadScene(NomeDoLevelJogo);
    }

    public void AbrirCreditos()
    {
        AudioManager.Instance.Play("Menu");
        MenuPrincipal.SetActive(false);
        MenuCreditos.SetActive(true);
    }

    public void FecharCreditos()
    {
        AudioManager.Instance.Play("Menu");
        MenuPrincipal.SetActive(true);
        MenuCreditos.SetActive(false);
    }



}

