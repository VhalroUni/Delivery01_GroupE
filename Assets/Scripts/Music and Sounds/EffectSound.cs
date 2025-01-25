using UnityEngine;

public class EffectSound : MonoBehaviour
{
    [SerializeField] private AudioClip collect1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ControlSound.instance.RunSound(collect1);
            Destroy(gameObject);
        }
    }
}
