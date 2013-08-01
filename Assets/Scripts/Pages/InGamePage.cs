using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InGamePage : BPage {
    FContainer gameNodes;
    Car c;
    SimpleTimer st;
    FLabel timer;
    FLabel bestTime;
    long bestTimeTicks = long.MaxValue;
    FSprite lapCollider;
    
    override public void Start() {
        ListenForUpdate(HandleUpdate);
        
        st = new SimpleTimer();
        st.Start();
        
        gameNodes = new FContainer();
        Futile.atlasManager.LoadImage("car");
        
        WEHTiled hurr = new WEHTiled("track");
        gameNodes.AddChild(hurr.DrawLayer(0));
        FContainer coll = hurr.DrawLayer(1);
        gameNodes.AddChild(coll);
        
        c = new Car(Futile.atlasManager.GetElementWithName("car"), coll);
        c.x = Futile.screen.width;
        c.y = -Futile.screen.height;
        c.rotation = 90;
        gameNodes.AddChild(c);
        
        lapCollider = new FSprite(Futile.whiteElement);
        lapCollider.scaleY = 5;
        lapCollider.SetPosition(1220, -832);
        lapCollider.alpha = 0;
        gameNodes.AddChild(lapCollider);
        
        RXWatcher.Watch(gameNodes);
        RXWatcher.Watch(lapCollider);
        
        this.AddChild(gameNodes);
        SetupUI();
    }
    
    void SetupUI(){
        FStage uiStage = new FStage("uiStage");
        timer = new FLabel("Abstract", st.TimeStamp);
        bestTime = new FLabel("Abstract", "Best:\n"+st.TimeStamp);
        timer.SetAnchor(0,1);
        timer.scale = 3;
        bestTime.SetAnchor(0,1);
        bestTime.x = Futile.screen.width - bestTime.textRect.width*3;
        bestTime.scale = 3;
        uiStage.AddChild(timer);
        uiStage.AddChild(bestTime);
        Futile.AddStage(uiStage);
    }

    void HandleUpdate() {
        if(lapCollider.GetTextureRectRelativeToContainer().CheckIntersect(c.GetTextureRectRelativeToContainer()) && st.s.ElapsedMilliseconds > 3000){
            if(st.s.ElapsedTicks < bestTimeTicks){
                bestTime.text = "Best:\n"+st.TimeStamp;
                bestTimeTicks = st.s.ElapsedTicks;
                bestTime.alpha = 0;
                Go.to(bestTime, 0.3f, new TweenConfig().floatProp("alpha", 1.0f).setIterations(3));
            }
            st.Restart();
            st.Start();
        }
        
        timer.text = st.TimeStamp;
        this.gameNodes.SetPosition(-c.GetPosition()/2);
    }
}
