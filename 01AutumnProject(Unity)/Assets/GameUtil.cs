using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtil {

    public static KeyCode GetKeyCodeByLineNum (int lineNum) {
        switch (lineNum) {
            case 0:
                return KeyCode.UpArrow;
            case 1:
                return KeyCode.LeftArrow;
            case 2:
                return KeyCode.DownArrow;
            case 3:
                return KeyCode.RightArrow;
            default:
                return KeyCode.None;
        }
    }
}

