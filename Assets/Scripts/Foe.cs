using System;
using System.Collections;
using System.Net.Sockets;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Foe : MonoBehaviour
{
   
    [SerializeField] private float _lifeTime = 10f;
    [SerializeField] private float _speed = 5f;

    private Rigidbody _foeRigidbody;
    private Vector3 _moveDirection;

    public event Action<Foe> Died;

    public void Initialize(float speed, Vector3 direction)
    {
        _speed = speed;
        _moveDirection = direction.normalized;

        _foeRigidbody.linearVelocity = Vector3.zero;
        _foeRigidbody.angularVelocity = Vector3.zero;

        if(_moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_moveDirection);
        }
    }

    private void Awake()
    {
        _foeRigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartCoroutine(LifeTimer());
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    private IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(_lifeTime);

        Died?.Invoke(this);
    }
}
