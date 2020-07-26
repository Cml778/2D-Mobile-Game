
public class Cucumber : Enemy
{

    //有限状态机  FSM    抽象类继承的方式实现状态机

    public void SetOff()
	{
        targetPoint.GetComponent<Bomb>().TurnOff();
	}
    
}
