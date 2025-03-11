using System;

//Класс, который задает переходы
namespace SecondProject.FSM
{
    public class Transition
    {
        public BaseState ToState { get; }
        //Условие, когда переход должен выполнится и превести в ToState - делегат
        public Func<bool> Condition { get; }

        public Transition(BaseState toState, Func<bool> condition)
        {
            ToState = toState;
            Condition = condition;
        }
    }
}
