using UnityEngine;

public enum Sound
{
    deslizar,
    Jump,
    ObjectHit,
    Checkpoint,
    MeteoritoExplosion,
    MeteoritoTransicion,
    Fall,
    AlienComming,
    AlienExit,
    AlienStay
}

public class SFXManager : MonoBehaviour
{
    public static SFXManager SI;

    private void Awake()
    {
        SI = SI == null ? this : SI;
    }

    //Referencias a los audios source respectivos
    [SerializeField] private AudioSource deslizar, salto, choqueObjeto, checkpoint, meteoritoExplosion, meteoritoTransicion, caer, ovniLlegada, ovniSalida, ovniDetenido;

    public void PlaySound(Sound soundToPlay)
    {
        switch (soundToPlay)
        {
            case Sound.deslizar:
                if(!deslizar.isPlaying) deslizar.PlayOneShot(deslizar.clip);
                break;
            case Sound.Jump:
                salto.PlayOneShot(salto.clip);
                break;
            case Sound.ObjectHit:
                choqueObjeto.PlayOneShot(choqueObjeto.clip);
                break;
            case Sound.AlienComming:
                ovniLlegada.PlayOneShot(ovniLlegada.clip);
                break;
            case Sound.Checkpoint:
                checkpoint.PlayOneShot(checkpoint.clip);
                break;
            case Sound.MeteoritoExplosion:
                if (!meteoritoExplosion.isPlaying) meteoritoExplosion.PlayOneShot(meteoritoExplosion.clip);
                break;
            case Sound.MeteoritoTransicion:
                meteoritoTransicion.Play();
                break;
            case Sound.Fall:
                caer.PlayOneShot(caer.clip);
                break;
            case Sound.AlienExit:
                ovniSalida.PlayOneShot(ovniSalida.clip);
                break;
            case Sound.AlienStay:
                ovniDetenido.PlayOneShot(ovniDetenido.clip);
                break;
        }
    }

    public void StopSound(Sound soundToPlay)
    {
        switch (soundToPlay)
        {
            case Sound.MeteoritoTransicion:
                meteoritoTransicion.Stop();
                break;
            
        }
    }
    private void Update()
    {
        if (GameManager.SI.currentGameState != GameState.InGame) return;

        if (!PlayerInput.SI.IsJumping)
        {
            PlaySound(Sound.deslizar);
        }
        else{
            deslizar.Stop();
        }
    }
}