using System;
using System.Diagnostics;

class SimpleTimer
{
    public Stopwatch s;
    public void Start(){
        s = new Stopwatch();
        s.Start();
    }
    
    public void Restart(){
        s.Reset();
    }
    
    public void Stop(){
        s.Stop();
    }
    
    public string TimeStamp{
        get {
            return (new DateTime(s.ElapsedTicks)).ToString(@"mm\:ss\:ff"); 
        }
    }
}