using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro 네임스페이스 추가

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int totalEnemiesToKill = 15; // 다음 씬으로 넘어가기 위한 처치 수
    [SerializeField] private TextMeshProUGUI enemyKillCountText; // TextMeshPro UI 텍스트 연결
    private int enemiesKilled = 0; // 현재 처치 수

    public static EnemyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 삭제되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // TextMeshPro 연결 확인 및 초기화
        if (enemyKillCountText == null)
        {
            enemyKillCountText = GameObject.Find("EnemyKillCountText")?.GetComponent<TextMeshProUGUI>();
            if (enemyKillCountText == null)
            {
                Debug.LogError("EnemyKillCountText is not assigned in the Inspector or cannot be found in the scene.");
            }
        }

        // UI 초기화
        UpdateKillCountUI();
    }


    public void EnemyKilled()
    {
        enemiesKilled++;
        Debug.Log($"Enemy killed! Total enemies killed: {enemiesKilled}");
        UpdateKillCountUI();

        // 처치 수가 목표에 도달하면 다음 씬 로드
        if (enemiesKilled >= totalEnemiesToKill)
        {
            LoadNextScene();
        }
    }

    private void UpdateKillCountUI()
    {
        if (enemyKillCountText != null)
        {
            enemyKillCountText.text = $"Enemies Killed: {enemiesKilled}";
            Debug.Log($"UI Updated: {enemyKillCountText.text}");
        }
        else
        {
            Debug.LogError("enemyKillCountText is not assigned!");
        }
    }


    private void LoadNextScene()
    {
        // 다음 씬 로드 (Build Settings에서 씬 인덱스를 사용)
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No more scenes to load. Check your Build Settings.");
        }
    }
}
