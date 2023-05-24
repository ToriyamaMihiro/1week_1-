using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{
    public GameObject BulletPrefab;

    /*---- 変数宣言 ----*/
    public float moveSpeed = 0.05f;
    Vector3 bulletPos;//弾の位置
    float xLimit = 26.0f;
    float yLimit = 18.0f;
    public int maxHp = 5;//最大HP
    public bool isDamage = false;
    public float bulletCoolTime = 50.0f;//弾のクールタイム
    float fireFrame = 0;
    public SpriteRenderer playerRenderer;


    int hp;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        /*---- 初期化 ----*/
        transform.position = new Vector3(0, -14.5f, 0);
        bulletPos = transform.Find("BulletPosition").localPosition;
        slider.value = (float)maxHp;
        hp = maxHp;
        playerRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        /*---- キー移動 ----*/
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-moveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(moveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, moveSpeed, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -moveSpeed, 0);
        }

        /*---- 弾の発射 ----*/
        if (Input.GetKey(KeyCode.Space))
        {
            fireFrame++;
            if (fireFrame % bulletCoolTime == 0)//10秒ごとに発射
            {
                Instantiate(BulletPrefab, transform.position + bulletPos, Quaternion.identity);
            }
            if (fireFrame >= 1000)
            {
                fireFrame = 0;
            }
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
        playerRenderer.color = new Color(1f, 1f, 1f, 1f);
    }
}
