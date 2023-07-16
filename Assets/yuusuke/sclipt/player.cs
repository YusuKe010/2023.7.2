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
    /// <summary>弾丸のプレハブ</summary>
    [SerializeField] GameObject m_bulletPrefab = default;
    /// <summary>銃口の位置を設定するオブジェクト</summary>
    [SerializeField] Transform m_muzzle = default;
    /// <summary>/// ＵＩパネルのオブジェクト/// </summary>
    [SerializeField] GameObject _clearPanel;
    [SerializeField] GameObject _playPanel;
    /// <summary>/// ライフのＵＩオブジェクト/// </summary>
    [SerializeField] GameObject[] _Life;
    [SerializeField] float _invicible_time = 1f;
    [SerializeField] Transform m_bullet;
    [SerializeField] CircleCollider2D _bulletCollider;
    AudioSource _charge;

    /// <summary>水平方向の入力値</summary>
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
        // 入力を受け取る
        m_h = Input.GetAxisRaw("Horizontal");
        // 各種入力を受け取る
        if (Input.GetButtonDown("Jump") && _wjump < 2)//ジャンプをする。_wjumpの後の数字を変えるとダブルジャンプが出来たりする
        {

            m_rb.AddForce(Vector2.up * m_jumpPower, ForceMode2D.Impulse);

            _wjump += 1;
        }
        // 設定に応じて左右を反転させる
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
            
            if (Input.GetButtonUp("Fire1"))//左クリックとCtrlで弾を打つ処理
            {
                _charge.Play();
                GameObject bullet = Instantiate(m_bulletPrefab);
                bullet.transform.position = m_muzzle.transform.position;
                
                //BulletControllerを取得する
                bullet b = bullet.GetComponent<bullet>();
                //スピードに自分の向き（SceleのX）をかけて画像の向きを変える
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

        //移動の処理(ベクトルを直接書き換えてる)
        Vector2 velocity = m_rb.velocity;
        velocity.x = m_h * m_movePower;
        m_rb.velocity = velocity;

        if (life <= 0)//ライフがゼロになった時の処理(ライフの数値を元に戻す、初期地点に戻る)
        {
            this.transform.position = _initialPosition;
            life = 3;
            _Life[0].SetActive(true);
            _Life[1].SetActive(true);
            _Life[2].SetActive(true);
            _Count = 2;
        }
        if (this.transform.position.y < -10f)//プレイヤーのｙ座標が-１０以下になると初期地点にもどる
        {
            this.transform.position = _initialPosition;
        }
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
        if (collision.gameObject.CompareTag("Ground"))//Groundタグに当たるとジャンプが出来るようになる
        {
            _wjump = 0;
        }
        if (collision.gameObject.CompareTag("Enemy"))//エネミーに当たるとライフが減り、1つ画像を見えなくする
        {
            //Debug.Log("hit");
            life -= 1;
            _Life[_Count].SetActive(false);
            _Count--;
        }
        if (collision.gameObject.CompareTag("Goal"))//ゴールに当たるとUIパネルを出して、初期地点にもどる
        {
            this.transform.position = _initialPosition;
            _clearPanel.SetActive(true);
            _playPanel.SetActive(false);
        }
        if (collision.gameObject.CompareTag("FallPoint"))//落ちたら初期地点にもどる
        {
            this.transform.position = _initialPosition;
        }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 敵と当たった場合
        if (collision.gameObject.CompareTag("Enemy"))//エネミーに当たるとライフが減り、1つ画像を見えなくする
        {
            if (_invicible_time_count > _invicible_time)//敵に当たり続けても一定間隔でダメージを受ける
            {
                //Debug.Log("hit");
                life -= 1;
                _invicible_time_count = 0f;
                _Life[_Count].SetActive(false);
                _Count--;
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
        SceneManager.LoadScene("GameStart");
    }


}



