/*
 * 화살 오브젝트를 1초에 한 개씩 생성하는 알고리즘
 * Update 메소드는 프레임마다 실행되고 앞 프레임과 현재 프레임 사이의 시간 차이는 Time.deltaTime에 대입
 * 프레임과 프레임 사이의 시간 차이를 대나무 통(delta변수)에 모으로(합계) 1초 이상이 되면 대나무 통을 비움
 * 대나무 통을 비우는 시점인 1초에 한 번씩 화살이 생성됨
 * Instantiate 메소드
 * 게임을 실행하는 도중에 게임 오브젝트를 생성할 수 있음
 * 화살 프리팹을 이용하여, 화살 인스턴스를 생성하는 메소드
 * Random.Range 메소드 : 랜덤 값을 쉽게 생성할 수 있는 방법
 * Random 클래스는 흔히 요구되는 다양한 타입의 랜덤 값을 쉽게 생성할 수 있는 방법을 제공
 * 사용자가 제공한 최소값과 최대값 사이의 임의의 숫자를 제공함
 * 첫 번째 매개변수보다 크거나 같고, 두 번째 매개변수보다 작은 범위에서 무작위 수를 랜덤하게 반환
 */


using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    /*
     * 제너레이터 스크립에 프리팹 전달 방법
     * arrowPrefab 변수에 프리팹 실체를 대입하기 위해서 public 접근 수식자
     * 멤버변수 선언 시 public으로 선언하면 Inspector 창에서 Prefab 설계도 대입할 수 있도록 보임
     * 화살 대량 생산을 위해서 양산기계(제너레이터스크립트)에 넘겨 줄 Prefab 설계도를 넘겨 주어야 함
     */
    public GameObject gArrowPrefab = null; //화살 Prefab을 넣을 빈오브젝트 상자 선언

    GameObject gArrowInstance = null; //화살 인스턴스 저장 변수

    float fArrowCreateSpan = 1.0f;   //화살 생성 변수 : 화살을 1초마다 생성 변수
    float fDeltaTime = 0.0f;        //앞 프레임과 현재 프레임 사이의 시간 차이를 저장하는 변수

    int nArrowPositionRange = 0;    //화살의 X좌표 Range 저장 변수

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
         * 화살을 1초(fArrowCreateSpan = 1.0f)마다 한 개씩 생성
         * 프레임당 누적 시간이 1초가 넘으면, 화살 생산
         */
        if(fDeltaTime > fArrowCreateSpan)
        {
            fDeltaTime = 0.0f; //프레임과 프레임 사이의 시간 차이 누적 변수 초기화

            /*
             * Instantiate 메소드 : 화살 프리팹을 이용하여, 화살 인스턴스를 생성하는 메소드
             * 매개변수로 프리팹을 전달하면, 반환값으로 프리팹 인스턴스를 돌려준다.
             * Instantiate 메소드를 사용하면 게임을 실행하는 도중에 게임오브젝트를 생성할 수 있음
             * RPG 게임이라면 수많은 아이템, 캐릭터, 배경 등 모든것들을 어떻게 미리 만들어 놓을 수 있을까?
             * 그러므로 게임오브젝트의 복제본을 생성
             * Instantiate(GameObejct original, Vector3 position, Quaternion rotation)
             * GameObejct original : 생성하고자 하는 게임오브젝트명, 현재 씬에 있는 게임오브젝트나 Prefab으로 선언된 객체를 의미함
             * Vector3 position : Vector3으로 생성될 위치를 설정함
             * Quaternion rotation : 생성될 게임오브젝트의 회전값을 지정
             */
            gArrowInstance = Instantiate(gArrowPrefab);

            /*
             * Random 클래스는 흔히 요구되는 다양한 타입의 랜덤 값을 생성할 수 있는 방법을 제공
             * Random.Range 메소드 : 사용자가 제공한 최소값과 최대값 사이의 임의의 숫자를 제공함
             * 제공된 최소값과 최대값이 정수인지 실수인지에 따라 정수 또는 실수를 반환함
             * 첫 번째 매개변수보다 크거나 같고, 두 번쨰 매개변수보다 작은 범위에서 무작위 수를 정수로 반환
             * 화살의 X좌표는 -6 6 사이에 불규칙하게 위치
             */
            nArrowPositionRange = Random.Range(-6, 7);

            gArrowInstance.transform.position = new Vector3(nArrowPositionRange, 7, 0);
        }
    }
}
