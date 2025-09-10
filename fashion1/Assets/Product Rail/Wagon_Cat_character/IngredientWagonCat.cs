using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientWagonCat : MonoBehaviour
{
    Animator anim;

    Storage storage;
    SpriteRenderer spriteRenderer;

    [SerializeField]
    Sprite[] cat_dir_sprite = { null, null, null, null }; // 순서대로 Up, Right, Down, Left

    [SerializeField]
    [Range(0, 1)]
    float character_speed;

    Vector3 character_dir;
    int character_dir_index;  // 0,1,2,3 순서대로 Up, Right, Down, Left

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        storage = GameObject.Find("Storage").GetComponent<Storage>();

        Character_Move(2);
        Sprite_Fade_IN();
    }

    void Update()
    {
        transform.position += character_dir * character_speed;
        anim.SetInteger("MoveDir", character_dir_index);
    }

    public void Sprite_Fade_IN()
    {
        Color color = new Color(1, 1, 1, 0);
        for (float i = 0.0f; i <= 1.0f; i += 0.01f)
        {
            color = new Color(1, 1, 1, i);
            spriteRenderer.color = color;
        }
    }
    public void Sprite_Fade_Out()
    {
        Color color = new Color(1, 1, 1, 1);
        for (float i = 1.0f; i >= 0.0f; i -= 0.01f)
        {
            color = new Color(1, 1, 1, i);
            spriteRenderer.color = color;

            if (i < 0.2f)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Character_Move(int dir)
    {
        character_dir_index = dir;

        if(character_dir_index == 0)
        {
            character_dir = new Vector3(0, 1, 0);
        }
        else if(character_dir_index == 1)
        {
            character_dir = new Vector3(1, 0, 0);
        }
        else if (character_dir_index == 2)
        {
            character_dir = new Vector3(0, -1, 0);
        }
        else if (character_dir_index == 3)
        {
            character_dir = new Vector3(-1, 0, 0);     
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.transform == transform.parent) //레일 아무대나 충돌
       {
            ClothesProductRail proudct_rail = collision.gameObject.GetComponent<ClothesProductRail>();
            Recipe recipe = proudct_rail.product_item_recipe;
            proudct_rail.Production_Start(recipe); //생산 시작

            Sprite_Fade_Out();
       }
    }
}
