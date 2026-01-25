using System.Collections.Generic;
using UnityEngine;

public class FoeSpawnPosition : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnAreas;

    private int _minNumber = 0;
    private int _maxNumber = 3;

    public Vector3 GetArea()
    {
        int areaIndex = UnityEngine.Random.Range(_minNumber, _maxNumber);

        return _spawnAreas[areaIndex].position;
    }
}
