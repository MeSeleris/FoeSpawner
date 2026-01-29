using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class Foe : MonoBehaviour
{

    [SerializeField] private float _lifeTime = 10f;
    [SerializeField] private float _speed = 5f;

    private Rigidbody _foeRigidbody;
    private Vector3 _currentDirection;

    public event Action<Foe> Died;

    public void Initialize(float speed, Vector3 direction)
    {
        _speed = speed;

        _foeRigidbody.linearVelocity = Vector3.zero;
        _foeRigidbody.angularVelocity = Vector3.zero;

        _currentDirection = direction.normalized;

        float randomY = UnityEngine.Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0, randomY, 0);
    }
    
    private void Awake()
    {
        _foeRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Translate(_currentDirection * _speed * Time.deltaTime, Space.World);
        transform.position += _currentDirection * _speed * Time.deltaTime;
    }

    private void OnEnable()
    {
        StartCoroutine(LifeTimer());
    }

    private IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(_lifeTime);

        Died?.Invoke(this);
    }
}
