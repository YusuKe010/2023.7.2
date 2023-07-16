using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{

    [SerializeField] float m_speed = 1f;

    void Start()
    {
        // ���x�x�N�g�������߂�
        GameObject[] player = GameObject.FindGameObjectsWithTag("Enemy");
        
        Vector2 v = player[0].transform.position - this.transform.position;
        v = v.normalized * m_speed;

        // ���x�x�N�g�����Z�b�g����
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = v;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
