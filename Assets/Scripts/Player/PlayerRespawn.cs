using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 respawnLocation;
    private void Awake()
    {
        respawnLocation = Vector2.right * (-4);
    }
    public void Respawn()
    {
        transform.position = respawnLocation;
    }
}
