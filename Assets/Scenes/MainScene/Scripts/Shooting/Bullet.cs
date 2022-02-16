using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [Space(10)]
    [SerializeField][Range(7, 15)] private float _flightSpeed;

    private void OnEnable()
    {
        if (_rigidbody) { _rigidbody.velocity = transform.right * _flightSpeed; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            Debug.Log("hit");
        }

        Destroy(gameObject);
    }
}
