﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base
{
    // Start is called before the first frame update
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }

}
