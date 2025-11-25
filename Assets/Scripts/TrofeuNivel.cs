using UnityEngine;

public class TrofeuNivel : MonoBehaviour
{
    public Sprite nivel1;
    public Sprite nivel2;
    public Sprite nivel3;

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        SistemaXP.OnNivelMudou += AtualizarTrofeu;
    }

    void OnDisable()
    {
        SistemaXP.OnNivelMudou -= AtualizarTrofeu;
    }

    void AtualizarTrofeu(int nivel)
    {
        switch (nivel)
        {
            case 1:
                sr.sprite = nivel1;
                break;
            case 2:
                sr.sprite = nivel2;
                break;
            case 3:
                sr.sprite = nivel3;
                break;
        }
    }
}
