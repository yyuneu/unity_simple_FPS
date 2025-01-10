using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    // 메인 메뉴 버튼
    public Button modeButton;
    public Button exitButton;

    // 패널들
    public GameObject mainMenuPanel;     // 메인 메뉴
<<<<<<< HEAD
<<<<<<< HEAD
     // 모드 선택 패널

    // 백 버튼들
    
=======
=======
>>>>>>> parent of 62c7dc3 (login)
    public GameObject modeSelectionPanel; // 모드 선택 패널

    // 백 버튼들
    public Button modeBackButton;    // 모드 선택에서 뒤로가기 버튼
<<<<<<< HEAD
>>>>>>> parent of 62c7dc3 (login)
=======
>>>>>>> parent of 62c7dc3 (login)

    // 오디오 관련 변수
    public AudioSource backgroundMusicSource;
    public AudioSource buttonClickSoundSource;
    public AudioClip backgroundMusicClip;
    public AudioClip buttonClickClip;

    void Start()
    {
        // 배경 음악 설정
        if (backgroundMusicSource != null && backgroundMusicClip != null)
        {
            if (!backgroundMusicSource.isPlaying)
            {
                backgroundMusicSource.loop = true;
                backgroundMusicSource.clip = backgroundMusicClip;
                backgroundMusicSource.Play();
            }
        }

        // 초기 설정
        mainMenuPanel.SetActive(true);   // 메인 메뉴는 처음에 보이게
<<<<<<< HEAD
<<<<<<< HEAD
        
        
=======
        modeSelectionPanel.SetActive(false); // 모드 선택 패널은 숨기기
        modeBackButton.gameObject.SetActive(false); // 모드 선택 패널에서는 뒤로가기 버튼 숨기기
>>>>>>> parent of 62c7dc3 (login)
=======
        modeSelectionPanel.SetActive(false); // 모드 선택 패널은 숨기기
        modeBackButton.gameObject.SetActive(false); // 모드 선택 패널에서는 뒤로가기 버튼 숨기기
>>>>>>> parent of 62c7dc3 (login)

        // 버튼 클릭 이벤트 등록
        modeButton.onClick.AddListener(OnModeButtonClicked);
        exitButton.onClick.AddListener(OnExitButtonClicked);

        // 백 버튼 클릭 이벤트 등록
<<<<<<< HEAD
<<<<<<< HEAD
        
=======
        modeBackButton.onClick.AddListener(OnModeBackButtonClicked);
>>>>>>> parent of 62c7dc3 (login)
=======
        modeBackButton.onClick.AddListener(OnModeBackButtonClicked);
>>>>>>> parent of 62c7dc3 (login)
    }

    // 모드 설정 버튼 클릭 시
    void OnModeButtonClicked()
    {
        PlayButtonClickSound();
        mainMenuPanel.SetActive(false);  // 메인 메뉴 숨기기
<<<<<<< HEAD
<<<<<<< HEAD
        SceneManager.LoadScene(2);

=======
        modeSelectionPanel.SetActive(true);  // 모드 선택 패널 보이기
        modeBackButton.gameObject.SetActive(true); // 모드 선택 패널에서만 뒤로가기 버튼 보이기
>>>>>>> parent of 62c7dc3 (login)
=======
        modeSelectionPanel.SetActive(true);  // 모드 선택 패널 보이기
        modeBackButton.gameObject.SetActive(true); // 모드 선택 패널에서만 뒤로가기 버튼 보이기
>>>>>>> parent of 62c7dc3 (login)
    }

    // 나가기 버튼 클릭 시
    void OnExitButtonClicked()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene(0); // 로그인 화면으로 돌아가게 설정
    }

    // 모드 선택 패널의 뒤로 가기 버튼 클릭 시
<<<<<<< HEAD
<<<<<<< HEAD
   
=======
=======
>>>>>>> parent of 62c7dc3 (login)
    void OnModeBackButtonClicked()
    {
        PlayButtonClickSound();
        modeSelectionPanel.SetActive(false);  // 모드 설정 패널 숨기기
        mainMenuPanel.SetActive(true);        // 메인 메뉴 다시 보이기
        modeBackButton.gameObject.SetActive(false); // 모드 선택 패널에서 뒤로가기 버튼 숨기기
    }
<<<<<<< HEAD
>>>>>>> parent of 62c7dc3 (login)
=======
>>>>>>> parent of 62c7dc3 (login)

    // 버튼 클릭 소리 재생
    void PlayButtonClickSound()
    {
        if (buttonClickSoundSource != null && buttonClickClip != null)
        {
            buttonClickSoundSource.PlayOneShot(buttonClickClip);  // 버튼 클릭 소리 재생
        }
    }
}
