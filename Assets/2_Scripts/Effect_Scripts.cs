using UnityEngine;

public class Effect_Scripts : MonoBehaviour
{
    public void Activate_Func(Vector2 _pos)
    {
        this.transform.position = _pos; // 이펙트 위치 설정
    }

    public void CallAni_Func()
    {
        GameObject.Destroy(this.gameObject); // 이펙트 제거
    }
}
