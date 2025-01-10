using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    // ���� �޴� ��ư
    public Button modeButton;
    public Button exitButton;

    // �гε�
    public GameObject mainMenuPanel;     // ���� �޴�
     // ��� ���� �г�

    // �� ��ư��
    

    // ����� ���� ����
    public AudioSource backgroundMusicSource;
    public AudioSource buttonClickSoundSource;
    public AudioClip backgroundMusicClip;
    public AudioClip buttonClickClip;

    void Start()
    {
        // ��� ���� ����
        if (backgroundMusicSource != null && backgroundMusicClip != null)
        {
            if (!backgroundMusicSource.isPlaying)
            {
                backgroundMusicSource.loop = true;
                backgroundMusicSource.clip = backgroundMusicClip;
                backgroundMusicSource.Play();
            }
        }

        // �ʱ� ����
        mainMenuPanel.SetActive(true);   // ���� �޴��� ó���� ���̰�
        
        

        // ��ư Ŭ�� �̺�Ʈ ���
        modeButton.onClick.AddListener(OnModeButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);

        // �� ��ư Ŭ�� �̺�Ʈ ���
        
    }

    // ��� ���� ��ư Ŭ�� ��
    void OnModeButtonClicked()
    {
        PlayButtonClickSound();
        mainMenuPanel.SetActive(false);  // ���� �޴� �����
        SceneManager.LoadScene(2);

    }

    // ������ ��ư Ŭ�� ��
    void OnExitButtonClicked()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene(0); // �α��� ȭ������ ���ư��� ����
    }

    // ��� ���� �г��� �ڷ� ���� ��ư Ŭ�� ��
   

    // ��ư Ŭ�� �Ҹ� ���
    void PlayButtonClickSound()
    {
        if (buttonClickSoundSource != null && buttonClickClip != null)
        {
            buttonClickSoundSource.PlayOneShot(buttonClickClip);  // ��ư Ŭ�� �Ҹ� ���
        }
    }
}
