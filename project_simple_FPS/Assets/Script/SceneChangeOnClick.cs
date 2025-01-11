using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeOnClick : MonoBehaviour
{
    public int nextSceneIndex; // 이동할 씬 번호
    private AudioSource audioSource; // 기존 AudioSource 참조

    void Start()
    {
        // 현재 GameObject의 AudioSource 가져오기
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.loop = true; // 반복 재생
            audioSource.Play(); // 배경음악 재생
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 화면 클릭 감지
        {
            SceneManager.LoadScene(nextSceneIndex); // 씬 번호로 전환
        }
    }
}