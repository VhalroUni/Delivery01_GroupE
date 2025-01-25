using UnityEngine;

public class EffectSound : MonoBehaviour
{
    [SerializeField] private AudioClip collectar1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ControlSound.instance.RunSound(collectar1);
            Destroy(gameObject);
        }
    }
}
