using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class min_button : MonoBehaviour
{
    public void close()
    {
        Yun_button.Reset();
        min_inter.delete = true;
        min_ui.Sopen = false;
    }
}
