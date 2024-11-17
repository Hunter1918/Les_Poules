using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ennemi : MonoBehaviour
{

    [Tooltip("The speed (in units/s) at which this enemy moves in the scene.")]
    public float speed = 1f;

    private Rigidbody _rigidbody = null;

    private void Awake()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rigidbody.position += (Vector3)transform.up * speed * Time.deltaTime;
    }

}
