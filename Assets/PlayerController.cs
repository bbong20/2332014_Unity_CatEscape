using System.Linq.Expressions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float fMaxPosition = 7.0f; //플레이어가 좌, 우 이동시 게임창을 벗어나지 않도록 Vector 최대값 설정 변수
    float fMinPosition = -7.0f; //플레이어가 좌, 우 이동시 게임창을 벗어나지 않도록 Vector 최소값 설정 변수
    float fPositionX = 0.0f;

    //SerializeField를 사용하여 기본 private 접근제어인 fPlayerMoveSpeed를 private 접근제어를 유지한 채로 Inspector 창에서 값을 유연하게 수정하기 위함
    [SerializeField] float fPlayerMoveSpeed = 10.0f; //플레이어의 이동 속도를 위한 변수

    bool isLeftMove = false, isRightMove = false; //화살표버튼 클릭 여부를 판단하기 위한 bool 변수

    /*
     * Start 메소드
     * 미리 정의된 특수 이벤트 함수로서, 이 특수 함수들을 C#에서는 함수를 메소드라고 함
     * MonoBehaviour 클래스가 초기화 될 때 호출되는 이벤트 함수
     * 프로그램이 시작할 때 한 번만 호출이 되는 함수로 보통 컴포넌트를 받아오거나 업데이트나 다른 함수에서 사용하기 위해 초기화 해주는 기능
     * 즉, Start() 메소드는 스크립트 인스턴스가 활성화된 경우에만 첫 번째 프레임 업데이트 전에 호출하므로 한 번만 실행
     * 씬 에셋에 포함된 모든 오비젝트에 대해 Update등 이 전에 호출된 모든 스크립트를 위한 Start 함수가 호출
     * 따라서 게임플레이 도중 오브젝트를 인스턴스화될 때는 실행되지 않음
     */
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
         * 디바이스 성능에 따른 실행 결과의 차이 없애기
         * 어떤 성능의 컴퓨터에서 동작해도 같은 속도로 움직이도록 하는 처리
         * 스마트폰은 60, 고속의 PC는 300이 될 수 있는 디바이스 성능에 따라 게임 동작에 영향을 미칠 수 있음
         * 프레임레이트를 60으로 고정
         */
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * 키가 눌렀는지 검출하기 위해서는 Input 클래스의 GetKeyDown 메소드를 사용함
         * 이 메소드는 매개변수로 전달한 키가 눌리는 순간 true를 한 번 반환한다.
         * GetKeyDown 메소드는 지금까지 사용하던 GetMouseButtonDown 메소드와 비슷하므로 쉽게 이해할 수 있을 것
         * 키를 누른 순간 : GetKeyDown()
         * 키를 누르고 있는 순간 : GetKey()
         * 키를 누르다가 뗀 순간 : GetKeyUp()
         */

        /*
        //왼쪽 화살표 키가 눌렀을 때 → Input.GetKeyDown(KeyCode.LeftArrow)
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Translate 메소드 : 오브젝트를 현재 좌표에서 인수 값만큼 이동시키는 메소드
            transform.Translate(-2, 0, 0); //왼쪽으로 -3만큼 이동
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(2, 0, 0); //오른쪽으로 3만큼 이동
        }
        */

        //GetKey를 사용하여 키가 눌러져 있을때 지속적인 이동
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Translate 메소드 : 오브젝트를 현재 좌표에서 인수 값만큼 이동시키는 메소드
            transform.Translate(-fPlayerMoveSpeed * Time.deltaTime, 0.0f, 0.0f); //왼쪽으로 -10.0f * Time.deltaTime 만큼 이동
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(fPlayerMoveSpeed * Time.deltaTime, 0.0f, 0.0f); //오른쪽으로 10.0f * Time.deltaTime 만큼 이동
        }

        //UI 화살표 버튼이 활성화되면 인수 값 만큼 이동시킴.
        if(isLeftMove)
        {
            transform.Translate(-fPlayerMoveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
        else if(isRightMove)
        {
            transform.Translate(fPlayerMoveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }

        /*
         * Mathf.Clamp(value, min, max) 메소드
         * 특정 값을 어떠한 범위에 제한시키고자 할 때 사용하는 메소드
         * value 값의 범위 : min <= value <= max
         * 최소/최대값을 설정하여 지정한 범위 이외의 값이 되지 않도록 할 때 사용
         * 플레이어가 움직일 수 있는 최소(fMinPositionX) / 최대(fMaxPostionX) 범위값을 설정하여 그 범위를 벗어나지 않도록한다.
         */

        fPositionX = Mathf.Clamp(transform.position.x, fMinPosition, fMaxPosition);
        transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);

        /*
        //Clamp 메소드를 사용하면 매개변수의 매개변수로 지정된 fMinPosX, fMaxPosX 범위내에서 return 값이 제한된다.
        fLimitXPosRange = Mathf.Clamp(transform.position.x, fMinPosX, fMaxPosX);

        transform.position = new Vector2(fLimitXPosRange, fGroundPosY);
        */
    }

    
    //방법.1
    /*
    public void RButtonDown() // 오른쪽 버튼을 눌렀을 시 작동되는 메소드
    {
        transform.Translate(40.0f * Time.deltaTime, 0, 0);
    }

    public void LButtonDown() // 왼쪽 버튼을 눌렀을 시 작동되는 메소드
    {
        transform.Translate(-40.0f * Time.deltaTime, 0, 0);
    }
    */

    //방법.2
    //Event Trigger 컴포넌트를 사용하여 Pointer Down, Up의 이벤트 활성화시, 각각 bool 변수를 참과 거짓으로 변경하는 메소드
    public void f_LMoveButtonDown()
    {
        isLeftMove = true;
    }

    public void f_LMoveButtonUp()
    {
        isLeftMove = false;
    }


    public void f_RMoveButtonDown()
    {
        isRightMove = true;
    }

    public void f_RMoveButtonUp()
    {
        isRightMove = false;
    }
}
