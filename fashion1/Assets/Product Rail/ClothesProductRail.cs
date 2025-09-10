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

    [Header("���� ������ / ���� ���� ������")]
    public Recipe product_item_recipe;        //���� ������
    public Recipe product_item_recipe_change; //���� ���� ������

    [Space(10f)]
    public SpriteRenderer product_item_sprite;

    [Space(10f)]
    [SerializeField]
    Animator cat_Effet; //����ȿ�� �����
    [SerializeField]
    GameObject ingredient_Wagon_Cat; //��� �����
    [SerializeField]
    GameObject wagon_cat; //���������
    private List<GameObject> product_item_prefab = new List<GameObject>();

    [Space(10f)]
    public Transform storage_cat_spawn_point; // ��� ����� ���� ��ġ
    public Transform drop_point_1;          //��ᰡ �����Ǵ� ��ġ
    public Transform drop_point_2;          //���� �����Ǵ� ��ġ
    public Transform cat_spawn_point;       //�ϼ� ���� ����� ���� ��ġ

    [Space(10f)]
    public float rail_product_speed;
    public float rail_product_output;
    public float rail_rucky_output;

    [Header("�ڵ��Ǹ�")]
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
            product_item_sprite.sprite = product_item_recipe.result_item.item_sprite; //����ǰ �̹��� ǥ��
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
            //�̰� �׻��� ��ȯ���� �ٲٱ�
            //Production_Start(recipe); 
            Spawn_Ingredient_Wagon_Cat(recipe);

        }
        else if (!storage.Storage_Finding(recipe))
        {
            Debug.Log("��� ����");
        }
        else if (!storage.Storage_Space_Finding(recipe.result_item, 1))
        {
            Debug.Log("���� ����");
        }
    }



    public void Production_Start(Recipe recipe)  //���� ����
    {
        for (int i = 0; i < recipe.ingredient.Count; i++) // ��� ����
        {
            for (int j = 0; j < recipe.ingredient_count[i]; j++)
            {
                float random = Random.Range(-0.1f, 0.1f);
                Vector2 drop_position = drop_point_1.position;
                Vector2 random_position = drop_position + new Vector2(random, 0);  //������ ������ ���� ��ġ

                product_item_prefab.Add
                    (Instantiate(recipe.ingredient[i].item_prefab, random_position, transform.rotation)); //������ ���� �� ����Ʈ ����
                storage.Storage_Remove(recipe.ingredient[i], 1, false); //â���� ������ ������
            }
            for (int j = 0; j < product_item_prefab.Count; j++)
            {
                product_item_prefab[j].transform.parent = this.gameObject.transform; //�ڽ����� �ֱ�
                ItemMove itme_move = product_item_prefab[j].GetComponent<ItemMove>(); //��ȯ�� �����ۿ� ���� �Է�
                itme_move.Set_Speed(rail_product_speed, playerinfo.item_rail_production_speed);  //��ȯ�� �����ۿ� �ӵ� �ο� (���� �ӵ�)
                itme_move.clothesProductRail = GetComponent<ClothesProductRail>();
            }
        }

    }

    public void Production_Finish(Recipe recipe) //���� ����
    {
        Debug.Log("����");
        if (product_item_prefab.Count == 0) //���� üũ
        {
            //Double_Check(recipe);
            time_num = 0;
            now_production = false;

            if (product_item_recipe_change != null)  //������ ����
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
                storage.Storage_Remove(recipe.ingredient[i], 1, false); //â���� ������ ������
            }
        }
        GameObject ingredient_Wagon_Cat_ =
            Instantiate(ingredient_Wagon_Cat, storage_cat_spawn_point.position, transform.rotation); //����� ������ ��ȯ
        ingredient_Wagon_Cat_.transform.parent = this.transform;
    }

    public void Ingredient_Remove(Collider2D collision) //��� ����
    {
        product_item_prefab.Remove(collision.gameObject);       //����Ʈ���� ������ ����
        Destroy(collision.gameObject);                          //������Ʈ ����
        if (product_item_prefab.Count == 0)                     //����Ʈ�� ���������
        {
            cat_Effet.SetTrigger("IsProduct");
            //Product_Result_Item(); 
        }
    }
    public void Product_Result_Item() //�ϼ������� ����
    {
        GameObject wagon_cat_ = Instantiate(wagon_cat, cat_spawn_point.position, transform.rotation); //����� ��ȯ
        wagon_cat_.GetComponent<Wagon_Cat>().auto_sell = auto_sales;

        GameObject result_item = Instantiate(product_item_recipe.result_item.item_prefab, drop_point_2.position, transform.rotation); // ������ ����
        result_item.transform.parent = this.transform.parent;                                                                         // �ڽ����� ��������
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
