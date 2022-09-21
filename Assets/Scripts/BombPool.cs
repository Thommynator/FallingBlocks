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
                var bomb = Instantiate(_bombPrefab);
                bomb.SetParentTo(this.transform);
                return bomb;
            },
            bomb =>
            {
                bomb.ResetTrigger();
                bomb.ResetVelocity();
                bomb.SetGameObjectActive();
            },
            bomb => bomb.SetGameObjectInactive(),
            bomb => Destroy(bomb.gameObject),
            true,
            10,
            40
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
