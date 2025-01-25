using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource; // Asigna tu AudioSource en el inspector.

    private static MusicManager instance; // Referencia única para evitar duplicados.

    void Awake()
    {
        // Comprobar si ya existe una instancia del objeto.
        if (instance == null)
        {
            instance = this; // Asignar esta instancia.
            DontDestroyOnLoad(gameObject); // Evitar que se destruya al cambiar de escena.
        }
        else
        {
            Destroy(gameObject); // Eliminar duplicados si ya hay un objeto persistente.
        }

        // Asegurarse de que la música esté reproduciéndose.
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.loop = true; // Configurar para que se reproduzca en bucle.
            musicSource.Play();
        }
    }
}
