using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class LevelGenerator : MonoBehaviour {
    public static LevelGenerator Instance;

    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _rows;
    [SerializeField] private int _cols;
    private ObjectPool<Cube> _cubePool;

    private Dictionary<Vector2Int, Cube> _map;
    private Vector3 _maxCoordinates;

    private Vector3 _minCoordinates;

    private WaitForSeconds _waitForTwoSeconds;

    void Awake() {
        Debug.Log("Instantiate Cube Pool", this);
        _waitForTwoSeconds = new WaitForSeconds(2);
        Instance = this;
        _map = new Dictionary<Vector2Int, Cube>();
        _cubePool = new ObjectPool<Cube>(
            () => Instantiate(_cubePrefab),
            cube => { cube.SpawnActions(); },
            cube => { cube.DeactivationActions(); },
            cube => Destroy(cube.gameObject),
            true,
            _rows * _cols,
            Mathf.RoundToInt((_rows * _cols) * 1.3f)
        );
    }

    void Start() {
        var halfCols = _cols / 2;
        _minCoordinates = new Vector3(-halfCols, 0, 0);
        _maxCoordinates = new Vector3(halfCols, 0, _rows);

        for (var col = 0; col < _cols; col++) {
            for (var row = 0; row < _rows; row++) {
                CreateNewCubeAtIfNotExisting(new Vector3(col - halfCols, 0, row));
            }
        }
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(0, 0, _rows / 2), new Vector3(_rows, 3, _cols));
    }

    private bool ExistsCubeAt(Vector3 position) {
        return _map.ContainsKey(position.To2dInt());
    }

    private Cube GetRandomCube() {
        return _map.ElementAt(Random.Range(0, _map.Count)).Value;
    }

    public Vector3 GetRandomCubePosition() {
        return GetRandomCube().transform.position;
    }

    public void CreateNewCubeAtIfNotExisting(Vector3 position) {
        if (ExistsCubeAt(position) || !IsInBounds(position)) {
            return;
        }

        Vector2Int position2D = position.To2dInt();

        var cube = _cubePool.Get();
        cube.SetPosition(new Vector3(position2D.x, 0, position2D.y));
        cube.SetParentTo(transform);
        SetCubeAt(cube, position2D);
    }

    public IEnumerator ResetCube(Cube cube) {
        // first mark the Cube position in the grid as free, which allows a respawn,
        // later release the Cube back to the pool, i.e. the block can fall down while another one is spawned again
        _map.Remove(cube.transform.position.To2dInt());
        yield return _waitForTwoSeconds;
        _cubePool.Release(cube);
    }

    private void SetCubeAt(Cube cube, Vector2Int position) {
        _map[position] = cube;
    }

    private bool IsInBounds(Vector3 position) {
        return position.x >= _minCoordinates.x
               && position.z >= _minCoordinates.z
               && position.x <= _maxCoordinates.x
               && position.z <= _maxCoordinates.z;
    }
}