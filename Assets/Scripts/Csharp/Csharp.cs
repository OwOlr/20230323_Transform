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

        //함수, Funtion, Method
        //C++의 사용법
        //Setter
        public void SetPriVal(int _priVal)
        {
            //예외처리가 가능하다.
            if (_priVal > 10) return;
            //갱신
            priVal = _priVal;
        }
        
        //Getter
        public int GetPriVal()
        {
            if (priVal < 0) return 0;
            //값 외부로 반환
            return priVal;
        }

        
        //C#과 Properties(프로퍼티)의 탄생
        //일반 변수처럼 사용 가능.
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

    //상속 (Inheritance)
    // Parent-Child, 부모 - 자식 (C++)
    // Super-Sub, 슈퍼-서브 (Java)
    // Base-Derived, 기본-파생 (C#)
    public class Parent
    {
        protected int parentVal;
        public virtual void ParentFunc()
        {
            Debug.Log("ParentVal : " + parentVal);

        }

        //반환형이 없으며, 클래스명과 똑같다.
        //생성자 Contructor
        public Parent()
        {
            Debug.Log("+++++++++++++++++++Parent Contructor");
        }
        public Parent(int i)
        {

        }
        //소멸자 Destrutor
        ~Parent()
        {
            Debug.Log("-------------------Parent Destrutor");
        }
    }

    //부모 클래스에 정의 하게 되면 자식에게 제한이 가는 것이 많아. 부모와 자식이 상속받는 코드를 모아 하나로 만든다.
    //추상화 클래스의 탄생
    //Abstract
    public abstract class AbstractParent
    {
        //Pure Virtual Class / Function     순수 가상 클래스/ 함수
        //정의가 없어 메모리상에 올릴 수 없다. AbstractParent abstractParent = new AbstractParent(); (불가능X)
        public abstract void ParentFunc();
    }

    //정의 자체 없는 클래스이기 때문에 상속 받을 때 반드시 재정의를 해줘야한다. -> 재정의를 강제화한다.
    public  class APChild : AbstractParent
    {
        public override void ParentFunc(){  }
    }

    public abstract class Weapon
    {
        //재정의를 안해도 되는 함수 그대로 사용
        public void Reload() { }
        //반드시 재정의를 해야하는 함수. (강제)
        public abstract void Fire();
    }

    //강제화 하는 virtual을 뛰어넘어 더욱 추상화 시킨다.
    //Interface 탄생
    //멤버변수가 없어야 진정한 추상화 그래서 멤버변수가 없다. 모든 함수들이 순수 가상 함수이여야 한다.
    //추상 클래스와 비슷하게 무조건 재정의를 해야한다.
    //유일하게 다중상속이 가능하다. 아무것도 정의가 없으니깐
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

        //Function Overloading - 함수 호출할 때 매개변수 타입으로 함수를 구별할 때 사용.
        public void Func() { }
        public void Func(int _i) { }
        public void Func(int i, float f){ }
        public void Func(float f) { }

        //Default Paramater 기본값 파라매터
        //들어오면 입력받은 값을 가져오고, 들어오지 않으면 정의한 매개변수로 가져온다.
        public void DefFunc(int _i = 10)
        {
            Debug.Log("_i : " + _i);
        }

        //Functions Overriding - 재정의
        public override void  ParentFunc()
        {
            Debug.Log("Child - ParentFunc");
        }

        
    }

    public class NewChild : Parent
    {
        //각 자식들에게 override 추가
        public override void  ParentFunc()
        {
            base.ParentFunc();
            Debug.Log("NewChild ParentFunc");
        }
    }


    private void Start()
    {
        ////Class의 동적할당은 new!
        ////CShap은 무조건 동적할당 해야한다. ->모든 객체는 Heap에 들어가기 때문
        //// * 구조체는 Heap에 들어가지 않는다.
        //ClassExam ce = new ClassExam();
        //ce.SetPriVal(10);
        ////set
        //ce.PriVal = 10;

        //Child child = new Child();
        //child.ParentFunc();
        //child.DefFunc();

        //Debug.Log("===========================");
        ////+class도 padding이 가능하다.
        ////부모가 자식으로 동적할당이 가능하다.
        ////그러나 parent 는 부모의 함수들만 알고 있다.
        ////Polymophism다형성
        //Parent parent = new Child();
        //parent.ParentFunc();    //자식 함수 호출을 원하지만 나오지않는다, -> 부모함수에 virtual과 자식 함수에override추가해보자 =
        ////그러나 child로 형변환 하면 자식의 함수에 접근이 가능하다.
        ////((Child)parent).ParentFunc();
        ////그러나 자식은 부모로 동적할당 할 수 없다.
        ////Child c = new Parent();

        //parent = new NewChild();
        //parent.ParentFunc();    //부모 함수 호출
        
        Parent p1 = new Parent();
        Parent p = new Parent(10);
        //Quaternion.Euler()

    }

}
