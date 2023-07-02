using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private float _move_Power = -3;
    private Rigidbody2D _rb_enemy;

    [SerializeField]
    private int _enemy_hp = 1;

    // Start is called before the first frame update
    void Start()
    {
        _rb_enemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // à⁄ìÆ
        Vector2 velocity = _rb_enemy.velocity;
        velocity.x = _move_Power;
        _rb_enemy.velocity = velocity;

        // óéâ∫ÇµÇΩèÍçá
        if(transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ï«Ç‚ìGÇ…ìñÇΩÇ¡ÇΩèÍçá
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy")
        {
            _move_Power *= -1;
        }

        // íeÇ∆ìñÇΩÇ¡ÇΩèÍçá
        /*
        if (collision.gameObject.tag == "Bullet")
        {
            _enemy_hp -= 1;

            // HPÇ™0Ç…Ç»Ç¡ÇΩÇÁ
            if(_enemy_hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        */
    }
}
