using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    /// <summary>���E�ړ������</summary>
    [SerializeField] float m_movePower = 5f;
    /// <summary>�W�����v�����</summary>
    [SerializeField] float m_jumpPower = 15f;
    /// <summary>���͂ɉ����č��E�𔽓]�����邩�ǂ����̃t���O</summary>
    [SerializeField] bool m_flipX = false;
    Rigidbody2D m_rb = default;
    SpriteRenderer m_sprite = default;
    /// <summary>�e�ۂ̃v���n�u</summary>
    [SerializeField] GameObject m_bulletPrefab = default;
    /// <summary>�e���̈ʒu��ݒ肷��I�u�W�F�N�g</summary>
    [SerializeField] Transform m_muzzle = default;

    /// <summary>���������̓��͒l</summary>
    float m_h;
    float m_scaleX;
    int _wjump = 0;
    int life = 3;
    Vector3 _initialPosition;

    // Start is called before the first frame update
    void Start()
    {

        m_rb = GetComponent<Rigidbody2D>();
        m_sprite = GetComponent<SpriteRenderer>();
        _initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // ���͂��󂯎��
        m_h = Input.GetAxisRaw("Horizontal");
        // �e����͂��󂯎��
        if (Input.GetButtonDown("Jump") && _wjump < 2)
        {

            m_rb.AddForce(Vector2.up * m_jumpPower, ForceMode2D.Impulse);

            _wjump += 1;
        }
        // �ݒ�ɉ����č��E�𔽓]������
        if (m_flipX)
        {
            FlipX(m_h);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(m_bulletPrefab);
            bullet.transform.position = m_muzzle.transform.position;

            //BulletController���擾����
            bullet b = bullet.GetComponent<bullet>();
            //�X�s�[�h�Ɏ����̌����iScele��X�j��������
            b.Speed *= transform.localScale.x;
        }


        Vector2 velocity = m_rb.velocity;
        velocity.x = m_h * m_movePower;
        m_rb.velocity = velocity;

        if(life <= 0)
        {
            this.transform.position = _initialPosition;
            life = 3;
        }
         if(this.transform.position.y > -10f) 
        {
            this.transform.position = _initialPosition;
        }
    }
    private void FixedUpdate()
    {
        // �͂�������̂� FixedUpdate �ōs��
        // m_rb.AddForce(Vector2.right * m_h * m_movePower, ForceMode2D.Force);
    }

    /// <summary>
    /// ���E�𔽓]������
    /// </summary>
    /// <param name="horizontal">���������̓��͒l</param>
    void FlipX(float horizontal)
    {
        /*
         * ������͂��ꂽ��L�����N�^�[�����Ɍ�����B
         * ���E�𔽓]������ɂ́ATransform:Scale:X �� -1 ���|����B
         * Sprite Renderer �� Flip:X �𑀍삵�Ă����]����B
         * */
        m_scaleX = this.transform.localScale.x;

        if (horizontal > 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
        else if (horizontal < 0)
        {
            this.transform.localScale = new Vector3(-1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _wjump = 0;
        }
        if (collision.gameObject.CompareTag("enemy"))
        {
            life -= 1;
        }

    }
    

}



