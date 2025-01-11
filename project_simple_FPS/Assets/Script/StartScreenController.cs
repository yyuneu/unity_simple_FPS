using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{
    void Update()
    {
        // ��� Ŭ������ �� ��ȯ
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // ���� ������ ��ȯ (�� �̸� ���� �ʿ�)
        SceneManager.LoadScene("StoryScene");
    }
}
