using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairBotEnemy : FollowerEnemy
{

    [SerializeField] private int _repairRange;
    [SerializeField] private float _repairIntervalSeconds;
    private bool _isRepairing;
    private ExplodeNearPlayer _explodeNearPlayer;

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
            List<Vector3> positions = GetSurroundingPositionsInCircle(transform.position, _repairRange);
            foreach (var position in positions)
            {
                LevelGenerator.Instance.CreateNewCubeAtIfNotExisting(position);
            }
            yield return new WaitForSeconds(_repairIntervalSeconds);
        }
    }

    /** 
    * Gets all 2D integer positions arround the current position inside the _repairRange radius.
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
                positions.Add(new Vector3(transform.position.x + x, 0, transform.position.z + z));
            }
        }
        return positions;
    }


}
