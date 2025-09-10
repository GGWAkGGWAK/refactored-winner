using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class Scroll : MonoBehaviour {

    public RectTransform List;
    public int count;
    private float pos;
    private float movepos;
    private bool IsScroll = false;

    // Use this for initialization
	void Start () 
    {
        pos = List.localPosition.x;
        movepos = List.rect.xMax - List.rect.xMax / count;
        Debug.Log(List.rect.xMax);
        Debug.Log(List.rect.xMin);
        Debug.Log(List.rect.xMax - List.rect.xMax / count + "|" + pos);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Right()
    {
        if(List.rect.xMin + List.rect.xMax/count == movepos)
        {

        }
        else
        {
            IsScroll = true;
            movepos = pos - List.rect.width / count;
            pos = movepos;
            StartCoroutine(scroll());
        }
    }

    public void Left()
    {
        if(List.rect.xMax - List.rect.xMax/count == movepos)
        {

        }
        else
        {
            IsScroll = true;
            movepos = pos + List.rect.width / count;
            pos = movepos;
            StartCoroutine(scroll());
        }
    }

    IEnumerator scroll()
    {
        while (IsScroll)
        {
            List.localPosition = Vector2.Lerp(List.localPosition, new Vector2(movepos, 0), Time.deltaTime * 5);
            if (Vector2.Distance(List.localPosition, new Vector2(movepos, 0)) < 0.1f)
            {
                IsScroll = false;
            }
            yield return null;
        }
    }
}
