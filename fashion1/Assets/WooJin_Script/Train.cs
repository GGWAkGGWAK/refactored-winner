using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net.NetworkInformation;
using UnityEngine.EventSystems;

public class Train : MonoBehaviour
{
    public class TrainInfo
    {
        public List<Item> trainInfoItems = new List<Item>();
        public bool rewardGiven = false;
        public bool canDepart = false;

        public PlayerInfo pInfo;

        // 생성자를 통해 필요한 참조를 초기화
        public TrainInfo(PlayerInfo playerInfo)
        {
            this.pInfo = playerInfo;
        }

        public void AddItem(Item item)
        {
            trainInfoItems.Add(item);
        }

        public void GiveReward()
        {
            if (!rewardGiven)
            {
                float totalReward = 0;
                foreach (Item item in trainInfoItems)
                {
                    totalReward += item.item_price * pInfo.train_reward_multiple;
                }
                pInfo.player_gold += Mathf.RoundToInt(totalReward);//반올림
                Debug.Log("기차 보상: " + totalReward);
                rewardGiven = true;
            }
            else
            {
                Debug.Log("대기시간을 기다려주세요");
            }
        }

        public void ResetItems()
        {
            trainInfoItems.Clear();
            rewardGiven = false;
        }
    }

    public GameObject storageObject;
    Storage storage;
    public GameObject pInfoObject;
    PlayerInfo pInfo;
    public GameObject recipesObject;
    RecipeBook recipeBook;

    public GameObject managerObject;
    GameManager gm;

    public GameObject item_Danger;

    public TrainInfo[] trains = new TrainInfo[3];

    public GameObject[] rewardButtons = new GameObject[3];
    public GameObject[] resetButtons = new GameObject[3];

    public GameObject[] trainItemImages1;
    public GameObject[] trainItemImages2;
    public GameObject[] trainItemImages3;
    public TextMeshProUGUI train1text;
    public TextMeshProUGUI train2text;
    public TextMeshProUGUI train3text;

    public int trainCount = 3;  // 조건 달성 시 플레이어 레벨 or 퀘스트로 개수를 늘릴 것

    public GameObject train1;
    public GameObject train2;
    public GameObject train3;

    public GameObject train1Head;
    public GameObject train2Head;
    public GameObject train3Head;

    private Rigidbody2D train1Rigidbody;
    private Rigidbody2D train2Rigidbody;
    private Rigidbody2D train3Rigidbody;
    [SerializeField]
    private Vector2[] trainHeadInitialPositions;

    public AudioSource truckSound;
    void Start()
    {
        storageObject = GameObject.Find("Storage");
        storage = storageObject.GetComponent<Storage>();

        pInfoObject = GameObject.Find("Playerinfo");
        pInfo = pInfoObject.GetComponent<PlayerInfo>();

        recipesObject = GameObject.Find("Recipe_Book");
        recipeBook = recipesObject.GetComponent<RecipeBook>();

        managerObject = GameObject.Find("GameManager");
        gm = managerObject.GetComponent<GameManager>();

        train1Rigidbody = train1Head.GetComponent<Rigidbody2D>();
        train2Rigidbody = train2Head.GetComponent<Rigidbody2D>();
        train3Rigidbody = train3Head.GetComponent<Rigidbody2D>();

        trainHeadInitialPositions = new Vector2[3];
        trainHeadInitialPositions[0] = train1Head.transform.position;
        trainHeadInitialPositions[1] = train2Head.transform.position;
        trainHeadInitialPositions[2] = train3Head.transform.position;

        for (int i = 0; i < trainCount; i++)
        {
            trains[i] = new TrainInfo(pInfo);
            trains[i].AddItem(TrainItemRoll());
            trains[i].AddItem(TrainItemRoll());
            trains[i].AddItem(TrainItemRoll());
            if (i == 0)
            {
                trainItemImages1[0].GetComponent<Image>().sprite = trains[i].trainInfoItems[0].item_sprite;
                trainItemImages1[1].GetComponent<Image>().sprite = trains[i].trainInfoItems[1].item_sprite;
                trainItemImages1[2].GetComponent<Image>().sprite = trains[i].trainInfoItems[2].item_sprite;
            }
            else if (i == 1)
            {
                trainItemImages2[0].GetComponent<Image>().sprite = trains[i].trainInfoItems[0].item_sprite;
                trainItemImages2[1].GetComponent<Image>().sprite = trains[i].trainInfoItems[1].item_sprite;
                trainItemImages2[2].GetComponent<Image>().sprite = trains[i].trainInfoItems[2].item_sprite;
            }
            else if (i == 2)
            {
                trainItemImages3[0].GetComponent<Image>().sprite = trains[i].trainInfoItems[0].item_sprite;
                trainItemImages3[1].GetComponent<Image>().sprite = trains[i].trainInfoItems[1].item_sprite;
                trainItemImages3[2].GetComponent<Image>().sprite = trains[i].trainInfoItems[2].item_sprite;
            }
        }

    }

