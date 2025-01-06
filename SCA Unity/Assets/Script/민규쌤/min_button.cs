using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class min_button : MonoBehaviour
{
    public void close()
    {
        Yun_button.Reset();
        min_ui.UI_opened = false;
        min_ui.delete = true;
        min_spawn.spawned = false;
        min_ui.ShopUI.SetActive(false);
    }
}
