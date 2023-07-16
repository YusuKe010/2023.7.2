using UnityEngine;

public class Spawner : MonoBehaviour
{
    // �G�̃v���n�u�p
    [SerializeField]
    private GameObject _ObjPrefab;

    [SerializeField]
    private int _spawner_hp = 5;

    // �X�|�[���Ԋu
    [SerializeField, Header("�����Ԋu")]
    private float _spawn_time = 1;

    [SerializeField, Header("�ő吶����")]
    private int _object_spawn_max = 1;

    [SerializeField, Header("�����ʒu")]
    Transform _spawnPos;

    [SerializeField, Header("���G�t���O")]
    private bool _invicibleFlag = false;

    // �X�|�[�����ԃJ�E���g
    private float _spawn_time_count = 0f;

    int _object_num;

    void Start()
    {

        // ���G�̏ꍇ
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
            }�@
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
        // ���G�łȂ��ꍇ
        if (!_invicibleFlag) {
            // �e�ɓ��������ꍇ
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
