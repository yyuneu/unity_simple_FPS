using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // 씬 전환을 위한 네임스페이스

public class LoginScreenManager : MonoBehaviour
{
    public InputField nicknameInput;   // 닉네임 InputField
    public InputField passwordInput;   // 패스워드 InputField
    public Text title;                 // Title 메시지
    public Text welcomeText;           // 환영 메시지
    public Button loginButton;         // 로그인 버튼

    public AudioSource correctAnswerSource;  // 성공 시 소리 AudioSource
    public AudioSource blip01Source;        // 실패 시 소리 AudioSource

    public int nextSceneIndex = 1; // 이동할 씬 이름

    void Start()
    {
        // 초기화
        title.gameObject.SetActive(true);
        welcomeText.gameObject.SetActive(false);

        // 로그인 버튼 클릭 이벤트 등록
        loginButton.onClick.AddListener(OnLoginButtonClicked);

        // AudioSource가 Play On Awake를 하지 않도록 설정 (Play On Awake 해제)
        correctAnswerSource.playOnAwake = false;
        blip01Source.playOnAwake = false;
    }

    void OnLoginButtonClicked()
    {
        // 닉네임과 패스워드 가져오기
        string nickname = nicknameInput.text;
        string password = passwordInput.text;

        // Title 숨기기
        title.gameObject.SetActive(false);

        if (!string.IsNullOrEmpty(nickname) && !string.IsNullOrEmpty(password))
        {
            // 로그인 성공 시 환영 메시지 출력
            welcomeText.gameObject.SetActive(true);
            welcomeText.text = $"{nickname}님 환영합니다!";

            // 로그인 성공 시 correct_answer3 소리 재생
            if (correctAnswerSource != null)
            {
                correctAnswerSource.Play();  // 성공 클립을 재생
            }

            // 일정 시간 후 새로운 씬으로 전환
            Invoke("LoadNextScene", 1.0f);  // 1초 후 씬 전환 (필요에 따라 시간 조정)
        }
        else
        {
            // 로그인 정보가 비어있을 경우 실패 메시지 출력
            welcomeText.gameObject.SetActive(true);
            welcomeText.text = "다시 입력해주세요.";

            // 로그인 실패 시 blip01 소리 재생
            if (blip01Source != null)
            {
                blip01Source.Play();  // 실패 클립을 재생
            }
        }
    }

    // 새로운 씬으로 전환하는 메서드
    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);  // 씬 전환
    }
}
