using System;
using System.Collections.Generic;
using System.Linq;

public class Reactor
{
    public InputCell CreateInputCell(int value)
        => new InputCell(value);

    public ComputeCell CreateComputeCell(IEnumerable<Cell> producers, Func<int[], int> compute)
    {
        var cell = new ComputeCell(producers, compute);

        foreach (var dependency in producers)
        {
            dependency.Updating += cell.OnDependencyUpdating;
            dependency.Changed += cell.OnDependencyChanged;
        }

        return cell;
    }
}

public abstract class Cell
{
    protected int _value;

    public virtual int Value
    {
        get => _value;

        protected set
        {
            if (_value != value)
            {
                _value = value;
                RaiseEvents();
            }
        }
    }

    protected void RaiseEvents()
    {
        Updating?.Invoke(this, Value);
        Changed?.Invoke(this, Value);
    }

    internal event EventHandler<int> Updating;
    public event EventHandler<int> Changed;
}

public class InputCell : Cell
{
    public InputCell(int value)
    {
        Value = value;
    }

    public new int Value { get => base.Value; set => base.Value = value; }
}

public class ComputeCell : Cell
{
    IEnumerable<Cell> producers;
    Func<int[], int> compute;

    int _oldValue;

    public ComputeCell(IEnumerable<Cell> producers, Func<int[], int> compute)
    {
        this.producers = producers;
        this.compute = compute;

        Recompute();
        _oldValue = Value;
    }

    private void Recompute()
    {
        _value = compute(producers.Select(v => v.Value).ToArray());
    }

    internal void OnDependencyUpdating(object sender, int value)
        => Recompute();

    internal void OnDependencyChanged(object sender, int value)
    {
        if (_oldValue != Value)
        {
            _oldValue = Value;
            RaiseEvents();
        }
    }
}