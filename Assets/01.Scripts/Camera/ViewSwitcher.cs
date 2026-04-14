using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSwitcher : MonoBehaviour
{
    public enum ViewMode { TopDown, FirstPerson }
    public ViewMode Current { get; private set; }

    public GameObject topDownCamera;
    public GameObject fpsCamera;

    public Renderer bodyRenderer;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            SwitchView();
        }
    }

    void SwitchView()
    {
        if(Current == ViewMode.TopDown)
        {
            EnterFPS();
        } else
        {
            EnterTopDown();
        }
    }

    void EnterFPS()
    {
        topDownCamera.SetActive(false);
        fpsCamera.SetActive(true);
        bodyRenderer.enabled = false;
        //에임이 있으면 에임도 꺼주기
        //fpsLook.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Current = ViewMode.FirstPerson;
    }

    void EnterTopDown()
    {
        fpsCamera.SetActive(false);
        topDownCamera.SetActive(true);
        Current = ViewMode.TopDown;
        bodyRenderer.enabled = true;
        //에임이 있으면 에임도 켜주기
        //fpsLook.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Current = ViewMode.TopDown;
    }
}
