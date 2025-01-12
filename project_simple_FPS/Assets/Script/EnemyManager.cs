using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int totalEnemiesToKill = 15; // ���� ������ �Ѿ�� ���� óġ ��
    [SerializeField] private TextMeshProUGUI enemyKillCountText; // TextMeshPro UI �ؽ�Ʈ ����
    private int enemiesKilled = 0; // ���� óġ ��

    public static EnemyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� �������� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // TextMeshPro ���� Ȯ�� �� �ʱ�ȭ
        if (enemyKillCountText == null)
        {
            enemyKillCountText = GameObject.Find("EnemyKillCountText")?.GetComponent<TextMeshProUGUI>();
            if (enemyKillCountText == null)
            {
                Debug.LogError("EnemyKillCountText is not assigned in the Inspector or cannot be found in the scene.");
            }
        }

        // UI �ʱ�ȭ
        UpdateKillCountUI();
    }


    public void EnemyKilled()
    {
        enemiesKilled++;
        Debug.Log($"Enemy killed! Total enemies killed: {enemiesKilled}");
        UpdateKillCountUI();

        // óġ ���� ��ǥ�� �����ϸ� ���� �� �ε�
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
        // ���� �� �ε� (Build Settings���� �� �ε����� ���)
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
