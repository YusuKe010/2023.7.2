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
        // 移動
        Vector2 velocity = _rb_enemy.velocity;
        velocity.x = _move_Power;
        _rb_enemy.velocity = velocity;

        // 落下した場合
        if(transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 壁や敵に当たった場合
        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy")
        {
            _move_Power *= -1;
        }

        // 弾と当たった場合
        /*
        if (collision.gameObject.tag == "Bullet")
        {
            _enemy_hp -= 1;

            // HPが0になったら
            if(_enemy_hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        */
    }
}
