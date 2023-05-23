using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandL : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float hand_speed = 0.1f;
    public float cricle_radius = 10.0f;
    public int maxHp = 20;//�ő�HP
    int hp;

    //�e�̔���
    float fire_frame = 0;
    Vector3 bullet_pos;//�e�̈ʒu
    public float bulletCoolTime = 25.0f;//�e�̃N�[���^�C��

    //���E�ړ��Ɏg���ϐ�
    private Vector3 StartPosition;
    private int direction = 1;
    private float moveTime = 0.0f;
    public float leftMoveSpeed = 1;

    private float moveTimeMax = 10.0f;//��ނ��Ƃ̌p������
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
        bullet_pos = transform.Find("BulletPosition").localPosition;
        StartPosition = transform.position;
    }

    void Circle()
    {
        Vector3 l_pos = transform.position;

        float deg = 180 * hand_speed * 10 * Time.time;

        //        float rad = hand_speed * Mathf.Deg2Rad * -1800 + Time.time;
        float rad = deg * Mathf.Deg2Rad; ;

        l_pos.x = Mathf.Cos(rad) * cricle_radius - 9;

        l_pos.y = Mathf.Sin(rad) * cricle_radius + 4;

        transform.position = l_pos;


    }

    // Update is called once per frame
    void Update()
    {

        switch (action)
        {
            //�~�ړ�
            case Action.CircleMove:
                Circle();
                break;

            //���E�ړ�
            case Action.LandRMove:

                transform.position = new Vector3(transform.position.x + leftMoveSpeed * Time.deltaTime * direction, StartPosition.y, StartPosition.z);

                if (transform.position.x >= -2)
                {
                    direction = -1;
                }
                if (transform.position.x <= -20)
                {
                    direction = 1;
                }
                break;
        }

        //�V�[���̐؂�ւ�
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

        //�e�̔���
        fire_frame++;
        if (fire_frame % bulletCoolTime == 0)//10�b���Ƃɔ���
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
