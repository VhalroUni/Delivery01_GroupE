using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    [SerializeField] private PlayerHP playerHPManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        SceneChanger.instance.LoadEnd();
    }

}
