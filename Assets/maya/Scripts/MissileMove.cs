using UnityEngine;

public class MissileMove : MonoBehaviour
{
    // �~�T�C���̈ړ����x
    [SerializeField, Header("�X�s�[�h")] 
    private float _speed = 5;

    // �~�T�C����Rigidbody
    private Rigidbody2D _rb_missile;

    [SerializeField]
    private Spawner _spawner;   // �X�|�i�[�̎擾

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
        // �~�T�C���̈ړ�
        _rb_missile.velocity = _velocity;
    }


    // �����蔻��
    ///*
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        // �ǂƓ��������ꍇ
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
    */

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

        Destroy(gameObject);
    }
}
