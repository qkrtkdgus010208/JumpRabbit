using UnityEngine;

public class Effect_Scripts : MonoBehaviour
{
    public void Activate_Func(Vector2 _pos)
    {
        this.transform.position = _pos; // ����Ʈ ��ġ ����
    }

    public void CallAni_Func()
    {
        GameObject.Destroy(this.gameObject); // ����Ʈ ����
    }
}
