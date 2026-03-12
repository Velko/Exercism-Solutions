using System;

public class CircularBuffer<T>
{
    T[] storage;
    int read_index;
    int write_index;
    bool is_full;

    public CircularBuffer(int capacity)
    {
        storage = new T[capacity];
        Clear();
    }

    public T Read()
    {
        if (read_index == write_index && !is_full)
            throw new InvalidOperationException();

        var result = storage[read_index];
        AdvanceRead();
        is_full = false;

        return result;
    }

    public void Write(T value)
    {
        if (is_full)
            throw new InvalidOperationException();

        DoWrite(value);
    }

    public void Overwrite(T value)
    {
        if (is_full)
            AdvanceRead();

        DoWrite(value);
    }

    private void AdvanceRead()
    {
        read_index = (read_index + 1) % storage.Length;
    }

    private void DoWrite(T value)
    {
        storage[write_index++] = value;
        write_index %= storage.Length;

        is_full = read_index == write_index;
    }

    public void Clear()
    {
        read_index = 0;
        write_index = 0;
        is_full = false;
    }
}
