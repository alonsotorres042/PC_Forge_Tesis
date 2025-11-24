//using UnityEngine;

//public class CPUSocketSlot : SlotBase // First example
//{
//    [SerializeField] private Animator openAnimation;

//    protected override void Start()
//    {
//        base.Start();
//        AddCondition(new SocketOpenCondition(() => isOpen));
//    }

//    private bool isOpen = false;

//    public void OpenSocket()
//    {
//        isOpen = true;
//        //openAnimation?.SetBool("OnOpenSocket", true);
//    }

//    public void CloseSocket()
//    {
//        isOpen = false;
//        //openAnimation?.SetBool("OnClosedSocket", true);
//    }
//}