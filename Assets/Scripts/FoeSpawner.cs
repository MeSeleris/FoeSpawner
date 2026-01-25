using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class FoeSpawner : MonoBehaviour
{
    [SerializeField] private Foe _prefabFoe;
    [SerializeField] private FoeSpawnPosition _position;

    private int _currentSize = 1;
    private int _maxCapacity = 5;
    private float _minSpeed = 3f;
    private float _maxSpeed = 7f;

    private WaitForSeconds _spawnDelay = new WaitForSeconds(1f);

    private ObjectPool<Foe> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Foe>(
            createFunc: () => Instantiate(_prefabFoe),
            actionOnGet: PrepareGetObject,
            actionOnRelease: PrepareReleaseObject,
            actionOnDestroy: PrepareDestroyObject,
            collectionCheck: true,
            defaultCapacity: _currentSize,
            maxSize: _maxCapacity
            );
    }

    private void Start()
    {
        StartCoroutine(SpawnFoe());
    }

    private void PrepareGetObject(Foe foe)
    {
        foe.Died += OnFoeDied;       

        Vector3 spawnPosition = _position.GetArea();
        foe.transform.position = spawnPosition;

        foe.gameObject.SetActive(true);

        float randomX = UnityEngine.Random.Range(-1f, 1f);
        float randomZ = UnityEngine.Random.Range(-1f, 1f);
        Vector3 randomDirection = new Vector3(randomX, 0, randomZ).normalized;

        if(randomDirection == Vector3.zero)
            randomDirection = Vector3.forward;

        float speed = Random.Range(_minSpeed, _maxSpeed);

        foe.Initialize(speed, randomDirection);
    }

    private void PrepareReleaseObject(Foe foe)
    {
        foe.gameObject.SetActive(false);
    }

    private void PrepareDestroyObject(Foe foe)
    {
        Destroy(foe.gameObject);
    }

    private void OnFoeDied(Foe foe)
    {
        foe.Died -= OnFoeDied;
        _pool.Release(foe);
    }

    private IEnumerator SpawnFoe()
    {
        while (enabled)
        {
            if (_pool.CountActive < _maxCapacity)
            {
                _pool.Get();
            }

            yield return _spawnDelay;
        }
    }

}
