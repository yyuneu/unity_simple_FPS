using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // �� ��ȯ�� ���� ���ӽ����̽�

public class LoginScreenManager : MonoBehaviour
{
    public InputField nicknameInput;   // �г��� InputField
    public InputField passwordInput;   // �н����� InputField
    public Text title;                 // Title �޽���
    public Text welcomeText;           // ȯ�� �޽���
    public Button loginButton;         // �α��� ��ư

    public AudioSource correctAnswerSource;  // ���� �� �Ҹ� AudioSource
    public AudioSource blip01Source;        // ���� �� �Ҹ� AudioSource

    public int nextSceneIndex = 1; // �̵��� �� �̸�

    void Start()
    {
        // �ʱ�ȭ
        title.gameObject.SetActive(true);
        welcomeText.gameObject.SetActive(false);

        // �α��� ��ư Ŭ�� �̺�Ʈ ���
        loginButton.onClick.AddListener(OnLoginButtonClicked);

        // AudioSource�� Play On Awake�� ���� �ʵ��� ���� (Play On Awake ����)
        correctAnswerSource.playOnAwake = false;
        blip01Source.playOnAwake = false;
    }

    void OnLoginButtonClicked()
    {
        // �г��Ӱ� �н����� ��������
        string nickname = nicknameInput.text;
        string password = passwordInput.text;

        // Title �����
        title.gameObject.SetActive(false);

        if (!string.IsNullOrEmpty(nickname) && !string.IsNullOrEmpty(password))
        {
            // �α��� ���� �� ȯ�� �޽��� ���
            welcomeText.gameObject.SetActive(true);
            welcomeText.text = $"{nickname}�� ȯ���մϴ�!";

            // �α��� ���� �� correct_answer3 �Ҹ� ���
            if (correctAnswerSource != null)
            {
                correctAnswerSource.Play();  // ���� Ŭ���� ���
            }

            // ���� �ð� �� ���ο� ������ ��ȯ
            Invoke("LoadNextScene", 1.0f);  // 1�� �� �� ��ȯ (�ʿ信 ���� �ð� ����)
        }
        else
        {
            // �α��� ������ ������� ��� ���� �޽��� ���
            welcomeText.gameObject.SetActive(true);
            welcomeText.text = "�ٽ� �Է����ּ���.";

            // �α��� ���� �� blip01 �Ҹ� ���
            if (blip01Source != null)
            {
                blip01Source.Play();  // ���� Ŭ���� ���
            }
        }
    }

    // ���ο� ������ ��ȯ�ϴ� �޼���
    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);  // �� ��ȯ
    }
}
