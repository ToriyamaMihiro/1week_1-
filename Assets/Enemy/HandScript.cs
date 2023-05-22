using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class HandScript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float hand_speed = 0.1f;
    public float cricle_radius = 5.0f;
    private Vector3 initPosition;
    float fire_frame = 0;
    Vector3 bullet_pos;//�e�̈ʒu
    public float bulletCoolTime = 25.0f;//�e�̃N�[���^�C��
    public int maxHp = 20;//�ő�HP
    int hp;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(10, 0, 0);
        initPosition = transform.position;
        bullet_pos = transform.Find("BulletPosition").localPosition;
        hp = maxHp;
    }

    void Circle()
    {
        Vector2 pos = transform.position;

        float rad = hand_speed * Mathf.Rad2Deg * Time.time;

        pos.x = Mathf.Cos(rad) * cricle_radius;

        pos.y = Mathf.Sin(rad) * cricle_radius;

        transform.position = pos ;
       
    }

    // Update is called once per frame
    void Update()
    {

        Circle();

        //�e�̔���
        fire_frame++;
        if (fire_frame % public float bulletCoolTime = 25.0f; == 0)//10�b���Ƃɔ���
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
