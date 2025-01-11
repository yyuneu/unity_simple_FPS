using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class StoryController : MonoBehaviour
{
    public Image storyImage; // �߾� �̹���
    public TextMeshProUGUI storyText; // �ؽ�Ʈ
    public Sprite[] storyImages; // �����̵� �̹��� �迭 (null ���)
    public string[] storyTexts; // �����̵� �ؽ�Ʈ �迭
    public AudioClip[] slideAudioClips; // �����̵庰 ����� �迭 (null ���)
    public AudioClip backgroundMusic; // ��ü �������
    public float typingSpeed = 0.05f; // �ؽ�Ʈ Ÿ���� �ӵ�

    private int currentSlideIndex = 0; // ���� �����̵� �ε���
    private bool isTyping = false; // Ÿ���� �� ����
    private AudioSource audioSource; // �����̵� ����� �ҽ�
    private AudioSource bgAudioSource; // ������� �ҽ�

    void Start()
    {
        // AudioSource �߰� �� �ʱ�ȭ
        audioSource = gameObject.AddComponent<AudioSource>();
        bgAudioSource = gameObject.AddComponent<AudioSource>();

        // ������� ���� �� ���
        if (backgroundMusic != null)
        {
            bgAudioSource.clip = backgroundMusic;
            bgAudioSource.loop = true; // ����� �ݺ� ���
            bgAudioSource.volume = 0.1f; // ����� ���� (1/5�� ����)
            bgAudioSource.Play();
        }

        ShowSlide();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isTyping) // ���콺 Ŭ�� & Ÿ���� �� �ƴ�
        {
            NextSlide();
        }
    }

    void ShowSlide()
    {
        // currentSlideIndex�� �迭 ������ �ʰ����� �ʵ��� üũ
        if (currentSlideIndex < storyTexts.Length &&
            (currentSlideIndex < storyImages.Length || storyImages[currentSlideIndex] == null))
        {
            if (storyImages[currentSlideIndex] != null)
            {
                // �̹����� �ִ� ���
                storyImage.gameObject.SetActive(true);
                storyImage.sprite = storyImages[currentSlideIndex];

                // �ؽ�Ʈ�� �̹��� �Ʒ��� ��ġ
                storyText.rectTransform.anchorMin = new Vector2(0.5f, 0.0f);
                storyText.rectTransform.anchorMax = new Vector2(0.5f, 0.0f);
                storyText.rectTransform.anchoredPosition = new Vector2(0, 100);
            }
            else
            {
                // �̹����� ���� ���
                storyImage.gameObject.SetActive(false);

                // �ؽ�Ʈ�� ȭ�� �߾ӿ� ��ġ
                storyText.rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                storyText.rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                storyText.rectTransform.anchoredPosition = Vector2.zero;
            }

            // �����̵� ����� �� ȿ���� ���
            PlaySlideAudio();

            // �ؽ�Ʈ ���
            StartCoroutine(TypeText(storyTexts[currentSlideIndex]));
        }
        else
        {
            // �迭 ������ �ʰ��ϸ� ���� ������ ��ȯ
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
        storyText.text = ""; // �ؽ�Ʈ �ʱ�ȭ
        foreach (char letter in text.ToCharArray())
        {
            storyText.text += letter; // ���� �ϳ��� �߰�
            yield return new WaitForSeconds(typingSpeed); // Ÿ���� �ӵ�
        }
        isTyping = false; // Ÿ���� ����
    }

    void PlaySlideAudio()
    {
        // ���� ȿ������ ��� ���̶�� ����
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // �����̵� ��ȯ ȿ���� ���
        if (currentSlideIndex < slideAudioClips.Length && slideAudioClips[currentSlideIndex] != null)
        {
            audioSource.clip = slideAudioClips[currentSlideIndex];
            audioSource.Play();
        }
    }

    void LoadNextScene()
    {
        // ���� ������ ��ȯ (�� �̸� ���� �ʿ�)
        UnityEngine.SceneManagement.SceneManager.LoadScene("aimLab");
    }
}
