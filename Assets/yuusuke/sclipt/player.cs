using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    /// <summary>左右移動する力</summary>
    [SerializeField] float m_movePower = 5f;
    /// <summary>ジャンプする力</summary>
    [SerializeField] float m_jumpPower = 15f;
    /// <summary>入力に応じて左右を反転させるかどうかのフラグ</summary>
    [SerializeField] bool m_flipX = false;
    Rigidbody2D m_rb = default;
    SpriteRenderer m_sprite = default;
    /// <summary>弾丸のプレハブ</summary>
    [SerializeField] GameObject m_bulletPrefab = default;
    /// <summary>銃口の位置を設定するオブジェクト</summary>
    [SerializeField] Transform m_muzzle = default;
    [SerializeField] Text _text;
    [SerializeField] GameObject _clearPanel;
    [SerializeField] GameObject _playPanel;
    float bulllettimer = 0;

    [SerializeField] float _invicible_time = 1f;
    float _invicible_time_count = 0f;

    /// <summary>水平方向の入力値</summary>
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
        _text.text = life.ToString();
        // 入力を受け取る
        m_h = Input.GetAxisRaw("Horizontal");
        // 各種入力を受け取る
        if (Input.GetButtonDown("Jump") && _wjump < 2)
        {

            m_rb.AddForce(Vector2.up * m_jumpPower, ForceMode2D.Impulse);

            _wjump += 1;
        }
        // 設定に応じて左右を反転させる
        if (m_flipX)
        {
            FlipX(m_h);
        }
        if (bulllettimer > 0.2f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                GameObject bullet = Instantiate(m_bulletPrefab);
                bullet.transform.position = m_muzzle.transform.position;

                //BulletControllerを取得する
                bullet b = bullet.GetComponent<bullet>();
                //スピードに自分の向き（SceleのX）をかける
                b.Speed *= transform.localScale.x;
                bulllettimer = 0;
            }
        }
        else
        {
            bulllettimer += Time.deltaTime;
        }


        Vector2 velocity = m_rb.velocity;
        velocity.x = m_h * m_movePower;
        m_rb.velocity = velocity;

        if(life <= 0)
        {
            this.transform.position = _initialPosition;
            life = 3;
            _text.text = life.ToString();
        }
         if(this.transform.position.y < -10f) 
         {
            this.transform.position = _initialPosition;
         }
    }
    private void FixedUpdate()
    {
        // 力を加えるのは FixedUpdate で行う
        // m_rb.AddForce(Vector2.right * m_h * m_movePower, ForceMode2D.Force);
    }

    /// <summary>
    /// 左右を反転させる
    /// </summary>
    /// <param name="horizontal">水平方向の入力値</param>
    void FlipX(float horizontal)
    {
        /*
         * 左を入力されたらキャラクターを左に向ける。
         * 左右を反転させるには、Transform:Scale:X に -1 を掛ける。
         * Sprite Renderer の Flip:X を操作しても反転する。
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("hit");
            life -= 1;
            _text.text = life.ToString();
        }
        if (collision.gameObject.CompareTag("Goal"))
        {
            this.transform.position = _initialPosition;
            _clearPanel.SetActive(true);
            _playPanel.SetActive(false);
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 敵と当たった場合
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (_invicible_time_count > _invicible_time)
            {
                //Debug.Log("hit");
                life -= 1;
                _text.text = life.ToString();
                _invicible_time_count = 0f;
            }
            else
            {
                _invicible_time_count += Time.deltaTime;    // 時間を増やす
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _invicible_time_count = 0f;
    }

    void GameClear()
    {
        SceneManager.LoadScene("GameClear");
    }
    

}



