using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private int y;
    private Vector2 respawnLocation;
    private void Awake()
    {
        respawnLocation = new Vector2(x, y); 
        Respawn(); 
    }

    public void OnEnable()
    {
        ControlPoint.OnEnter += AssignRespawnLocaltion;
    }

    public void OnDisable()
    {
        ControlPoint.OnEnter += AssignRespawnLocaltion;
    }

    public void Respawn()
    {
        transform.position = respawnLocation;
    }

    public void AssignRespawnLocaltion(ControlPoint cp) 
    {
        respawnLocation = cp.transform.position;    
    }
}
