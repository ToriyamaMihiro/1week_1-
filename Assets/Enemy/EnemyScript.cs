using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    //変数
    public int maxHp = 10;
    int hp;
    bool isDeath = false;
    bool isHit = false;
    public float stopTime = 0.0f;
    public Slider slider;
    public GameObject explosion_effect;
    public GameObject enemy;
    float hitTimer = 0.0f;
    public float moveSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = (float)maxHp;
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            hitTimer++;
            if (hitTimer >= 30)
            {
                isHit = false;
                hitTimer = 0;
            }
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        //動く
        

    }
    void SceneLoad()
    {
        SceneManager.LoadScene("GameClear");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // 当たったのがプレイヤーの弾
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            isHit = true;
            hp--;
            slider.value = (float)hp / (float)maxHp;

            // 弾も消す
            Destroy(other.gameObject);
        }
        if (hp <= 0)
        {
            GameObject effect = Instantiate(explosion_effect) as GameObject;
            effect.transform.position = transform.position;

            isDeath = true;
            // 自身を消す
            if (isDeath)
            {
                enemy.SetActive(false);
                Invoke("SceneLoad", 1f);

            }
        }
    }
}
