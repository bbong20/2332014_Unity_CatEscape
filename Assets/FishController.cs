/*
 * 물고기가 위에서 아래로 3초에 하나씩 떨어지는 기능
 * 물고기가 게임화면 밖으로 나가면 물고기 오브젝트를 소멸시키는 기능
 */

using UnityEngine;

public class FishController : MonoBehaviour
{
    GameObject gPlayer = null; //플레이어 오브젝트 변수
    GameObject gDirector = null; //디렉터 오브젝트 변수

    Vector2 vFishCirclePoint = Vector2.zero;    //물고기를 둘러싼 원의 중심 좌표
    Vector2 vPlayerCirclePoint = Vector2.zero;      //플레이어를 둘러싼 원의 중심 좌표
    Vector2 vFishPlayerDistance = Vector2.zero;    //물고기에서 플레이어까지의 벡터값

    float fFishRadius = 0.5f;           //물고기 원의 반지름
    float fPlayerRadius = 1.0f;         //플레이어 원의 반지름
    float fFishPlayerDistance = 0.0f;   //물고기의 중심으로 부터 플레이어 중심까지의 거리 변수

    //int nFishCount = 0; //얻은 물고기 갯수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gPlayer = GameObject.Find("player"); //플레이어 오브젝트 찾기
        gDirector = GameObject.Find("GameDirector"); //게임디렉터 오브젝트 찾기
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0.0f, -0.1f, 0.0f); //물고기가 아래 방향으로 0.1만큼 이동한다.

        if (transform.position.y < -5.0f) //물고기 오브젝트가 y좌표 -5.0f인 지면 아래로 간다면 오브젝트를 파괴
        {
            Destroy(gameObject);
        }

        vFishCirclePoint = transform.position;                          //물고기의 위치 저장
        vPlayerCirclePoint = gPlayer.transform.position;                //플레이어의 위치 저장
        vFishPlayerDistance = vFishCirclePoint - vPlayerCirclePoint;    //물고기와 플레이어간의 거리

        fFishPlayerDistance = vFishPlayerDistance.magnitude;    //벡터의 길이를 구하는 magnitude 메소드를 사용하여 충돌 판정을 위한 거리를 산정한다.

        if(fFishPlayerDistance < fFishRadius + fPlayerRadius)   //물고기와 플레이어 사이의 거리 < 물고기 반지름 + 플레이어 반지름 : 충돌
        {
            gDirector.GetComponent<GameDirector>().f_UpdateFishAmountCount(); //물고기 개수 카운트 메소드 호출

            Destroy(gameObject); //오브젝트 삭제
        }
        
    }
}
