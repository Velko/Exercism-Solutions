using System;

public class CircularBuffer<T>
{
    T[] storage;
    int read_index;
    int write_index;

    public CircularBuffer(int capacity)
    {
        storage = new T[capacity];
        Clear();
    }

    public T Read()
    {
        if (IsEmpty)
            throw new InvalidOperationException();

        var result = storage[WrapToSize(read_index)];
        read_index = Increment(read_index);

        return result;
    }

    public void Write(T value)
    {
        if (IsFull)
            throw new InvalidOperationException();

        DoWrite(value);
    }

    public void Overwrite(T value)
    {
        if (IsFull)
            read_index = Increment(read_index); // pretend to read

        DoWrite(value);
    }

    private void DoWrite(T value)
    {
        storage[WrapToSize(write_index)] = value;
        write_index = Increment(write_index);
    }

    public void Clear()
    {
        read_index = 0;
        write_index = 0;
    }

    /* Full/empty detection and range of indices:
       * both read_index and write_index are wrapped around at twice the storage size;
       * when accessing the storage array, indices have to be passed through WrapToSize(),
         taking modulo of correct size;
       * buffer is considered empty, when both indices are equal;
       * buffer is considered full, when WrapToSize()d indices point to same element, but
         they are not equal. Thus the distance between them is equal to size of the buffer;

       Why implement this way? Testing an idea for hardware-based FIFO. When buffer size is
       a power of 2, the modulo operation comes for free - just ignore (not connect) the
       bits you're not interested in.
    */

    public bool IsEmpty
        => read_index == write_index;

    public bool IsFull
        => WrapToSize(read_index) == WrapToSize(write_index)
            && read_index != write_index;

    private int WrapToSize(int index)
        => index % storage.Length;

    private int Increment(int index)
        => (index + 1) % (storage.Length * 2);
}