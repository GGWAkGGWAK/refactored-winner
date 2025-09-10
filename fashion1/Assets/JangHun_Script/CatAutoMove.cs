using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAutoMove : MonoBehaviour
{
    public float moveSpeed = 7f;
    Rigidbody2D rb;
    void Update()
    {
        rb=GetComponent<Rigidbody2D>();
        // 오른쪽으로 이동
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Storageee")
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }
}
