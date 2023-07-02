using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float _move_Power = -3;
    private Rigidbody2D _rb_enemy;

    // Start is called before the first frame update
    void Start()
    {
        _rb_enemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = _rb_enemy.velocity;
        velocity.x = _move_Power;
        _rb_enemy.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy")
        {
            _move_Power *= -1;
        }
    }
}
