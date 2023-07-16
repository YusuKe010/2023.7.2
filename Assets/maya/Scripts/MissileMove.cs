using UnityEngine;

public class MissileMove : MonoBehaviour
{
    // ミサイルの移動速度
    [SerializeField, Header("スピード")] 
    private float _movePower = 5;

    // ミサイルのRigidbody
    private Rigidbody2D _rb_missile;

    [SerializeField]
    private Spawner _spawner;   // スポナーの取得

    void Start()
    {
        _rb_missile = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // ミサイルの移動
        Vector2 velocity = _rb_missile.velocity;
        velocity.x = _movePower;
        _rb_missile.velocity = velocity;
    }


    // 当たり判定
    /*
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        // 壁と当たった場合
        if(collision.gameObject.CompareTag("Wall"))
        {
            Death();
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            Death();
        }
    }
    */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 壁と当たった場合
        if (collision.gameObject.CompareTag("Wall"))
        {
            Death();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Death();
        }
    }

    // スポナースクリプトのセット
    public void SetSpawner(Spawner spawner)
    {
        _spawner = spawner;
    }

    // オブジェクトの破壊
    private void Death()
    {
        if (_spawner)
        {
            _spawner.DeleteObject();
        }

        Destroy(this.gameObject, 0.1f);
    }
}
