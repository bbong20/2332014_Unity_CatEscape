using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;   //UI 관련 네임스페이스를 추가하는 역할
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using TMPro;
using UnityEngine.Rendering.Universal;

/*
 * [현재 프로그램의 문제분석]
 * 게임 재시작의 구동방식에 문제가 있다.
 * 현재는 SceneManagement를 사용하여 씬을 다시 로딩하는 방식이다.
 * 완전한 초기화를 보장하나, 로딩이 발생하고 시스템 자원을 소모한다.
 * 
 * 따라서, 각 스크립트에 초기화 메소드를 배치하여 재시작 버튼이 모든 게임 상태를 초기 상태로 되돌리는 방식이 필요하다.
 * 프로그램의 편의성, 메모리 최적화, 확장성을 고려한다면 싱글톤 패턴을 고려해 볼 만하다. 다만 싱글톤 패턴의 단점에 주의하자.
 * 싱글톤 패턴을 사용한다면 클래스 간 데이터 공유를 원활히 할 수 있다.
 */


public class GameDirector : MonoBehaviour
{
    /*
     * HpGauge Image Object를 저장할 멤버 변수
     * 감독 스크립트를 사용해 HP 게이지를 갱신하려면 감독 스크립트가 HP 게이지의 실체를 조작할 수 있어야 함
     * 그러기 위해서 Object 변수를 선언해서 HpGauge Image Object를 저장
     */
    GameObject gHpGauge = null;             //HP게이지 오브젝트 변수
    GameObject gRestartButton = null;       //재시작 버튼 오브젝트 변수
    GameObject gTextGameover = null;        //게임오버 UI 오브젝트 변수
    GameObject gTextGameClear = null;       //게임클리어 UI 오브젝트 변수
    GameObject gTextTimer = null;           //타이머 UI 오브젝트 변수
    GameObject gTextQuantityFish = null;    //물고기 아이템 수량 UI 오브젝트 변수

    [SerializeField]                    //private 접근 유효
    float fMaxTimeLimit = 30.0f;        //제한 시간 변수 30초 지정
    
    float fMaxArrowFallSpeed = 0.5f;    //화살의 최고 낙하 속도
    float fArrowSpeedIncreaseRate = 0.005f;  //화살의 낙하속도 증가율

    public float fArrowFallSpeed = 0.1f; //화살의 기본 낙하 속도, ArrowController에서 사용되어야 하므로 public 접근 제어

    int nGameClearFishCount = 10;       //게임 클리어 조건
    int nFishCount = 0;                 //누적 물고기 개수 변수


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
         * HpGauge 오브젝트 찾기
         * 씬 안에서 오브젝트를 찾는 메소드 : Find
         * Find 메소드는 오브젝트 이름을 인수로 전달하고, 인수 이름이 씬에 존재하면 해당 오브젝트를 반환
         * 플레이어의 좌표를 구하기 위해서 플레이어를 검색하여 오브젝트 변수에 저장
         * 각 오브젝트 상자에 대등하는 오브젝트를 씬 안에서 찾아 넣어야 함
         */
        gHpGauge = GameObject.Find("hpgauge");

        //불러오기
        gRestartButton = GameObject.Find("RestartButton");  //재시작 버튼 오브젝트
        gTextGameover = GameObject.Find("TextGameover");    //게임오버 문구 오브젝트
        gTextGameClear = GameObject.Find("TextGameClear");  //게임클리어 문구 오브젝트
        gTextTimer = GameObject.Find("TextTimer");          //타이머 UI 오브젝트 
        gTextQuantityFish = GameObject.Find("TextQuantityFish"); //물고기 수량 UI 오브젝트

