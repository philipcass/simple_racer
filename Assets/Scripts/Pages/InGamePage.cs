using UnityEngine;
using System.Collections;

public class InGamePage : BPage {

    override public void Start() {
    }

    override public void HandleAddedToStage() {
        Futile.instance.SignalUpdate += HandleUpdate;
        base.HandleAddedToStage();  
    }

    override public void HandleRemovedFromStage() {
        Futile.instance.SignalUpdate -= HandleUpdate;
        base.HandleRemovedFromStage();  
    }
 
    void HandleUpdate() {
    }
}
