using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCursor : MonoBehaviour
{
    public bool showCursor = false;
    // Start is called before the first frame update
    void Start()
    {
        if (showCursor) Show();
        else Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Show()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Hide()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
