using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class HandScript : MonoBehaviour
{
    public float hand_speed = 0.1f;
    public float cricle_radius = 5.0f;
    private Vector3 initPosition;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(10, 0, 0);
        initPosition = transform.position;
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
     


    }
}
