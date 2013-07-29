using UnityEngine;
using System.Collections;
using System;

public class Car : FSprite {

    public float Speed {get; set;}
    float _topSpeed = 10;
    public FContainer Colliders {get; set;}
    bool _accelerating = false;
    bool _honkin = false;
    bool _drivin = false;
    public Car(FAtlasElement element, FContainer grid) :base(element){
        ListenForUpdate(Update);
        Speed = 0;
        Colliders = grid;
        FSoundManager.PlayMusic("loop_0");
    }
    void Update(){
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        var angH = Input.GetAxis("RightH");
        var angV = Input.GetAxis("RightV");
        
        if(angH > 0.5f)
            this.rotation+=3;
        else if(angH < -0.5f)
            this.rotation-=3;
  
        if(this.Speed > 0 && _drivin != true){
            FSoundManager.PlayMusic("loop_1", 0.2f);
            _drivin = true;
            Debug.Log("YEEEHAW");
        }else if (this.Speed == 0 && _drivin == true){
            FSoundManager.PlayMusic("loop_0");
            _drivin = false;
            Debug.Log("naw");
        }
        
        Move(0, vInput);
        
        if(Input.GetKeyUp(KeyCode.Space) && _honkin == false)
            CoroutineRunner.StartFutileCoroutine(Honk());
    }
    
    IEnumerator Honk(){
        _honkin = true;
        FSoundManager.PlaySound("hornC4");
        yield return new WaitForSeconds(1.5f);
        _honkin = false;
    }
    
    
    void Move(float hInput, float vInput){
        Quaternion rotation = Quaternion.AngleAxis(this.rotation,-Vector3.forward);
        Vector2 direction = Vector2.zero;
  
        this._accelerating = false;
        if(vInput > 0){
            this.Speed+=0.33f;
        }else {
            this.Speed-=0.33f;
        }
        this.Speed = Mathf.Clamp(this.Speed, 0, this._topSpeed);
        if(isColliding()){
            this.Speed = -this.Speed*0.90f;
        }
        direction = Vector2.up*this.Speed;
        direction = rotation*direction;
        Vector2 newpos = direction+this.GetPosition();
        this.SetPosition(newpos);
    }
    
    
    bool isColliding(){
        
        for(int i = 0; i < Colliders.GetChildCount(); i++){
            FSprite s = (FSprite)Colliders.GetChildAt(i);
            if(s.GetTextureRectRelativeToContainer().CheckIntersect(this.GetTextureRectRelativeToContainer()))
                return true;
        }
        return false;
    }
}