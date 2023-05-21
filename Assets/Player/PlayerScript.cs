using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject BulletPrefab;

    /*---- •Ï”éŒ¾ ----*/
    public float move_speed = 0.01f;
    Vector3 bullet_pos;//’e‚ÌˆÊ’u

    // Start is called before the first frame update
    void Start()
    {
        /*---- ‰Šú‰» ----*/
        transform.position = new Vector3(0, -5, 0);
        bullet_pos = transform.Find("BulletPosition").localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        /*---- ƒL[ˆÚ“® ----*/
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

        /*---- ’e‚Ì”­Ë ----*/
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(BulletPrefab,transform.position + bullet_pos,Quaternion.identity);
        }
    }
}
