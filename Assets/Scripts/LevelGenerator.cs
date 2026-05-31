using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    public static LevelGenerator Instance;

    [SerializeField] private Cube cubePrefab;
    [SerializeField] private int rows;
    [SerializeField] private int cols;

    private Dictionary<Vector2Int, Cube> _map;
    private Vector3 _maxCoordinates;

    private Vector3 _minCoordinates;

    private WaitForSeconds _waitForTwoSeconds;
    private float spawnHeight = 0;

    void Awake() {
        Instance = this;
        _map = new Dictionary<Vector2Int, Cube>();
    }

    void Start() {
        var halfCols = cols / 2;
        _minCoordinates = new Vector3(-halfCols, spawnHeight, 0);
        _maxCoordinates = new Vector3(halfCols, spawnHeight, rows);

        for (var col = 0; col < cols; col++) {
            for (var row = 0; row < rows; row++) {
                CreateNewCube(new Vector3(col - halfCols, spawnHeight, row));
            }
        }
    }

    public void EnableCube(Vector2Int position) {
        var cube = _map.GetValueOrDefault(position, null);
        if (cube == null || cube.isActiveAndEnabled) {
            return;
        }

        cube.SetPosition(new Vector3(position.x, spawnHeight, position.y));
        cube.SpawnActions();
    }

    private Cube GetRandomCube() {
        return _map.ElementAt(Random.Range(0, _map.Count)).Value;
    }

    public Vector3 GetRandomCubePosition() {
        return GetRandomCube().transform.position;
    }


    private void CreateNewCube(Vector3 position) {
        if (!IsInBounds(position)) {
            return;
        }

        var cube = Instantiate(cubePrefab);
        cube.SetPosition(position);
        cube.SetParentTo(transform);
        SetCubeAt(cube, position.To2dInt());
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