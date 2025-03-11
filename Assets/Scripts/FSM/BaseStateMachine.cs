using SecondProject.Exceptions;
using System.Collections.Generic;

namespace SecondProject.FSM
{
    public class BaseStateMachine
    {
        private BaseState _currentState;
        //Какие состояния есть
        private List<BaseState> _states;
        //Какие есть переходы (<откуда, куда и при каком условии>)
        private Dictionary<BaseState, List<Transition>> _transitions;

        public BaseStateMachine()
        {
            _states = new List<BaseState>();
            _transitions = new Dictionary<BaseState, List<Transition>>();
        }
        //Установка стартового состояния
        public void SetInitialState(BaseState state)
        {
            _currentState = state;
        }
        //Добавление состояний в автомат
        public void AddState(BaseState state, List<Transition> transitions)
        {
            if (!_states.Contains(state))
            {
                _states.Add(state);
                _transitions.Add(state, transitions);
            }
            else
            {
                throw new AlreadyExistsException($"State {state.GetType()} already exists in state machine!");
            }
        }
        //Метод,обновляющий состояния машины каждый кадр
        public void Update()
        {
            foreach (var transition in _transitions[_currentState])
            {
                if (transition.Condition())
                {
                    _currentState = transition.ToState;
                    break;
                }
            }

            _currentState.Execute();
        }
    }
}
