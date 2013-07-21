using UnityEngine;
using System.Collections;

public class InGamePage : BPage {

    override public void Start() {
        ListenForUpdate(HandleUpdate);
    }

    void HandleUpdate() {
    }
}
