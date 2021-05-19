
using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Text_hyouji : MonoBehaviour
{
    // Start is called before the first frame update
   

    //問題用の文字
    private string[][] ZHT = new string[7][] {
        new string[]{ "a", "s", "d", "f", "g", "h", "j", "k", "l" },
        new string[]{ "q", "w", "e", "r", "t", "y", "u", "i", "o", "p" },
        new string[]{ "a", "s", "d", "f", "g", "h", "j", "k", "l","q", "w", "e", "r", "t", "y", "u", "i", "o", "p"},
        new string[]{ "z", "x", "c", "v", "b", "n", "m",".","," },
        new string[]{ "a", "s", "d", "f", "g", "h", "j", "k", "l", "q", "w", "e", "r", "t", "y", "u", "i", "o", "p" ,"z", "x", "c", "v", "b", "n", "m" },
        new string[]{ "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "-" },
        new string[]{ "a", "s", "d", "f", "g", "h", "j", "k", "l", "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "z", "x", "c", "v", "b", "n", "m" ,"0", "1", "2", "3", "4", "5", "6", "7","8","9","-",",","."}
    };

    //ミスったのがどれかを判定するためのもの
    private string[] MHT = { "a", "s", "d", "f", "g", "h", "j", "k", "l", "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "z", "x", "c", "v", "b", "n", "m" ,"0", "1", "2", "3", "4", "5", "6", "7","8","9","-",",","."};
    
    private GameObject[] MKeyObjects;//全てのキーボードのオブジェクト
    private GameObject[] MKeyMoji;//全てのキーボードオブジェクトの子のテキストを持つオブジェクト
    private Text[] MKeycolor;//全てのKeyMojiの中のテキスト
    private Key_Yubi[] MKeyscript;//全てのKeyの中のスクリプト


    public GameObject Text_GameObject;//テキスト表示オブジェクト
    private Text Maintext;//問題テキスト

    private int Mn = 0;//問題番号
    private int Zmn = 0;//前回の問題番号
    private int KMn = 0;//何段目のキーボード...したのKKObjectから読み取る

    
    private GameObject KKObject;//今回何段目のキーボードかを読み込むためのオブジェクト

    private int MissNumber = 0;//ミス回数
    public GameObject MissNumberObject;//ミスカウントオブジェクト
    private int SeikaiNumber = 0;//正解した回数
    private bool owari = false;//目的達成


    private GameObject Misson;//ミスの音が出るプレファブ

    private Text Misstext;//ミス回数のテキスト

    private GameObject[] KeyObjects;//使うキーボードのオブジェクト
    private GameObject[] KeyMoji;//使うキーボードオブジェクトの子のテキストを持つオブジェクト
    private Text[] Keycolor;//使うKeyMojiの中のテキスト
    private Key_Yubi[] Keyscript;//使うKeyの中のスクリプト

    

    void Start()
    {
        Misson = (GameObject)Resources.Load("Prefab/MissSE");
        KKObject = GameObject.Find("GameNumber(Clone)");
        KMn = KKObject.GetComponent<GameNumberobject>().GN;//今回どの段を使うか読み取る
        MissNumber = 0;
        SeikaiNumber = 0;
        owari = false;
        Misstext = MissNumberObject.GetComponent<Text>();
        Misstext.text = "ミス: " + MissNumber.ToString();
        this.Maintext = Text_GameObject.GetComponent<Text>();
        FindKeyObjects();
        FindMKeyObjects();
        //FindNumberObjects();

        //最初の問題を表示
        Mn = UnityEngine.Random.Range(0, ZHT[KMn].Length);
        Zmn = Mn;
        this.Maintext.text = ZHT[KMn][Mn].ToUpper();
        KeyObjects[Mn].GetComponent<Renderer>().material.color = Color.black;
        Keycolor[Mn].color = Color.white;
        Keyscript[Mn].Redhe();
    }

    void FindKeyObjects()//今回使う問題のKeyオブジェクトとその子のテキストを変数に入れておく
    {
        KeyObjects = new GameObject[ZHT[KMn].Length];
        KeyMoji = new GameObject[ZHT[KMn].Length];
        Keycolor = new Text[ZHT[KMn].Length];
        Keyscript = new Key_Yubi[ZHT[KMn].Length];
        for (int i = 0; i < ZHT[KMn].Length; i++)
        {
            KeyObjects[i] = GameObject.Find(ZHT[KMn][i].ToUpper() + "_mass");
            GameObject Kodomo = KeyObjects[i].transform.GetChild(0).gameObject;
            KeyMoji[i] = Kodomo.transform.GetChild(0).gameObject;
            Keycolor[i] = KeyMoji[i].GetComponent<Text>();
            Keyscript[i] = KeyObjects[i].GetComponent<Key_Yubi>();
        }
    }
    void FindMKeyObjects()//間違えたときのために全てのkeyオブジェクトとその子のテキストを変数に入れておく
    {
        MKeyObjects = new GameObject[MHT.Length];
        MKeyMoji = new GameObject[MHT.Length];
        MKeycolor = new Text[MHT.Length];
        MKeyscript = new Key_Yubi[MHT.Length];
        for (int i = 0; i < MHT.Length; i++)
        {
            MKeyObjects[i] = GameObject.Find(MHT[i].ToUpper() + "_mass");
            GameObject MKodomo = MKeyObjects[i].transform.GetChild(0).gameObject;
            MKeyMoji[i] = MKodomo.transform.GetChild(0).gameObject;
            MKeycolor[i] = MKeyMoji[i].GetComponent<Text>();
            MKeyscript[i] = MKeyObjects[i].GetComponent<Key_Yubi>();
        }
    }


    void Mnumber()//次の問題番号を決める。前回と同じなら違う番号する
    {
        for (int i = 0; ; i++)
        {
            Mn =UnityEngine.Random.Range(0, ZHT[KMn].Length);
            if (Zmn == Mn) continue;
            else break;
        }
        Zmn = Mn;
    }

    void Seikai() 　//正解した時の処理
    {
        SeikaiNumber += 1;

        //今の問題の文字とKEYとそれに対応する指をもとの色へ
        Keyscript[Mn].Motohe();
        KeyObjects[Mn].GetComponent<Renderer>().material.color = Color.white;
        Keycolor[Mn].color = Color.black;


        //正解回数に達していたら終わり、それ以外なら次の問題を表示
        if (SeikaiNumber >= 100) { 
            this.Maintext.text = "終了";
            owari = true;
        }
        else
        {
            Mnumber();
            this.Maintext.text = ZHT[KMn][Mn].ToUpper();
            if (ZHT[KMn][Mn] == ",") this.Maintext.text = "、";
            if (ZHT[KMn][Mn] == ".") this.Maintext.text = "。";

            //次の問題の文字とKEYとそれに対応する指の色を変える
            KeyObjects[Mn].GetComponent<Renderer>().material.color = Color.black;
            Keycolor[Mn].color = Color.white;
            Keyscript[Mn].Redhe();
        }
    }

    void Matigai()//間違えて押したら呼び出す
    {
        Instantiate(Misson, this.transform.position, Quaternion.identity);//ミスの音を出す
        if (!owari) { MissNumber += 1; }    //まだ終わってないならミス回数を増やす
        Misstext.text = "ミス: " + MissNumber.ToString();　//ミス回数のテキストを更新

        //ミスしたのがどのキーかを検索してそのキーを赤くする
        for (int i = 0;i<MHT.Length; i++)
        {
            if (Input.GetKeyDown(MHT[i]))
            {
                MKeyscript[i].Akahe();
            }

        }
    }

    void Title()//タイトルへ
    {
        SceneManager.LoadScene("Title");
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && (!Input.GetMouseButton(0)&& !Input.GetMouseButton(1) && !Input.GetMouseButton(2)))
        {
            if (Input.GetKeyDown(ZHT[KMn][Mn]))
            {
                Seikai();
            }
            else if (Input.GetKey(KeyCode.Return))
            {
                Title();
            }
            else
            {
                //Debug.Log("miss");
                Matigai();
            }
        }
       
    }

    
   


}
