using UnityEngine;

public enum Sound
{
    /*deslizar,
    MeteoritoExplosion,
    MeteoritoTransicion,
    AlienComming,
    AlienExit,
    AlienStay*/

    Jump,
    FallDeath,
    FallGround,
    Dizzy,
    ObjectHit,
    MapChange,
    NextLevel,
    MapTurn,
    PlatformHit1,
    PlatformHit2,
    PlatformHit3,
    GameOver
}

public class SFXManager : MonoBehaviour
{
    public static SFXManager SI;

    private void Awake()
    {
        SI = SI == null ? this : SI;
    }

    //Referencias a los audios source respectivos
    [SerializeField] private AudioSource jump, fallDeath, fallGround, dizzy, objectHit, mapChange, nextLevel, mapTurn, platformHit1, gameOver;

    public void PlaySound(Sound soundToPlay)
    {
        switch (soundToPlay)
        {
            /*case Sound.Jump:
                if(!deslizar.isPlaying) deslizar.PlayOneShot(deslizar.clip);
                break;*/
            case Sound.Jump:
                jump.PlayOneShot(jump.clip);
                break;
            case Sound.FallDeath:
                fallDeath.PlayOneShot(fallDeath.clip);
                break;
            case Sound.FallGround:
                fallGround.PlayOneShot(fallGround.clip);
                break;
            case Sound.Dizzy:
                if (!dizzy.isPlaying) dizzy.PlayOneShot(dizzy.clip);
                break;
            case Sound.ObjectHit:
                if (!objectHit.isPlaying) objectHit.PlayOneShot(objectHit.clip);
                break;
            case Sound.MapChange:
                mapChange.Play();
                break;
            case Sound.NextLevel:
                nextLevel.PlayOneShot(nextLevel.clip);
                break;
            case Sound.MapTurn:
                mapTurn.PlayOneShot(mapTurn.clip);
                break;
            case Sound.PlatformHit1:
                if(!platformHit1.isPlaying) platformHit1.PlayOneShot(platformHit1.clip);
                break;
            case Sound.GameOver:
                gameOver.PlayOneShot(gameOver.clip);
                break;
        }
    }

    public void StopSound(Sound soundToPlay)
    {
        switch (soundToPlay)
        {
            /*case Sound.MeteoritoTransicion:
                meteoritoTransicion.Stop();
                break;*/
            
        }
    }

}