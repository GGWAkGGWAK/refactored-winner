using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;
using Unity.VisualScripting;
public class TimeShop : MonoBehaviour
{

    public int buyingCount;

    public GameObject[] timeShopImages;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI item1Text;
    public TextMeshProUGUI item2Text;
    public TextMeshProUGUI item3Text;


    [SerializeField]
    private List<Item> currentItems; // 현재 상점에 있는 아이템 리스트
    [SerializeField]
    private List<Recipe> currentRecipe;

    public GameObject storageObject;
    Storage storage;
    public GameObject pInfoObject;
    PlayerInfo pInfo;
    public GameObject recipesObject;
    RecipeBook recipeBook;
    public GameObject itemObject;
    ItemBook itemBook;

    public GameObject gold_Danger;
    public GameObject storage_Danger;
    public GameObject recipe_Danger;
    public bool recipeOn;           //레시피가 출현 했다면?
    public bool recipeOn2;
    public bool recipeOn3;

    public Button[] canRecipeButton;  //레시피가 나오는 버튼

    int index;
    int recipeIndex;
    void Start()
    {
        index = 0;
        recipeIndex = 0;
        pInfoObject = GameObject.Find("Playerinfo");
        pInfo = pInfoObject.GetComponent<PlayerInfo>();

        storageObject = GameObject.Find("Storage");
        storage = storageObject.GetComponent<Storage>();

        recipesObject = GameObject.Find("Recipe_Book");
        recipeBook = recipesObject.GetComponent<RecipeBook>();

        itemObject = GameObject.Find("Item_Book");
        itemBook = itemObject.GetComponent<ItemBook>();


        currentItems = new List<Item>();
        buyingCount = 100;
    }


    void Update()
    {
        timeText.text = pInfo.time_shop_refresh_time.ToString("F1");

        if (recipeOn)
        {
            recipeIndex = 0;
            index = -1;
            item1Text.text = Mathf.RoundToInt(currentRecipe[recipeIndex].result_item.item_price * 30 * pInfo.time_shop_discount).ToString();
        }
        else
        {
            recipeIndex = -1;
            index = 0;
            item1Text.text = Mathf.RoundToInt(currentItems[index].item_price * buyingCount * pInfo.time_shop_discount).ToString();
        }

        if (recipeOn2)
        {
            item2Text.text = Mathf.RoundToInt(currentRecipe[recipeIndex + 1].result_item.item_price * 30 * pInfo.time_shop_discount).ToString();
        }
        else
        {
            item2Text.text = Mathf.RoundToInt(currentItems[index + 1].item_price * buyingCount * pInfo.time_shop_discount).ToString();
        }

        if (recipeOn3)
        {
            if(recipeOn && !recipeOn2)
            {
                item3Text.text = Mathf.RoundToInt(currentRecipe[recipeIndex + 1].result_item.item_price * 30 * pInfo.time_shop_discount).ToString();
            }
            else if(!recipeOn && recipeOn2)
            {
                item3Text.text = Mathf.RoundToInt(currentRecipe[recipeIndex + 2].result_item.item_price * 30 * pInfo.time_shop_discount).ToString();
            }
            else if (recipeOn && recipeOn2)
            {
                item3Text.text = Mathf.RoundToInt(currentRecipe[recipeIndex + 2].result_item.item_price * 30 * pInfo.time_shop_discount).ToString();
            }
            else if(!recipeOn && !recipeOn2)
            {
                item3Text.text = Mathf.RoundToInt(currentRecipe[recipeIndex + 1].result_item.item_price * 30 * pInfo.time_shop_discount).ToString();
            }

        }
        else
        {
            if (recipeOn && !recipeOn2)
            {
                item3Text.text = Mathf.RoundToInt(currentItems[index + 2].item_price * buyingCount * pInfo.time_shop_discount).ToString();
            }
            else if (!recipeOn && recipeOn2)
            {
                item3Text.text = Mathf.RoundToInt(currentItems[index + 1].item_price * buyingCount * pInfo.time_shop_discount).ToString();
            }
            else if (recipeOn && recipeOn2)
            {
                item3Text.text = Mathf.RoundToInt(currentItems[index + 1].item_price * buyingCount * pInfo.time_shop_discount).ToString();
            }
            else if (!recipeOn && !recipeOn2)
            {
                item3Text.text = Mathf.RoundToInt(currentItems[index + 2].item_price * buyingCount * pInfo.time_shop_discount).ToString();
            }
            
        }

        
        

        
    }
    
