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
    public GameObject modeSelectionPanel; // ��� ���� �г�

    // �� ��ư��
    public Button modeBackButton;    // ��� ���ÿ��� �ڷΰ��� ��ư

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
        modeSelectionPanel.SetActive(false); // ��� ���� �г��� �����
        modeBackButton.gameObject.SetActive(false); // ��� ���� �гο����� �ڷΰ��� ��ư �����

        // ��ư Ŭ�� �̺�Ʈ ���
        modeButton.onClick.AddListener(OnModeButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);

        // �� ��ư Ŭ�� �̺�Ʈ ���
        modeBackButton.onClick.AddListener(OnModeBackButtonClicked);
    }

    // ��� ���� ��ư Ŭ�� ��
    void OnModeButtonClicked()
    {
        PlayButtonClickSound();
        mainMenuPanel.SetActive(false);  // ���� �޴� �����
        modeSelectionPanel.SetActive(true);  // ��� ���� �г� ���̱�
        modeBackButton.gameObject.SetActive(true); // ��� ���� �гο����� �ڷΰ��� ��ư ���̱�
    }

    // ������ ��ư Ŭ�� ��
    void OnExitButtonClicked()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene(0); // �α��� ȭ������ ���ư��� ����
    }

    // ��� ���� �г��� �ڷ� ���� ��ư Ŭ�� ��
    void OnModeBackButtonClicked()
    {
        PlayButtonClickSound();
        modeSelectionPanel.SetActive(false);  // ��� ���� �г� �����
        mainMenuPanel.SetActive(true);        // ���� �޴� �ٽ� ���̱�
        modeBackButton.gameObject.SetActive(false); // ��� ���� �гο��� �ڷΰ��� ��ư �����
    }

    // ��ư Ŭ�� �Ҹ� ���
    void PlayButtonClickSound()
    {
        if (buttonClickSoundSource != null && buttonClickClip != null)
        {
            buttonClickSoundSource.PlayOneShot(buttonClickClip);  // ��ư Ŭ�� �Ҹ� ���
        }
    }
}
