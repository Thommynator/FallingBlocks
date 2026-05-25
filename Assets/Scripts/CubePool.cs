using UnityEngine;
using UnityEngine.Pool;

public class CubePool {
    private readonly ObjectPool<Cube> _pool;

    public CubePool(Cube cubePrefab, int initialSize, int maxSize) {
        _pool = new ObjectPool<Cube>(
            () => Object.Instantiate(cubePrefab),
            cube => { cube.SpawnActions(); },
            cube => { cube.DeactivationActions(); },
            cube => Object.Destroy(cube.gameObject),
            true,
            initialSize,
            maxSize);
    }

    public Cube Get() {
        return _pool.Get();
    }

    public void Release(Cube cube) {
        _pool.Release(cube);
    }
}