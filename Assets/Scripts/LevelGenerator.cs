using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Pool;

public class LevelGenerator : MonoBehaviour
{

    public static LevelGenerator Instance;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _rows;
    [SerializeField] private int _cols;

    private Vector3 minCoordinates;
    private Vector3 maxCoordinates;
    private ObjectPool<Cube> _cubePool;

    private Dictionary<Vector2Int, Cube> _map;

    void Awake()
    {
        Debug.Log("Instantiate Cube Pool");
        Instance = this;
        _map = new Dictionary<Vector2Int, Cube>();
        _cubePool = new ObjectPool<Cube>(
            () => Instantiate<Cube>(_cubePrefab),
            cube => 
            {
                cube.SpawnActions();
            },
            cube =>
            {
                cube.DeactivationActions();
            },
            cube => Destroy(cube.gameObject),
            true,
            _rows * _cols,
            Mathf.RoundToInt((_rows * _cols) * 1.3f)
            );
    }

    void Start()
    {
        minCoordinates = new Vector3(-_cols / 2, 0, 0);
        maxCoordinates = new Vector3(_cols / 2, 0, _rows);

        for (int col = 0; col < _cols; col++)
        {
            for (int row = 0; row < _rows; row++)
            {
                CreateNewCubeAtIfNotExisting(new Vector3(col - _cols / 2, 0, row));
            }
        }
    }

    public Cube GetCubeAt(Vector3 position)
    {
        return _map.TryGetValue(position.To2dInt(), out Cube cube) ? cube : null;
    }

    public Cube GetRandomCube()
    {
        Cube randomCube;
        do
        {
            randomCube = _map.ElementAt(Random.Range(0, _map.Count)).Value;
        } while (randomCube == null);

        return randomCube;
    }

    public Vector3 GetRandomCubePosition()
    {
        return GetRandomCube().transform.position;
    }

    public void CreateNewCubeAtIfNotExisting(Vector3 position)
    {

        if (GetCubeAt(position) != null || !IsInBounds(position))
        {
            return;
        }

        Vector2Int position2D = position.To2dInt();

        var cube = _cubePool.Get();
        cube.transform.position = new Vector3(position2D.x, 0, position2D.y);
        cube.transform.SetParent(this.gameObject.transform);
        SetCubeAt(cube, position2D);
    }

    public IEnumerator ResetCube(Cube cube)
    {
        // first mark the Cube position in the grid as free, which allows a respawn,
        // later release the Cube back to the pool
        SetCubeAt(null, cube.transform.position.To2dInt());
        yield return new WaitForSeconds(2);
        _cubePool.Release(cube);
    }

    private void SetCubeAt(Cube cube, Vector2Int position)
    {
        _map[position] = cube;
    }

    public bool IsInBounds(Vector3 position)
    {
        return position.x >= minCoordinates.x
            && position.z >= minCoordinates.z
            && position.x <= maxCoordinates.x
            && position.z <= maxCoordinates.z;
    }

    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int col = 0; col < _cols; col++)
        {
            for (int row = 0; row < _rows; row++)
            {
                Gizmos.DrawWireCube(new Vector3(col - _cols / 2, -0.5f, row), Vector3.one);
            }
        }
    }
}
