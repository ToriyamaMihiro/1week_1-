using UnityEngine;

public class HandScript : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float handSpeed = 0.1f;
    public float cricleRadius = 10.0f;
    public int maxHp = 20;//�ő�HP
    int hp;

    //�e�̔���
    float fireFrame = 0;
    Vector3 bulletPos;//�e�̈ʒu
    public float bulletCoolTime = 25.0f;//�e�̃N�[���^�C��

    //���E�ړ��Ɏg���ϐ�
    private Vector3 StartPosition;
    private int direction = 1;
    private float moveTime = 0.0f;
    public float rightMoveSpeed = 1;


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
        transform.position = new Vector3(10, 0, 0);
        StartPosition = transform.position;
        bulletPos = transform.Find("BulletPosition").localPosition;
        hp = maxHp;
    }

    void Circle()
    {
        Vector2 pos = transform.position;
        float deg = 180 * handSpeed * 10 * Time.time;

        float rad = deg * Mathf.Deg2Rad; ;

        pos.x = Mathf.Cos(rad) * cricleRadius + 10;

        pos.y = Mathf.Sin(rad) * cricleRadius + 4;

        transform.position = pos;
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
        fireFrame++;
        if (fireFrame % bulletCoolTime == 0)//10�b���Ƃɔ���
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
            Debug.Log("Destroy");
        }

    }

}