        //비활성화
        gRestartButton.SetActive(false);    //게임 시작시 재시작 버튼 비활성화
        gTextGameClear.SetActive(false);    //게임 시작시 게임 클리어 문구 비활성화
        gTextGameover.SetActive(false);     //게임 시작시 게임 오버 문구 비활성화

    }

    // Update is called once per frame
    void Update()
    {
        f_GameTimeLimit();      //게임 시간을 제한
        f_PrintRemainTime();    //남은 게임 시간을 출력
    }

    /*
    * 나중에 화살 컨트롤러에서 HP 게이지 표시를 줄이는 처리를 호출할 것을 고려해
    * HP 게이지의 처리는 public 메소드를 작성
    * 화살과 플레이어가 충돌했을 때 화살 컨트롤러가 f_DecreaseHp() 메소드를 호출함
    * 메소드의 기능은 화살과 플레이어가 충돌했을 때 Image 오브젝트(hpGauge)의 fillAmount를 줄여
    * Hp게이지를 표시하는 비율을 10% 낮춤
    */
    public void f_DecreaseHp()
    {
        /*
         * 유니티 오브젝트는 GameObject라는 빈 상자에 설정 자료(컴포넌트)를 추가해서 기느을 확장함
         * 예 : 오브젝트를 물리적으로 움직이게 하려면 Rigidbody 컴퍼넌트 추가
         * 예 : 소리를 내게 하려면 AudioSource 컴포넌트 추가
         * 예 : 자체 기능을 늘리고 싶다면 스크립트 컴포넌트를 추가함
         * 컴포넌트 접근 방법 : GetComponent<>()
         * GetComponent는 게임 오브젝트에 대해'XX 컴포넌트를 주세요'라고 부탁하면,
         * 해당되는 컴포넌트(기능)을 돌려주는 메소드
         * 예 : AudioSource 컴포넌트를 원하면 → GetComponent<AudioSource>()
         * 예 : Text 컴포넌트를 원하면 → GetComponent<Text>()
         * 예 : 직접 만든 스크립트도 컴포넌트의 일종이므로 GetComponent 메소드를 사용해서 구할 수 있음
         * 
         * 화살과 플레이어가 충돌했을 때 Image 오브젝트(HpGauge)의 fillAmount를 줄여
         * HP 게이지를 표시하는 비율을 10% 낮춤
         */
        gHpGauge.GetComponent<Image>().fillAmount -= 0.1f;

        //1번 방식(씬 전환)
        /*
        if (gHpGauge.GetComponent<Image>().fillAmount == 0.0f)
        {
            SceneManager.LoadScene("EndScene");
        }
        */
        
        //2번 방식(시간을 멈춤)
        if (gHpGauge.GetComponent<Image>().fillAmount == 0.0f)
        {
            f_GameOver();
        }
    }

    //SetActive or Behaviour.enabled

    /*
     * [Unity Manual]
     * Time Class
     * time : 프로젝트 재생 시작 후 경과한 시간 초 단위로 반환합니다.
     * deltaTime : 마지막 프레임이 완료된 후 경과한 시간을 초 단위로 반환합니다. 
     *             이 값은 게임이나 앱이 실행될 때 초당 프레임(FPS) 속도에 따라 다릅니다.
     */


    //플레이어의 체력이 0이되면 게임을 멈추고, 게임 오버 문구와 재시작 버튼을 띄우는 메소드
    void f_GameOver()
    {
        Time.timeScale = 0.0f; //게임 내 시간흐름 멈춤

        gTextGameover.SetActive(true);  //게임 오버 문구 활성화
        gRestartButton.SetActive(true); //재시작 버튼 문구 활성화
    }

    //게임 클리어 조건을 만족할 경우 호출되는 메소드
    void f_GameClear()
    {
        Time.timeScale = 0.0f; //게임 내 시간흐름 멈춤

        gTextGameClear.SetActive(true); //클리어 문구 활성화
        gRestartButton.SetActive(true); //재시작 버튼 문구 활성화
    }

    //게임 재시작 버튼 기능을 위한 메소드, 게임의 시간을 다시 흐르게 하고 게임씬을 다시 불러온다.
    //OnClick 사용을 위한 public 접근 제어
    public void f_GameRestart()
    {
        Time.timeScale = 1.0f; //게임 내 시간흐름

        SceneManager.LoadScene("GameScene"); //게임 씬 재로드
    }


    //게임 시간을 제한하는 메소드
    void f_GameTimeLimit()
    {
        if(fMaxTimeLimit > 0.0f) //제한 시간이 0초가 아니면
        {
            fMaxTimeLimit -= Time.deltaTime; //프레임 시간 만큼 감소 

            //게임의 시간이 지남에 따라 화살의 낙하 속도를 증가율 만큼 가산함
            if(fArrowFallSpeed < fMaxArrowFallSpeed) 
            {
                fArrowFallSpeed += Time.deltaTime * fArrowSpeedIncreaseRate;
            }

            if(fMaxTimeLimit <= 0.0f) //제한 시간이 0초면
            {
                f_GameOver(); //게임 오버
            }
        }
    }

    /*
     * 남은 시간을 출력하는 메소드
     * [추가 기능]
     * - 남은 시간이 10초 이하일 경우 UI가 빨간색으로 변경되며 깜박이게 된다.
     */
    void f_PrintRemainTime()
    {
        //gTextTimer.GetComponent<TextMeshProUGUI>().text = "Time : " + fMaxTimeLimit.ToString("F1") + "sec"; //남은 시간을 소수점 1번째 자리까지 출력

        TextMeshProUGUI textMeshProUGUI = gTextTimer.GetComponent<TextMeshProUGUI>(); //다중 사용이 예상되어 선언

        float fBlinkSpan = Mathf.PingPong(Time.time * 3, 1.0f); //PingPong 메소드를 사용하여 1.0f초에 3번(Time.time * 3) 깜박이는 효과를 구현하기 위한 깜박임 주기 변수
                                                                //출력 메소드에서만 사용하기에 지역변수로 선언

        textMeshProUGUI.text = "Time : " + fMaxTimeLimit.ToString("F1") + " sec"; //남은 시간을 소수점 1번째 자리까지 출력

        if (fMaxTimeLimit <= 10.0f) //남은 시간이 10초 이하일 경우
        {
            
            textMeshProUGUI.color = new Color(1.0f, fBlinkSpan, fBlinkSpan); //시간 출력 UI의 RGB 값을 1,0,0 으로 변경(빨간색)
        }
        else
        {
            textMeshProUGUI.color = new Color(1.0f, 1.0f, 1.0f); //흰색
        }
    }


    /*
     * 플레이어가 획득한 물고기 갯수를 출력하고, 물고기 개수가 클리어 조건이라면 게임 클리어시키는 메소드
     * FishController에서 사용하기 위해 public 접근 제어
     */
    public void f_UpdateFishAmountCount()
    {
        nFishCount++; //메소드 호출시 물고기 개수 증가

        gTextQuantityFish.GetComponent<TextMeshProUGUI>().text = "Fish : " + nFishCount + "/" + nGameClearFishCount; //물고기의 개수를 출력 : Fish : 1/10

        if(nFishCount >= nGameClearFishCount) //물고기 개수가 클리어 조건을 충족할 경우
        {
            f_GameClear(); //게임 클리어 메소드 호출
        }
    }
}
