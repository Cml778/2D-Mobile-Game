//抽象类，定义方法，只声明，在继承类中实现。无法在抽象类中实现非抽象的方法
public abstract class EnemybaseState
{
    public abstract void EnterState(Enemy enemy);
    public abstract void OnUpdate(Enemy enemy);
}
