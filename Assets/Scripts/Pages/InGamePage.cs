using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InGamePage : BPage {
    FContainer gameNodes;
    Car c;
    SimpleTimer st;
    FLabel timer;
    override public void Start() {
        ListenForUpdate(HandleUpdate);
        
        st = new SimpleTimer();
        st.Start();
        
        gameNodes = new FContainer();
        WEHTiled hurr = new WEHTiled("track");
        Futile.atlasManager.LoadImage("car");
        
        gameNodes.AddChild(hurr.DrawLayer(0));
        FContainer coll = hurr.DrawLayer(1);
        gameNodes.AddChild(coll);
        c = new Car(Futile.atlasManager.GetElementWithName("car"), coll);
        c.x = Futile.screen.width;
        c.y = -Futile.screen.height;
        gameNodes.AddChild(c);
        RXWatcher.Watch(gameNodes);
        RXWatcher.Watch(c);
        this.AddChild(gameNodes);
        SetupUI();
    }
    
    void SetupUI(){
        FStage uiStage = new FStage("uiStage");
        timer = new FLabel("Abstract", st.TimeStamp);
        timer.SetAnchor(0,1);
        timer.scale = 3;
        uiStage.AddChild(timer);
        Futile.AddStage(uiStage);
    }

    void HandleUpdate() {
        timer.text = st.TimeStamp;
        this.gameNodes.SetPosition(-c.GetPosition()/2);
    }
}
