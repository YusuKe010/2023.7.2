using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bullet : MonoBehaviour
{
    /// <summary>
    ///Å@íeÇÃÇ∆Ç‘ë¨Ç≥
    /// </summary>
    [SerializeField] float bulletspeed = 10.0f;
    /// <summary>
    /// íeÇÃè¡Ç¶ÇÈéûä‘
    /// </summary>
    [SerializeField] float bullettime = 3.0f;
    // Start is called before the first frame update
    public float Speed { get => bulletspeed; set => bulletspeed = value; }

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.right * bulletspeed;
        Destroy(this.gameObject, bullettime);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Spawner") )
        {
            Destroy(this.gameObject);
        }
    }
}
