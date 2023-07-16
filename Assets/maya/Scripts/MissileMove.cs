using UnityEngine;

public class MissileMove : MonoBehaviour
{
    // �~�T�C���̈ړ����x
    [SerializeField, Header("�X�s�[�h")] 
    private float _movePower = 5;

    // �~�T�C����Rigidbody
    private Rigidbody2D _rb_missile;

    [SerializeField]
    private Spawner _spawner;   // �X�|�i�[�̎擾

    void Start()
    {
        _rb_missile = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // �~�T�C���̈ړ�
        Vector2 velocity = _rb_missile.velocity;
        velocity.x = _movePower;
        _rb_missile.velocity = velocity;
    }


    // �����蔻��
    /*
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        // �ǂƓ��������ꍇ
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
        // �ǂƓ��������ꍇ
        if (collision.gameObject.CompareTag("Wall"))
        {
            Death();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Death();
        }
    }

    // �X�|�i�[�X�N���v�g�̃Z�b�g
    public void SetSpawner(Spawner spawner)
    {
        _spawner = spawner;
    }

    // �I�u�W�F�N�g�̔j��
    private void Death()
    {
        if (_spawner)
        {
            _spawner.DeleteObject();
        }

        Destroy(this.gameObject, 0.1f);
    }
}
