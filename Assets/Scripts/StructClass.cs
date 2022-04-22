using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace StructClass
{
    /// <summary>
    /// elements with their order are saved here
    /// </summary>
    public enum ElementType
    {
        s, p, d, f
    }

    /// <summary>
    /// structure of the element, includes: ID, name, abbrevation, type, mass, radius, number of bonds, color
    /// </summary>
    [Serializable]
    public struct ElementData
    {
        public int m_id;
        public string m_name;
        public string m_abbre;
        public ElementType m_type;
        public float m_mass;
        public float m_radius;
        public int m_bondNum;
        public int m_hybridization;
        public Color m_color;
        public ElementData(int id, string name, string abbre, ElementType type, float mass, float radius, int count, int hyb, float red, float green, float blue)
        {
            m_id = id;
            m_name = name;
            m_abbre = abbre;
            m_type = type;
            m_mass = mass;
            m_radius = radius;
            m_bondNum = count;
            m_hybridization = hyb;
            m_color = new Color(red, green, blue);
        }

        public ElementData(int id, string name, string abbre, ElementType type, float mass, float radius, int count, int hyb, Color color)
        {
            m_id = id;
            m_name = name;
            m_abbre = abbre;
            m_type = type;
            m_mass = mass;
            m_radius = radius;
            m_bondNum = count;
            m_hybridization = hyb;
            m_color = color;
        }
    }

    // hybrid
    // 


    /// <summary>
    /// structure of an atom in cml
    /// </summary>
    public struct cmlAtom
    {
        public int id;
        public string abbre;
        public int hybrid;
        public Vector3 pos;
        public cmlAtom(int _id, string name, int hybridisation, Vector3 _pos)
        {
            id = _id;
            abbre = name;
            hybrid = hybridisation;
            pos = _pos;
        }
    }

    //Bindungsl�nge

    /// <summary>
    /// structure of a bond in cml
    /// </summary>
    public struct cmlBond
    {
        public int id1;
        public int id2;
        public float order;
        public cmlBond(int a, int b, float c)
        {
            id1 = a;
            id2 = b;
            order = c;
        }
    }

    /// <summary>
    /// cmlData combines the list of atoms and bonds in cml format
    /// </summary>
    public struct cmlData
    {
        public Vector3 molePos;
        public cmlAtom[] atomArray;
        public cmlBond[] bondArray;
        public cmlData(Vector3 pos, List<cmlAtom> a, List<cmlBond> b)
        {
            molePos = pos;
            atomArray = a.ToArray();
            bondArray = b.ToArray();
        }
    }
    /// <summary>
    /// contain the status of each buttons of a controller
    /// </summary>
    public struct ControllerStatus
    {
        public float TriggerPress;
        public float Primary2D;
        public float Secondary2D;
        public float GripPress;
        public void SetValue(float triggerPress, float primary2D, float secondary2D, float grip)
        {
            TriggerPress = triggerPress;
            Primary2D = primary2D;
            Secondary2D = secondary2D;
            GripPress = grip;
        }
    }
    /// <summary>
    /// which hand
    /// </summary>
    public enum WhichHand
    {
        Any = 0,
        left = 1,
        right = 2
    }
}