using UnityEngine;
using UnityEngine.UI;

public class AimSettingsManager : MonoBehaviour
{
    public Toggle circularToggle;  // ���� ���� Toggle
    public Toggle squareToggle;    // �簢�� ���� Toggle
    public Toggle crossToggle;     // ������ ���� Toggle

    void Start()
    {
        // �� Toggle�� �̺�Ʈ ������ �߰�
        circularToggle.onValueChanged.AddListener(OnAimShapeChanged);
        squareToggle.onValueChanged.AddListener(OnAimShapeChanged);
        crossToggle.onValueChanged.AddListener(OnAimShapeChanged);
    }

    // ���� ��� ���� �� ȣ��Ǵ� �޼���
    void OnAimShapeChanged(bool isOn)
    {
        // ���õ� ���� ��翡 ���� ó��
        if (circularToggle.isOn)
        {
            Debug.Log("���� ���� ����");
            // ���� ���� ó�� �ڵ�
        }
        else if (squareToggle.isOn)
        {
            Debug.Log("�簢�� ���� ����");
            // �簢�� ���� ó�� �ڵ�
        }
        else if (crossToggle.isOn)
        {
            Debug.Log("������ ���� ����");
            // ������ ���� ó�� �ڵ�
        }
    }
}
