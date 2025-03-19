using UnityEngine;

public class shoo : MonoBehaviour
{
    public GameObject player;
    private Transform fromHere;
    private float time;

    void Update()
    {
        fromHere = player.transform;

        if (Input.GetMouseButtonDown(0)) 
        {
            MakeOneBullet(fromHere);
        }
    }

    void MakeOneBullet(Transform here) 
    {
        var bullet = PoolManager.GetObject();
        
        if (bullet != null) 
        {
            bullet.transform.position = here.position;
        }
    }
}
