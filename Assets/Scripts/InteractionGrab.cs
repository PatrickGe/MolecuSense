using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionGrab : MonoBehaviour
{

    private Atom m_currentAtom = null;
    public List<Atom> m_Interactables = new List<Atom>();
    private Vector3 lastPosController;
    private Vector3 deltaPos;
    private Vector3 deltaContr;
    private Quaternion lastRotController;
    private Quaternion lastRotationMolecule;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Atom"))
        {
            return;
        }
        m_Interactables.Add(other.gameObject.GetComponent<Atom>());

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Atom"))
        {
            return;
        }
        m_Interactables.Remove(other.gameObject.GetComponent<Atom>());
    }


    private Atom GetNearestAtom()
    {
        Atom nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (Atom interactable in m_Interactables)
        {
            if (interactable != null)
            {
                distance = (interactable.transform.position - transform.position).sqrMagnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = interactable;
                }
            }

        }
        return nearest;
    }


    public void Pickup()
    {
        m_currentAtom = GetNearestAtom();
        //Null check
        if (!m_currentAtom)
            return;
        m_currentAtom.isGrabbed = true;

        // Already held, check
        if (m_currentAtom.m_ActiveHand)
            m_currentAtom.m_ActiveHand.GetComponent<InteractionGrab>().Drop();

        // why? seems to lead to strange jumps ... trying 0;



        //Debug.Log("deltaPos: " + deltaPos);
        //Debug.Log("PosGlob: " + GlobalCtrl.Instance.atomWorld.transform.position);
        //Debug.Log("PosAtom: " + m_currentAtom.transform.parent.transform.position);

        //Transform whole molecule or single atom depending on which mode is active
        if (GlobalCtrl.Instance.allAtomMode)
        {
            //deltaPos = GlobalCtrl.Instance.atomWorld.transform.position - m_currentAtom.transform.parent.transform.position;
            deltaPos = m_currentAtom.transform.parent.transform.position - m_currentAtom.transform.position;
            deltaContr = m_currentAtom.transform.parent.transform.position - transform.position;
            //            m_currentAtom.transform.parent.transform.position = transform.position + deltaPos + deltaContr; // have to fix rotation for this
            m_currentAtom.transform.parent.transform.position = transform.position + deltaPos;
        }
        else
        {
            deltaPos = new Vector3(0f, 0f, 0f);
            deltaContr = m_currentAtom.transform.position - transform.position; // deltaContr allows for a "soft touch" of atoms
            m_currentAtom.transform.position = transform.position + deltaPos + deltaContr;
        }



        //Fix Rotation
        lastRotationMolecule = m_currentAtom.transform.parent.transform.rotation;
        lastPosController = transform.position;
        lastRotController = transform.rotation;
    }

    public void Drop()
    {
        // Null check
        if (!m_currentAtom)
            return;

        if (GlobalCtrl.Instance.collision)
        {
            Atom d1 = GlobalCtrl.Instance.collider1;
            Atom d2 = GlobalCtrl.Instance.collider2;

            Atom a1 = Atom.Instance.dummyFindMain(d1);
            Atom a2 = Atom.Instance.dummyFindMain(d2);

            if (!Atom.Instance.alreadyConnected(a1, a2))
                GlobalCtrl.Instance.MergeMolecule(GlobalCtrl.Instance.collider1, GlobalCtrl.Instance.collider2);

        }
        // else if Distanz zu anderem zu groß, Molekül trennen

        //Clear
        m_currentAtom.isGrabbed = false;
        m_currentAtom.m_ActiveHand = null;
        m_currentAtom = null;
    }

    public void HoldDown()
    {
        //Grab single atom which is not connected to the molecule
        if (m_currentAtom != null && m_currentAtom.transform.parent == null)
        {
            m_currentAtom.transform.position = transform.position + deltaContr;
        }
        //Grab molecule
        else
        {
            //Transform whole molecule or single atom depending on which mode is active
            if (GlobalCtrl.Instance.allAtomMode && m_currentAtom != null)
            {
                m_currentAtom.transform.parent.transform.rotation = transform.rotation * Quaternion.Inverse(lastRotController) * lastRotationMolecule;
                deltaPos = m_currentAtom.transform.parent.transform.position - m_currentAtom.transform.position;
                m_currentAtom.transform.parent.transform.position = transform.position + deltaPos;
                //                m_currentAtom.transform.parent.transform.position = transform.position + deltaContr;   // have to fix rotation, this does not work

            }
            else
            {
                if (m_currentAtom != null)
                    m_currentAtom.transform.position = transform.position + deltaContr;
            }

        }
    }

}
