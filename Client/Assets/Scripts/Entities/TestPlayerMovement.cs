using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    private float _horizontalMovement = 0;
    private float _verticalMovement = 0;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private Rigidbody2D _rigidbody;

    //�Ʒ� �� �� �÷��̾� �����Ͻ� �� �������ֽø� �˴ϴ�.
    [SerializeField] private GameObject _footCollider;
    public bool isNearPortal = false;
    public PortalType portalType;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        _horizontalMovement = Input.GetAxis("Horizontal");
        _verticalMovement = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(_horizontalMovement,0 , 0 ) * Time.deltaTime * moveSpeed);

        bool isJump = Input.GetKeyDown(KeyCode.Space);
        bool isPortal = Input.GetKeyDown(KeyCode.W);
        if ( isJump )
        {
            _rigidbody.AddForce(new Vector3(0, 1, 0) * jumpForce, ForceMode2D.Impulse);
            _footCollider.SetActive(false);
            Invoke("ActivateFoot", 0.5f);
        }
        if(isPortal&&isNearPortal)
        {
            MapManager.Instance.LoadNextMap(portalType);
        }
    }

    private void ActivateFoot()
    {
        _footCollider.SetActive(true);
    }

}
