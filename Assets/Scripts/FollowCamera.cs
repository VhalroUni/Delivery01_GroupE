using UnityEngine;

public class FollowCam : MonoBehaviour
{

    public float follow_speed = 2f;
    public float y_offset = 1f;
    public float x_offset = 5f;
    [SerializeField] private Transform target;

    void Update()
    {

        Vector3 new_pos = new Vector3(target.position.x - x_offset, target.position.y + y_offset, -10f);

        transform.position = Vector3.Slerp(transform.position, new_pos, follow_speed * Time.deltaTime);

    }
}
