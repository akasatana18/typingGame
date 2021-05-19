using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Yubi : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject yubi;//このキーを押す指を指定しておく。
   

    void Start()
    {
        //yubi.GetComponent<Renderer>().material.color = Color.white;
    }
    public void Redhe()//指を赤色へ
    {
        yubi.GetComponent<Renderer>().material.color = Color.red;
    }

    public void Motohe()//指を元の色に
    {
        yubi.GetComponent<Renderer>().material.color = Color.white;
    }

    public void Akahe()//キーが間違えて押されたときにキーを少しの間赤色にする。
    {
        StartCoroutine("Colorchange");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Colorchange() 
    {
        this.GetComponent<Renderer>().material.color = Color.red;　
        yield return new WaitForSeconds(0.3f);
        if (this.GetComponent<Renderer>().material.color != Color.black)//上の0.3秒の間に次の問題へいっている可能性があるため黒色になっていないかを確認する。
        {
            this.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
