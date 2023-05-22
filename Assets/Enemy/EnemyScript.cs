using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //�ϐ�
    int maxHp = 10;
    int hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // ���������̂��v���C���[�̒e
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            hp--;
            // �e������
            Destroy(other.gameObject);
        }
        if (hp <= 0)
        {
            // ���g������
            Destroy(gameObject);
        }
    }
}
