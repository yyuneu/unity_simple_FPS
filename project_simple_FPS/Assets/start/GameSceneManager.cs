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
<<<<<<< HEAD
<<<<<<< HEAD
     // ��� ���� �г�

    // �� ��ư��
    
=======
=======
>>>>>>> parent of 62c7dc3 (login)
    public GameObject modeSelectionPanel; // ��� ���� �г�

    // �� ��ư��
    public Button modeBackButton;    // ��� ���ÿ��� �ڷΰ��� ��ư
<<<<<<< HEAD
>>>>>>> parent of 62c7dc3 (login)
=======
>>>>>>> parent of 62c7dc3 (login)

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
<<<<<<< HEAD
<<<<<<< HEAD
        
        
=======
        modeSelectionPanel.SetActive(false); // ��� ���� �г��� �����
        modeBackButton.gameObject.SetActive(false); // ��� ���� �гο����� �ڷΰ��� ��ư �����
>>>>>>> parent of 62c7dc3 (login)
=======
        modeSelectionPanel.SetActive(false); // ��� ���� �г��� �����
        modeBackButton.gameObject.SetActive(false); // ��� ���� �гο����� �ڷΰ��� ��ư �����
>>>>>>> parent of 62c7dc3 (login)

        // ��ư Ŭ�� �̺�Ʈ ���
        modeButton.onClick.AddListener(OnModeButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);

        // �� ��ư Ŭ�� �̺�Ʈ ���
<<<<<<< HEAD
<<<<<<< HEAD
        
=======
        modeBackButton.onClick.AddListener(OnModeBackButtonClicked);
>>>>>>> parent of 62c7dc3 (login)
=======
        modeBackButton.onClick.AddListener(OnModeBackButtonClicked);
>>>>>>> parent of 62c7dc3 (login)
    }

    // ��� ���� ��ư Ŭ�� ��
    void OnModeButtonClicked()
    {
        PlayButtonClickSound();
        mainMenuPanel.SetActive(false);  // ���� �޴� �����
<<<<<<< HEAD
<<<<<<< HEAD
        SceneManager.LoadScene(2);

=======
        modeSelectionPanel.SetActive(true);  // ��� ���� �г� ���̱�
        modeBackButton.gameObject.SetActive(true); // ��� ���� �гο����� �ڷΰ��� ��ư ���̱�
>>>>>>> parent of 62c7dc3 (login)
=======
        modeSelectionPanel.SetActive(true);  // ��� ���� �г� ���̱�
        modeBackButton.gameObject.SetActive(true); // ��� ���� �гο����� �ڷΰ��� ��ư ���̱�
>>>>>>> parent of 62c7dc3 (login)
    }

    // ������ ��ư Ŭ�� ��
    void OnExitButtonClicked()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene(0); // �α��� ȭ������ ���ư��� ����
    }

    // ��� ���� �г��� �ڷ� ���� ��ư Ŭ�� ��
<<<<<<< HEAD
<<<<<<< HEAD
   
=======
=======
>>>>>>> parent of 62c7dc3 (login)
    void OnModeBackButtonClicked()
    {
        PlayButtonClickSound();
        modeSelectionPanel.SetActive(false);  // ��� ���� �г� �����
        mainMenuPanel.SetActive(true);        // ���� �޴� �ٽ� ���̱�
        modeBackButton.gameObject.SetActive(false); // ��� ���� �гο��� �ڷΰ��� ��ư �����
    }
<<<<<<< HEAD
>>>>>>> parent of 62c7dc3 (login)
=======
>>>>>>> parent of 62c7dc3 (login)

    // ��ư Ŭ�� �Ҹ� ���
    void PlayButtonClickSound()
    {
        if (buttonClickSoundSource != null && buttonClickClip != null)
        {
            buttonClickSoundSource.PlayOneShot(buttonClickClip);  // ��ư Ŭ�� �Ҹ� ���
        }
    }
}
