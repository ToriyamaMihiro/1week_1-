using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //変数
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
        // 当たったのがプレイヤーの弾
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            hp--;
            // 弾も消す
            Destroy(other.gameObject);
        }
        if (hp <= 0)
        {
            // 自身を消す
            Destroy(gameObject);
        }
    }
}
