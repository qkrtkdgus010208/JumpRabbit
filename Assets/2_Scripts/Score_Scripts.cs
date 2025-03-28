using TMPro;
using UnityEngine;

public class Score_Scripts : MonoBehaviour
{
    [SerializeField] private TextMeshPro tmp = null; // ������ ǥ���ϴ� TextMeshPro ������Ʈ

    // ���� Ȱ��ȭ �Լ�
    public void Activate_Func(string _str, Color _color)
    {
        this.tmp.text = _str; // ���� �ؽ�Ʈ ����
        this.tmp.color = _color; // ���� ���� ����
    }

    // ���� ��Ȱ��ȭ �Լ�
    public void Deactivate_Func()
    {
        GameObject.Destroy(this.gameObject); // ���� ��ü �ı�
    }

    // �ִϸ��̼� ���� �� ȣ��Ǵ� �Լ� (���� ��Ȱ��ȭ �Լ� ȣ��)
    public void CallAni_Deactivate_Func()
    {
        this.Deactivate_Func(); // ���� ��Ȱ��ȭ �Լ� ȣ��
    }
}