﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum WorldObject
    {
        Unknown,
        Player,
        Monster,
    }
    public enum State
    {
        Die,
        Moving,
        Idle,
        Channeling,
        Skill,
        Jumping,
        Falling,
    }
public enum Layer
    {
         Monster=8,
         Ground=9,
         Block=10,
    }
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
        GameClear
    }
    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
    public enum UIEvent
    {
        Click,
        Drag, 
    }
    public enum MouseEvent
    {
        Press,
        PointerDown,
        PointerUp,
        Click,
    }
    public enum CameraMode
    {
        QuarterView
    }
}
