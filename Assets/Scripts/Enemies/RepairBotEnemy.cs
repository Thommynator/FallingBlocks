using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairBotEnemy : FollowerEnemy
{

    [SerializeField] private int _repairRange;
    [SerializeField] private float _repairIntervalSeconds;
    private bool _isRepairing;
    private ExplodeNearPlayer _explodeNearPlayer;

    private WaitForSeconds _repairIntervalWaitForSeconds;

    private void Awake()
    {
        _repairIntervalWaitForSeconds = new WaitForSeconds(_repairIntervalSeconds);
    }

    public override void Start()
    {
        base.Start();
        TryGetComponent<ExplodeNearPlayer>(out _explodeNearPlayer);
        StartCoroutine(RepairSurroundingCubes());
    }

    public void SetIsRepairing(bool newValue)
    {
        _isRepairing = newValue;
    }

    private IEnumerator RepairSurroundingCubes()
    {
        while (!_explodeNearPlayer.IsExploded())
        {
            List<Vector3> positions = GetSurroundingPositionsInSquare(transform.position, _repairRange);
            foreach (var position in positions)
            {
                LevelGenerator.Instance.CreateNewCubeAtIfNotExisting(position);
            }
            yield return _repairIntervalWaitForSeconds;
        }
    }

    /** 
    * Gets all positions arround the current position inside the radius in a circle.
    */
    public List<Vector3> GetSurroundingPositionsInCircle(Vector3 position, int radius)
    {
        List<Vector3> positions = new List<Vector3>();
        for (int x = -radius; x <= radius; x++)
        {
            float rootValue = radius * radius - x * x;
            int zMax = Mathf.Approximately(rootValue, 0.0f) ? 0 : Mathf.FloorToInt(Mathf.Sqrt(rootValue));
            for (int z = -zMax; z <= zMax; z++)
            {
                positions.Add(new Vector3(position.x + x, 0, position.z + z));
            }
        }
        return positions;
    }

    /** 
    * Gets all 2D positions arround the current position inside the radius in a square.
    */
    public List<Vector3> GetSurroundingPositionsInSquare(Vector3 position, int radius)
    {
        List<Vector3> positions = new List<Vector3>();
        for (int x = -radius; x <= radius; x++)
        {
            for (int z = -radius; z <= radius; z++)
            {
                positions.Add(new Vector3(position.x + x, 0, position.z + z));
            }
        }
        return positions;
    }
}