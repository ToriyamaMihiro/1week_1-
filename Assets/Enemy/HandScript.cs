using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class HandScript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float hand_speed = 0.1f;
    public float cricle_radius = 10.0f;
    public int maxHp = 20;//最大HP
    int hp;

    //弾の発射
    float fire_frame = 0;
    Vector3 bullet_pos;//弾の位置
    public float bulletCoolTime = 25.0f;//弾のクールタイム

    //左右移動に使う変数
    private Vector3 StartPosition;
    private int direction = 1;
    private float moveTime = 0.0f;
    public float rightMoveSpeed = 1;


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
        transform.position = new Vector3(10, 0, 0);
        StartPosition = transform.position;
        bullet_pos = transform.Find("BulletPosition").localPosition;
        hp = maxHp;
    }

    void Circle()
    {
        Vector2 pos = transform.position;
        float deg = 180 * hand_speed * 10 * Time.time;

        float rad = deg * Mathf.Deg2Rad; ;

        pos.x = Mathf.Cos(rad) * cricle_radius + 10;

        pos.y = Mathf.Sin(rad) * cricle_radius + 4;

        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        switch (action)
        {
            //円移動
            case Action.CircleMove:
                Circle();
                break;

            //左右移動
            case Action.LandRMove:

                transform.position = new Vector3(transform.position.x + rightMoveSpeed * Time.deltaTime * direction, StartPosition.y, StartPosition.z);

                if (transform.position.x >= 20)
                {
                    direction = -1;
                }
                if (transform.position.x <= 2)
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
        fire_frame++;
        if (fire_frame % bulletCoolTime == 0)//10秒ごとに発射
        {
            Instantiate(BulletPrefab, transform.position + bullet_pos, Quaternion.identity);
        }
        if (fire_frame >= 1000)
        {
            fire_frame = 0;
        }
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
            Debug.Log("Destroy");
        }

    }

}
