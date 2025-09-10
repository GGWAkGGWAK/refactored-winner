using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagon_Cat : MonoBehaviour
{
    Animator anim;

    PlayerInfo playerinfo;
    Storage storage;
    SpriteRenderer spriteRenderer;
    QuestManager questManager;

    [SerializeField]
    Sprite[] cat_dir_sprite = { null, null, null, null }; // 순서대로 Up, Right, Down, Left

    public Item result_item;
    public int result_item_count;

    [SerializeField]
    [Range(0,1)]
    float character_speed;

    public bool auto_sell; //자동판매 생성시 값 받아옴
    public bool now_Move;

    Vector3 character_dir;
    int character_dir_index;  // 0,1,2,3 순서대로 Up, Right, Down, Left

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        storage = GameObject.Find("Storage").GetComponent<Storage>();
        playerinfo = GameObject.Find("Playerinfo").GetComponent<PlayerInfo>();
        questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();

        character_speed = 0;
        now_Move = false;

        Sprite_Fade_IN();
    }

    void Update()
    {
        if(now_Move)
        {
            transform.position += character_dir * character_speed;
            anim.SetInteger("MoveDir", character_dir_index);
        }
    }

    public void Set_Result_Info(Recipe recipe, int count) // 레시피로 정보 받아오기
    {
        result_item = recipe.result_item;
        result_item_count = count;
    }

    public void Set_Result_Info(Item item, int count) //아이템으로 정보 받아오기
    {
        result_item = item;
        result_item_count = count;
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
        now_Move = true;

        character_speed = 0.06f;

        if (character_dir_index == 0)
        {
            character_dir = new Vector3(0, 1, 0);
        }
        else if (character_dir_index == 1)
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
        if(collision.tag == "Finish")
        {
            if(auto_sell)
            {
                playerinfo.player_gold += (result_item.item_price * result_item_count);
                questManager.gold_targetAmount += (result_item.item_price * result_item_count);
            }
            else if(!auto_sell)
            {
                if (storage.Storage_Space_Finding(result_item, result_item_count)) //창고 자리 있을시 
                {
                    storage.Storage_Add(result_item, result_item_count);
                }
                else
                {
                    //스탁? 그냥 팔기?
                }
            }
            Sprite_Fade_Out();
        }
    }
}