    void Update()
    {
        train1text.text = pInfo.train_reward_time1.ToString("F0");
        train2text.text = pInfo.train_reward_time2.ToString("F0");
        train3text.text = pInfo.train_reward_time3.ToString("F0");

        if (pInfo.train1Started && pInfo.train_reward_time1 <= 0)
        {
            pInfo.train_reward_time1 -= Time.deltaTime;
            TrainRoll(0);
            pInfo.train1Started = false;

        }
        else if (!pInfo.train1Started)
        {
            pInfo.train_reward_time1 = pInfo.train_main_reward_time1;

            train1.SetActive(true);
            train1Head.SetActive(true);
            train1Head.transform.position = trainHeadInitialPositions[0];
        }

        if (pInfo.train2Started && pInfo.train_reward_time2 <= 0)
        {
            pInfo.train_reward_time2 -= Time.deltaTime;
            TrainRoll(1);
            pInfo.train2Started = false;

        }
        else if (!pInfo.train2Started)
        {
            pInfo.train_reward_time2 = pInfo.train_main_reward_time2;
            train2.SetActive(true);
            train2Head.SetActive(true);
            train2Head.transform.position = trainHeadInitialPositions[1];
        }

        if (pInfo.train3Started && pInfo.train_reward_time3 <= 0)
        {
            pInfo.train_reward_time3 -= Time.deltaTime;
            TrainRoll(2);
            pInfo.train3Started = false;

        }
        else if (!pInfo.train3Started)
        {
            pInfo.train_reward_time3 = pInfo.train_main_reward_time3;
            train3.SetActive(true);
            train3Head.SetActive(true);
            train3Head.transform.position = trainHeadInitialPositions[2];
        }

        if (pInfo.train_reward_time1 <= 0)
        {
            trains[0].GiveReward();
            gm.trainExclamationMark.SetActive(false);
        }
        else if (pInfo.train_reward_time2 <= 0)
        {
            trains[1].GiveReward();
            gm.trainExclamationMark.SetActive(false);
        }
        else if (pInfo.train_reward_time2 <= 0)
        {
            trains[2].GiveReward();
            gm.trainExclamationMark.SetActive(false);
        }

    }

    public void GiveReward(int trainIndex)
    {
        HandleTrainDeparture(trainIndex);
        if (trainIndex == 0)
        {
            pInfo.train_reward_time1 = pInfo.train_main_reward_time1;
        }
        else if (trainIndex == 1)
        {
            pInfo.train_reward_time2 = pInfo.train_main_reward_time2;
        }
        else if (trainIndex == 2)
        {
            pInfo.train_reward_time3 = pInfo.train_main_reward_time3;
        }
    }

    public void ResetItems(int trainIndex)
    {
        trains[trainIndex].ResetItems();
        TrainRoll(trainIndex);
    }

    public Item TrainItemRoll() //아이템 배정 함수
    {
        if (recipeBook.unlock_recipe.Count == 0)
        {
            Debug.LogError("아이템 배정 X");
            return null;
        }
        int randomIndex = Random.Range(0, recipeBook.unlock_recipe.Count);
        return recipeBook.unlock_recipe[randomIndex].result_item;
    }

    public void TrainRoll(int trainIndex) // 기차 내 아이템 리롤 함수
    {
        trains[trainIndex] = new TrainInfo(pInfo);
        trains[trainIndex].AddItem(TrainItemRoll());
        trains[trainIndex].AddItem(TrainItemRoll());
        trains[trainIndex].AddItem(TrainItemRoll());
        if (trainIndex == 0)
        {
            trainItemImages1[0].GetComponent<Image>().sprite = trains[trainIndex].trainInfoItems[0].item_sprite;
            trainItemImages1[1].GetComponent<Image>().sprite = trains[trainIndex].trainInfoItems[1].item_sprite;
            trainItemImages1[2].GetComponent<Image>().sprite = trains[trainIndex].trainInfoItems[2].item_sprite;
        }
        else if (trainIndex == 1)
        {
            trainItemImages2[0].GetComponent<Image>().sprite = trains[trainIndex].trainInfoItems[0].item_sprite;
            trainItemImages2[1].GetComponent<Image>().sprite = trains[trainIndex].trainInfoItems[1].item_sprite;
            trainItemImages2[2].GetComponent<Image>().sprite = trains[trainIndex].trainInfoItems[2].item_sprite;
        }
        else if (trainIndex == 2)
        {
            trainItemImages3[0].GetComponent<Image>().sprite = trains[trainIndex].trainInfoItems[0].item_sprite;
            trainItemImages3[1].GetComponent<Image>().sprite = trains[trainIndex].trainInfoItems[1].item_sprite;
            trainItemImages3[2].GetComponent<Image>().sprite = trains[trainIndex].trainInfoItems[2].item_sprite;
        }
    }

