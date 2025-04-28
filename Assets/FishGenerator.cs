/*
 * 물고기 오브젝트를 5초에 한 개씩 생성하는 알고리즘 
 */

using UnityEngine;

public class FishGenerator : MonoBehaviour
{
    /*
     * 제너레이터 스크립에 프리팹 전달 방법
     * fishPrefab 변수에 프리팹 실체를 대입하기 위해서 public 접근 수식자
     * 멤버변수 선언 시 public으로 선언하면 Inspector 창에서 Prefab 설계도 대입할 수 있도록 보임
     * 물고기 대량 생산을 위해서 양산기계(제너레이터스크립트)에 넘겨 줄 Prefab 설계도를 넘겨 주어야 함
     */
    public GameObject gFishPrefab = null; //물고기 프리팹 오브젝트 변수
    GameObject gFishInstance = null;      //물고기 인스턴스 변수

    [SerializeField]                //private 접근 유효
    float fFishCreateSpan = 2.0f;   //물고기 생성 변수 : 물고기를 기본 2초마다 생성
    

    float fDeltaTime = 0.0f;        //앞 프레임과 현재 프레임 사이의 시간 차이를 저장하는 변수
    int nFishPositionRange = 0;    //물고기의 X좌표 Range 저장 변수

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Update 메소드는 프레임마다 실행되고 앞 프레임과 현재 프레임 사이의 시간 차에는 Time.deltaTime에 대입됨
         * Time.deltaTime은 한 프레임 당 실행하는 시간을 뜻하는데, 값을 float 형태로 반환하고 단위는 초를 사용함
         * 즉, 프레임과 프레임 사이의 시간 차이를 fDeltaTime 변수에 누적
         */
        fDeltaTime += Time.deltaTime;

            /*
             * Instantiate 메소드 : 물고기 프리팹을 이용하여, 물고기 인스턴스를 생성하는 메소드
             * 매개변수로 프리팹을 전달하면, 반환값으로 프리팹 인스턴스를 돌려준다.
             * Instantiate 메소드를 사용하면 게임을 실행하는 도중에 게임오브젝트를 생성할 수 있음
             * RPG 게임이라면 수많은 아이템, 캐릭터, 배경 등 모든것들을 어떻게 미리 만들어 놓을 수 있을까?
             * 그러므로 게임오브젝트의 복제본을 생성
             * Instantiate(GameObejct original, Vector3 position, Quaternion rotation)
             * GameObejct original : 생성하고자 하는 게임오브젝트명, 현재 씬에 있는 게임오브젝트나 Prefab으로 선언된 객체를 의미함
             * Vector3 position : Vector3으로 생성될 위치를 설정함
             * Quaternion rotation : 생성될 게임오브젝트의 회전값을 지정
             */

        if (fDeltaTime > fFishCreateSpan)
        {
            fDeltaTime = 0.0f;

            gFishInstance = Instantiate(gFishPrefab);

            nFishPositionRange = Random.Range(-6, 7); // nFishPositionRange에 -6~7 사이 난수를 발생시켜 저장

            gFishInstance.transform.position = new Vector3(nFishPositionRange, 7, 0); //발생된 난수를 통해 물고기 좌표 이동
        }
    }
}
