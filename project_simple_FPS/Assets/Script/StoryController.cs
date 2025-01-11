using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class StoryController : MonoBehaviour
{
    public Image storyImage; // 중앙 이미지
    public TextMeshProUGUI storyText; // 텍스트
    public Sprite[] storyImages; // 슬라이드 이미지 배열 (null 허용)
    public string[] storyTexts; // 슬라이드 텍스트 배열
    public AudioClip[] slideAudioClips; // 슬라이드별 오디오 배열 (null 허용)
    public AudioClip backgroundMusic; // 전체 배경음악
    public float typingSpeed = 0.05f; // 텍스트 타이핑 속도

    private int currentSlideIndex = 0; // 현재 슬라이드 인덱스
    private bool isTyping = false; // 타이핑 중 여부
    private AudioSource audioSource; // 슬라이드 오디오 소스
    private AudioSource bgAudioSource; // 배경음악 소스

    void Start()
    {
        // AudioSource 추가 및 초기화
        audioSource = gameObject.AddComponent<AudioSource>();
        bgAudioSource = gameObject.AddComponent<AudioSource>();

        // 배경음악 설정 및 재생
        if (backgroundMusic != null)
        {
            bgAudioSource.clip = backgroundMusic;
            bgAudioSource.loop = true; // 배경음 반복 재생
            bgAudioSource.volume = 0.1f; // 배경음 볼륨 (1/5로 설정)
            bgAudioSource.Play();
        }

        ShowSlide();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTyping) // 마우스 클릭 & 타이핑 중 아님
        {
            NextSlide();
        }
    }

    void ShowSlide()
    {
        // currentSlideIndex가 배열 범위를 초과하지 않도록 체크
        if (currentSlideIndex < storyTexts.Length &&
            (currentSlideIndex < storyImages.Length || storyImages[currentSlideIndex] == null))
        {
            if (storyImages[currentSlideIndex] != null)
            {
                // 이미지가 있는 경우
                storyImage.gameObject.SetActive(true);
                storyImage.sprite = storyImages[currentSlideIndex];

                // 텍스트를 이미지 아래로 배치
                storyText.rectTransform.anchorMin = new Vector2(0.5f, 0.0f);
                storyText.rectTransform.anchorMax = new Vector2(0.5f, 0.0f);
                storyText.rectTransform.anchoredPosition = new Vector2(0, 100);
            }
            else
            {
                // 이미지가 없는 경우
                storyImage.gameObject.SetActive(false);

                // 텍스트를 화면 중앙에 배치
                storyText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                storyText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                storyText.rectTransform.anchoredPosition = Vector2.zero;
            }

            // 슬라이드 오디오 및 효과음 재생
            PlaySlideAudio();

            // 텍스트 출력
            StartCoroutine(TypeText(storyTexts[currentSlideIndex]));
        }
        else
        {
            // 배열 범위를 초과하면 다음 씬으로 전환
            LoadNextScene();
        }
    }

    void NextSlide()
    {
        currentSlideIndex++;
        ShowSlide();
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        storyText.text = ""; // 텍스트 초기화
        foreach (char letter in text.ToCharArray())
        {
            storyText.text += letter; // 글자 하나씩 추가
            yield return new WaitForSeconds(typingSpeed); // 타이핑 속도
        }
        isTyping = false; // 타이핑 종료
    }

    void PlaySlideAudio()
    {
        // 기존 효과음이 재생 중이라면 중지
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // 슬라이드 전환 효과음 재생
        if (currentSlideIndex < slideAudioClips.Length && slideAudioClips[currentSlideIndex] != null)
        {
            audioSource.clip = slideAudioClips[currentSlideIndex];
            audioSource.Play();
        }
    }

    void LoadNextScene()
    {
        // 다음 씬으로 전환 (씬 이름 수정 필요)
        UnityEngine.SceneManagement.SceneManager.LoadScene("aimLab");
    }
}
