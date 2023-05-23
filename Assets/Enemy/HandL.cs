using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandL : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float hand_speed = 0.1f;
    public float cricle_radius = 10.0f;
    private Vector3 initPosition;
    float fire_frame = 0;
    Vector3 bullet_pos;//�e�̈ʒu
    public float bulletCoolTime = 25.0f;//�e�̃N�[���^�C��
    public int maxHp = 20;//�ő�HP
    int hp;
    Vector3 l_pos;
    float first_pos_y = 4;

    private void Awake()
    {
        hp = maxHp;

    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-9, first_pos_y, 0);
        initPosition = transform.position;
        bullet_pos = transform.Find("BulletPosition").localPosition;
    }

    void Circle()
    {
        l_pos = transform.position;

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

        Circle();

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
