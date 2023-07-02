using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    /// <summary>
    ///　弾のとぶ速さ
    /// </summary>
    [SerializeField] float bulletspeed = 10.0f;
    /// <summary>
    /// 弾の消える時間
    /// </summary>
    [SerializeField] float bullettime = 5.0f;
    // Start is called before the first frame update
    public float Speed { get => bulletspeed; set => bulletspeed = value; }

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.right * bulletspeed;

        Destroy(this.gameObject, bullettime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