    IEnumerator Delay(int trainIndex)
    {
        Debug.Log("기차 출발!");
        if (trainIndex == 0)
        {
            StartCoroutine(MoveTrain(trainIndex, pInfo.train_main_reward_time1 / 150)); // 기차 이동 코루틴 실행
            yield return new WaitForSeconds(pInfo.train_main_reward_time1); // waitTime만큼 대기
            //trains[trainIndex].GiveReward(); // 보상 지급
        }
        else if (trainIndex == 1)
        {
            StartCoroutine(MoveTrain(trainIndex, pInfo.train_main_reward_time2 / 150)); // 기차 이동 코루틴 실행
            yield return new WaitForSeconds(pInfo.train_main_reward_time2); // waitTime만큼 대기
            //trains[trainIndex].GiveReward(); // 보상 지급
        }
        else if (trainIndex == 2)
        {
            StartCoroutine(MoveTrain(trainIndex, pInfo.train_main_reward_time3 / 150)); // 기차 이동 코루틴 실행
            yield return new WaitForSeconds(pInfo.train_main_reward_time3); // waitTime만큼 대기
            //trains[trainIndex].GiveReward(); // 보상 지급
        }

        Debug.Log("기차 도착!");

        // waitTime 후에 trainHead를 활성화
        switch (trainIndex)
        {
            case 0:
                train1Head.transform.position = trainHeadInitialPositions[0];
                train1Head.SetActive(true);
                break;
            case 1:
                train2Head.transform.position = trainHeadInitialPositions[1];
                train2Head.SetActive(true);
                break;
            case 2:
                train3Head.transform.position = trainHeadInitialPositions[2];
                train3Head.SetActive(true);
                break;
        }
    }

    IEnumerator MoveTrain(int trainIndex, float fastDuration)
    {
        GameObject trainHead = null;
        Vector2 initialPosition = Vector2.zero;
        Rigidbody2D trainRigidbody = null;

        switch (trainIndex)
        {
            case 0:
                trainHead = train1Head;
                initialPosition = trainHeadInitialPositions[0];
                trainRigidbody = train1Rigidbody;
                break;
            case 1:
                trainHead = train2Head;
                initialPosition = trainHeadInitialPositions[1];
                trainRigidbody = train2Rigidbody;
                break;
            case 2:
                trainHead = train3Head;
                initialPosition = trainHeadInitialPositions[2];
                trainRigidbody = train3Rigidbody;
                break;
        }

        Vector2 targetPosition = initialPosition + Vector2.left * 100; // 왼쪽으로 10 단위 이동

        float elapsedTime = 0;

        while (elapsedTime < fastDuration)
        {
            float t = elapsedTime / fastDuration;
            trainRigidbody.MovePosition(Vector2.Lerp(initialPosition, targetPosition, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        trainRigidbody.MovePosition(targetPosition);
        trainHead.SetActive(false); // 기차가 도착한 후 비활성화

        //yield return new WaitForSeconds(1); // 기차가 도착해서 멈춰 있는 시간

        //trainRigidbody.MovePosition(initialPosition); // 처음 자리로 돌아옴
    }

    private void HandleTrainDeparture(int trainIndex)
    {
        TrainInfo train = trains[trainIndex];
        bool canDepart = true;

        // 창고에 배정된 기차의 아이템이 있는지 확인
        foreach (Item item in train.trainInfoItems)
        {
            // 창고에서 해당 아이템이 충분한지 확인
            if (!storage.Storage_Finding(item, 1)) // 1은 해당 아이템을 1개씩 검사함을 의미
            {
                canDepart = false;
                Debug.Log("아이템이 충분하지 않아 기차가 출발하지 않습니다. trainIndex: " + trainIndex);
                item_Danger.SetActive(true);
                return; // 아이템 부족으로 출발 불가
            }
        }

        // 출발 가능한 경우
        if (canDepart)
        {
            // 창고에서 아이템을 소모하고 기차 출발
            foreach (Item item in train.trainInfoItems)
            {
                storage.Storage_Remove(item, 1, false);
                storage.Storage_Organization();
            }

            switch (trainIndex)
            {
                case 0:
                    pInfo.train1Started = true;
                    train1.SetActive(false);
                    break;
                case 1:
                    pInfo.train2Started = true;
                    train2.SetActive(false);
                    break;
                case 2:
                    pInfo.train3Started = true;
                    train3.SetActive(false);
                    break;
            }
            truckSound.Play();
            StartCoroutine(Delay(trainIndex));  // 기차 출발 후 코루틴 실행
        }
        else
        {
            Debug.Log("창고에 해당 아이템이 충분하지 않아 기차 " + (trainIndex + 1) + " 출발할 수 없습니다!");
            item_Danger.SetActive(true);
        }

    }

    public void TrainTransformReset()
    {
        if (pInfo.train1Started)
        {
            train1Head.SetActive(false);
        }

        if (pInfo.train2Started)
        {
            train2Head.SetActive(false);
        }
        if (pInfo.train3Started)
        {
            train3Head.SetActive(false);
        }
    }

}
