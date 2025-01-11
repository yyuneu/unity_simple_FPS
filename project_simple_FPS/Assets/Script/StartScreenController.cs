using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{
    void Update()
    {
        // 배경 클릭으로 씬 전환
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // 다음 씬으로 전환 (씬 이름 변경 필요)
        SceneManager.LoadScene("StoryScene");
    }
}
