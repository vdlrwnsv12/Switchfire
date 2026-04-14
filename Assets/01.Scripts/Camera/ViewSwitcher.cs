using UnityEngine;

public class ViewSwitcher : MonoBehaviour
{
    public enum ViewMode { TopDown, FirstPerson }
    public ViewMode Current { get; private set; }

    public Camera topDownCamera;
    public Camera fpsCamera;
    public Renderer bodyRenderer;

    public TopDownAimer topDownAimer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            SwitchView();
    }

    void SwitchView()
    {
        if (Current == ViewMode.TopDown) EnterFPS();
        else EnterTopDown();
    }

    void EnterFPS()
    {
        topDownCamera.enabled = false;
        fpsCamera.enabled = true;
        bodyRenderer.enabled = false;
        topDownAimer.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Current = ViewMode.FirstPerson;
    }

    void EnterTopDown()
    {
        fpsCamera.enabled = false;
        topDownCamera.enabled = true;
        bodyRenderer.enabled = true;
        topDownAimer.enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Current = ViewMode.TopDown;
    }
}
