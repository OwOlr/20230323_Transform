using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Csharp : MonoBehaviour
{
    //Ecapsulation
    public class ClassExam
    {
        int defVal;
        private int priVal;

        

        public int pubVal;

        //�Լ�, Funtion, Method
        //C++�� ����
        //Setter
        public void SetPriVal(int _priVal)
        {
            //����ó���� �����ϴ�.
            if (_priVal > 10) return;
            //����
            priVal = _priVal;
        }
        
        //Getter
        public int GetPriVal()
        {
            if (priVal < 0) return 0;
            //�� �ܺη� ��ȯ
            return priVal;
        }

        
        //C#�� Properties(������Ƽ)�� ź��
        //�Ϲ� ����ó�� ��� ����.
        public int PriVal
        {
            get { return priVal; }
            set {
                    if (value > 10) return;
                    priVal = value; 
                }
        }
        public int val1
        {
            get { return priVal; }
            set { priVal = value; }
        }
        public int val2 { get; set; }
    }

    //��� (Inheritance)
    // Parent-Child, �θ� - �ڽ� (C++)
    // Super-Sub, ����-���� (Java)
    // Base-Derived, �⺻-�Ļ� (C#)
    public class Parent
    {
        protected int parentVal;
        public virtual void ParentFunc()
        {
            Debug.Log("ParentVal : " + parentVal);

        }

        //��ȯ���� ������, Ŭ������� �Ȱ���.
        //������ Contructor
        public Parent()
        {
            Debug.Log("+++++++++++++++++++Parent Contructor");
        }
        public Parent(int i)
        {

        }
        //�Ҹ��� Destrutor
        ~Parent()
        {
            Debug.Log("-------------------Parent Destrutor");
        }
    }

    //�θ� Ŭ������ ���� �ϰ� �Ǹ� �ڽĿ��� ������ ���� ���� ����. �θ�� �ڽ��� ��ӹ޴� �ڵ带 ��� �ϳ��� �����.
    //�߻�ȭ Ŭ������ ź��
    //Abstract
    public abstract class AbstractParent
    {
        //Pure Virtual Class / Function     ���� ���� Ŭ����/ �Լ�
        //���ǰ� ���� �޸𸮻� �ø� �� ����. AbstractParent abstractParent = new AbstractParent(); (�Ұ���X)
        public abstract void ParentFunc();
    }

    //���� ��ü ���� Ŭ�����̱� ������ ��� ���� �� �ݵ�� �����Ǹ� ������Ѵ�. -> �����Ǹ� ����ȭ�Ѵ�.
    public  class APChild : AbstractParent
    {
        public override void ParentFunc(){  }
    }

    public abstract class Weapon
    {
        //�����Ǹ� ���ص� �Ǵ� �Լ� �״�� ���
        public void Reload() { }
        //�ݵ�� �����Ǹ� �ؾ��ϴ� �Լ�. (����)
        public abstract void Fire();
    }

    //����ȭ �ϴ� virtual�� �پ�Ѿ� ���� �߻�ȭ ��Ų��.
    //Interface ź��
    //��������� ����� ������ �߻�ȭ �׷��� ��������� ����. ��� �Լ����� ���� ���� �Լ��̿��� �Ѵ�.
    //�߻� Ŭ������ ����ϰ� ������ �����Ǹ� �ؾ��Ѵ�.
    //�����ϰ� ���߻���� �����ϴ�. �ƹ��͵� ���ǰ� �����ϱ�
    public interface IWeapon { }
    public interface IWeapon2 { }
    public class NewWeapon : IWeapon,IWeapon2
    {
        
    }

    public class Child : Parent
    {
        private int childVal;

        public Child()
        {
            Debug.Log("+++++++++++++++++++Child Contructor");
        }
        ~Child()
        {
            Debug.Log("-------------------Child Destructor");
        }
        public void ChildFunc()
        {
            Debug.Log("ChildVal : " + childVal);
            ParentFunc();
            parentVal  = 10;
        }

        //Function Overloading - �Լ� ȣ���� �� �Ű����� Ÿ������ �Լ��� ������ �� ���.
        public void Func() { }
        public void Func(int _i) { }
        public void Func(int i, float f){ }
        public void Func(float f) { }

        //Default Paramater �⺻�� �Ķ����
        //������ �Է¹��� ���� ��������, ������ ������ ������ �Ű������� �����´�.
        public void DefFunc(int _i = 10)
        {
            Debug.Log("_i : " + _i);
        }

        //Functions Overriding - ������
        public override void  ParentFunc()
        {
            Debug.Log("Child - ParentFunc");
        }

        
    }

    public class NewChild : Parent
    {
        //�� �ڽĵ鿡�� override �߰�
        public override void  ParentFunc()
        {
            base.ParentFunc();
            Debug.Log("NewChild ParentFunc");
        }
    }


    private void Start()
    {
        ////Class�� �����Ҵ��� new!
        ////CShap�� ������ �����Ҵ� �ؾ��Ѵ�. ->��� ��ü�� Heap�� ���� ����
        //// * ����ü�� Heap�� ���� �ʴ´�.
        //ClassExam ce = new ClassExam();
        //ce.SetPriVal(10);
        ////set
        //ce.PriVal = 10;

        //Child child = new Child();
        //child.ParentFunc();
        //child.DefFunc();

        //Debug.Log("===========================");
        ////+class�� padding�� �����ϴ�.
        ////�θ� �ڽ����� �����Ҵ��� �����ϴ�.
        ////�׷��� parent �� �θ��� �Լ��鸸 �˰� �ִ�.
        ////Polymophism������
        //Parent parent = new Child();
        //parent.ParentFunc();    //�ڽ� �Լ� ȣ���� �������� �������ʴ´�, -> �θ��Լ��� virtual�� �ڽ� �Լ���override�߰��غ��� =
        ////�׷��� child�� ����ȯ �ϸ� �ڽ��� �Լ��� ������ �����ϴ�.
        ////((Child)parent).ParentFunc();
        ////�׷��� �ڽ��� �θ�� �����Ҵ� �� �� ����.
        ////Child c = new Parent();

        //parent = new NewChild();
        //parent.ParentFunc();    //�θ� �Լ� ȣ��
        
        Parent p1 = new Parent();
        Parent p = new Parent(10);
        //Quaternion.Euler()

    }

}
