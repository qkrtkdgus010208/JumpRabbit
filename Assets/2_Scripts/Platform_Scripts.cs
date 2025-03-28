using UnityEngine;

public class Platform_Scripts : MonoBehaviour
{
    [SerializeField] private BoxCollider2D col = null; // �÷����� BoxCollider2D ������Ʈ
    [SerializeField] private SpriteRenderer srdr = null; // �÷����� SpriteRenderer ������Ʈ
    [SerializeField] private Animation anim = null; // �÷����� Animation ������Ʈ
    [SerializeField] private int score; // �÷����� ����

    // �÷��� x ������ ������ ��ȯ�ϴ� ������Ƽ
    public float GetHalfSizeX => this.col.size.x * 0.5f;

    // �÷��� ��ġ�� �����ϴ� �Լ�
    public void Activate_Func(Vector2 _pos, bool _isFirst)
    {
        this.transform.position = _pos; // �÷��� ��ġ ����

        // ù ��° �÷����� �ƴϰ�, ���� Ȯ���� ���� ������ ����
        if (!_isFirst && Random.value < DataBase_Manager.Instance.itemSpawnPer)
        {
            Item_Scripts _baseItemClass = DataBase_Manager.Instance.baseItemClass; // �⺻ ������ Ŭ����
            Item_Scripts _itemClass = GameObject.Instantiate<Item_Scripts>(_baseItemClass); // ������ �ν��Ͻ� ����
            _itemClass.Activate_Func(this.transform.position, this.GetHalfSizeX); // ������ Ȱ��ȭ
        }
    }

    // �ش� �÷����� ������ŭ�� �߰��϶�� �˸��� �Լ�
    public void Onlanding_Func()
    {
        ScoreSystem_Manager.Instance.AddScore_Func(this.score, this.transform.position); // ���� �߰�

        this.anim.Play(); // �ִϸ��̼� ���
    }
}