    public void Reroll()
    {
        currentRecipe.Clear();
        currentItems.Clear();
        Debug.Log("Reroll!");

        canRecipeButton[0].onClick.RemoveAllListeners();
        canRecipeButton[1].onClick.RemoveAllListeners();
        canRecipeButton[2].onClick.RemoveAllListeners();
        

        List<Recipe> availableRecipes = new List<Recipe>();

        foreach (var recipe in recipeBook.all_recipe)
        {
            if (!recipeBook.unlock_recipe.Contains(recipe))
            {
                availableRecipes.Add(recipe);
            }
        }

        int rand = UnityEngine.Random.Range(1, 10);
        int rand2 = UnityEngine.Random.Range(1, 10);
        int rand3 = UnityEngine.Random.Range(1, 10);

        if (recipeBook.unlock_recipe == recipeBook.all_recipe)
        {
            rand = 1;
            rand2 = 1;
            rand3 = 1;
        }

        HashSet<Recipe> selectedRecipes = new HashSet<Recipe>();
        HashSet<Item> selectedItems = new HashSet<Item>();
        //1번 버튼
        if (rand >= 8)
        {
            recipeOn = true;
            if (availableRecipes.Count > 0)
            {
                //Recipe recipeItem = availableRecipes[UnityEngine.Random.Range(0, availableRecipes.Count)];
                Recipe recipeItem;
                do
                {
                    recipeItem = availableRecipes[UnityEngine.Random.Range(0, availableRecipes.Count)];
                } while (selectedRecipes.Contains(recipeItem));
                Debug.Log("Recipe: " + recipeItem.result_item.ToString()); // 예시로 로그 출력
                currentRecipe.Add(recipeItem);
                timeShopImages[0].GetComponent<Image>().sprite = recipeItem.result_item.item_sprite;
            }
            else
            {
                Debug.Log("더 이상 추가할 레시피가 없습니다.");
            }
        }
        else
        {
            recipeOn = false;
            //Item partsItem0 = itemBook.ingredeints[UnityEngine.Random.Range(0, itemBook.ingredeints.Count)];
            Item partsItem0;
            do
            {
                partsItem0 = itemBook.ingredeints[UnityEngine.Random.Range(0, itemBook.ingredeints.Count)];
            } while (selectedItems.Contains(partsItem0));
            Debug.Log("Parts: " + partsItem0.item_name);
            currentItems.Add(partsItem0);
            timeShopImages[0].GetComponent<Image>().sprite = partsItem0.item_sprite;
        }
        if (recipeOn)
        {
            UnityAction timebuyRecipe1 = () => TimeBuyRecipe(recipeIndex);
            canRecipeButton[0].onClick.AddListener(timebuyRecipe1);
        }
        else
        {
            UnityAction timebuyitem1 = () => TimeBuyItem(index);
            canRecipeButton[0].onClick.AddListener(timebuyitem1);
        }


        //2번버튼
        if (rand2 >= 8)
        {
            recipeOn2 = true;
            if (availableRecipes.Count > 0)
            {
                //Recipe recipeItem = availableRecipes[UnityEngine.Random.Range(0, availableRecipes.Count)];
                Recipe recipeItem;
                do
                {
                    recipeItem = availableRecipes[UnityEngine.Random.Range(0, availableRecipes.Count)];
                } while (selectedRecipes.Contains(recipeItem));
                Debug.Log("Recipe: " + recipeItem.result_item.ToString()); // 예시로 로그 출력
                currentRecipe.Add(recipeItem);
                timeShopImages[1].GetComponent<Image>().sprite = recipeItem.result_item.item_sprite;
            }
            else
            {
                Debug.Log("더 이상 추가할 레시피가 없습니다.");
            }
        }
        else
        {
            recipeOn2 = false;
            // 랜덤으로 재료 아이템 선택
            //Item partsItem = itemBook.ingredeints[UnityEngine.Random.Range(0, itemBook.ingredeints.Count)];
            Item partsItem;
            do
            {
                partsItem = itemBook.ingredeints[UnityEngine.Random.Range(0, itemBook.ingredeints.Count)];
            } while (selectedItems.Contains(partsItem));
            Debug.Log("Parts: " + partsItem.item_name);
            currentItems.Add(partsItem);
            timeShopImages[1].GetComponent<Image>().sprite = partsItem.item_sprite;

        }

        if (recipeOn2)
        {
            UnityAction timebuyRecipe2 = () => TimeBuyRecipe(recipeIndex + 1);
            canRecipeButton[1].onClick.AddListener(timebuyRecipe2);
        }
        else
        {
            UnityAction timebuyitem2 = () => TimeBuyItem(index + 1);
            canRecipeButton[1].onClick.AddListener(timebuyitem2);
        }


        //3번 버튼
        if (rand3 >= 8)
        {
            recipeOn3 = true;
            if (availableRecipes.Count > 0)
            {
                //Recipe recipeItem = availableRecipes[UnityEngine.Random.Range(0, availableRecipes.Count)];
                Recipe recipeItem;
                do
                {
                    recipeItem = availableRecipes[UnityEngine.Random.Range(0, availableRecipes.Count)];
                } while (selectedRecipes.Contains(recipeItem));
                Debug.Log("Recipe: " + recipeItem.result_item.ToString()); // 예시로 로그 출력
                currentRecipe.Add(recipeItem);
                timeShopImages[2].GetComponent<Image>().sprite = recipeItem.result_item.item_sprite;
            }
            else
            {
                Debug.Log("더 이상 추가할 레시피가 없습니다.");
            }
        }
        else
        {
            recipeOn3 = false;
            // 랜덤으로 재료 아이템 선택
            //Item partsItem2 = itemBook.ingredeints[UnityEngine.Random.Range(0, itemBook.ingredeints.Count)];
            Item partsItem2;
            do
            {
                partsItem2 = itemBook.ingredeints[UnityEngine.Random.Range(0, itemBook.ingredeints.Count)];
            } while (selectedItems.Contains(partsItem2));
            Debug.Log("Parts2: " + partsItem2.item_name);
            currentItems.Add(partsItem2);
            timeShopImages[2].GetComponent<Image>().sprite = partsItem2.item_sprite;
        }
        if (recipeOn3)
        {
            if (recipeOn && !recipeOn2)
            {
                UnityAction timebuyRecipe3 = () => TimeBuyRecipe(recipeIndex + 1);
                canRecipeButton[2].onClick.AddListener(timebuyRecipe3);
            }
            else if (!recipeOn && recipeOn2)
            {
                UnityAction timebuyRecipe3 = () => TimeBuyRecipe(recipeIndex + 2);
                canRecipeButton[2].onClick.AddListener(timebuyRecipe3);
            }
            else if (recipeOn && recipeOn2)
            {
                UnityAction timebuyRecipe3 = () => TimeBuyRecipe(recipeIndex + 2);
                canRecipeButton[2].onClick.AddListener(timebuyRecipe3);
            }
            else if (!recipeOn && !recipeOn2)
            {
                UnityAction timebuyRecipe3 = () => TimeBuyRecipe(recipeIndex + 1);
                canRecipeButton[2].onClick.AddListener(timebuyRecipe3);
            }
            
        }
        else
        {
            if (recipeOn && !recipeOn2)
            {
                UnityAction timebuyitem3 = () => TimeBuyItem(index + 2);
                canRecipeButton[2].onClick.AddListener(timebuyitem3);
            }
            else if (!recipeOn && recipeOn2)
            {
                UnityAction timebuyitem3 = () => TimeBuyItem(index + 1);
                canRecipeButton[2].onClick.AddListener(timebuyitem3);
            }
            else if (recipeOn && recipeOn2)
            {
                UnityAction timebuyitem3 = () => TimeBuyItem(index + 1);
                canRecipeButton[2].onClick.AddListener(timebuyitem3);
            }
            else if (!recipeOn && !recipeOn2)
            {
                UnityAction timebuyitem3 = () => TimeBuyItem(index + 2);
                canRecipeButton[2].onClick.AddListener(timebuyitem3);
            }
            
        }
        

    }



