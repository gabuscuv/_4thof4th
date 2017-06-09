using System;
using System.Collections;
[Serializable()]
public class scene{
    private bool player;
    private String bgpath;
    private bool FX;
    private bool enemy;
    private ArrayList enemylist;
    private bool beep_bgm;
    private String beep_path;
    private bool synth_bgm;
    private bool pcm_bgm;
    private string text;
    public scene(bool player, String bgpath, bool FX, bool enemy, ArrayList enemylist, bool beep_bgm,
        String beep_path, bool synth_bgm, bool pcm_bgm,string text){
        this.player = player;
        this.bgpath = bgpath;
        this.FX = FX;
        this.enemy = enemy;
        this.enemylist = enemylist;
        this.beep_bgm = beep_bgm;
        this.beep_path = beep_path;
        this.synth_bgm = synth_bgm;
        this.Pcm_bgm = pcm_bgm;
        this.text = text;
    }

    public bool Player { get => player; set => player = value; }
    public string Bgpath { get => bgpath; set => bgpath = value; }
    public bool FX1 { get => FX; set => FX = value; }
    public bool Enemy { get => enemy; set => enemy = value; }
    public ArrayList Enemylist { get => enemylist; set => enemylist = value; }
    public bool Beep_bgm { get => beep_bgm; set => beep_bgm = value; }
    public string Beep_path { get => beep_path; set => beep_path = value; }
    public bool Synth_bgm { get => synth_bgm; set => synth_bgm = value; }
    public bool Pcm_bgm { get => pcm_bgm; set => pcm_bgm = value; }
    public string Text { get => text; set => text = value; }
}


