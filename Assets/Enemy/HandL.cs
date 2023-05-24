using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandL : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float handSpeed = -0.1f;
    public float cricleRadius = 10.0f;
    public int maxHp = 20;//最大HP
    int hp;
    bool isHit = false;
    public GameObject explosion_effect;
    float hitTimer = 0.0f;

    //弾の発射
    float fireFrame = 0;
    Vector3 bulletPos;//弾の位置
    public float bulletCoolTime = 25.0f;//弾のクールタイム

    //左右移動に使う変数
    private Vector3 StartPosition;
    private int direction = 1;
    private float moveTime = 0.0f;
    public float leftMoveSpeed = 1;

    private float moveTimeMax = 10.0f;//種類ごとの継続時間
    Action action = Action.CircleMove;

    private void Awake()
    {
        hp = maxHp;

    }

    enum Action
    {
        CircleMove,
        LandRMove
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-9, 0, 0);
        bulletPos = transform.Find("BulletPosition").localPosition;
        StartPosition = transform.position;
    }

    void Circle()
    {
        Vector3 l_pos = transform.position;

        float deg = 180 * handSpeed * 10 * Time.time;

        //        float rad = hand_speed * Mathf.Deg2Rad * -1800 + Time.time;
        float rad = deg * Mathf.Deg2Rad; ;

        l_pos.x = Mathf.Cos(rad) * cricleRadius - 9;

        l_pos.y = Mathf.Sin(rad) * cricleRadius + 4;

        transform.position = l_pos;


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

        switch (action)
        {
            //円移動
            case Action.CircleMove:
                Circle();
                break;

            //左右移動
            case Action.LandRMove:

                transform.position = new Vector3(transform.position.x + leftMoveSpeed * Time.deltaTime * direction, StartPosition.y, StartPosition.z);

                if (transform.position.x >= 0)
                {
                    direction = -1;
                }
                if (transform.position.x <= -20)
                {
                    direction = 1;
                }
                break;
        }

        //シーンの切り替え
        moveTime += Time.deltaTime;
        if (moveTime > moveTimeMax)
        {
            if (action == Action.LandRMove)
            {
                action = Action.CircleMove;

            }
            else if (action == Action.CircleMove)
            {
                action = Action.LandRMove;
            }
            moveTime = 0f;
        }

        //弾の発射
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

    void OnTriggerEnter2D(Collider2D other)
    {
        // 当たったのがプレイヤーの弾
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            hp--;
            isHit = true;
            // 弾も消す
            Destroy(other.gameObject);
        }
        if (hp <= 0)
        {
            GameObject effect = Instantiate(explosion_effect) as GameObject;
            effect.transform.position = transform.position;
            // 自身を消す
            Destroy(gameObject);
        }

    }

}