    public void TimeBuyItem(int num)
    {
        if (pInfo.player_gold >= Mathf.RoundToInt(currentItems[num].item_price * buyingCount * pInfo.time_shop_discount))
        {
            if (storage.Storage_Space_Finding(currentItems[num], buyingCount))
            {
                pInfo.player_gold -= Mathf.RoundToInt(currentItems[num].item_price * buyingCount * pInfo.time_shop_discount);
                storage.Storage_Add(currentItems[num], buyingCount);
            }
            else
            {
                Debug.LogError("창고에 자리가 없습니다!");
                storage_Danger.SetActive(true);
            }
        }
        else
        {
            Debug.Log("보유 골드가 부족합니다!");
            gold_Danger.SetActive(true);
        }
    }

    public void TimeBuyRecipe(int num)
    {

        if (pInfo.player_gold >= Mathf.RoundToInt(currentRecipe[num].result_item.item_price * 30 * pInfo.time_shop_discount))
        {
            if (!recipeBook.unlock_recipe.Contains(currentRecipe[num]))
            {
                pInfo.player_gold -= Mathf.RoundToInt(currentRecipe[num].result_item.item_price * 30 * pInfo.time_shop_discount);
                recipeBook.Unlock_Recipe(currentRecipe[num]);

                GameObject parentObject1 = GameObject.Find("Total_Rail_Object"); // 부모 오브젝트 이름을 정확히 입력하세요.
                GameObject parentObject2 = GameObject.Find("Total_Rail_Object");
                GameObject parentObject3 = GameObject.Find("Total_Rail_Object");
                if (parentObject1 != null)
                {
                    try
                    {
                        Transform level0child = parentObject3.transform.GetChild(0);
                        Transform level1child = level0child.transform.GetChild(0);
                        Transform level2child = level1child.transform.GetChild(1);
                        Transform level3child = level2child.transform.GetChild(0);
                        Transform level4child = level3child.transform.GetChild(0);
                        Transform level5child = level4child.transform.GetChild(5);
                        Transform level6Child = level5child.GetChild(2);
                        Transform level7Child = level6Child.GetChild(0);
                        Transform target = level7Child.GetChild(0);

                        if (target != null)
                        {
                            // target의 자식 중에서 특정 이름을 가진 오브젝트를 찾습니다.
                            string targetChildName = "Item_Frame (" + currentRecipe[num].result_item.item_name + ")"; // 찾고자 하는 자식 오브젝트의 이름
                            Transform namedChild = null;

                            foreach (Transform child in target)
                            {
                                if (child.name == targetChildName)
                                {
                                    namedChild = child;
                                    break;
                                }
                            }

                            if (namedChild != null)
                            {
                                // 찾은 자식의 2번 자식을 비활성화합니다.
                                Transform secondChild = namedChild.GetChild(2);
                                if (secondChild != null)
                                {
                                    secondChild.gameObject.SetActive(false); // 오브젝트를 비활성화합니다.
                                }
                                else
                                {
                                    Debug.LogError("찾은 자식의 2번 자식을 찾을 수 없습니다.");
                                }
                            }
                            else
                            {
                                Debug.LogError("목표 자식 오브젝트를 찾을 수 없습니다: " + targetChildName);
                            }
                            SortChildren(target);
                        }
                        else
                        {
                            Debug.LogError("목표 오브젝트를 찾을 수 없습니다.");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("오브젝트 경로를 찾는 도중 오류 발생: " + e.Message);
                    }
                }
                else
                {
                    Debug.LogError("부모 게임 오브젝트를 찾을 수 없습니다.");
                }
                if (parentObject2 != null)
                {
                    try
                    {
                        Transform level0child = parentObject3.transform.GetChild(1);
                        Transform level1child = level0child.transform.GetChild(0);
                        Transform level2child = level1child.transform.GetChild(1);
                        Transform level3child = level2child.transform.GetChild(0);
                        Transform level4child = level3child.transform.GetChild(0);
                        Transform level5child = level4child.transform.GetChild(5);
                        Transform level6Child = level5child.GetChild(2);
                        Transform level7Child = level6Child.GetChild(0);
                        Transform target = level7Child.GetChild(0);

                        if (target != null)
                        {
                            // target의 자식 중에서 특정 이름을 가진 오브젝트를 찾습니다.
                            string targetChildName = "Item_Frame (" + currentRecipe[num].result_item.item_name + ")"; // 찾고자 하는 자식 오브젝트의 이름
                            Transform namedChild = null;

                            foreach (Transform child in target)
                            {
                                if (child.name == targetChildName)
                                {
                                    namedChild = child;
                                    break;
                                }
                            }

                            if (namedChild != null)
                            {
                                // 찾은 자식의 2번 자식을 비활성화합니다.
                                Transform secondChild = namedChild.GetChild(2);
                                if (secondChild != null)
                                {
                                    secondChild.gameObject.SetActive(false); // 오브젝트를 비활성화합니다.
                                }
                                else
                                {
                                    Debug.LogError("찾은 자식의 2번 자식을 찾을 수 없습니다.");
                                }
                            }
                            else
                            {
                                Debug.LogError("목표 자식 오브젝트를 찾을 수 없습니다: " + targetChildName);
                            }
                            SortChildren(target);
                        }
                        else
                        {
                            Debug.LogError("목표 오브젝트를 찾을 수 없습니다.");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("오브젝트 경로를 찾는 도중 오류 발생: " + e.Message);
                    }
                }
                else
                {
                    Debug.LogError("부모 게임 오브젝트를 찾을 수 없습니다.");
                }

                if (parentObject3 != null)
                {
                    try
                    {
                        // 지정된 경로를 따라 자식 오브젝트를 찾습니다.
                        Transform level0child = parentObject3.transform.GetChild(2);
                        Transform level1child = level0child.transform.GetChild(0);
                        Transform level2child = level1child.transform.GetChild(1);
                        Transform level3child = level2child.transform.GetChild(0);
                        Transform level4child = level3child.transform.GetChild(0);
                        Transform level5child = level4child.transform.GetChild(5);
                        Transform level6Child = level5child.GetChild(2);
                        Transform level7Child = level6Child.GetChild(0);
                        Transform target = level7Child.GetChild(0);

                        if (target != null)
                        {
                            // target의 자식 중에서 특정 이름을 가진 오브젝트를 찾습니다.
                            string targetChildName = "Item_Frame (" + currentRecipe[num].result_item.item_name + ")"; // 찾고자 하는 자식 오브젝트의 이름
                            Transform namedChild = null;

                            foreach (Transform child in target)
                            {
                                if (child.name == targetChildName)
                                {
                                    namedChild = child;
                                    break;
                                }
                            }

                            if (namedChild != null)
                            {
                                // 찾은 자식의 2번 자식을 비활성화합니다.
                                Transform secondChild = namedChild.GetChild(2);
                                if (secondChild != null)
                                {
                                    secondChild.gameObject.SetActive(false); // 오브젝트를 비활성화합니다.
                                }
                                else
                                {
                                    Debug.LogError("찾은 자식의 2번 자식을 찾을 수 없습니다.");
                                }
                            }
                            else
                            {
                                Debug.LogError("목표 자식 오브젝트를 찾을 수 없습니다: " + targetChildName);
                            }
                            SortChildren(target);
                        }
                        else
                        {
                            Debug.LogError("목표 오브젝트를 찾을 수 없습니다.");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("오브젝트 경로를 찾는 도중 오류 발생: " + e.Message);
                    }
                }
                else
                {
                    Debug.Log("부모 게임 오브젝트를 찾을 수 없습니다.");
                }


            }
            else
            {
                recipe_Danger.SetActive(true);
                Debug.Log("이미 존재하는 레시피입니다!");
            }

        }
        else
        {
            gold_Danger.SetActive(true);
            Debug.Log("보유 골드가 부족합니다!");
        }

    }
    private void SortChildren(Transform parentTransform)
    {
        if (parentTransform != null)
        {
            try
            {
                List<Transform> children = new List<Transform>();

                foreach (Transform child in parentTransform)
                {
                    children.Add(child);
                }

                // 언락된 레시피가 위로, 나머지가 아래로 정렬
                children.Sort((x, y) =>
                {
                    bool xUnlocked = recipeBook.unlock_recipe.Exists(recipe => recipe.result_item.item_name == x.name.Replace("Item_Frame (", "").Replace(")", ""));
                    bool yUnlocked = recipeBook.unlock_recipe.Exists(recipe => recipe.result_item.item_name == y.name.Replace("Item_Frame (", "").Replace(")", ""));

                    if (xUnlocked && !yUnlocked) return -1;
                    if (!xUnlocked && yUnlocked) return 1;
                    return 0;
                });

                // 정렬된 자식 오브젝트들을 다시 부모의 자식으로 설정
                for (int i = 0; i < children.Count; i++)
                {
                    children[i].SetSiblingIndex(i);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("자식 오브젝트를 정렬하는 도중 오류 발생: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("부모 게임 오브젝트를 찾을 수 없습니다.");
        }
    }
}
