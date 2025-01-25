using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public float followSpeed = 2f;
    public float yOffset = 1f;
    public float xOffset = 5f;
    [SerializeField] private Transform target;

    void Update()
    {
        Vector3 new_pos = new Vector3(target.position.x - xOffset, target.position.y + yOffset, -10f);

        transform.position = Vector3.Slerp(transform.position, new_pos, followSpeed * Time.deltaTime);
    }
}
