using System.Collections.Generic;
using UnityEngine;

public class ClothesProductRail : MonoBehaviour
{
    private PlayerInfo playerinfo;
    private SystemInfo systeminfo;
    private Storage storage;

    [SerializeField]
    GameObject rail_upgrade_Ui;
    bool rail_ugrade_Ui_on;

    [Header("현재 레시피 / 변경 예정 레시피")]
    public Recipe product_item_recipe;        //현재 레시피
    public Recipe product_item_recipe_change; //변경 예정 레시피

    [Space(10f)]
    public SpriteRenderer product_item_sprite;

    [Space(10f)]
    [SerializeField]
    Animator cat_Effet; //제작효과 고양이
    [SerializeField]
    GameObject ingredient_Wagon_Cat; //재료 고양이
    [SerializeField]
    GameObject wagon_cat; //수레고양이
    private List<GameObject> product_item_prefab = new List<GameObject>();

    [Space(10f)]
    public Transform storage_cat_spawn_point; // 재료 고양이 생성 위치
    public Transform drop_point_1;          //재료가 생성되는 위치
    public Transform drop_point_2;          //옷이 생성되는 위치
    public Transform cat_spawn_point;       //완성 수레 고양이 생성 위치

    [Space(10f)]
    public float rail_product_speed;
    public float rail_product_output;
    public float rail_rucky_output;

    [Header("자동판매")]
    public bool auto_sales;

    [SerializeField]
    bool now_production = false;

    [SerializeField]
    float time_num;
    float checking_time;

    public AudioSource workingSound;
    void Start()
    {
        playerinfo = GameObject.Find("Playerinfo").GetComponent<PlayerInfo>();
        systeminfo = GameObject.Find("Systeminfo").GetComponent<SystemInfo>();
        storage = GameObject.Find("Storage").GetComponent<Storage>();

        storage_cat_spawn_point = GameObject.Find("Storage_Cat_Spawn_Point").transform;

        rail_product_speed = 3f;
        rail_product_output = 1f;
        time_num = 0;
        checking_time = 1f;
        rail_ugrade_Ui_on = false;
        auto_sales = false;
    }

    void Update()
    {
        if (product_item_recipe != null)
        {
            product_item_sprite.sprite = product_item_recipe.result_item.item_sprite; //생산품 이미지 표시
        }

        time_num += Time.deltaTime;

        if (time_num > checking_time)
        {
            if (product_item_recipe != null)
            {
                if (!now_production)
                {
                    Double_Check(product_item_recipe); time_num = 0;
                    time_num = 0;
                }
                else
                {
                    time_num = 0;
                }
            }
        }

        //////////------- TEST -------//////////
        if (Input.GetKeyDown(KeyCode.U))
        {
            Rail_Upgrade_UI_ON_OFF();
        }
    }

    public void Double_Check(Recipe recipe)
    {
        if (storage.Storage_Finding(recipe) &&
            storage.Storage_Space_Finding(recipe.result_item, 1))
        {
            now_production = true;
            //이걸 그새끼 소환으로 바꾸기
            //Production_Start(recipe); 
            Spawn_Ingredient_Wagon_Cat(recipe);

        }
        else if (!storage.Storage_Finding(recipe))
        {
            Debug.Log("재료 부족");
        }
        else if (!storage.Storage_Space_Finding(recipe.result_item, 1))
        {
            Debug.Log("공간 부족");
        }
    }



