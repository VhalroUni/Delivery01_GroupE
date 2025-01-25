using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private int y;
    private Vector2 respawnLocation;
    private void Awake()
    {
        //respawnLocation = Vector2.right * (-4);
        respawnLocation = new Vector2(x, y); //!
        Respawn(); //!
    }
    private void Update()
    {
        respawnLocation = new Vector2(x, y);
    }
    public void Respawn()
    {
        transform.position = respawnLocation;
    }
}
