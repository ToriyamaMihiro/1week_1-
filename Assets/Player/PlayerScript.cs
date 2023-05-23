using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{
    public GameObject BulletPrefab;

    /*---- 変数宣言 ----*/
    public float move_speed = 0.015f;
    Vector3 bullet_pos;//弾の位置
    float xLimit = 26.0f;
    float yLimit = 18.0f;
    public int maxHp = 5;//最大HP
    public bool isDamage = false;
    public SpriteRenderer renderer;


    int hp;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        /*---- 初期化 ----*/
        transform.position = new Vector3(0, -8, 0);
        bullet_pos = transform.Find("BulletPosition").localPosition;
        slider.value = (float)maxHp;
        hp = maxHp;
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        /*---- キー移動 ----*/
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-move_speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(move_speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, move_speed, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -move_speed, 0);
        }

        /*---- 弾の発射 ----*/
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(BulletPrefab, transform.position + bullet_pos, Quaternion.identity);
        }


        Vector3 player_pos = transform.position;
        player_pos.x = Mathf.Clamp(player_pos.x, -xLimit, xLimit);
        player_pos.y = Mathf.Clamp(player_pos.y, -yLimit, yLimit);

        transform.position = new Vector2(player_pos.x, player_pos.y);

        if (isDamage)
        {
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // 当たったのがエネミーの弾
        if (!isDamage && other.gameObject.CompareTag("EnemyBullet"))
        {
            hp--;

            isDamage = true;
            StartCoroutine("WaitForIt");
            slider.value = (float)hp / (float)maxHp;

            // 弾も消す
            Destroy(other.gameObject);
        }
        if (hp <= 0)
        {
            SceneManager.LoadScene("GameOver");
            // 自身を消す
            Destroy(gameObject);
        }

    }
    IEnumerator WaitForIt()
    {
        // 3秒間処理を止める
        yield return new WaitForSeconds(3.0f);

        // １秒後ダメージフラグをfalseにして点滅を戻す
        isDamage = false;
        renderer.color = new Color(1f, 1f, 1f, 1f);
    }
}
