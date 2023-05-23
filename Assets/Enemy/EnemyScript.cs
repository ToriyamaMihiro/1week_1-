using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    //�ϐ�
    int maxHp = 10;
    int hp;
    public Slider slider;
    public GameObject explosion_effect;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = (float)maxHp;
        hp = maxHp;
        //explosion_effect = (GameObject)Resources.Load("Explosion");
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // ���������̂��v���C���[�̒e
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            hp--;
            slider.value = (float)hp / (float)maxHp;
            // �e������
            Destroy(other.gameObject);
        }
        if (hp <= 0)
        {
            //GameObject effect = Instantiate(explosion_effect) as GameObject;
            //effect.transform.position = transform.position;
            //Instantiate(explosion_effect, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            SceneManager.LoadScene("GameClear");
            // ���g������
            Destroy(gameObject);
        }
    }
}
