using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //Array con las pistas de audio de las diferentes escenas
    //Es importante llevar un orden como si de un cd se tratara
    public AudioSource[] audioTracks;

    //Pista actual en reproduccion
    public int currentTrack;

    //Variable que me indica si debe o no reproducirse la pista 
    public bool audioCanBePlayed;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (audioCanBePlayed)
        {
            if (!audioTracks[currentTrack].isPlaying)
            {
                audioTracks[currentTrack].Play();
            }
        }

        if(PlayerController.SI.State == PlayerState.Normal)
        {
            audioTracks[1].pitch = 1f;
            audioTracks[1].volume = 1f;
        }

        else if(PlayerController.SI.State == PlayerState.Dizzy)
        {
            audioTracks[1].pitch = 0.8f;
            audioTracks[1].volume = 0.4f;
        }
    }

    //Metodo que me cambia de pista
    public void PlayNewTrack(int newTrack)
    {
        audioTracks[currentTrack].Stop();
        currentTrack = newTrack;
        audioTracks[currentTrack].Play();
    }

    public void StopSound()
    {
        audioCanBePlayed = false;
        audioTracks[currentTrack].Stop();

    }
}