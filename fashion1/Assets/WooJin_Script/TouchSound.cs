using UnityEngine;

public class TouchSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // ���� ������Ʈ�� �߰��� AudioSource ������Ʈ�� �����ɴϴ�.
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ����� ��ġ �Է� ó��
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaySound();
        }
        // ������ �� PC�� ���콺 Ŭ�� �Է� ó��
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
