using UnityEngine;

public class TouchSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // 게임 오브젝트에 추가된 AudioSource 컴포넌트를 가져옵니다.
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 모바일 터치 입력 처리
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaySound();
        }
        // 에디터 및 PC용 마우스 클릭 입력 처리
        if (Input.GetMouseButtonDown(0))
        {
            PlaySound();
        }
    }

    void PlaySound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
