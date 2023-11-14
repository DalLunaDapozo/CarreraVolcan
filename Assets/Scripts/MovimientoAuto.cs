using UnityEngine;

public class MovimientoAuto : MonoBehaviour
{

    private float moveInput;
    private float turnInput;
    private bool isGrounded;

    public float airDrag;
    public float groundDrag;

    public Rigidbody esfera;

    public float velocidad_de_movimiento;
    public float rev_velocidad_de_movimiento;

    public float velocidad_de_rotacion;

    public LayerMask layer;

    void Start()
    {
        esfera.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Vertical");
        turnInput = Input.GetAxisRaw("Horizontal");

        moveInput *= moveInput > 0 ? velocidad_de_movimiento : rev_velocidad_de_movimiento;

        transform.position = esfera.transform.position;

        float newRotation = turnInput * velocidad_de_rotacion * Time.deltaTime * Input.GetAxisRaw("Vertical");
        transform.Rotate(0f, newRotation, 0f, Space.World);

        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, layer);

        transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;

        if (isGrounded)
        {
            esfera.drag = groundDrag;
        }
        else
        {
            esfera.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        if (isGrounded)
        {
             esfera.AddForce(transform.forward * moveInput, ForceMode.Acceleration);
        }
        else
        {
            esfera.AddForce(transform.up * -20f);
        }
    }
}
