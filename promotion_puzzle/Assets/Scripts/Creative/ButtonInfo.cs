using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ButtonInfo : MonoBehaviour
{
    public Vector2 pos;
    public State state = State.o;
    public void Init(Vector2 pos, State state)
    {
        this.pos = pos;
        this.state = state;
        GetComponentInChildren<TMP_Text>().text = state.ToString();
    }
    public void OnClicked()
    {
        state = GetNextEnumValue(state);
        ChangeText();
    }
    public void ChangeText()
    {
        GetComponentInChildren<TMP_Text>().text = state.ToString();
    }
    static State GetNextEnumValue(State state)
    {
        State[] values = (State[])Enum.GetValues(typeof(State));
        int index = Array.IndexOf(values, state);

        if (index < values.Length - 1)
        {
            return values[index + 1]; // ŽŸ‚Ì—v‘f
        }
        return values[0]; // ÅŒã‚È‚çÅ‰‚É–ß‚·
    }
}
