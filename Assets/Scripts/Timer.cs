using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    //-------------------------
    //完成关卡的时间 (单位为秒)
    public float MaxTime = 360f;

    //-------------------------
    //倒计时
    [SerializeField]
    private float CountDown = 0;

    //-------------------------
    // start()是初始化函数

    // Start is called before the first frame update
    private void Start()
    {
        CountDown = MaxTime;
    }

    // Update is called once per frame
    private void Update()
    {
        //时间减少
        CountDown -= Time.deltaTime;

        //当时间耗尽之后重启关卡t
        if (CountDown <= 0)
        {
        }
    }
}