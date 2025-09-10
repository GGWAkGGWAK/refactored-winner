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

        // �����ڸ� ���� �ʿ��� ������ �ʱ�ȭ
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
                pInfo.player_gold += Mathf.RoundToInt(totalReward);//�ݿø�
                Debug.Log("���� ����: " + totalReward);
                rewardGiven = true;
            }
            else
            {
                Debug.Log("���ð��� ��ٷ��ּ���");
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

    public int trainCount = 3;  // ���� �޼� �� �÷��̾� ���� or ����Ʈ�� ������ �ø� ��

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

    public Item TrainItemRoll() //������ ���� �Լ�
    {
        if (recipeBook.unlock_recipe.Count == 0)
        {
            Debug.LogError("������ ���� X");
            return null;
        }
        int randomIndex = Random.Range(0, recipeBook.unlock_recipe.Count);
        return recipeBook.unlock_recipe[randomIndex].result_item;
    }

    public void TrainRoll(int trainIndex) // ���� �� ������ ���� �Լ�
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
        Debug.Log("���� ���!");
        if (trainIndex == 0)
        {
            StartCoroutine(MoveTrain(trainIndex, pInfo.train_main_reward_time1 / 150)); // ���� �̵� �ڷ�ƾ ����
            yield return new WaitForSeconds(pInfo.train_main_reward_time1); // waitTime��ŭ ���
            //trains[trainIndex].GiveReward(); // ���� ����
        }
        else if (trainIndex == 1)
        {
            StartCoroutine(MoveTrain(trainIndex, pInfo.train_main_reward_time2 / 150)); // ���� �̵� �ڷ�ƾ ����
            yield return new WaitForSeconds(pInfo.train_main_reward_time2); // waitTime��ŭ ���
            //trains[trainIndex].GiveReward(); // ���� ����
        }
        else if (trainIndex == 2)
        {
            StartCoroutine(MoveTrain(trainIndex, pInfo.train_main_reward_time3 / 150)); // ���� �̵� �ڷ�ƾ ����
            yield return new WaitForSeconds(pInfo.train_main_reward_time3); // waitTime��ŭ ���
            //trains[trainIndex].GiveReward(); // ���� ����
        }

        Debug.Log("���� ����!");

        // waitTime �Ŀ� trainHead�� Ȱ��ȭ
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

        Vector2 targetPosition = initialPosition + Vector2.left * 100; // �������� 10 ���� �̵�

        float elapsedTime = 0;

        while (elapsedTime < fastDuration)
        {
            float t = elapsedTime / fastDuration;
            trainRigidbody.MovePosition(Vector2.Lerp(initialPosition, targetPosition, t));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        trainRigidbody.MovePosition(targetPosition);
        trainHead.SetActive(false); // ������ ������ �� ��Ȱ��ȭ

        //yield return new WaitForSeconds(1); // ������ �����ؼ� ���� �ִ� �ð�

        //trainRigidbody.MovePosition(initialPosition); // ó�� �ڸ��� ���ƿ�
    }

    private void HandleTrainDeparture(int trainIndex)
    {
        TrainInfo train = trains[trainIndex];
        bool canDepart = true;

        // â�� ������ ������ �������� �ִ��� Ȯ��
        foreach (Item item in train.trainInfoItems)
        {
            // â���� �ش� �������� ������� Ȯ��
            if (!storage.Storage_Finding(item, 1)) // 1�� �ش� �������� 1���� �˻����� �ǹ�
            {
                canDepart = false;
                Debug.Log("�������� ������� �ʾ� ������ ������� �ʽ��ϴ�. trainIndex: " + trainIndex);
                item_Danger.SetActive(true);
                return; // ������ �������� ��� �Ұ�
            }
        }

        // ��� ������ ���
        if (canDepart)
        {
            // â���� �������� �Ҹ��ϰ� ���� ���
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
            StartCoroutine(Delay(trainIndex));  // ���� ��� �� �ڷ�ƾ ����
        }
        else
        {
            Debug.Log("â�� �ش� �������� ������� �ʾ� ���� " + (trainIndex + 1) + " ����� �� �����ϴ�!");
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
