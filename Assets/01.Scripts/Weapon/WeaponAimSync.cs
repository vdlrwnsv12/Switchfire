using UnityEngine;

// AimPivot이 시점에 따라 올바른 방향을 바라보도록 동기화
public class WeaponAimSync : MonoBehaviour
{
    public ViewSwitcher viewSwitcher;
    public Transform fpsCamera;

    void Update()
    {
        if (viewSwitcher.Current == ViewSwitcher.ViewMode.FirstPerson)
            transform.rotation = fpsCamera.rotation;
    }
}
