using UnityEngine;

public class Item_Scripts : MonoBehaviour
{
    // ������ Ȱ��ȭ �Լ�
    public void Activate_Func(Vector2 _pos, float _rangeX)
    {
        // _rangeX ���� ������ ������ x ��ġ�� ����ϰ�, y ��ġ�� _pos.y + 0.85f�� ����
        float _x = Random.Range(-_rangeX, _rangeX) + _pos.x;
        this.transform.position = new Vector2(_x, _pos.y + 0.85f); // ������ ��ġ ����
    }

    // �浹 ó�� �Լ�
    private void OnTriggerEnter2D(Collider2D _col)
    {
        // �浹�� ��ü�� Player_Scripts ������Ʈ�� ������ �ִ��� Ȯ��
        if (_col.transform.TryGetComponent(out Player_Scripts _playerClass))
        {
            // ���ʽ� ����
            float _itemBonus = DataBase_Manager.Instance.itemBonus; // ������ ���ʽ� �� ��������
            Vector2 _thisPos = this.transform.position; // ���� ������ ��ġ
            ScoreSystem_Manager.Instance.AddBonus_Func(_itemBonus, _thisPos, false); // ���ʽ� �߰� �Լ� ȣ��

            // ������ ��ü �ı�
            GameObject.Destroy(this.gameObject);
        }
    }
}
