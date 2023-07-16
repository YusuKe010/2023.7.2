using UnityEngine;

public class Spawner : MonoBehaviour
{
    // 敵のプレハブ用
    [SerializeField]
    private GameObject _ObjPrefab;

    [SerializeField]
    private int _spawner_hp = 5;

    // スポーン間隔
    [SerializeField, Header("生成間隔")]
    private float _spawn_time = 1;

    [SerializeField, Header("最大生成個数")]
    private int _object_spawn_max = 1;

    [SerializeField, Header("生成位置")]
    Transform _spawnPos;

    [SerializeField, Header("無敵フラグ")]
    private bool _invicibleFlag = false;

    // スポーン時間カウント
    private float _spawn_time_count = 0f;

    int _object_num;

    void Start()
    {

        // 無敵の場合
        if (_invicibleFlag)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(1f, 0f, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _spawn_time_count += Time.deltaTime;

        if (_spawn_time_count > _spawn_time && _object_num < _object_spawn_max)
        {
            _object_num++;
            var gm_object = Instantiate(_ObjPrefab, _spawnPos.position, transform.rotation);
            if (gm_object.GetComponent<EnemyMove>() != null)
            {
                gm_object.GetComponent<EnemyMove>().SetSpawner(this);
            }　
            else if(gm_object.GetComponent<MissileMove>() != null)
            {
                gm_object.GetComponent<MissileMove>().SetSpawner(this);
            }

            // var enemy_rb = GetComponent<Rigidbody2D>();
            _spawn_time_count = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 無敵でない場合
        if (!_invicibleFlag) {
            // 弾に当たった場合
            if (collision.gameObject.tag == "Bullet")
            {
                _spawner_hp--;

                if (_spawner_hp <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    public void DeleteObject()
    {
        _object_num--;
    }
}
