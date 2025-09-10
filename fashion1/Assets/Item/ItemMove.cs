using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    public ClothesProductRail clothesProductRail;
    Wagon_Cat wagon_cat;

    Rigidbody2D rigid2d;

    public int item_count;

    public float item_speed;
    public float result_speed;

    public Vector3 item_dir;
    bool now_move = false;

    float railY;
    void Awake()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        item_speed = 1f;
    }
    void Start()
    {
        item_dir = new Vector3(1, 0, 0);
    }
    void Update()
    {
        if(now_move)
        {
            transform.position = transform.position + (result_speed * item_dir * Time.deltaTime);
        }

        if(transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }

    }

    public void Set_Speed(float rail_speed, float system_speed)
    {
        result_speed = item_speed * ( rail_speed + system_speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item_rail")
        {
            now_move = true;
            rigid2d.gravityScale = 0;
            rigid2d.velocity = new Vector3(rigid2d.velocity.x, 0, 0);
        }
        else if (collision.tag == "Stop_point")
        {
            now_move = false;
            rigid2d.gravityScale = 1;
            rigid2d.AddForce(new Vector3(5,5, 0), ForceMode2D.Impulse);
        }
        else if( collision.tag == "Product_finish") // == ¿Ö°Ç Ä¹
        {
            clothesProductRail.Production_Finish(clothesProductRail.product_item_recipe);
            wagon_cat = collision.GetComponent<Wagon_Cat>();
            wagon_cat.Set_Result_Info(clothesProductRail.product_item_recipe, item_count);
            wagon_cat.Character_Move(0);
            Destroy(gameObject);
        }
    }
}
