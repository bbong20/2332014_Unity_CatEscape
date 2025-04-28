using System.Linq.Expressions;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float fMaxPosition = 7.0f; //�÷��̾ ��, �� �̵��� ����â�� ����� �ʵ��� Vector �ִ밪 ���� ����
    float fMinPosition = -7.0f; //�÷��̾ ��, �� �̵��� ����â�� ����� �ʵ��� Vector �ּҰ� ���� ����
    float fPositionX = 0.0f;

    //SerializeField�� ����Ͽ� �⺻ private ���������� fPlayerMoveSpeed�� private ������� ������ ä�� Inspector â���� ���� �����ϰ� �����ϱ� ����
    [SerializeField] float fPlayerMoveSpeed = 10.0f; //�÷��̾��� �̵� �ӵ��� ���� ����

    bool isLeftMove = false, isRightMove = false; //ȭ��ǥ��ư Ŭ�� ���θ� �Ǵ��ϱ� ���� bool ����

    /*
     * Start �޼ҵ�
     * �̸� ���ǵ� Ư�� �̺�Ʈ �Լ��μ�, �� Ư�� �Լ����� C#������ �Լ��� �޼ҵ��� ��
     * MonoBehaviour Ŭ������ �ʱ�ȭ �� �� ȣ��Ǵ� �̺�Ʈ �Լ�
     * ���α׷��� ������ �� �� ���� ȣ���� �Ǵ� �Լ��� ���� ������Ʈ�� �޾ƿ��ų� ������Ʈ�� �ٸ� �Լ����� ����ϱ� ���� �ʱ�ȭ ���ִ� ���
     * ��, Start() �޼ҵ�� ��ũ��Ʈ �ν��Ͻ��� Ȱ��ȭ�� ��쿡�� ù ��° ������ ������Ʈ ���� ȣ���ϹǷ� �� ���� ����
     * �� ���¿� ���Ե� ��� ������Ʈ�� ���� Update�� �� ���� ȣ��� ��� ��ũ��Ʈ�� ���� Start �Լ��� ȣ��
     * ���� �����÷��� ���� ������Ʈ�� �ν��Ͻ�ȭ�� ���� ������� ����
     */
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
         * ����̽� ���ɿ� ���� ���� ����� ���� ���ֱ�
         * � ������ ��ǻ�Ϳ��� �����ص� ���� �ӵ��� �����̵��� �ϴ� ó��
         * ����Ʈ���� 60, ����� PC�� 300�� �� �� �ִ� ����̽� ���ɿ� ���� ���� ���ۿ� ������ ��ĥ �� ����
         * �����ӷ���Ʈ�� 60���� ����
         */
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * Ű�� �������� �����ϱ� ���ؼ��� Input Ŭ������ GetKeyDown �޼ҵ带 �����
         * �� �޼ҵ�� �Ű������� ������ Ű�� ������ ���� true�� �� �� ��ȯ�Ѵ�.
         * GetKeyDown �޼ҵ�� ���ݱ��� ����ϴ� GetMouseButtonDown �޼ҵ�� ����ϹǷ� ���� ������ �� ���� ��
         * Ű�� ���� ���� : GetKeyDown()
         * Ű�� ������ �ִ� ���� : GetKey()
         * Ű�� �����ٰ� �� ���� : GetKeyUp()
         */

        /*
        //���� ȭ��ǥ Ű�� ������ �� �� Input.GetKeyDown(KeyCode.LeftArrow)
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Translate �޼ҵ� : ������Ʈ�� ���� ��ǥ���� �μ� ����ŭ �̵���Ű�� �޼ҵ�
            transform.Translate(-2, 0, 0); //�������� -3��ŭ �̵�
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Translate(2, 0, 0); //���������� 3��ŭ �̵�
        }
        */

        //GetKey�� ����Ͽ� Ű�� ������ ������ �������� �̵�
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Translate �޼ҵ� : ������Ʈ�� ���� ��ǥ���� �μ� ����ŭ �̵���Ű�� �޼ҵ�
            transform.Translate(-fPlayerMoveSpeed * Time.deltaTime, 0.0f, 0.0f); //�������� -10.0f * Time.deltaTime ��ŭ �̵�
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(fPlayerMoveSpeed * Time.deltaTime, 0.0f, 0.0f); //���������� 10.0f * Time.deltaTime ��ŭ �̵�
        }

        //UI ȭ��ǥ ��ư�� Ȱ��ȭ�Ǹ� �μ� �� ��ŭ �̵���Ŵ.
        if(isLeftMove)
        {
            transform.Translate(-fPlayerMoveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
        else if(isRightMove)
        {
            transform.Translate(fPlayerMoveSpeed * Time.deltaTime, 0.0f, 0.0f);
        }

        /*
         * Mathf.Clamp(value, min, max) �޼ҵ�
         * Ư�� ���� ��� ������ ���ѽ�Ű���� �� �� ����ϴ� �޼ҵ�
         * value ���� ���� : min <= value <= max
         * �ּ�/�ִ밪�� �����Ͽ� ������ ���� �̿��� ���� ���� �ʵ��� �� �� ���
         * �÷��̾ ������ �� �ִ� �ּ�(fMinPositionX) / �ִ�(fMaxPostionX) �������� �����Ͽ� �� ������ ����� �ʵ����Ѵ�.
         */

        fPositionX = Mathf.Clamp(transform.position.x, fMinPosition, fMaxPosition);
        transform.position = new Vector3(fPositionX, transform.position.y, transform.position.z);

        /*
        //Clamp �޼ҵ带 ����ϸ� �Ű������� �Ű������� ������ fMinPosX, fMaxPosX ���������� return ���� ���ѵȴ�.
        fLimitXPosRange = Mathf.Clamp(transform.position.x, fMinPosX, fMaxPosX);

        transform.position = new Vector2(fLimitXPosRange, fGroundPosY);
        */
    }

    
    //���.1
    /*
    public void RButtonDown() // ������ ��ư�� ������ �� �۵��Ǵ� �޼ҵ�
    {
        transform.Translate(40.0f * Time.deltaTime, 0, 0);
    }

    public void LButtonDown() // ���� ��ư�� ������ �� �۵��Ǵ� �޼ҵ�
    {
        transform.Translate(-40.0f * Time.deltaTime, 0, 0);
    }
    */

    //���.2
    //Event Trigger ������Ʈ�� ����Ͽ� Pointer Down, Up�� �̺�Ʈ Ȱ��ȭ��, ���� bool ������ ���� �������� �����ϴ� �޼ҵ�
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
