using System.Threading;
using UnityEngine;

public class bullet : MonoBehaviour
{
    float time;
    bool facingRight;

    void Update()
    {
        if (gameObject.activeSelf) 
        {
            time += Time.deltaTime;

            if (time >= 2f) 
            {
                gameObject.SetActive(false);
                time = 0;
            }
        }
    }
}
