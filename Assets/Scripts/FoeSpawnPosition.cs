using System.Collections.Generic;
using UnityEngine;

public class FoeSpawnPosition : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnAreas;

    private int _minNumber = 0;

    public Vector3 GetArea()
    {
        int areaIndex = UnityEngine.Random.Range(_minNumber, _spawnAreas.Length);

        return _spawnAreas[areaIndex].position;
    }
}
