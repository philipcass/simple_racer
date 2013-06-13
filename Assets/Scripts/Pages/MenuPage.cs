using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class MenuPage : BPage {
    
    List<FButton> menuElements = new List<FButton>();
    SfxrSynth synth = new SfxrSynth();
    
    Color tone1 = Color.black;
    Color tone2;
    
    override public void Start() {
        tone2 = GameUtils.HexToColor("00E850");
        FSprite leftSide = new FSprite(Futile.whiteElement);
        leftSide.color = tone1;
        leftSide.height = Futile.screen.height;
        leftSide.width = Futile.screen.width * 0.66f;
        
        FSprite rightSide = new FSprite(Futile.whiteElement);
        rightSide.color = tone2;
        rightSide.height = Futile.screen.height;
        rightSide.width = Futile.screen.width * 0.33f;
        rightSide.scaleX *= 2;
        rightSide.scaleY *= 2;
        
        
        rightSide.x += Futile.screen.halfWidth;
        rightSide.rotation += 22.5f;

        this.AddChild(leftSide);
        this.AddChild(rightSide);
        setupMenu();
        FSoundManager.Init();
        synth.SetParentTransform(GameObject.Find("FSoundManager").transform);
    }
 
    FSprite selector = new FSprite(Futile.whiteElement.name);
    public void setupMenu() {
        FContainer menuSelector = new FContainer();
        menuSelector.x = Futile.screen.width * 0.3f;
        menuSelector.y = -Futile.screen.height * 0.2f;
        
        selector.color = tone1;
        selector.height = 48;
        selector.width = Futile.screen.width*2;
        menuSelector.AddChild(selector);
        
        FButton toAdd;
        
        toAdd = addButton("Play");
        menuSelector.AddChild(toAdd);
        toAdd = addButton("Options");
        toAdd.y -= 48;
        menuSelector.AddChild(toAdd);
        toAdd = addButton("Exit");
        toAdd.y -= 96;
        menuSelector.AddChild(toAdd);

        this.AddChild(menuSelector);
        menuElements[0].label.color = tone2;
    }
    
    FButton addButton(string name) {
        FButton button = new FButton(Futile.whiteElement.name);
        button.AddLabel("Abstract", name, tone1);
        button.sprite.alpha = 0;
        button.scale *= 2.5f;
        menuElements.Add(button);
        return button;
    }
    
    override public void HandleAddedToStage() {
        Futile.instance.SignalUpdate += HandleUpdate;
        base.HandleAddedToStage();  
    }

    override public void HandleRemovedFromStage() {
        Futile.instance.SignalUpdate -= HandleUpdate;
        base.HandleRemovedFromStage();  
    }

    int menuIndex = 0;
    bool inputEnabled = true;

    void HandleUpdate() {
        synth.parameters.SetSettingsString("2,,0.166,,0.1806,0.4482,,,,,,,,,,,,,1,,,0.1,,0.5");

        if(true){
            if(Input.GetKeyUp(KeyCode.S) && menuIndex < menuElements.Count -1) {
                menuElements[menuIndex].label.color = tone1;
                menuIndex++;
                inputEnabled = false;
                Go.killAllTweensWithTarget(selector);
                Go.to(selector, 0.33f, new TweenConfig().setEaseType(EaseType.QuadOut).floatProp("y", menuElements[menuIndex].y).onComplete(hurr));
                synth.Play();
            } else if(Input.GetKeyUp(KeyCode.W) && menuIndex > 0) {
                menuElements[menuIndex].label.color = tone1;
                menuIndex--;
                inputEnabled = false;
                Go.killAllTweensWithTarget(selector);
                Go.to(selector, 0.33f, new TweenConfig().setEaseType(EaseType.QuadOut).floatProp("y", menuElements[menuIndex].y).onComplete(hurr));
                synth.Play();
            }
            
            if(Input.GetKeyUp(KeyCode.Space)) {
                inputEnabled = false;
                Go.to(selector, 1, new TweenConfig().setEaseType(EaseType.QuadOut).colorProp("color", tone2).floatProp("height", Futile.screen.height*2).onComplete(LoadGamePage));
                menuElements.ForEach(delegate(FButton b)
                {
                    b.label.color = tone2;
                });
                synth.parameters.SetSettingsString("0,0.15,0.35,,0.68,0.4357,,,,,,,,,,,,,1,,,0.1,,0.5");
                synth.Play();
            }
        }
    }

    void hurr (AbstractTween hurr)
    {
        menuElements[menuIndex].label.color = tone2;
        inputEnabled = true;
    }
    
    void LoadGamePage (AbstractTween hurr)
    {
        //BaseMain.Instance.GoToPage(BPageType.InGamePage);
    }
}