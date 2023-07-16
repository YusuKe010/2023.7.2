using UnityEngine;

public class MissileMove : MonoBehaviour
{
    // ミサイルの移動速度
    [SerializeField, Header("スピード")] 
    private float _speed = 5;

    // ミサイルのRigidbody
    private Rigidbody2D _rb_missile;

    [SerializeField]
    private Spawner _spawner;   // スポナーの取得

    private float _angle = 0;

    private Vector2 _velocity;

    void Start()
    {
        _rb_missile = GetComponent<Rigidbody2D>();

        _angle = transform.rotation.eulerAngles.z;

        transform.eulerAngles += new Vector3( 0, 0, 180f);

        _velocity.x = _speed * Mathf.Cos(_angle * Mathf.Deg2Rad);
        _velocity.y = _speed * Mathf.Sin(_angle * Mathf.Deg2Rad);
    }

    void Update()
    {
        // ミサイルの移動
        _rb_missile.velocity = _velocity;
    }


    // 当たり判定
    ///*
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        // 壁と当たった場合
        if(collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player") 
            || collision.gameObject.CompareTag("Ground") )
        {
            Death();
        }
    }
    //*/

    /*
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
    */

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

        Destroy(gameObject);
    }
}
