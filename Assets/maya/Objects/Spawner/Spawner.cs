using UnityEngine;

public class Spawner : MonoBehaviour
{
    // �G�̃v���n�u�p
    [SerializeField]
    private GameObject _EnemyPrefab;

    [SerializeField]
    private int _spawner_hp = 5;

    // �X�|�[���Ԋu
    [SerializeField]
    private float _spawn_time = 1;

    [SerializeField, Header("�����ʒu")]
    Transform _spawnPos;
    // �X�|�[�����ԃJ�E���g
    private float _spawn_time_count = 0f;

    int _enemyNum;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _spawn_time_count += Time.deltaTime;

        if (_spawn_time_count > _spawn_time && _enemyNum < 3)
        {
            _enemyNum++;
            var enemy = Instantiate(_EnemyPrefab, _spawnPos.position, Quaternion.identity);
            enemy.GetComponent<EnemyMove>().SetSpawner(this);

            var enemy_rb = GetComponent<Rigidbody2D>();
            _spawn_time_count = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            _spawner_hp--;

            if (_spawner_hp <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void DeleteEnemy()
    {
        _enemyNum--;
    }
}
