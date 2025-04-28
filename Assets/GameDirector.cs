using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;   //UI ���� ���ӽ����̽��� �߰��ϴ� ����
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
using TMPro;
using UnityEngine.Rendering.Universal;

/*
 * [���� ���α׷��� �����м�]
 * ���� ������� ������Ŀ� ������ �ִ�.
 * ����� SceneManagement�� ����Ͽ� ���� �ٽ� �ε��ϴ� ����̴�.
 * ������ �ʱ�ȭ�� �����ϳ�, �ε��� �߻��ϰ� �ý��� �ڿ��� �Ҹ��Ѵ�.
 * 
 * ����, �� ��ũ��Ʈ�� �ʱ�ȭ �޼ҵ带 ��ġ�Ͽ� ����� ��ư�� ��� ���� ���¸� �ʱ� ���·� �ǵ����� ����� �ʿ��ϴ�.
 * ���α׷��� ���Ǽ�, �޸� ����ȭ, Ȯ�强�� ����Ѵٸ� �̱��� ������ ����� �� ���ϴ�. �ٸ� �̱��� ������ ������ ��������.
 * �̱��� ������ ����Ѵٸ� Ŭ���� �� ������ ������ ��Ȱ�� �� �� �ִ�.
 */


public class GameDirector : MonoBehaviour
{
    /*
     * HpGauge Image Object�� ������ ��� ����
     * ���� ��ũ��Ʈ�� ����� HP �������� �����Ϸ��� ���� ��ũ��Ʈ�� HP �������� ��ü�� ������ �� �־�� ��
     * �׷��� ���ؼ� Object ������ �����ؼ� HpGauge Image Object�� ����
     */
    GameObject gHpGauge = null;             //HP������ ������Ʈ ����
    GameObject gRestartButton = null;       //����� ��ư ������Ʈ ����
    GameObject gTextGameover = null;        //���ӿ��� UI ������Ʈ ����
    GameObject gTextGameClear = null;       //����Ŭ���� UI ������Ʈ ����
    GameObject gTextTimer = null;           //Ÿ�̸� UI ������Ʈ ����
    GameObject gTextQuantityFish = null;    //����� ������ ���� UI ������Ʈ ����

    [SerializeField]                    //private ���� ��ȿ
    float fMaxTimeLimit = 30.0f;        //���� �ð� ���� 30�� ����
    
    float fMaxArrowFallSpeed = 0.5f;    //ȭ���� �ְ� ���� �ӵ�
    float fArrowSpeedIncreaseRate = 0.005f;  //ȭ���� ���ϼӵ� ������

    public float fArrowFallSpeed = 0.1f; //ȭ���� �⺻ ���� �ӵ�, ArrowController���� ���Ǿ�� �ϹǷ� public ���� ����

    int nGameClearFishCount = 10;       //���� Ŭ���� ����
    int nFishCount = 0;                 //���� ����� ���� ����


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
         * HpGauge ������Ʈ ã��
         * �� �ȿ��� ������Ʈ�� ã�� �޼ҵ� : Find
         * Find �޼ҵ�� ������Ʈ �̸��� �μ��� �����ϰ�, �μ� �̸��� ���� �����ϸ� �ش� ������Ʈ�� ��ȯ
         * �÷��̾��� ��ǥ�� ���ϱ� ���ؼ� �÷��̾ �˻��Ͽ� ������Ʈ ������ ����
         * �� ������Ʈ ���ڿ� ����ϴ� ������Ʈ�� �� �ȿ��� ã�� �־�� ��
         */
        gHpGauge = GameObject.Find("hpgauge");

        //�ҷ�����
        gRestartButton = GameObject.Find("RestartButton");  //����� ��ư ������Ʈ
        gTextGameover = GameObject.Find("TextGameover");    //���ӿ��� ���� ������Ʈ
        gTextGameClear = GameObject.Find("TextGameClear");  //����Ŭ���� ���� ������Ʈ
        gTextTimer = GameObject.Find("TextTimer");          //Ÿ�̸� UI ������Ʈ 
        gTextQuantityFish = GameObject.Find("TextQuantityFish"); //����� ���� UI ������Ʈ

