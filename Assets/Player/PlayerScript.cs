using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject BulletPrefab;

    /*---- 変数宣言 ----*/
    public float move_speed = 0.01f;
    Vector3 bullet_pos;//弾の位置

    // Start is called before the first frame update
    void Start()
    {
        /*---- 初期化 ----*/
        transform.position = new Vector3(0, -5, 0);
        bullet_pos = transform.Find("BulletPosition").localPosition;
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
            Instantiate(BulletPrefab,transform.position + bullet_pos,Quaternion.identity);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
{
    // 当たったのがプレイヤーの弾
    if (other.gameObject.CompareTag("Enemy"))
    {
        // 自身を消す
        Destroy(gameObject);

        // 弾も消す
        Destroy(other.gameObject);
    }
}
}
