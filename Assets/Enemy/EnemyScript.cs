using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    //変数
    public int maxHp = 10;
    int hp;
    public float stopTime;
    public Slider slider;
    public GameObject explosion_effect;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = (float)maxHp;
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
            slider.value = (float)hp / (float)maxHp;
            // 弾も消す
            Destroy(other.gameObject);
        }
        if (hp <= 0)
        {
            GameObject effect = Instantiate(explosion_effect) as GameObject;
            effect.transform.position = transform.position;
            stopTime += Time.time;

            if (stopTime >= 10)
            {
                SceneManager.LoadScene("GameClear");
            }
            // 自身を消す
            Destroy(gameObject);
        }
    }
}