        //��Ȱ��ȭ
        gRestartButton.SetActive(false);    //���� ���۽� ����� ��ư ��Ȱ��ȭ
        gTextGameClear.SetActive(false);    //���� ���۽� ���� Ŭ���� ���� ��Ȱ��ȭ
        gTextGameover.SetActive(false);     //���� ���۽� ���� ���� ���� ��Ȱ��ȭ

    }

    // Update is called once per frame
    void Update()
    {
        f_GameTimeLimit();      //���� �ð��� ����
        f_PrintRemainTime();    //���� ���� �ð��� ���
    }

    /*
    * ���߿� ȭ�� ��Ʈ�ѷ����� HP ������ ǥ�ø� ���̴� ó���� ȣ���� ���� �����
    * HP �������� ó���� public �޼ҵ带 �ۼ�
    * ȭ��� �÷��̾ �浹���� �� ȭ�� ��Ʈ�ѷ��� f_DecreaseHp() �޼ҵ带 ȣ����
    * �޼ҵ��� ����� ȭ��� �÷��̾ �浹���� �� Image ������Ʈ(hpGauge)�� fillAmount�� �ٿ�
    * Hp�������� ǥ���ϴ� ������ 10% ����
    */
    public void f_DecreaseHp()
    {
        /*
         * ����Ƽ ������Ʈ�� GameObject��� �� ���ڿ� ���� �ڷ�(������Ʈ)�� �߰��ؼ� ����� Ȯ����
         * �� : ������Ʈ�� ���������� �����̰� �Ϸ��� Rigidbody ���۳�Ʈ �߰�
         * �� : �Ҹ��� ���� �Ϸ��� AudioSource ������Ʈ �߰�
         * �� : ��ü ����� �ø��� �ʹٸ� ��ũ��Ʈ ������Ʈ�� �߰���
         * ������Ʈ ���� ��� : GetComponent<>()
         * GetComponent�� ���� ������Ʈ�� ����'XX ������Ʈ�� �ּ���'��� ��Ź�ϸ�,
         * �ش�Ǵ� ������Ʈ(���)�� �����ִ� �޼ҵ�
         * �� : AudioSource ������Ʈ�� ���ϸ� �� GetComponent<AudioSource>()
         * �� : Text ������Ʈ�� ���ϸ� �� GetComponent<Text>()
         * �� : ���� ���� ��ũ��Ʈ�� ������Ʈ�� �����̹Ƿ� GetComponent �޼ҵ带 ����ؼ� ���� �� ����
         * 
         * ȭ��� �÷��̾ �浹���� �� Image ������Ʈ(HpGauge)�� fillAmount�� �ٿ�
         * HP �������� ǥ���ϴ� ������ 10% ����
         */
        gHpGauge.GetComponent<Image>().fillAmount -= 0.1f;

        //1�� ���(�� ��ȯ)
        /*
        if (gHpGauge.GetComponent<Image>().fillAmount == 0.0f)
        {
            SceneManager.LoadScene("EndScene");
        }
        */
        
        //2�� ���(�ð��� ����)
        if (gHpGauge.GetComponent<Image>().fillAmount == 0.0f)
        {
            f_GameOver();
        }
    }

    //SetActive or Behaviour.enabled

    /*
     * [Unity Manual]
     * Time Class
     * time : ������Ʈ ��� ���� �� ����� �ð� �� ������ ��ȯ�մϴ�.
     * deltaTime : ������ �������� �Ϸ�� �� ����� �ð��� �� ������ ��ȯ�մϴ�. 
     *             �� ���� �����̳� ���� ����� �� �ʴ� ������(FPS) �ӵ��� ���� �ٸ��ϴ�.
     */


    //�÷��̾��� ü���� 0�̵Ǹ� ������ ���߰�, ���� ���� ������ ����� ��ư�� ���� �޼ҵ�
    void f_GameOver()
    {
        Time.timeScale = 0.0f; //���� �� �ð��帧 ����

        gTextGameover.SetActive(true);  //���� ���� ���� Ȱ��ȭ
        gRestartButton.SetActive(true); //����� ��ư ���� Ȱ��ȭ
    }

    //���� Ŭ���� ������ ������ ��� ȣ��Ǵ� �޼ҵ�
    void f_GameClear()
    {
        Time.timeScale = 0.0f; //���� �� �ð��帧 ����

        gTextGameClear.SetActive(true); //Ŭ���� ���� Ȱ��ȭ
        gRestartButton.SetActive(true); //����� ��ư ���� Ȱ��ȭ
    }

    //���� ����� ��ư ����� ���� �޼ҵ�, ������ �ð��� �ٽ� �帣�� �ϰ� ���Ӿ��� �ٽ� �ҷ��´�.
    //OnClick ����� ���� public ���� ����
    public void f_GameRestart()
    {
        Time.timeScale = 1.0f; //���� �� �ð��帧

        SceneManager.LoadScene("GameScene"); //���� �� ��ε�
    }


    //���� �ð��� �����ϴ� �޼ҵ�
    void f_GameTimeLimit()
    {
        if(fMaxTimeLimit > 0.0f) //���� �ð��� 0�ʰ� �ƴϸ�
        {
            fMaxTimeLimit -= Time.deltaTime; //������ �ð� ��ŭ ���� 

            //������ �ð��� ������ ���� ȭ���� ���� �ӵ��� ������ ��ŭ ������
            if(fArrowFallSpeed < fMaxArrowFallSpeed) 
            {
                fArrowFallSpeed += Time.deltaTime * fArrowSpeedIncreaseRate;
            }

            if(fMaxTimeLimit <= 0.0f) //���� �ð��� 0�ʸ�
            {
                f_GameOver(); //���� ����
            }
        }
    }

    /*
     * ���� �ð��� ����ϴ� �޼ҵ�
     * [�߰� ���]
     * - ���� �ð��� 10�� ������ ��� UI�� ���������� ����Ǹ� �����̰� �ȴ�.
     */
    void f_PrintRemainTime()
    {
        //gTextTimer.GetComponent<TextMeshProUGUI>().text = "Time : " + fMaxTimeLimit.ToString("F1") + "sec"; //���� �ð��� �Ҽ��� 1��° �ڸ����� ���

        TextMeshProUGUI textMeshProUGUI = gTextTimer.GetComponent<TextMeshProUGUI>(); //���� ����� ����Ǿ� ����

        float fBlinkSpan = Mathf.PingPong(Time.time * 3, 1.0f); //PingPong �޼ҵ带 ����Ͽ� 1.0f�ʿ� 3��(Time.time * 3) �����̴� ȿ���� �����ϱ� ���� ������ �ֱ� ����
                                                                //��� �޼ҵ忡���� ����ϱ⿡ ���������� ����

        textMeshProUGUI.text = "Time : " + fMaxTimeLimit.ToString("F1") + " sec"; //���� �ð��� �Ҽ��� 1��° �ڸ����� ���

        if (fMaxTimeLimit <= 10.0f) //���� �ð��� 10�� ������ ���
        {
            
            textMeshProUGUI.color = new Color(1.0f, fBlinkSpan, fBlinkSpan); //�ð� ��� UI�� RGB ���� 1,0,0 ���� ����(������)
        }
        else
        {
            textMeshProUGUI.color = new Color(1.0f, 1.0f, 1.0f); //���
        }
    }


    /*
     * �÷��̾ ȹ���� ����� ������ ����ϰ�, ����� ������ Ŭ���� �����̶�� ���� Ŭ�����Ű�� �޼ҵ�
     * FishController���� ����ϱ� ���� public ���� ����
     */
    public void f_UpdateFishAmountCount()
    {
        nFishCount++; //�޼ҵ� ȣ��� ����� ���� ����

        gTextQuantityFish.GetComponent<TextMeshProUGUI>().text = "Fish : " + nFishCount + "/" + nGameClearFishCount; //������� ������ ��� : Fish : 1/10

        if(nFishCount >= nGameClearFishCount) //����� ������ Ŭ���� ������ ������ ���
        {
            f_GameClear(); //���� Ŭ���� �޼ҵ� ȣ��
        }
    }
}
