using UnityEngine;
using UnityEngine.Pool;

public class BombPool : MonoBehaviour
{

    public static BombPool Instance;
    [SerializeField] private Bomb _bombPrefab;
    private ObjectPool<Bomb> _bombPool;

    void Awake()
    {
        Instance = this;
        _bombPool = new ObjectPool<Bomb>(
            () =>
            {
                var bomb = Instantiate<Bomb>(_bombPrefab);
                bomb.transform.SetParent(this.transform);
                return bomb;
            },
            bomb =>
            {
                bomb.GetComponent<Rigidbody>().velocity = Vector3.zero;
                bomb.gameObject.SetActive(true);
            },
            bomb => bomb.gameObject.SetActive(false),
            bomb => Destroy(bomb.gameObject),
            true,
            10,
            30
        );
    }

    public Bomb GetBomb()
    {
        return _bombPool.Get();
    }

    public void ReleaseBomb(Bomb bomb)
    {
        _bombPool.Release(bomb);
    }



}
