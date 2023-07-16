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
    /// <summary>�e�ۂ̃v���n�u</summary>
    [SerializeField] GameObject m_bulletPrefab = default;
    /// <summary>�e���̈ʒu��ݒ肷��I�u�W�F�N�g</summary>
    [SerializeField] Transform m_muzzle = default;
    /// <summary>/// �t�h�p�l���̃I�u�W�F�N�g/// </summary>
    [SerializeField] GameObject _clearPanel;
    [SerializeField] GameObject _playPanel;
    /// <summary>/// ���C�t�̂t�h�I�u�W�F�N�g/// </summary>
    [SerializeField] GameObject[] _Life;
    [SerializeField] float _invicible_time = 1f;
    [SerializeField] Transform m_bullet;
    [SerializeField] CircleCollider2D _bulletCollider;
    AudioSource _charge;

    /// <summary>���������̓��͒l</summary>
    float m_h;
    float m_scaleX;
    float _invicible_time_count = 0f;
    float bullettimer = 0;
    float bullettimer_count = 0;
    int _wjump = 0;
    int life = 3;
    int _Count = 2;

    bool _bulletScale;

    Vector3 _initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        _charge = GetComponent<AudioSource>();
        m_rb = GetComponent<Rigidbody2D>();
        _initialPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // ���͂��󂯎��
        m_h = Input.GetAxisRaw("Horizontal");
        // �e����͂��󂯎��
        if (Input.GetButtonDown("Jump") && _wjump < 2)//�W�����v������B_wjump�̌�̐�����ς���ƃ_�u���W�����v���o�����肷��
        {

            m_rb.AddForce(Vector2.up * m_jumpPower, ForceMode2D.Impulse);

            _wjump += 1;
        }
        // �ݒ�ɉ����č��E�𔽓]������
        if (m_flipX)
        {
            FlipX(m_h);
        }
        if (bullettimer > 0.15f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _bulletScale = true;
                
            }
            if(_bulletScale)
            {
                if (bullettimer_count > 1.0f)
                {
                    m_bullet.localScale = Vector2.one * 0.9f;
                }
                else
                {
                    bullettimer_count += Time.deltaTime;
                }
            }
            
            if (Input.GetButtonUp("Fire1"))//���N���b�N��Ctrl�Œe��ł���
            {
                _charge.Play();
                GameObject bullet = Instantiate(m_bulletPrefab);
                bullet.transform.position = m_muzzle.transform.position;
                
                //BulletController���擾����
                bullet b = bullet.GetComponent<bullet>();
                //�X�s�[�h�Ɏ����̌����iScele��X�j�������ĉ摜�̌�����ς���
                b.Speed *= transform.localScale.x;
                bullettimer = 0;
                m_bullet.localScale = Vector2.one * 0.3f;
                bullettimer_count = 0;
                _bulletScale = false;
            }

        }
        else
        {
            bullettimer += Time.deltaTime;
        }

        //�ړ��̏���(�x�N�g���𒼐ڏ��������Ă�)
        Vector2 velocity = m_rb.velocity;
        velocity.x = m_h * m_movePower;
        m_rb.velocity = velocity;

        if (life <= 0)//���C�t���[���ɂȂ������̏���(���C�t�̐��l�����ɖ߂��A�����n�_�ɖ߂�)
        {
            this.transform.position = _initialPosition;
            life = 3;
            _Life[0].SetActive(true);
            _Life[1].SetActive(true);
            _Life[2].SetActive(true);
            _Count = 2;
        }
        if (this.transform.position.y < -10f)//�v���C���[�̂����W��-�P�O�ȉ��ɂȂ�Ə����n�_�ɂ��ǂ�
        {
            this.transform.position = _initialPosition;
        }
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
        if (collision.gameObject.CompareTag("Ground"))//Ground�^�O�ɓ�����ƃW�����v���o����悤�ɂȂ�
        {
            _wjump = 0;
        }
        if (collision.gameObject.CompareTag("Enemy"))//�G�l�~�[�ɓ�����ƃ��C�t������A1�摜�������Ȃ�����
        {
            //Debug.Log("hit");
            life -= 1;
            _Life[_Count].SetActive(false);
            _Count--;
        }
        if (collision.gameObject.CompareTag("Goal"))//�S�[���ɓ������UI�p�l�����o���āA�����n�_�ɂ��ǂ�
        {
            this.transform.position = _initialPosition;
            _clearPanel.SetActive(true);
            _playPanel.SetActive(false);
        }
        if (collision.gameObject.CompareTag("FallPoint"))//�������珉���n�_�ɂ��ǂ�
        {
            this.transform.position = _initialPosition;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // �G�Ɠ��������ꍇ
        if (collision.gameObject.CompareTag("Enemy"))//�G�l�~�[�ɓ�����ƃ��C�t������A1�摜�������Ȃ�����
        {
            if (_invicible_time_count > _invicible_time)//�G�ɓ����葱���Ă����Ԋu�Ń_���[�W���󂯂�
            {
                //Debug.Log("hit");
                life -= 1;
                _invicible_time_count = 0f;
                _Life[_Count].SetActive(false);
                _Count--;
            }
            else
            {
                _invicible_time_count += Time.deltaTime;    // ���Ԃ𑝂₷
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _invicible_time_count = 0f;
    }

    void GameClear()
    {
        SceneManager.LoadScene("GameStart");
    }


}



