public delegate void ValueChanged<TValue>(TValue vOld, TValue vNew);

public abstract class Field<T> : UIElement
{
    private T _value;
    public T Value
    {
        get
        {
            return _value;
        }
        set
        {
            T vOld = _value;
            T vNew = value;

            if (vOld == null && vNew == null ||
                (vOld?.Equals(vNew) ?? false))
            {
                return;
            }

            PreviewValueChanged?.Invoke(vOld, vNew);
            _value = value;
            ValueChanged?.Invoke(vOld, vNew);
        }
    }

    public event ValueChanged<T> PreviewValueChanged;
    public event ValueChanged<T> ValueChanged;
}