    public void Production_Start(Recipe recipe)  //생산 시작
    {
        for (int i = 0; i < recipe.ingredient.Count; i++) // 재료 투하
        {
            for (int j = 0; j < recipe.ingredient_count[i]; j++)
            {
                float random = Random.Range(-0.1f, 0.1f);
                Vector2 drop_position = drop_point_1.position;
                Vector2 random_position = drop_position + new Vector2(random, 0);  //아이템 프리펩 생성 위치

                product_item_prefab.Add
                    (Instantiate(recipe.ingredient[i].item_prefab, random_position, transform.rotation)); //프리펩 생성 밑 리스트 저장
                storage.Storage_Remove(recipe.ingredient[i], 1, false); //창고에서 아이템 꺼내기
            }
            for (int j = 0; j < product_item_prefab.Count; j++)
            {
                product_item_prefab[j].transform.parent = this.gameObject.transform; //자식으로 넣기
                ItemMove itme_move = product_item_prefab[j].GetComponent<ItemMove>(); //소환된 아이템에 정보 입력
                itme_move.Set_Speed(rail_product_speed, playerinfo.item_rail_production_speed);  //소환된 아이템에 속도 부여 (생산 속도)
                itme_move.clothesProductRail = GetComponent<ClothesProductRail>();
            }
        }

    }

    public void Production_Finish(Recipe recipe) //생산 종료
    {
        Debug.Log("생산");
        if (product_item_prefab.Count == 0) //상태 체크
        {
            //Double_Check(recipe);
            time_num = 0;
            now_production = false;

            if (product_item_recipe_change != null)  //레시피 변경
            {
                product_item_recipe = product_item_recipe_change;
                product_item_recipe_change = null;
            }
        }
    }

    void Spawn_Ingredient_Wagon_Cat(Recipe recipe)
    {
        for (int i = 0; i < recipe.ingredient.Count; i++)
        {
            for (int j = 0; j < recipe.ingredient_count[i]; j++)
            {
                storage.Storage_Remove(recipe.ingredient[i], 1, false); //창고에서 아이템 꺼내기
            }
        }
        GameObject ingredient_Wagon_Cat_ =
            Instantiate(ingredient_Wagon_Cat, storage_cat_spawn_point.position, transform.rotation); //고양이 프리팹 소환
        ingredient_Wagon_Cat_.transform.parent = this.transform;
    }

    public void Ingredient_Remove(Collider2D collision) //재료 제거
    {
        product_item_prefab.Remove(collision.gameObject);       //리스트에서 아이템 삭제
        Destroy(collision.gameObject);                          //오브젝트 삭제
        if (product_item_prefab.Count == 0)                     //리스트가 비어있으면
        {
            cat_Effet.SetTrigger("IsProduct");
            //Product_Result_Item(); 
        }
    }
    public void Product_Result_Item() //완성아이템 생성
    {
        GameObject wagon_cat_ = Instantiate(wagon_cat, cat_spawn_point.position, transform.rotation); //고양이 소환
        wagon_cat_.GetComponent<Wagon_Cat>().auto_sell = auto_sales;

        GameObject result_item = Instantiate(product_item_recipe.result_item.item_prefab, drop_point_2.position, transform.rotation); // 아이템 생성
        result_item.transform.parent = this.transform.parent;                                                                         // 자식으로 가져오기
        ItemMove item_move = result_item.GetComponent<ItemMove>();
        item_move.clothesProductRail = GetComponent<ClothesProductRail>();
        item_move.Set_Speed(rail_product_speed, playerinfo.item_rail_production_speed);
        item_move.item_count = (int)rail_product_output;
    }
    void Auto_Sales_Item(Item item, int count)
    {
        playerinfo.player_gold += (item.item_price * count);
    }
    void Rail_Upgrade_UI_ON_OFF()
    {
        if (rail_ugrade_Ui_on) { rail_ugrade_Ui_on = false; }
        else if (!rail_ugrade_Ui_on) { rail_ugrade_Ui_on = true; }

        rail_upgrade_Ui.SetActive(rail_ugrade_Ui_on);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ingredient")
        {
            Ingredient_Remove(collision);
        }
    }
    public void AutoSale(bool check)
    {
        if(check == true)
        {
            auto_sales = true;
        }
        else
        {
            auto_sales = false;
        }
    }
    public void NextRecipe(Recipe recipe)
    {
        product_item_recipe_change = recipe;
    }
}
