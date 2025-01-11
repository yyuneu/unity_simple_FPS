using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeOnClick : MonoBehaviour
{
    public int nextSceneIndex; // �̵��� �� ��ȣ
    private AudioSource audioSource; // ���� AudioSource ����

    void Start()
    {
        // ���� GameObject�� AudioSource ��������
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.loop = true; // �ݺ� ���
            audioSource.Play(); // ������� ���
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ȭ�� Ŭ�� ����
        {
            SceneManager.LoadScene(nextSceneIndex); // �� ��ȣ�� ��ȯ
        }
    }
}