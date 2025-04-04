using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] private GameObject player;
    private PlayerHP playerHPManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            playerHPManager = player.GetComponent<PlayerHP>();
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        Coins.OnCoinCollected += Win;
        PlayerHP.OnDeath += Lose;
    }
    
    private void OnDisable()
    {
        Coins.OnCoinCollected -= Win;
        PlayerHP.OnDeath -= Lose;
    }

    private void Win(Coins coin)
    {
        if (coin.CompareTag("win"))
        {
            SceneChanger.instance.LoadEnd();
        }
    }
    private void Lose()
    {
        playerHPManager.ResetHP();
        SceneChanger.instance.LoadEnd();
    }

}
